using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Helper;
using Mockify.API.Services;

namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    [EnableCors("MyCorsPolicy")]
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
            return Ok(_mockDataService.GetUserMockData(limit));       
        }

        [HttpGet("getInternetMock/{limit}")]
        public IActionResult GetInternetMock(int limit)
        {
            return Ok(_mockDataService.GetInternetMockData(limit));
        }

        [HttpGet("getLocationMock/{limit}")]
        public IActionResult GetLocationMock(int limit)
        {
            return Ok(_mockDataService.GetLocaltionMockData(limit));
        }

        [HttpGet("getLoremMock/{limit}")]
        public IActionResult GetLoremMock(int limit)
        {
            return Ok(_mockDataService.GetLoremMockData(limit));
        }

        [HttpGet("getNotificationMock/{limit}")]
        public IActionResult GetNotificationMock(int limit)
        {
            return Ok(_mockDataService.GetNotificationMockData(limit));
        }

        [HttpGet("getPaymentMock/{limit}")]
        public IActionResult GetPaymentMock(int limit)
        {
            return Ok(_mockDataService.GetPaymentMockData(limit));
        }
        
        [HttpGet("getVehicleMock/{limit}")]
        public IActionResult GetVehicleMock(int limit)
        {
            return Ok(_mockDataService.GetVehicleMockData(limit));
        }

        [HttpGet("getRandomizerMock/{limit}")]
        public IActionResult GetRandomizerMock(int limit)
        {
            return Ok(_mockDataService.GetRandomizerMockData(limit));
        }

        [HttpGet("getFileSystemMock/{limit}")]
        public IActionResult GetFileSystemMock(int limit)
        {
            return Ok(_mockDataService.GetFileSystemMockData(limit));
        }

        [HttpGet("getCustomMock/{limit}")]
        public IActionResult GetCustomMock([FromBody] CustomCategoryRequestItems customCategoryRequestItems, int limit)
        {
            return Ok();
        }

    }
}
