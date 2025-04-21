namespace Mockify.API.DTO
{
    public class TemplateDTO
    {
        public string Name { get; set; }
        public TemplateItem [] Content { get; set; }
    }
    public class TemplateItem
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
