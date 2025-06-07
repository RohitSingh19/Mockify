using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.DTO;
using Mockify.API.Helper;
using Mockify.API.Models;
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
        public IActionResult GetUsersMock(int limit)
        {
            return Ok(new ApiResponse<List<User>>
            {
                Data = _mockDataService.GetUserMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("internet/{limit}")]
        public IActionResult GetInternetMock(int limit)
        {
            return Ok(new ApiResponse<List<Internet>>
            {
                Data = _mockDataService.GetInternetMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("location/{limit}")]
        public IActionResult GetLocationMock(int limit)
        {
            return Ok(new ApiResponse<List<Location>>
            {
                Data = _mockDataService.GetLocationMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("lorem/{limit}")]
        public IActionResult GetLoremMock(int limit)
        {
            return Ok(new ApiResponse<List<Lorem>>
            {
                Data = _mockDataService.GetLoremMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("notification/{limit}")]
        public IActionResult GetNotificationMock(int limit)
        {
            return Ok(new ApiResponse<List<Notification>>
            {
                Data = _mockDataService.GetNotificationMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
            
        }

        [HttpGet("payment/{limit}")]
        public IActionResult GetPaymentMock(int limit)
        {
            return Ok(new ApiResponse<List<Payment>>
            {
                Data = _mockDataService.GetPaymentMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }
        
        [HttpGet("vehicle/{limit}")]
        public IActionResult GetVehicleMock(int limit)
        {
            return Ok(new ApiResponse<List<Vehicle>>
            {
                Data = _mockDataService.GetVehicleMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("randomizer/{limit}")]
        public IActionResult GetRandomizerMock(int limit)
        {
            return Ok(new ApiResponse<List<Randomizer>>
            {
                Data = _mockDataService.GetRandomizerMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("fileSystem/{limit}")]
        public IActionResult GetFileSystemMock(int limit)
        {
            return Ok(new ApiResponse<List<FileSystem>>
            {
                Data = _mockDataService.GetFileSystemMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpGet("image/{limit}")]
        public IActionResult GetImageMock(int limit)
        {
            return Ok(new ApiResponse<List<Image>>
            {
                Data = _mockDataService.GetImageMockData(limit),
                Message = "Success",
                StatusCode = 200,
                Success = true
            });
        }

        [HttpPost("custom/{limit}")]
        public IActionResult GetCustomMock([FromBody] CustomCategoryRequestItems customCategoryRequestItems, int limit)
        {
            return Ok(new ApiResponse<string>
            {
                Data = _mockDataService.GenerateCustomMockJson(limit, customCategoryRequestItems),
                Message = "Success",
                StatusCode = 201,
                Success = true
            });
        }

    }
}
