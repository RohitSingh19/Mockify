using Mockify.API.Models.DB;

namespace Mockify.API.Services
{
    public interface IUser
    {
        Task<User> AddUser(User user);
        Task<User> GetUser(User user);
        Task<bool> AddTemplate(string email);
        Task<bool> DeleteTemplate(string email, string templateId);
    }
}
