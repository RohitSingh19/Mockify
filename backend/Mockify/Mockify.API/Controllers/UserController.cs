using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.DTO;
using Mockify.API.Helper;
using Mockify.API.Services;

namespace Mockify.API.Controllers
{
    [Route("api/v1/user-template")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("save/{email}")]
        public async Task<IActionResult> SaveTemplate([FromBody] TemplateDTO templateDTO, string email)
        {
            return Ok(new ApiResponse<object>
            {
                Data = await _userService.AddTemplate(email, templateDTO.TemplateName, templateDTO),
                Message = "Success",
                StatusCode = 201,
                Success = true
            });
        }

        [HttpGet("getAllTemplates")]
        public async Task<IActionResult> GetAllTemplates(string email)
        {
            return Ok(new ApiResponse<object>
            {
                Data = await _userService.GetTemplates(email),
                Message = "Success",
                StatusCode = 201,
                Success = true
            });
        }
    }
}
