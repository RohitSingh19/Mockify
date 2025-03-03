using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;


namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    public class AuthController : Controller
    {
        private readonly IConfiguration Configuration;
        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost("auth/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenRequest request)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { Configuration["Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);
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
