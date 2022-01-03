using Core.Application.Models;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("index");

            if (remoteError != null)
            {
                return BadRequest();
            }

            var loginInfo = await _userService.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return LocalRedirect("/account/login?error_message=Sorry, cannot log in with the third party");
            }

            var response = await _userService.ExternalLoginSignInAsync(loginInfo);
            if (response.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            return LocalRedirect($"/account/login?error_message={response.Message}");
        }
    }
}
