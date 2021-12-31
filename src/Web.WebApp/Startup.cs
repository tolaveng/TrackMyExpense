using Core.Application.IRepositories;
using Core.Infrastructure.Configurations;
using Core.Infrastructure.Database.Identity;
using Core.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Core.Application.Services;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;
using Core.Application.Mapper;
using Core.Infrastructure.Mapper;
using Core.Infrastructure.Database;
using Core.Application.Settings;

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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.LoginPath = "/account/login";
            });
            services.Configure<CookiePolicyOptions>(opt =>
            {
                opt.CheckConsentNeeded = context => true;
                //opt.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetAssembly(typeof(DataMapperProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AppMapperProfile)));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Infrastructure
            AddDatabase(services);

            // .Net Identity
            AddAppIdentity(services, Environment);

            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();

            // Config Settings
            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));

            // Config route option
            services.Configure<RouteOptions>(opt =>
            {
                opt.LowercaseUrls = true;
                opt.LowercaseQueryStrings = true;
            });
            
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                SeedAppUser.SeedAdminUser(app);
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
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

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
            services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt => { 
                if (env.IsDevelopment()) 
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.User.RequireUniqueEmail = true;
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
                    opt.User.RequireUniqueEmail = true;
                }
                // custom reset password token
                opt.Tokens.PasswordResetTokenProvider = "CustomPasswordResetProvider";
                })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<PasswordResetTokenProvider<AppIdentityUser>>("CustomPasswordResetProvider");

            // Set all identity token expiry to 2 days
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(2));

            //// Set password reset token expiry to 3 hours
            //services.Configure<PasswordResetTokenProviderOptions>(opt =>
            //    opt.TokenLifespan = TimeSpan.FromHours(3));
        }
    }
}
