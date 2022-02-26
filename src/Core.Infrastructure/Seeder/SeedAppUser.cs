using Core.Domain.Constants;
using Core.Domain.Entities;
using Core.Infrastructure.Database;
using Core.Infrastructure.Database.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Core.Infrastructure.Seeder
{
    public static class SeedAppUser
    {
        public static async void SeedAdminUser(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                //var dbContextFactory = serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                //var context = await dbContextFactory.CreateDbContextAsync();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppIdentityUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppIdentityRole>>();

                var adminUserId = Guid.Parse("68b59f54-fe8c-4a03-b597-c4771560c60b");
                
                var adminUser = new AppIdentityUser
                {
                    Id = adminUserId,
                    UserName = "admin@local.dev",
                    FullName = "Administrator User",
                    NormalizedUserName = "ADMIN@LOCAL.DEV",
                    Email = "admin@local.dev",
                    NormalizedEmail = "ADMIN@LOCAL.DEV",
                    PhoneNumber = "0400000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Currency = "AUD"
                };

                if (!context.Users.Any(u => u.UserName == adminUser.UserName))
                {
                    //var password = new PasswordHasher<AppUser>();
                    //var hashed = password.HashPassword(adminUser, "admin");
                    //devUser.PasswordHash = hashed;

                    var result = await userManager.CreateAsync(adminUser, "admin");
                    if (!result.Succeeded) return;
                    await userManager.AddToRoleAsync(adminUser, UserBaseRole.Admin);

                    // Create subscription
                    var subscription = new Subscription()
                    {
                        Id = Guid.NewGuid(),
                        UserId = adminUserId,
                        SubscriptionType = Domain.Enums.SubscriptionType.Unlimited,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = adminUserId
                    };
                    context.Subscriptions.Add(subscription);
                    context.SaveChanges();
                }
            }
        }
    }
}
