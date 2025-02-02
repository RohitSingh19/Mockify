using Mockify.API.DTO;

namespace Mockify.API.Services
{
    public interface ICategoryService
    {
        List<GetCategoryDTO> GetAllCategories();
    }
}
