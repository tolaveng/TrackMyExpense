using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Services.IServices;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> ConfirmEmailTokenAsync(Guid userId, string token)
        {
            return await _userRepository.ConfirmEmailTokenAsync(userId, token);
        }

        public async Task<Result<Guid>> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            return await _userRepository.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                return _mapper.Map<UserDto>(user);
            }
            return null;
        }

        public async Task<SignInResult> SignInAsync(string email, string password, bool remember)
        {
            return await _userRepository.SignInAsync(email, password, remember);
        }

        public async Task SignOutAsync()
        {
            await _userRepository.SignOutAsync();
        }
    }
}