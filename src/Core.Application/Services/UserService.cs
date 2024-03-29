﻿using AutoMapper;
using Core.Application.Common;
using Core.Application.IRepositories;
using Core.Application.Models;
using Core.Application.Services.IServices;
using Core.Domain.Constants;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

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

        public async Task<GenericResponse<Guid>> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);

            if (string.IsNullOrWhiteSpace(user.CultureInfo))
            {
                user.CultureInfo = DefaultConstants.DefaultCultureInfo;
            }

            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            return await _userRepository.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await _userRepository.GetExternalAuthenticationSchemesAsync();
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _userRepository.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Password = string.Empty;
                userDto.TimeZone = !string.IsNullOrWhiteSpace(userDto.TimeZone) ? userDto.TimeZone : DefaultConstants.DefaultTimeZone;
                userDto.CultureInfo = !string.IsNullOrWhiteSpace(userDto.CultureInfo) ? userDto.CultureInfo : DefaultConstants.DefaultCultureInfo;
                return userDto;
            }
            return null;
        }

        public bool IsEmailConfirmed(Guid userId)
        {
            return _userRepository.IsEmailConfirmed(userId);
        }

        public async Task<SignInResponse> SignInAsync(string email, string password, bool remember)
        {
            return await _userRepository.SignInAsync(email, password, remember);
        }

        public async Task SignOutAsync()
        {
            await _userRepository.SignOutAsync();
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _userRepository.GetExternalLoginInfoAsync();
        }

        public async Task<GenericResponse<Guid>> ExternalLoginSignInAsync(ExternalLoginInfo loginInfo)
        {
            return await _userRepository.ExternalLoginSignInAsync(loginInfo);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            return await _userRepository.GeneratePasswordResetTokenAsync(userId);
        }

        public async Task<bool> CheckPasswordAsync(Guid userId, string password)
        {
            return await _userRepository.CheckPasswordAsync(userId, password);
        }
        public async Task<bool> ResetPasswordAsync(Guid userId, string token, string password)
        {
            return await _userRepository.ResetPasswordAsync(userId, token, password);
        }

        public PagedResponse<UserDto> GetUsers(string search, Pagination pagination)
        {
            var result = _userRepository.GetUsers(search, pagination);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(result.Data);
            return PagedResponse<UserDto>.Result(userDtos, result.TotalCount);
        }

        public int GetCount()
        {
            return _userRepository.GetCount();
        }

        public UserDto GetById(Guid userId)
        {
            var appUser = _userRepository.GetById(userId);
            if (appUser == null) throw new ApplicationException("User not found.");

            var userDto = _mapper.Map<UserDto>(appUser);
            userDto.Password = string.Empty;
            userDto.TimeZone = !string.IsNullOrWhiteSpace(userDto.TimeZone) ? userDto.TimeZone : DefaultConstants.DefaultTimeZone;
            userDto.CultureInfo = !string.IsNullOrWhiteSpace(userDto.CultureInfo) ? userDto.CultureInfo : DefaultConstants.DefaultCultureInfo;
            return userDto;
        }

        public async Task<GenericResponse<bool>> UpdateUserAsync(UserDto userDto)
        {
            var appUser = _mapper.Map<AppUser>(userDto);
            return await _userRepository.UpdateUserAsync(appUser);
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            return await _userRepository.UpdatePasswordAsync(userId, currentPassword, newPassword);
        }
    }
}