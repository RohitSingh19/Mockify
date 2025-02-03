using Mockify.API.Models;

namespace Mockify.API.Services
{
    public interface IMockDataService
    {
        List<User> getUserMockData(int limit);
        List<Internet> getInternetMockData(int limit);
        List<Location> getLocaltionMockData(int limit);
        List<Lorem> getLoremMockData(int limit);
        List<Payment> getPaymentMockData(int limit);
        List<Vehicle> getVehicleMockData(int limit);
        List<Notification> getNotificationMockData(int limit);
    }
}
