using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.DTO;
using Mockify.API.Helper;
using Mockify.API.Services;

namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet("categories")]
        public IActionResult GetUsersMock()
        {
            return Ok(new ApiResponse<List<GetCategoryDTO>> {
                Data = _categoryService.GetAllCategories(),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("customMockFields")]
        public IActionResult GetCustomMock()
        {
            return Ok(new ApiResponse<GetCategoryDTO>
            {
                Data = _categoryService.GetCustomMockModel(),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

    }
}
