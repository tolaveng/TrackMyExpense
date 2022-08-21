using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Core.Application.Services;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;
using Core.Application.Settings;
using Microsoft.AspNetCore.Http;
using MudBlazor.Services;
using Core.Infrastructure.Seeder;
using MudBlazor;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Core.Ioc;
using System.Net;
using System.Security.Claims;
using System;
using Core.Infrastructure.Database;
using OpenIddict.Validation;
using System.Diagnostics;
using Microsoft.AspNetCore;
using static OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreHandlers;
using Microsoft.IdentityModel.Tokens;

namespace Web.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IWebHostEnvironment environment
            )
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public IAuthUserService AuthUserService { get; }

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
                opt.MinimumSameSitePolicy = SameSiteMode.Lax; //for OAuth
            });

            // Requires all users to be authenticated - NOT WORK with Blazor razor component
            // Solution: add [Authorize] attribute to _Import.razor
            //services.AddAuthorization(opt =>
            //{
            //    opt.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});

            // Cor.Ioc
            services.ConfigureServices(Configuration, Environment);

            // OpendIddict
            // https://documentation.openiddict.com/guides/getting-started.html
            services.AddOpenIddict()
                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and models.
                    // Note: call ReplaceDefaultEntities() to replace the default entities.
                    options.UseEntityFrameworkCore()
                           .UseDbContext<AppDbContext>();
                })
                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                    // Enable the token endpoint.
                    options.SetTokenEndpointUris("/connect/token")
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetUserinfoEndpointUris("/connect/userinfo")
                        .SetIntrospectionEndpointUris("/connect/introspect")
                        ;

                    // Flow
                    //options.AllowClientCredentialsFlow();
                    options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();
                    options.AllowRefreshTokenFlow();

                    // Using reference tokens means the actual access and refresh tokens
                    // are stored in the database and different tokens, referencing the actual
                    // tokens (in the db), are used in request headers. The actual tokens are not
                    // made public.
                    //options.UseReferenceAccessTokens();
                    //options.UseReferenceRefreshTokens();

                    //options.AddEncryptionKey(new SymmetricSecurityKey(
                    //    Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

                    // Register scopes (permissions)
                    options.RegisterScopes("api");

                    // Set the lifetime of your tokens
                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));

                    // Register the signing and encryption credentials.
                    options.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate();

                    // Encryption and signing of tokens
                    options.AddEphemeralEncryptionKey()
                        .AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption();

                    // Register the ASP.NET Core host and configure the ASP.NET Core options.
                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough()
                           .EnableAuthorizationEndpointPassthrough();
                })
                // Register the OpenIddict validation handler.
                // Note: the OpenIddict validation handler is only compatible with the
                // default token format or with reference tokens and cannot be used with
                // JWT tokens. For JWT tokens, use the Microsoft JWT bearer handler.
                // Register the OpenIddict validation components.
                .AddValidation(options =>
                 {
                     // Import the configuration from the local OpenIddict server instance.
                     options.UseLocalServer();

                     // Register the ASP.NET Core host.
                     options.UseAspNetCore();
                 });

            // Web App
            services.AddTransient<IReCaptchaService, ReCaptchaService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IPageHtmlService, PageHtmlService>();

            // Config Settings
            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));
            services.Configure<FileUploadSetting>(Configuration.GetSection("FileUploadSetting"));
            services.Configure<ReCaptchaSetting>(Configuration.GetSection("ReCaptcha"));
            services.Configure<AzureStorageSetting>(Configuration.GetSection("AzureStorage"));

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

            services.AddHttpClient();

            services.AddRazorPages();
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            //services.AddSignalR(opt =>
            //{
            //    opt.EnableDetailedErrors = true;
            //    opt.MaximumReceiveMessageSize = long.MaxValue;
            //});
            services.AddServerSideBlazor()
                .AddHubOptions(options => options.MaximumReceiveMessageSize = 5 * 1024 * 1024); //5MB
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
                app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
            }


            // Cread OpenIdConnect client
            SeedApiClient.CreateApiClient(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0
            var runDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            app.UseStaticFiles(new StaticFileOptions
            {

                FileProvider = new PhysicalFileProvider(Path.Combine(runDir, "StaticFiles")),
                RequestPath = "/staticfiles"
            });

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            // Server local file storage, otherwise using cloud
            if (env.IsDevelopment())
            {
                // Serve local upload file
                var fileUploadSetting = Configuration.GetSection("FileUploadSetting").Get<FileUploadSetting>();
                if (!Directory.Exists(fileUploadSetting.UploadDir))
                {
                    Directory.CreateDirectory(fileUploadSetting.UploadDir);
                }
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(fileUploadSetting.UploadDir),
                    RequestPath = new PathString(fileUploadSetting.UploadWebUrl),
                    // allow authenticated user only
                    OnPrepareResponse = ctx =>
                    {
                        if (!ctx.Context.Request.Path.ToString().ToLower().Contains("attachments")) return;

                        // if user access to his/her own attachment directory
                        if (ctx.Context.User != null && ctx.Context.User.Identity.IsAuthenticated)
                        {
                            var userIdClaim = ctx.Context.User.FindFirst(ClaimTypes.NameIdentifier);
                            if (userIdClaim == null ||
                                string.IsNullOrEmpty(userIdClaim.Value) ||
                                !ctx.Context.Request.Path.ToString().Contains(userIdClaim.Value))
                            {
                                    ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                    ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
                                    ctx.Context.Response.ContentLength = 0;
                                    ctx.Context.Response.Body = Stream.Null;
                            }
                        } else {
                            ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
                            ctx.Context.Response.ContentLength = 0;
                            ctx.Context.Response.Body = Stream.Null;
                        }
                    }
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
