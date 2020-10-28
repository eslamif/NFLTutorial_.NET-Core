using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NFLTutorial.Models;

namespace NFLTutorial {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            //Enable MVC with NewtonSoftJson (use Json to serialize/deserialize objects in sessions)
            services.AddControllersWithViews().AddNewtonsoftJson();

            //Lowercase routing
            services.AddRouting(options => options.LowercaseUrls = true);

            //Register TeamContext with DbContext
            services.AddDbContext<TeamContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TeamContext")));

            //Enable web browser caching
            services.AddMemoryCache();

            //Enable sessions
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 5);     //5 minute timeout
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();       //Enable sessions
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
