using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Models;
using System.Reflection;

namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("getCategories")]
        public IActionResult GetUsersMock()
        {
            var data = from t in Assembly.GetExecutingAssembly().GetTypes()
                       where t.IsClass && t.Namespace == "Mockify.API.Models"
                       select t;
            
            List<string> categories = data.Select(x=> x.Name).ToList();

            return Ok(categories);
        }
    }
}
