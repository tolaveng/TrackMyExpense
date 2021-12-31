using System.Threading.Tasks;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Web.WebApp.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IUserService _userService;
        
        public LogoutModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            await _userService.SignOutAsync();
            
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return LocalRedirect("/");
            }
        }
        
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _userService.SignOutAsync();
            
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return LocalRedirect("/");
            }
        }
    }
}