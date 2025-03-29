
namespace Mockify.API.Services
{
    public class User : IUser
    {
        public Task<bool> AddTemplate(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTemplate(string email, string templateId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
