using System.Threading.Tasks;
using Core.Application.Common;
using Core.Application.Models;

namespace Core.Application.Services.IServices
{
    public interface IUserService
    {
        Task<SignInResult> SignInAsync(string email, string password, bool remember);
        Task SignOutAsync();
    }
}