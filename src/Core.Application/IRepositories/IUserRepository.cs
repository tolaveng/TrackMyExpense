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
        Result<AppUser> GetById(Guid userId);
        Task<Result<AppUser>> GetByEmail(string email);

        Task<Result<AppUser>> CreateUser(AppUser appUser);
        Task<Result<AppUser>> UpdateUser(AppUser appUser);

        Task<bool> Disabled(Guid userId);
        
        Task<SignInResult> SignInAsync(string email, string password, bool remember);
        Task SignOutAsync();
    }
}
