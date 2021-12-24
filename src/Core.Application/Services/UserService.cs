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