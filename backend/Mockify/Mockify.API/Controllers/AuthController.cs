using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Models;

namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    public class AuthController : Controller
    {
        [HttpPost("auth/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenRequest request)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { "----" } // Replace with your Client ID
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);

                // Here you can create a user in your database or issue a JWT
                return Ok(new
                {
                    Email = payload.Email,
                    Name = payload.Name,
                    GoogleId = payload.Subject
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Invalid token", Error = ex.Message });
            }
        }
    }

    public class GoogleTokenRequest
    {
        public string Token { get; set; }
    }
}
