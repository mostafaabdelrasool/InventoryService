using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Administration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
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
            services.AddMvc();
            #region CrossOriging
            services.AddCors();
            #endregion
            #region Authentication
            services.AddAuthentication()
              .AddJwtBearer("IdentityApiKey", x =>
              {
                  x.RequireHttpsMetadata = false;
                  //x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                  //{
                  //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppConfiguration:Key").Value)),
                  //    //ValidAudiences = new[] { "orders", "basket", "locations", "marketing", "mobileshoppingagg", "webshoppingagg" }
                  //};
              });
            #endregion
            services.AddOcelot(Configuration).AddAdministration("/administration", "secret");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            #region CrossOriging
            app.UseCors(builder =>
                       builder.WithOrigins("http://localhost:4200", "http://localhost:3000")
                       .AllowAnyHeader().AllowAnyMethod());
            #endregion
            app.UseHttpsRedirection();
            await app.UseOcelot();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

        }
    }
}
