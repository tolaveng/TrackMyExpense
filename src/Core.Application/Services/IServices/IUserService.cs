using Core.Application.Common;
using Core.Application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Services.IServices
{
    public interface IUserService
    {
        Task<SignInResponse> SignInAsync(string email, string password, bool remember);
        Task SignOutAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<GenericResponse<Guid>> CreateUserAsync(UserDto userDto);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<bool> ConfirmEmailTokenAsync(Guid userId, string token);
        bool IsEmailConfirmed(Guid userId);

        // External Login
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<GenericResponse<string>> ExternalLoginSignInAsync(ExternalLoginInfo loginInfo);
    }
}