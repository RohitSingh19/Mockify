using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Helper;
using Mockify.API.Services;

namespace Mockify.API.Controllers
{
    [Route("api/v1/")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class MockDataController : ControllerBase
    {
        private IMockDataService _mockDataService;
        public MockDataController(IMockDataService mockDataService)
        {
              _mockDataService = mockDataService;
        }

        [HttpGet("user/{limit}")]
        public IActionResult GetUsersMock(int limit = 100)
        {
            return Ok(_mockDataService.GetUserMockData(limit));       
        }

        [HttpGet("internet/{limit}")]
        public IActionResult GetInternetMock(int limit = 100)
        {
            return Ok(_mockDataService.GetInternetMockData(limit));
        }

        [HttpGet("location/{limit}")]
        public IActionResult GetLocationMock(int limit = 100)
        {
            return Ok(_mockDataService.GetLocaltionMockData(limit));
        }

        [HttpGet("lorem/{limit}")]
        public IActionResult GetLoremMock(int limit = 100)
        {
            return Ok(_mockDataService.GetLoremMockData(limit));
        }

        [HttpGet("notification/{limit}")]
        public IActionResult GetNotificationMock(int limit = 100)
        {
            return Ok(_mockDataService.GetNotificationMockData(limit));
        }

        [HttpGet("payment/{limit}")]
        public IActionResult GetPaymentMock(int limit = 100)
        {
            return Ok(_mockDataService.GetPaymentMockData(limit));
        }
        
        [HttpGet("vehicle/{limit}")]
        public IActionResult GetVehicleMock(int limit = 100)
        {
            return Ok(_mockDataService.GetVehicleMockData(limit));
        }

        [HttpGet("randomizer/{limit}")]
        public IActionResult GetRandomizerMock(int limit = 100)
        {
            return Ok(_mockDataService.GetRandomizerMockData(limit));
        }

        [HttpGet("fileSystem/{limit}")]
        public IActionResult GetFileSystemMock(int limit = 100)
        {
            return Ok(_mockDataService.GetFileSystemMockData(limit));
        }

        [HttpPost("custom/{limit}")]
        public IActionResult GetCustomMock([FromBody] CustomCategoryRequestItems customCategoryRequestItems, int limit = 100)
        {
            return Ok(_mockDataService.GenerateCustomMockJson(limit, customCategoryRequestItems));
        }

    }
}
