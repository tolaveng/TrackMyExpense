using Core.Application.Models;
using Core.Application.Services.IServices;
using System.Security.Claims;

namespace Web.WebApi.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        public async Task<UserDto> GetUser([Service] IAuthUserService authUserService,
            ClaimsPrincipal claimsPrincipal)
        {
            var user = await authUserService.GetAuthUserAsync();
            return user ?? new UserDto();
        }
    }
}
