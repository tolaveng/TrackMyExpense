using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty] public InputModel Input { get; set; }
        
        public string ReturnUrl { get; set; }
        
        [TempData]
        public string ErrorMessage { get; set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
            ErrorMessage = string.Empty;
            
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/";
            // check absolute url is not base url, do nothing
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                if (Uri.IsWellFormedUriString(returnUrl, UriKind.Absolute) && !returnUrl.StartsWith(baseUrl))
                {
                    return Unauthorized();
                }
            }
            else
            {
                returnUrl = baseUrl;   
            }

            if (!ModelState.IsValid) return Page();
            var result = await _userService.SignInAsync(Input.Email, Input.Password, Input.RememberMe);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);    
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2FA", new {ReturnUrl = returnUrl, RememberMe = Input.RememberMe});    
            }

            ErrorMessage = "Invalid Email and/or Password.";
            return Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [StringLength(32, ErrorMessage = "Email too long (32 character limit)")]
            public string Email { get; set; }

            [Required]
            [StringLength(32, ErrorMessage = "Password too long (32 character limit)")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }
    }
}