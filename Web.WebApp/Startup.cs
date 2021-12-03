using Core.Infrastructure.Database;
using Core.Infrastructure.Database.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.WebApp.Data;

namespace Web.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Infrastructure
            AddDatabase(services);

            // .Net Identity
            AddAppIdentity(services, Environment);

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void AddDatabase(IServiceCollection services)
        {
            services.Configure<PostgresOptions>(Configuration);
            services.AddSingleton<IAppDbContextConfigurator, AppDbContextConfigurator>();
            services.AddDbContext<AppDbContext>();
        }

        private void AddAppIdentity(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddIdentity<User, Role>(opt => {

                if (env.IsDevelopment())
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 4;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;

                }
                else
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.SignIn.RequireConfirmedPhoneNumber = true;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                }
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            ;
        }
    }
}
