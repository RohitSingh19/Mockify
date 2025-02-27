using Bogus;
using Mockify.API.Helper;
using Mockify.API.Models;
using Mockify.API.Models.Custom;
using Internet = Mockify.API.Models.Internet;
using Lorem = Mockify.API.Models.Lorem;
using Randomizer = Mockify.API.Models.Randomizer;
using Vehicle = Mockify.API.Models.Vehicle;

namespace Mockify.API.Services
{
    public class MockDataService : IMockDataService
    {
        public List<Internet> GetInternetMockData(int limit)
        {
            var fakeInternet = new Faker<Internet>()
                .RuleFor(u => u.Avatar, f=> f.Internet.Avatar())
                .RuleFor(u=> u.Email, f=> f.Internet.Email())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Ip, f => f.Internet.Ip())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Protocol, f => f.Internet.Protocol())
                .Generate(limit);

            return fakeInternet;
        }

        public List<Location> GetLocaltionMockData(int limit)
        {
            var fakeLocation = new Faker<Location>()
                .RuleFor(u => u.LocationId, f => f.Random.Number(1,limit))
                .RuleFor(u => u.Address, f => f.Person.Address.State + f.Person.Address.Street)
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
                .RuleFor(u => u.Latitude, f => f.Address.Latitude().ToString())
                .RuleFor(u => u.Longitude, f => f.Address.Longitude().ToString())
               .Generate(limit);

            return fakeLocation;
        }

        public List<Lorem> GetLoremMockData(int limit)
        {
            var fakeLorem = new Faker<Lorem>()
               .RuleFor(u => u.Word, f => f.Lorem.Word())
               .RuleFor(u => u.Words, f=> f.Lorem.Words())
               .RuleFor(u => u.Letter, f => f.Lorem.Letter())
               .RuleFor(u => u.Sentence, f => f.Lorem.Sentence())
               .RuleFor(u => u.Slug, f => f.Lorem.Slug())
               .Generate(limit);
            
            return fakeLorem;
        }

        public List<Notification> GetNotificationMockData(int limit)
        {
            var fakeNotification = new Faker<Notification>()
               .RuleFor(u => u.NotificationId, f => f.Random.Number(1, limit))
               .RuleFor(u => u.Date, f => f.Date.Recent())
               .RuleFor(u => u.Message, f => f.Lorem.Text())
               .RuleFor(u => u.UserId, f => f.Random.Guid().ToString())
               .Generate(limit);

            return fakeNotification;
        }

        public List<Payment> GetPaymentMockData(int limit)
        {
            var fakePayment = new Faker<Payment>()
               .RuleFor(u => u.PaymentId, f => f.Random.Number(1, limit))
               .RuleFor(u => u.OrderId, f => f.Random.Number(1, limit))
               .RuleFor(u => u.PaymentDate, f => f.Date.Recent())
               .RuleFor(u => u.Amount, f => f.Finance.Amount())
               .Generate(limit);

            return fakePayment;
        }

        public List<Randomizer> GetRandomizerMockData(int limit)
        {
            var fakeRandomizerMock = new Faker<Randomizer>()
                   .RuleFor(u => u.Word, f => f.Random.Word())
                   .RuleFor(u => u.Even, f => f.Random.Even(0,100000))
                   .RuleFor(u => u.Odd, f => f.Random.Odd(1, 100001))
                   .RuleFor(u => u.Double, f => f.Random.Double(1, limit))
                   .RuleFor(u => u.Decimal, f => f.Random.Decimal(1, limit))
                   .RuleFor(u => u.Word, f => f.Random.Word())
                   .RuleFor(u => u.Boolean, f => f.Random.Bool())
                   .RuleFor(u => u.Hash, f => f.Random.Hash())
                   .RuleFor(u => u.Guid, f => f.Random.Guid().ToString())
                   .Generate(limit);

            return fakeRandomizerMock;
        }

        public List<FileSystem> GetFileSystemMockData(int limit)
        {
            var fakeFileSystemMock = new Faker<FileSystem>()
                .RuleFor(u => u.FilePath, f => f.System.FilePath())
                .RuleFor(u => u.DirectoryPath, f => f.System.DirectoryPath())
                .RuleFor(u => u.FileExtention, f => f.System.FileExt())
                .RuleFor(u => u.FileName, f => f.System.FileName())
                .RuleFor(u => u.FileType, f => f.System.FileType())
                .Generate(limit);

            return fakeFileSystemMock;
        }

        public List<User> GetUserMockData(int limit)
        {
            var fakeUser = new Faker<User>(locale: "en_IND")
            .RuleFor(u => u.Id, f => f.Random.Number(1, limit))
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .Generate(limit);
            return fakeUser;
        }

        public List<Vehicle> GetVehicleMockData(int limit)
        {
            var fakeVehicle = new Faker<Vehicle>()
               .RuleFor(u => u.VehicleId, f => f.Random.Number(1, limit))
               .RuleFor(u => u.Fuel, f => f.Vehicle.Fuel())
               .RuleFor(u => u.Model, f => f.Vehicle.Model())
               .RuleFor(u => u.Manufacturer, f => f.Vehicle.Manufacturer())
               .RuleFor(u => u.Type, f => f.Vehicle.Type())
               .Generate(limit);

            return fakeVehicle;
        }

        public string GenerateCustomMockJson(int limit, CustomCategoryRequestItems customCategoryRequestItems)
        {
            var customMockData = new Faker<CustomMockModel>();
            List<string> requestObj = new();  
            foreach (var item in customCategoryRequestItems.Items)
            {
                //Type type = ((object)item.CustomValue).GetType(); 
                var customValue = item.CustomValue;
                switch (item.FieldName.ToLower())
                {
                    case "number": 
                    {
                            int customNumber = int.MinValue;
                            int.TryParse(customValue, out customNumber);    
                            customMockData.RuleFor(u => u.Number, f => (customNumber == 0) ? f.Random.Number(1, limit) : customNumber);
                            requestObj.Add("number");
                            break;
                    }
                    case "username":
                    {
                            customMockData.RuleFor(u => u.UserName, f => (customValue.Length == 0) ? f.Internet.UserName() : customValue);
                            requestObj.Add("username");
                            break;
                    }
                    case "gender":
                    {
                            customMockData.RuleFor(u => u.Gender, f => (customValue.Length == 0) ? f.Person.Gender.ToString() : customValue);
                            requestObj.Add("gender");
                            break;
                    }
                    case "password":
                        {
                            customMockData.RuleFor(u => u.Password, f => (customValue.Length == 0) ? f.Internet.Password() : customValue);
                            requestObj.Add("password");
                            break;
                        }
                    case "email":
                        {
                            customMockData.RuleFor(u => u.Email, f => (customValue.Length == 0) ? f.Internet.Email() : customValue);
                            requestObj.Add("email");
                            break;
                        }
                    case "datetime":
                        {
                            DateTime dateTime = DateTime.Now;
                            DateTime.TryParse(customValue, out dateTime);   
                            customMockData.RuleFor(u => u.DateTime, f => (dateTime == DateTime.Now) ? f.Date.Future() : dateTime);
                            requestObj.Add("datetime");
                            break;
                        }
                    case "address":
                        {
                            customMockData.RuleFor(u => u.Address, f => (customValue.Length == 0) ? f.Address.FullAddress() : customValue);
                            requestObj.Add("address");
                            break;
                        }
                    case "city":
                        {
                            
                            customMockData.RuleFor(u => u.City, f => (customValue.Length == 0) ? f.Address.City() : customValue);
                            requestObj.Add("city");
                            break;
                        }
                    case "country":
                        {   
                            customMockData.RuleFor(u => u.Country, f => (customValue.Length == 0) ? f.Address.Country() : customValue);
                            requestObj.Add("country");
                            break;
                        }
                    case "zipcode":
                        {   
                            customMockData.RuleFor(u => u.ZipCode, f => (customValue.Length == 0) ? f.Address.ZipCode() : customValue);
                            requestObj.Add("zipcode");
                            break;
                        }
                    case "latitude":
                        {   
                            double customDouble = Double.MinValue;
                            Double.TryParse(customValue, out customDouble);     
                            customMockData.RuleFor(u => u.Latitude, f => (customDouble == Double.MinValue) ? f.Address.Latitude(-180, 180) : customDouble);
                            requestObj.Add("latitude");
                            break;
                        }
                    case "longitude":
                        {
                            double customDouble = Double.MinValue;
                            Double.TryParse(customValue, out customDouble);
                            customMockData.RuleFor(u => u.Longitude, f => (customDouble == Double.MinValue) ? f.Address.Longitude(-180, 180) : customDouble);
                            requestObj.Add("longitude");
                            break;
                        }
                    case "boolean":
                        {
                            bool customBool = false;
                            Boolean.TryParse(customValue, out customBool);
                            customMockData.RuleFor(u => u.Boolean, f => (customValue.Length == 0) ? f.Random.Bool() : customBool);
                            requestObj.Add("boolean");
                            break;
                        }
                    case "hash":
                        {   
                            customMockData.RuleFor(u => u.Hash, f => (customValue.Length == 0) ? f.Random.Hash() : customValue);
                            requestObj.Add("hash");
                            break;
                        }
                    case "guid":
                        {   
                            Guid guid = Guid.NewGuid();
                            Guid.TryParse(customValue, out guid);
                            customMockData.RuleFor(u => u.Guid, f => (customValue.Length == 0) ? f.Random.Guid() : guid);
                            requestObj.Add("guid");
                            break;
                        }
                }        
            }
                        
            return filter(customMockData.Generate(limit).ToList(), requestObj);
        }

        private string filter(List<CustomMockModel> response, List<string> request)
        {
            var result = response.Select(u => CreateSelectiveObject(u, request));
            return System.Text.Json.JsonSerializer.Serialize(result);
            
        }
        private object CreateSelectiveObject(CustomMockModel customMock, List<string> fields)
        {
            var dict = new Dictionary<string, object>();

            foreach (var field in fields)
            {
                switch (field.ToLower())
                {
                    case "username":
                        dict["username"] = customMock.UserName;
                        break;
                    case "number":
                        dict["number"] = customMock.Number;
                        break;
                    case "gender":
                        dict["gender"] = customMock.Gender;
                        break;
                    case "password":
                        dict["password"] = customMock.Password;
                        break;
                    case "email":
                        dict["email"] = customMock.Email;
                        break;
                    case "datetime":
                        dict["datetime"] = customMock.DateTime;
                        break;
                    case "address":
                        dict["address"] = customMock.Address;
                        break;
                    case "city":
                        dict["city"] = customMock.City;
                        break;
                    case "counrty":
                        dict["counrty"] = customMock.Country;
                        break;
                    case "zipcode":
                        dict["zipcode"] = customMock.ZipCode;
                        break;
                    case "latitude":
                        dict["latitude"] = customMock.Latitude;
                        break;
                    case "longitude":
                        dict["longitude"] = customMock.Longitude;
                        break;
                    case "boolean":
                        dict["boolean"] = customMock.Boolean;
                        break;
                    case "hash":
                        dict["hash"] = customMock.Hash;
                        break;
                    case "guid":
                        dict["guid"] = customMock.Guid;
                        break;

                }
            }

            return new System.Dynamic.ExpandoObject() as IDictionary<string, object> switch
            {
                var expando => expando.Concat(dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            };
        }

    }
}
