using Core.Domain.Entities;
using Core.Application.IRepositories;
using Core.Infrastructure.Database;
using Core.Infrastructure.Database.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Common;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Core.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext context,
            UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager,
            IMapper mapper,
            ILogger<UserRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResponse<Guid>> CreateUserAsync(AppUser appUser)
        {
            try
            {
                var user = _mapper.Map<AppIdentityUser>(appUser);
                var result = await _userManager.CreateAsync(user, appUser.Password);
                if (result.Succeeded)
                {
                    return GenericResponse<Guid>.Success(user.Id);
                }

                return GenericResponse<Guid>.Failure("Cannot create a new user",
                    result.Errors.Select(x => x.Description).ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return GenericResponse<Guid>.Failure("Cannot create a new user");
        }

        public async Task<bool> Disabled(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.IsDisabled = true;
                await _userManager.UpdateAsync(user);
                return true;
            }
            return false;
        }

        public async Task<SignInResponse> SignInAsync(string email, string password, bool remember)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, remember, false);
                //var result = await _signInManager.CheckPasswordSignInAsync(email, password, false);
                return new SignInResponse(result.Succeeded, result.RequiresTwoFactor, result.IsLockedOut);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex.StackTrace);
            }
            return SignInResponse.Failure();
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return _mapper.Map<AppUser>(user);
            }
            return null;
        }
        
        public async Task<GenericResponse<AppUser>> GetByUsername(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                var appUser = _mapper.Map<AppUser>(user);
                return GenericResponse<AppUser>.Success(appUser);
            }
            return GenericResponse<AppUser>.Failure("User not found");
        }

        public AppUser GetById(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                return _mapper.Map<AppUser>(user);
            }
            return null;
        }


        public async Task<GenericResponse<bool>> UpdateUserAsync(AppUser appUser)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == appUser.Id);
            if (user != null)
            {
                user.PhoneNumber = appUser.PhoneNumber;
                user.FullName = appUser.FullName;
                user.Subcription = appUser.Subcription;
                user.Wallet = appUser.Wallet;
                //user.Email = appUser.Email;

                await _userManager.UpdateAsync(user);
                return GenericResponse<bool>.Success();
            }
            return GenericResponse<bool>.Failure("User not found");
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                return await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            return null;
        }

        public async Task<bool> ConfirmEmailTokenAsync(Guid userId, string token)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user)) return true;

                var result = await _userManager.ConfirmEmailAsync(user, token);
                return result.Succeeded;
            }
            return false;
        }

        public bool IsEmailConfirmed(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                return _userManager.IsEmailConfirmedAsync(user).Result;
            }
            return false;
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<GenericResponse<string>> ExternalLoginSignInAsync(ExternalLoginInfo loginInfo)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, false, true);
            if (result.Succeeded)
            {
                return GenericResponse<string>.Success(string.Empty);
            }
            
            var email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var fullName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name);
            if (email == null)
            {
                return GenericResponse<string>.Failure("Log in failed. Cannot get an email from your account.");
            }

            // check if user exist
            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                appUser = new AppIdentityUser()
                {
                    Email = email,
                    UserName = email,
                    FullName = fullName,
                    EmailConfirmed = true
                };
                var createResult = await _userManager.CreateAsync(appUser);
                if (!createResult.Succeeded)
                {
                    return GenericResponse<string>.Failure("Unexpected error occured. Please try again later");
                }
            }

            // add to login and sign in
            var addResult = await _userManager.AddLoginAsync(appUser, loginInfo);
            if (addResult.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
                return GenericResponse<string>.Success(string.Empty);
            }

            return GenericResponse<string>.Failure("Unexpected error occured. Please try again later");
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                return await _userManager.GeneratePasswordResetTokenAsync(user);
            }
            return null;
        }

        public async Task<bool> ResetPasswordAsync(Guid userId, string token, string password)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {   
                var result = await _userManager.ResetPasswordAsync(user, token, password);
                return result.Succeeded;
            }
            return false;
        }
    }
}
