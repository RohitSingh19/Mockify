using Mockify.API.DTO;

namespace Mockify.API.Services
{
    public interface ITemplateService
    {
        Task<bool> AddTemplate(string email, TemplateDTO templateContent);
        Task<List<Models.DB.Template>> GetTemplates(string email);
        Task<bool> DeleteTemplate(string email, string templateName);
        Task<bool> UpdateTemplate(string email, TemplateDTO content, string templateName);
        Task<Models.DB.Template> GetTemplate(string email, string templateName);
    }
}
