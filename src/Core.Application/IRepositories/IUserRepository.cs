using Core.Application.Common;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.IRepositories
{
    public interface IUserRepository
    {
        AppUser GetById(Guid userId);
        Task<AppUser> GetByEmailAsync(string email);

        Task<GenericResponse<Guid>> CreateUserAsync(AppUser appUser);

        Task<bool> Disabled(Guid userId);
        
        Task<SignInResponse> SignInAsync(string email, string password, bool remember);
        Task SignOutAsync();
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task<bool> ConfirmEmailTokenAsync(Guid userId, string token);
        Task<bool> ResetPasswordAsync(Guid userId, string token, string password);
        bool IsEmailConfirmed(Guid userId);

        // External Login
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<GenericResponse<string>> ExternalLoginSignInAsync(ExternalLoginInfo loginInfo);

        int GetCount();
        PaginationResponse<AppUser> GetUsers(string search, Pagination pagination);
        Task<GenericResponse<bool>> UpdateUserAsync(AppUser appUser);
    }
}
