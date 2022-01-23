using Core.Application.Models;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public AuthUserService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public async Task<UserDto?> GetAuthUserAsync()
        {
            if (_httpContextAccessor.HttpContext.User == null)
            {
                return null;
            }

            var user = _httpContextAccessor.HttpContext.User;
            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value) && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return _userService.GetById(userId);
            }

            var userEmailClaim = user.FindFirst(ClaimTypes.Email);
            if (userEmailClaim != null && !string.IsNullOrEmpty(userEmailClaim.Value))
            {
                return await _userService.GetUserByEmailAsync(userEmailClaim.Value);
            }

            return null;
        }
    }
}
