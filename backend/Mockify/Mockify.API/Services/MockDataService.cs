﻿using Bogus;
using Mockify.API.Helper;
using Mockify.API.Models;
using Mockify.API.Models.Custom;
using Randomizer = Mockify.API.Models.Randomizer;

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
                .RuleFor(u => u.IP, f => f.Internet.Ip())
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

        public List<CustomMockModel> GenerateCustomMockJson(int limit, CustomCategoryRequestItems customCategoryRequestItems)
        {
            var customMockData = new Faker<CustomMockModel>();
            foreach (var item in customCategoryRequestItems.Items)
            {
                var customValue = item.CustomValue;
                switch(item.FieldName)
                {
                    
                    case "Number": 
                    {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Number, f => f.Random.Number(1, limit));
                            else
                                customMockData.RuleFor(u => u.Number, item.CustomValue);
                            break;
                    }
                    case "UserName":
                    {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.UserName, f => f.Internet.UserName());
                            else
                                customMockData.RuleFor(u => u.UserName, item.CustomValue);
                            break;
                     }
                    case "Gender":
                    {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Gender, f => f.Person.Gender.ToString());
                            else
                                customMockData.RuleFor(u => u.Gender, item.CustomValue);
                            break;
                    }
                    case "Password":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Password, f => f.Internet.Password());
                            else
                                customMockData.RuleFor(u => u.Password, item.CustomValue);
                            break;
                        }
                    case "Email":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Email, f => f.Internet.Email());
                            else
                                customMockData.RuleFor(u => u.Email, item.CustomValue);
                            break;
                        }
                    case "DateTime":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.DateTime, f => f.Date.Future());
                            else
                                customMockData.RuleFor(u => u.DateTime, item.CustomValue);
                            break;
                        }
                    case "Address":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Address, f => f.Address.FullAddress());
                            else
                                customMockData.RuleFor(u => u.Address, item.CustomValue);
                            break;
                        }
                    case "City":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.City, f => f.Address.City());
                            else
                                customMockData.RuleFor(u => u.City, item.CustomValue);
                            break;
                        }
                    case "Country":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Country, f => f.Address.Country());
                            else
                                customMockData.RuleFor(u => u.Country, item.CustomValue);
                            break;
                        }
                    case "ZipCode":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.ZipCode, f => f.Address.ZipCode());
                            else
                                customMockData.RuleFor(u => u.ZipCode, item.CustomValue);
                            break;
                        }
                    case "Latitude":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Latitude, f => f.Address.Latitude(-180, 180));
                            else
                                customMockData.RuleFor(u => u.Latitude, item.CustomValue);
                            break;
                        }
                    case "Longitude":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Longitude, f => f.Address.Longitude(-180, 180));
                            else
                                customMockData.RuleFor(u => u.Longitude, item.CustomValue);
                            break;
                        }
                    case "Boolean":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Boolean, f => f.Random.Bool());
                            else
                                customMockData.RuleFor(u => u.Boolean, item.CustomValue);
                            break;
                        }
                    case "Hash":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Hash, f => f.Random.Hash());
                            else
                                customMockData.RuleFor(u => u.Hash, item.CustomValue);
                            break;
                        }
                    case "Guid":
                        {
                            if (customValue != null)
                                customMockData.RuleFor(u => u.Guid, f => f.Random.Guid());
                            else
                                customMockData.RuleFor(u => u.Guid, item.CustomValue);
                            break;
                        }
                }        
            }
            return customMockData.Generate(limit).ToList();
        }

    }
}
