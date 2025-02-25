using Mockify.API.DTO;
using Mockify.API.Helper;
using Mockify.API.Models.Custom;
using System.ComponentModel;
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
                categoryDTO.Category = model.Name.ToSpaceSeparated();
                categoryDTO.EndpointToGetMockData = model.Name.ToLower();
                categoryDTO.Properties = model.GetProperties().Select(x => new Property {
                                         Name = x.Name.ToSpaceSeparated(),
                                         Label = x.Name.ToSpaceSeparated(),
                                         Type = x.PropertyType.Name.ToDataType(),
                                         Description = model.GetProperty(x.Name)?.GetCustomAttribute<DescriptionAttribute>()?.Description
                }).ToList();
                categoryList.Add(categoryDTO);
            }
            return categoryList;
        }

        public GetCategoryDTO GetCustomMockModel()
        {
            var customAttributeModel = (from t in Assembly.GetExecutingAssembly().GetTypes()
                         where t.IsClass && t.Namespace == "Mockify.API.Models.Custom"
                         select t).FirstOrDefault();
                       
            GetCategoryDTO categoryDTO = new();

            categoryDTO.Category = customAttributeModel.Name;
            categoryDTO.Properties = customAttributeModel.GetProperties().Select(x => new Property
            {
                Name = x.Name,
                Type = x.PropertyType.Name.ToDataType(),
                Label = x.Name.ToSpaceSeparated(),
                Description = customAttributeModel.GetProperty(x.Name).GetCustomAttribute<DescriptionAttribute>().Description
            }).ToList();
            
            return categoryDTO;
        }
    }
}
