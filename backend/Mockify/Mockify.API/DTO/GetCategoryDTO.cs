using System.Globalization;

namespace Mockify.API.DTO
{
    public class GetCategoryDTO
    {
        public string Category { get; set; }

        public string EndPoint { get; set; }
        public List<Property> Properties { get; set; }
    }

    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }

        public string Description { get; set; }
    }
}
