using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Models;
using Mockify.API.Services;
using System.Reflection;

namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
        _categoryService = categoryService;
        }

        [HttpGet("getCategories")]
        public IActionResult GetUsersMock()
        {
           var categories = _categoryService.GetAllCategories();
           return Ok(categories);
        }


    }
}
