using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(_mockDataService.getUserMockData(limit));       
        }

        [HttpGet("getInternetMock/{limit}")]
        public IActionResult GetInternetMock(int limit)
        {
            return Ok(_mockDataService.getInternetMockData(limit));
        }

        [HttpGet("getLocationMock/{limit}")]
        public IActionResult GetLocationMock(int limit)
        {
            return Ok(_mockDataService.getLocaltionMockData(limit));
        }

        [HttpGet("getLoremMock/{limit}")]
        public IActionResult GetLoremMock(int limit)
        {
            return Ok(_mockDataService.getLoremMockData(limit));
        }

        [HttpGet("getNotificationMock/{limit}")]
        public IActionResult GetNotificationMock(int limit)
        {
            return Ok(_mockDataService.getNotificationMockData(limit));
        }

        [HttpGet("getPaymentMock/{limit}")]
        public IActionResult GetPaymentMock(int limit)
        {
            return Ok(_mockDataService.getPaymentMockData(limit));
        }
        [HttpGet("getVehicleMock/{limit}")]
        public IActionResult GetVehicleMock(int limit)
        {
            return Ok(_mockDataService.getVehicleMockData(limit));
        }

    }
}
