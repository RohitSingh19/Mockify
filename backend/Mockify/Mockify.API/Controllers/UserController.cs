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
                StatusCode = 200,
                Success = true
            });
        }

        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> Delete(string email, string templateName)
        {
            return Ok(new ApiResponse<object>
            {
                Data = await _userService.DeleteTemplate(email, templateName),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpPut("update/{email}")]
        public async Task<IActionResult> Update([FromBody] TemplateDTO templateDTO, string email, string templateName)
        {
            return Ok(new ApiResponse<object>
            {
                Data = await _userService.UpdateTemplate(email, templateName, templateDTO),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }
    }
}
