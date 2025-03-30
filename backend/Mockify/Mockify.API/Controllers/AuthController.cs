using Bogus.DataSets;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Models;
using Mockify.API.Services;
using Newtonsoft.Json.Linq;


namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            Configuration = configuration;
            _userService = userService;
        }

        [HttpPost("auth/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenRequest request)
        {
            
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { Configuration["Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);
            if (payload == null) { return BadRequest(); }
                
            Models.DB.User user = new Models.DB.User();

            user.Email = payload.Email;
            user.Name = payload.Name;
            user.LastLogin = DateTime.Now;
            user.Token = request.Token;

            await _userService.AddUser(user);

            return Ok(new
            {
                Email = payload.Email,
                Name = payload.Name,
                Picture = payload.Picture,
                Token = request.Token
            });
            
        }
    }

    public class GoogleTokenRequest
    {
        public string Token { get; set; }
    }
}
