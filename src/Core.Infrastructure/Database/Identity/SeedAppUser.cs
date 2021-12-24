using Core.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Core.Infrastructure.Database.Identity
{
    public static class SeedAppUser
    {
        public static async void SeedAdminUser(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppIdentityUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppIdentityRole>>();

                var adminUser = new AppIdentityUser
                {
                    UserName = "admin@local.dev",
                    FullName = "Administrator User",
                    NormalizedUserName = "ADMIN@LOCAL.DEV",
                    Email = "admin@local.dev",
                    NormalizedEmail = "ADMIN@LOCAL.DEV",
                    PhoneNumber = "1",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if (!context.Users.Any(u => u.UserName == adminUser.UserName))
                {
                    //var password = new PasswordHasher<AppUser>();
                    //var hashed = password.HashPassword(adminUser, "admin");
                    //devUser.PasswordHash = hashed;

                    await userManager.CreateAsync(adminUser, "admin");
                    await userManager.AddToRoleAsync(adminUser, UserBaseRole.Admin);
                }
            }
        }
    }
}
