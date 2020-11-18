using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRIS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IRIS
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
            var connection = Configuration.GetConnectionString("IRISDatabase");
            services.AddDbContext<ORXI_CCMContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<ORXI_CCMxml>(options => options.UseSqlServer(connection));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "xml",
                    pattern: "Xml/{action=Index}/{id?}",
                    defaults: new { controller = "IrisXml", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=IrisProcessed}/{action=Index}/{id?}");
            });
        }
    }
}
