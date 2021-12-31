using Core.Application.Common;
using Core.Domain.Entities;
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

        Task<Result<Guid>> CreateUserAsync(AppUser appUser);
        Task<Result<bool>> UpdateUserAsync(AppUser appUser);

        Task<bool> Disabled(Guid userId);
        
        Task<SignInResult> SignInAsync(string email, string password, bool remember);
        Task SignOutAsync();
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<bool> ConfirmEmailTokenAsync(Guid userId, string token);
    }
}
