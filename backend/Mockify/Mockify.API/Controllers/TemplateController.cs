using Google.Apis.Auth;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mockify.API.DTO;
using Mockify.API.Helper;
using Mockify.API.Services;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Mockify.API.Controllers
{
    [Route("api/v1/template")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly IConfiguration _configuration;
        public TemplateController(ITemplateService templateService, IConfiguration configuration)
        {
            _templateService = templateService;
            _configuration = configuration;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveTemplate([FromHeader(Name = "Authorization")]string userToken, 
            [FromBody] TemplateDTO templateDTO)
        {
            var userEmail = await GetUserEmail(userToken);            
            if (string.IsNullOrEmpty(userEmail)) {
                BadRequestObjectResult();
            }

            return Ok(new ApiResponse<object>
            {
                Data = await _templateService.AddTemplate(userEmail, templateDTO),
                Message = "Success",
                StatusCode = 201,
                Success = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTemplates([FromHeader(Name = "Authorization")] string userToken)
        {
            var userEmail = await GetUserEmail(userToken);
            if (string.IsNullOrEmpty(userEmail))
            {
                BadRequestObjectResult();
            }

            return Ok(new ApiResponse<object>
            {
                Data = await _templateService.GetTemplates(userEmail),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpDelete("delete/{templateName}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string userToken, string templateName)
        {
            var userEmail = await GetUserEmail(userToken);
            if (string.IsNullOrEmpty(userEmail))
            {
                BadRequestObjectResult();
            }

            return Ok(new ApiResponse<object>
            {
                Data = await _templateService.DeleteTemplate(userEmail, templateName),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpPut("update/{templateName}")]
        public async Task<IActionResult> Update([FromBody] TemplateDTO templateDTO,
                [FromHeader(Name = "Authorization")] string userToken, string templateName)
        {
            var userEmail = await GetUserEmail(userToken);
            if (string.IsNullOrEmpty(userEmail))
            {
                BadRequestObjectResult();
            }

            return Ok(new ApiResponse<object>
            {
                Data = await _templateService.UpdateTemplate(userEmail, templateDTO, templateName),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });

        
        }

        private async Task<string> GetUserEmail(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            if (payload == null) {
                return string.Empty;
            }
            return payload.Email;
        }
        
        private BadRequestObjectResult BadRequestObjectResult()
        {
            return BadRequest(new ApiResponse<object>
            {
                Data = null,
                Message = "Invalid token",
                StatusCode = 400,
                Success = false
            });
        }

    }
}
