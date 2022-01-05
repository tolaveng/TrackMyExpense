using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Core.Application.Services.IServices;
using Core.Application.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IReCaptchaService _reCaptchaService;

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty(Name = "g-recaptcha-response")]
        public string RecaptchaResponse {get; set;}

        public int LoginAttempt { get; set; }

        [BindProperty]
        public IEnumerable<AuthenticationScheme> ExternalLoginProviders { get; set; }

        public LoginModel(IUserService userService, IReCaptchaService reCaptchaService)
        {
            _userService = userService;
            _reCaptchaService = reCaptchaService;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
            ErrorMessage = string.Empty;
            LoginAttempt = HttpContext.Session.GetInt32("LoginAttempt") ?? 0;

            ExternalLoginProviders = await _userService.GetExternalAuthenticationSchemesAsync();

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid) return Page();

            // Login attempt and reCaptcha
            LoginAttempt = HttpContext.Session.GetInt32("LoginAttempt")?? 0;
            if (LoginAttempt >= 3)
            {
                if (string.IsNullOrWhiteSpace(RecaptchaResponse))
                {
                    ErrorMessage = "Security check (reCAPTCHA) is invalid";
                    return Page();
                }
                var isReCaptchaValid = await _reCaptchaService.ValidateCaptcha(RecaptchaResponse);
                if (!isReCaptchaValid)
                {
                    ErrorMessage = "Security check (reCAPTCHA) is not correct";
                    return Page();
                }
            }
            HttpContext.Session.SetInt32("LoginAttempt", ++LoginAttempt);

            // Return Url
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

            var result = await _userService.SignInAsync(Input.Email, Input.Password, Input.RememberMe);
            if (result.Succeeded)
            {
                HttpContext.Session.SetInt32("LoginAttempt", 0);
                return Redirect(returnUrl);    
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2FA", new {ReturnUrl = returnUrl, RememberMe = Input.RememberMe});    
            }

            ErrorMessage = "Please check your email and password are valid!";
            //Input.Email = String.Empty;
            Input.Password = String.Empty;
            //ModelState.Clear();
            return Page();
        }

        public IActionResult OnPostExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _userService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Email is invalid")]
            [StringLength(32, ErrorMessage = "Email too long (32 character limit)")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [StringLength(32, ErrorMessage = "Password too long (32 character limit)")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }
    }
}