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

        public async Task<bool> AddTemplate(string email, string templateName, TemplateDTO templateContent)
        {
            var user = await GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }

            if (user.Templates.Any(x => x.Name.ToLower() == templateName.ToLower()))
            {
                throw new Exception("Duplcate template name, please choose another one.");
            }

            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            var template = new Models.DB.Template
            {
                Name = templateName,
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(templateContent.Templates)
            };

            var update = Builders<User>.Update.Push(x => x.Templates, template);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.MatchedCount > 0;

        }

        public async Task<bool> AddUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return true;
        }

        public async Task<bool> DeleteTemplate(string email, string templateName)
        {
            var user = await GetUser(email);
            if (user == null) {
                throw new Exception("User email not found in system.");
            }

            if (!user.Templates.Any(x => x.Name.ToLower() == templateName.ToLower())) {
                throw new Exception("Template not found");
            }
            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            var update = Builders<User>.Update.PullFilter(x => x.Templates, builder => builder.Name.ToLower() == templateName.ToLower());
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.MatchedCount > 0;
        }

        public async Task<Models.DB.Template> GetTemplate(string email, string templateName)
        {
            var user = await GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }

            var template = user.Templates.FirstOrDefault(x => x.Name.ToLower() == templateName.ToLower());
            return template ?? new Models.DB.Template();
        }

        public async Task<List<Models.DB.Template>> GetTemplates(string email)
        {
            var user = await GetUser(email);
            if (user == null) {
                throw new Exception("User email not found in system.");
            }
            var templates = user.Templates;
            return templates.ToList();
        }

        public async Task<User> GetUser(string email)
        {
            return await _userCollection.Find<User>(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateTemplate(string email, string templateName, TemplateDTO content)
        {
            var user = await GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }

            if (!user.Templates.Any(x => x.Name.ToLower() == templateName.ToLower()))
            {
                throw new Exception("Template not found");
            }

            if (await DeleteTemplate(email, templateName))
            {
                await AddTemplate(email, templateName, content);
                return true;
            }
            else
            {
                throw new Exception($"Failed to update template: {email}");
            }
        }
    }
}
