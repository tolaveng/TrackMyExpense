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
        
        public string ReturnUrl { get; set; }
        
        public LogoutModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
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