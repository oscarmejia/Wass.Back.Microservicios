using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Wass.Back.Empresa.Configuraciones;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa
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

            services.AddControllers();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureRepositories(Configuration)
                .AddMiddleware()
                .AddCorsConfiguration()
                .AddConnectionProvider(Configuration)
                .AddAppSettings(Configuration);

            services.AddMvc()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddControllersAsServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wass.Back.Empresa", Version = "v1" });
            });

            //services.AddCors(options => {
            //    options.AddPolicy("AllowAll", build =>
            //    {
            //        build
            //        .AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowCredentials()
            //        .WithOrigins("http://localhost:4200", "http://localhost:8100")
            //            .SetIsOriginAllowedToAllowWildcardSubdomains();
            //    });


            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wass.Back.Empresa v1"));
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
