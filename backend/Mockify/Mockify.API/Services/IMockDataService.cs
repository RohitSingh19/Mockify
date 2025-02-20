using Mockify.API.Helper;
using Mockify.API.Models;
using Mockify.API.Models.Custom;

namespace Mockify.API.Services
{
    public interface IMockDataService
    {
        List<User> GetUserMockData(int limit);
        List<Internet> GetInternetMockData(int limit);
        List<Location> GetLocaltionMockData(int limit);
        List<Lorem> GetLoremMockData(int limit);
        List<Payment> GetPaymentMockData(int limit);
        List<Vehicle> GetVehicleMockData(int limit);
        List<Notification> GetNotificationMockData(int limit);
        List<Randomizer> GetRandomizerMockData(int limit);
        List<FileSystem> GetFileSystemMockData(int limit);

        List<CustomMockModel> GenerateCustomMockJson(int limit, CustomCategoryRequestItems customCategoryRequestItems);

    }
}
