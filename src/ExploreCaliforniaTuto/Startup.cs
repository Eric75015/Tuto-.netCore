using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCaliforniaTuto.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExploreCaliforniaTuto
{
    public class Startup
    {
        private readonly IConfigurationRoot conf;

        public Startup(IHostingEnvironment env)
        {
            conf = new ConfigurationBuilder().AddEnvironmentVariables()
                                                 .AddJsonFile(env.ContentRootPath + "/config.json")
                                                 .AddJsonFile(env.ContentRootPath + "/config.development.json")
                                                 .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<SpecialsDataContext>();
            services.AddTransient<FormattingService>();

            services.AddTransient<FeatureToggles>( provider => new FeatureToggles
            {
                EnableDeveloperExceptions = conf.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")
            });

            services.AddDbContext<BlogDataContext>(builder =>
                {
                    var connect = conf.GetConnectionString("BlogDataContext");
                    builder.UseSqlServer(connect);
                }

            );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            FeatureToggles features)
        {
            loggerFactory.AddConsole();

            app.UseExceptionHandler("/error.html");

            //if (conf.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))
            if (features.EnableDeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.Use(async (context, next) =>
            {
                if(context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("Error!");

                await next();
            });

            app.UseMvc(builder => builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}"));

            app.UseFileServer();
            //app.Run(async context => { await context.Response.WriteAsync(" How are you?"); }); 
        }
    }
}
