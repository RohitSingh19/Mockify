using Bogus.DataSets;
using Microsoft.Extensions.Options;
using Mockify.API.DTO;
using Mockify.API.Models.DB;
using MongoDB.Driver;

namespace Mockify.API.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IUserService _userService;
        private readonly IMongoCollection<User> _userCollection;
        public TemplateService(IUserService userService, IOptions<MockifyDatabaseSettings> databaseSettings)
        {
            _userService = userService;
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(databaseSettings.Value.UsersCollectionName);
        }
        public async Task<bool> AddTemplate(string email, TemplateDTO templateDTO)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }
            
            if (user.Templates.Any(x => x.Name.ToLower() == templateDTO.Name.ToLower()))
            {
                throw new Exception("Duplicate template name, please choose another one.");
            }

            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            var template = new Template
            {
                Name = templateDTO.Name,
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(templateDTO.Content)
            };

            var update = Builders<User>.Update.Push(x => x.Templates, template);
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.MatchedCount > 0;

        }

        public async Task<bool> DeleteTemplate(string email, string templateName)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }

            if (!user.Templates.Any(x => x.Name.ToLower() == templateName.ToLower()))
            {
                throw new Exception("Template not found");
            }
            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            var update = Builders<User>.Update.PullFilter(x => x.Templates, builder => builder.Name.ToLower() == templateName.ToLower());
            var result = await _userCollection.UpdateOneAsync(filter, update);
            return result.MatchedCount > 0;
        }

        public async Task<Models.DB.Template> GetTemplate(string email, string templateName)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }

            var template = user.Templates.FirstOrDefault(x => x.Name.ToLower() == templateName.ToLower());
            return template ?? new Models.DB.Template();
        }

        public async Task<List<Models.DB.Template>> GetTemplates(string email)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }
            var templates = user.Templates;
            return templates.ToList();
        }



        public async Task<bool> UpdateTemplate(string email, TemplateDTO templateDTO)
        {
            var user = await _userService.GetUser(email);
            if (user == null)
            {
                throw new Exception("User email not found in system.");
            }

            if (!user.Templates.Any(x => x.Name.ToLower() == templateDTO.Name.ToLower()))
            {
                throw new Exception("Template not found");
            }

            if (await DeleteTemplate(email, templateDTO.Name))
            {
                await AddTemplate(email, templateDTO);
                return true;
            }
            else
            {
                throw new Exception($"Failed to update template: {email}");
            }
        }
    }
}
