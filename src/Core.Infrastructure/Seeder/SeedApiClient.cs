using Core.Infrastructure.Configurations;
using Core.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using System;
using System.Linq;

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

                var oidcSetting = serviceScope.ServiceProvider.GetRequiredService<IOptions<OidcSetting>>().Value;

                if (string.IsNullOrWhiteSpace(oidcSetting.ClientId) ||
                    string.IsNullOrWhiteSpace(oidcSetting.ClientSecret))
                {
                    throw new InvalidOperationException("Client id and secret must be set");
                }

                var manager = serviceScope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
                var existingClientApp = manager.FindByClientIdAsync(oidcSetting.ClientId).GetAwaiter().GetResult();
                if (existingClientApp == null)
                {
                    // Studpid, RedirectUris is readonly,
                    var redirectUris = oidcSetting.RedirectUris.Split(',')
                        .Select(x => new Uri(x.Trim())).ToArray();
                    var redirectUri1 = redirectUris[0];
                    var redirectUri2 = redirectUris.Length > 1 ? redirectUris[1] : null;

                    manager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = oidcSetting.ClientId,
                        ClientSecret = oidcSetting.ClientSecret,
                        RedirectUris = { redirectUri1, redirectUri2 },
                        PostLogoutRedirectUris = { new Uri(oidcSetting.PostLogoutRedirectUri) },
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
