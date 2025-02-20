using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Mockify.API.Helper
{
    public class CustomCategoryRequestItem
    {
        public string FieldName { get; set; }
        public object CustomValue { get; set; }

    }
    
    public class CustomCategoryRequestItems
    {
        public List<CustomCategoryRequestItem> Items { get; set; }
    }

}
