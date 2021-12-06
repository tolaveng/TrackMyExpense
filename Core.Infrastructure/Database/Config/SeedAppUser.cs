using Core.Domain.Enums;
using Core.Infrastructure.Database.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Config
{
    public static class SeedAppUser
    {
        public static async void SeedDeveloperUser(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

                var devUser = new AppUser
                {
                    UserName = "dev",
                    FullName = "Developer",
                    NormalizedUserName = "DEV",
                    Email = "dev@dev.local",
                    NormalizedEmail = "DEV@DEV.LOCAL",
                    PhoneNumber = "1",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if (!context.Users.Any(u => u.UserName == devUser.UserName))
                {
                    //var password = new PasswordHasher<AppUser>();
                    //var hashed = password.HashPassword(devUser, "dev");
                    //devUser.PasswordHash = hashed;

                    await userManager.CreateAsync(devUser, "dev");
                    await userManager.AddToRoleAsync(devUser, UserBaseRole.Developer.ToString());
                }
            }
        }
    }
}
