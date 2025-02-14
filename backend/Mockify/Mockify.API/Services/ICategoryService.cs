using Mockify.API.DTO;
using Mockify.API.Models.Custom;

namespace Mockify.API.Services
{
    public interface ICategoryService
    {
        List<GetCategoryDTO> GetAllCategories();

        GetCategoryDTO GetCustomMockModel();
    }
}
