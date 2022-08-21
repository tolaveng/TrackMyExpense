using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace Web.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiEndPoint : Controller
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test, no authorize.");
        }

        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpGet("authorizetest")]
        public IActionResult AuthorizeTest()
        {
            var subject = User.GetClaim("sub");
            var name = User.Identity.Name;
            return Ok("Done, authorize only.");
        }
    }
}
