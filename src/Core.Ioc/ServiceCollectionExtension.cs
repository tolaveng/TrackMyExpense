using Core.Application.IRepositories;
using Core.Application.Mapper;
using Core.Application.Providers;
using Core.Application.Providers.IProviders;
using Core.Application.Services;
using Core.Application.Services.IServices;
using Core.Infrastructure.Configurations;
using Core.Infrastructure.Database;
using Core.Infrastructure.Database.Identity;
using Core.Infrastructure.Mapper;
using Core.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;


namespace Core.Ioc
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetAssembly(typeof(DataMapperProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AppMapperProfile)));

            services.AddMediatR(Assembly.GetExecutingAssembly(),
                typeof(IUnitOfWork).Assembly,
                typeof(UnitOfWork).Assembly);

            //services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(UserDto)));

            // Infrastructure
            AddDatabase(services, configuration, env);

            // .Net Identity
            AddAppIdentity(services, env);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ISysAttributeService, SysAttributeService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<IFileDirectoryProvider, FileDirectoryProvider>();
            services.AddSingleton<IUriResolver, UriResolver>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ICurrencyProvider, CurrencyProvider>();

            // Factory and Ioc
            services.AddSingleton<IFileUploadFactory, FileUploadFactory>();
            services.AddSingleton<LocalFileUploadService>()
            .AddSingleton<IFileUploadService, LocalFileUploadService>(x => x.GetService<LocalFileUploadService>());
            services.AddSingleton<AzureFileUploadService>()
            .AddSingleton<IFileUploadService, AzureFileUploadService>(x => x.GetService<AzureFileUploadService>());

        }

        private static void AddDatabase(IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddOptions();
            var databaseSetting = configuration.GetRequiredSection("DatabaseSetting");
            services.Configure<DatabaseSetting>(databaseSetting);

            services.AddPooledDbContextFactory<AppDbContext>(opt => {
                if (env.IsDevelopment()) opt.EnableSensitiveDataLogging();
                opt.UseNpgsql(DatabaseConnection.GetConnectionString(databaseSetting.Get<DatabaseSetting>()));
            });

            //services.AddDbContext<AppDbContext>();
            services.AddScoped<AppDbContext>(x =>
                x.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext()
            );
        }

        private static void AddAppIdentity(IServiceCollection services, IWebHostEnvironment env)
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
