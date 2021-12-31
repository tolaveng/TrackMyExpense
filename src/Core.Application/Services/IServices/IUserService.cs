using Core.Application.Common;
using Core.Application.Models;

namespace Core.Application.Services.IServices
{
    public interface IUserService
    {
        Task<SignInResult> SignInAsync(string email, string password, bool remember);
        Task SignOutAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<Result<Guid>> CreateUserAsync(UserDto userDto);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<bool> ConfirmEmailTokenAsync(Guid userId, string token);
    }
}