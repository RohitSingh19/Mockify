using Microsoft.Extensions.Options;
using Mockify.API.DTO;
using Mockify.API.Models.DB;
using MongoDB.Driver;

namespace Mockify.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<MockifyDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
        }

        public async Task<bool> AddUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return true;
        }

        public async Task<User> GetUser(string email)
        {
            return await _userCollection.Find<User>(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
