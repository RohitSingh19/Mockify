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
    public class MockDataController : ControllerBase
    {
        private IMockDataService _mockDataService;
        public MockDataController(IMockDataService mockDataService)
        {
              _mockDataService = mockDataService;
        }

        [HttpGet("getUserMock/{limit}")]
        public IActionResult GetUsersMock(int limit)
        {
            return Ok(_mockDataService.getUserMockData());       
        }

    }
}
