using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Persistance.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using Inventory.Persistance.Extensions;
using Inventory.Application.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace Inventory.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            #region Authentication and Authorization
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", policy =>
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser()
                );
            });
            #endregion
            #region Database 
            services.AddDbContext<NorthwindContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
         
            #region DI
            services.AddPersistance();
            services.AddApplication();
            #endregion
            #region CrossOriging
            services.AddCors();
            #endregion
            #region Swagger
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MO API", Version = "v1" });
            });
            #endregion
            services.AddControllers();
            #region MVC
            services.AddMvc().AddJsonOptions(x => {
                var settings = x.JsonSerializerOptions;
                settings.IgnoreNullValues = true;
                settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDeveloperExceptionPage();
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #region CrossOriging
            app.UseCors(builder =>
                       builder.WithOrigins("http://localhost:4200", "http://localhost:3000")
                       .AllowAnyHeader().AllowAnyMethod());
            #endregion
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
