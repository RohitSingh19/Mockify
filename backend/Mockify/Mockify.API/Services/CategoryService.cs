using Mockify.API.DTO;
using System.Reflection;

namespace Mockify.API.Services
{
    public class CategoryService : ICategoryService
    {
        public List<GetCategoryDTO> GetAllCategories()
        {
            var models = from t in Assembly.GetExecutingAssembly().GetTypes()
                       where t.IsClass && t.Namespace == "Mockify.API.Models"
                       select t;

            var categoryList = new List<GetCategoryDTO>();
            GetCategoryDTO categoryDTO;
            foreach (var model in models) 
            {
                categoryDTO = new GetCategoryDTO();
                categoryDTO.Category = model.Name;
                categoryDTO.Properties = model.GetProperties().Select(x => new Property {
                                         Name = x.Name,
                                         Type = x.PropertyType.Name,
                                         }).ToList();
                categoryDTO.EndPoint = $"get{model.Name}Mock";
                categoryList.Add(categoryDTO);
            }
            return categoryList;
        }

    }
}
