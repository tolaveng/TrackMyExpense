using Core.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Seeder
{
    public static class SeedApiClient
    {
        public static void CreateApiClient(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();

                var manager = serviceScope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
                var existingClientApp = manager.FindByClientIdAsync("api-client").GetAwaiter().GetResult();
                if (existingClientApp == null)
                {
                    manager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "api-client",
                        ClientSecret = "499D56FA-B47B-5199-BA61-B298D431C318",
                        RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                        PostLogoutRedirectUris = { new Uri("http://localhost:53507/signout-callback-oidc") },
                        //AQAAAAEAACcQAAAAEPO7vne7va67W61jDZwOZSTF1FfLAcKKK40zf0n3yHSmIDmvMToZjs0cXlqhyOXwuQ==
                        DisplayName = "Api Client",
                        Permissions = {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.Endpoints.Introspection,

                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            //OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,

                            OpenIddictConstants.Permissions.ResponseTypes.Code,

                            OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                        },
                        
                    }).GetAwaiter().GetResult();
                }

            }
        }
    }
}
