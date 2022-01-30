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
using Microsoft.AspNetCore.Http;
using MudBlazor.Services;
using Core.Infrastructure.Seeder;
using MudBlazor;
using Microsoft.EntityFrameworkCore;

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
            var oAuthFacebook = Configuration.GetSection("OAuthFacebook").Get<OAuthSetting>();
            var oAuthGoogle = Configuration.GetSection("OAuthGoogle").Get<OAuthSetting>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/account/login";
                    opt.LogoutPath = "/account/logout";
                    opt.Cookie.SameSite = SameSiteMode.None;
                })
                .AddFacebook(opt =>
                {
                    opt.AppId = oAuthFacebook.ClientId;
                    opt.AppSecret = oAuthFacebook.ClientSecret;
                    opt.AccessDeniedPath = "/account/login";
                })
                .AddGoogle(opt => {
                    opt.ClientId = oAuthGoogle.ClientId;
                    opt.ClientSecret = oAuthGoogle.ClientSecret;
                })
                ;

            services.Configure<CookiePolicyOptions>(opt =>
            {
                opt.CheckConsentNeeded = context => true;
                //opt.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Requires all users to be authenticated - NOT WORK with Blazor razor component
            // Solution: add [Authorize] attribute to _Import.razor
            //services.AddAuthorization(opt =>
            //{
            //    opt.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetAssembly(typeof(DataMapperProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AppMapperProfile)));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IUnitOfWork).Assembly);
            //services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(UserDto)));

            // Infrastructure
            AddDatabase(services);
            services.AddTransient<IReCaptchaService, ReCaptchaService>();

            // .Net Identity
            AddAppIdentity(services, Environment);
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthUserService, AuthUserService>();

            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<ISysAttributeService, SysAttributeService>();
            services.AddScoped<IPageHtmlService, PageHtmlService > ();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();

            // Config Settings
            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));
            services.Configure<ReCaptchaSetting>(Configuration.GetSection("ReCaptcha"));

            // Config route option
            services.Configure<RouteOptions>(opt =>
            {
                opt.LowercaseUrls = true;
                opt.LowercaseQueryStrings = true;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(opt => {
                //opt.IdleTimeout = TimeSpan.FromMinutes(30);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
            

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            services.AddRazorPages();
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
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

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void AddDatabase(IServiceCollection services)
        {
            services.AddOptions();
            var databaseSetting = Configuration.GetRequiredSection("DatabaseSetting");
            services.Configure<DatabaseSetting>(databaseSetting);
            
            services.AddPooledDbContextFactory<AppDbContext>(opt => {
                if (Environment.IsDevelopment()) opt.EnableSensitiveDataLogging();
                opt.UseNpgsql(DatabaseConnection.GetConnectionString(databaseSetting.Get<DatabaseSetting>()));
            });

            //services.AddDbContext<AppDbContext>();
            services.AddScoped<AppDbContext>(x =>
                x.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext()
            );
        }

        private void AddAppIdentity(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;

                if (env.IsDevelopment()) 
                {
                    opt.Password.RequiredLength = 4;
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

            // Set password reset token expiry to 24 hours
            services.Configure<PasswordResetTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(24));
        }
    }
}
