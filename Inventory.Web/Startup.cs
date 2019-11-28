using Inventory.Web.Extensions;
using Inventory.Web.Helpers.BasicAuthentication;
using Inventory.Web.Helpers.BasicAuthentication.Events;
using Inventory.Web.Models;
using Inventory.Web.Sentry;
using Inventory.Web.Sentry.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Text;
using Inventory.Application.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory.Persistance.Models;
using Inventory.Persistance.Extensions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Inventory.Web.Controllers;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using EventBus.Abstractions;
using EventBus;
using Inventory.Application.Config;

namespace Inventory.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //#region Core2.1
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //#endregion
            #region Sentry
            services.Configure<SentryOptions>(Configuration.GetSection("Sentry"));
            services.AddScoped<IErrorReporter, SentryErrorReporter>();
            #endregion
            #region AppConfiguration
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            // Global access to Configuration
            services.AddSingleton(c => Configuration);
            #endregion
            #region Database 
            services.AddDbContext<NorthwindContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            #region Authentication and Authorization
            // Add basic authentication. Primary used for external api access
            //services
            //  .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
            //  .AddBasicAuthentication(
            //  options =>
            //  {
            //      options.Realm = "MO";
            //      options.Events = new BasicAuthenticationEvents() { };
            //  });

            services.AddDbContext<NorthwindContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<NorthwindContext>();
            // Add JWT Bearer. This should be replaced when we have setup the Identity server
            services
              .AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppConfiguration:Key").Value)),
                      ValidAudience = Configuration.GetSection("AppConfiguration:SiteUrl").Value,
                      ValidateIssuerSigningKey = true,
                      ValidateLifetime = true,
                      ValidIssuer = Configuration.GetSection("AppConfiguration:SiteUrl").Value
                  };
              });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Authenticated", policy =>
            //        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser()
            //    );
            //});
            #endregion

            #region MVC
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(x =>
            {
                var settings = x.SerializerSettings;
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.DefaultValueHandling = DefaultValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            #endregion
            #region DI
            services.AddPersistance();
            services.AddApplication();
            services.AddWeb();
            #endregion
            #region CrossOriging
            services.AddCors();
            #endregion
            #region Swagger
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "String API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
            #endregion
            //#region Message
            //services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            //{
            //    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            //    var factory = new ConnectionFactory()
            //    {
            //        HostName = Configuration["EventBusConnection"],
            //        DispatchConsumersAsync = true
            //    };

            //    if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
            //    {
            //        factory.UserName = Configuration["EventBusUserName"];
            //    }

            //    if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
            //    {
            //        factory.Password = Configuration["EventBusPassword"];
            //    }

            //    var retryCount = 5;
            //    if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
            //    {
            //        retryCount = int.Parse(Configuration["EventBusRetryCount"]);
            //    }

            //    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            //});
            //RegisterEventBus(services);
            //#endregion
            #region config
            IConfigurationSection sec = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            services.Configure<ConnectionsConfig>(op=> {
                op.ConnectionString = sec.Value;
            });
            #endregion
        }
        private void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];
            services.AddSingleton<IEventBus, EventBusHandlerRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusHandlerRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new EventBusHandlerRabbitMQ(rabbitMQPersistentConnection,
                                            logger,
                                            eventBusSubcriptionsManager,
                                            subscriptionClientName,
                                            retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

         //   eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Dev
            if (env.IsDevelopment())
            {
                app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    #region DB migration
                    // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                    var context = serviceScope.ServiceProvider.GetService<NorthwindContext>();
                    context.Database.Migrate();
                    #endregion
                }
                #region Swagger
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger(c => { });
                // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
                #endregion
            }
            #endregion
            #region Production
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCookiePolicy();
                app.UseExceptionHandler("/Home/Error");
                try
                {
                    // Migrate DB here
                    // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                }
                catch { }
            }
            #endregion
            #region CrossOriging
            app.UseCors(builder =>
                       builder.WithOrigins("http://localhost:4200", "http://localhost:3000")
                       .AllowAnyHeader().AllowAnyMethod());
            #endregion
            app.UseMiddleware<SentryMiddleware>();
            app.UseAuthentication();
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //#region Messaging
            //ConfigureEventBus(app);
            //#endregion
        }
    }
}
