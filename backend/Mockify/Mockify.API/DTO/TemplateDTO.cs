namespace Mockify.API.DTO
{
    public class Template
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class TemplateDTO
    {
        public string TemplateName { get; set; }
        public List<Template> Templates { get; set; }
    }
}
