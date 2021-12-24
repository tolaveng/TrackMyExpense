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
using SignInResult = Core.Application.Common.SignInResult;

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

        public async Task<Result<AppUser>> CreateUser(AppUser appUser)
        {
            var user = _mapper.Map<AppIdentityUser>(appUser);
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return Result<AppUser>.Success(appUser);
            }
            return Result<AppUser>.Failure("Cannot create a new user",
                result.Errors.Select(x => x.Description).ToArray());
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

        public async Task<SignInResult> SignInAsync(string email, string password, bool remember)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, remember, false);
                //var result = await _signInManager.CheckPasswordSignInAsync(email, password, false);
                return new SignInResult(result.Succeeded, result.RequiresTwoFactor, result.IsLockedOut);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex.StackTrace);
            }
            return SignInResult.Failure();
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Result<AppUser>> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var appUser = _mapper.Map<AppUser>(user);
                return Result<AppUser>.Success(appUser);
            }
            return Result<AppUser>.Failure("User not found");
        }
        
        public async Task<Result<AppUser>> GetByUsername(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                var appUser = _mapper.Map<AppUser>(user);
                return Result<AppUser>.Success(appUser);
            }
            return Result<AppUser>.Failure("User not found");
        }

        public Result<AppUser> GetById(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null)
            {
                var appUser = _mapper.Map<AppUser>(user);
                return Result<AppUser>.Success(appUser);
            }
            return Result<AppUser>.Failure("User not found");
        }


        public async Task<Result<AppUser>> UpdateUser(AppUser appUser)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == appUser.Id);
            if (user == null)
            {
                user.PhoneNumber = appUser.PhoneNumber;
                user.FullName = appUser.FullName;
                user.Subcription = appUser.Subcription;
                user.Wallet = appUser.Wallet;
                user.Email = appUser.Email;

                await _userManager.UpdateAsync(user);
                return Result<AppUser>.Success(appUser);
            }
            return Result<AppUser>.Failure("User not found");
        }
    }
}
