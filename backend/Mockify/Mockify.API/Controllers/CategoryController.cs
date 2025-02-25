using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
           return Ok(_categoryService.GetAllCategories());
        }

        [HttpGet("customMockFields")]
        public IActionResult GetCustomMock()
        {
            return Ok(_categoryService.GetCustomMockModel());
        }

    }
}
