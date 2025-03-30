using Mockify.API.Models.DB;

namespace Mockify.API.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<User> GetUser(string email);
        Task<bool> AddTemplate(string email);
        Task<bool> DeleteTemplate(string email, string templateId);
    }
}
