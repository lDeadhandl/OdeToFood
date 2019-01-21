using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
            services.AddSingleton<IGreeter, Greeter>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // For every request that arrives the code inside Configure decides what components respond to that request
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              IConfiguration configuration,
                              IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(ConfigureRoutes);

            app.Run(async (context) =>
            {
                var greeting = greeter.GetMessageOfTheDay();
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // Conventional Routing
            // creates a default route so that if someone types
            // websitename/controllername/action
            // Default is HomeController and Index action
            routeBuilder.MapRoute("Default", "" +
                "{controller=Home}/{action=Index}/{id?}");
                
        }
    }
}
