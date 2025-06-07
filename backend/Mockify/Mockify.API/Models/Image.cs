using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Image
    {
        [Description("An image Url")]
        public string Url { get; set; }

        [Description("A random category")]
        public string Category { get; set; }
        
        [Description("width of an image")]
        public int Width { get; set; }
        
        [Description("Height of an image")]
        public int Height { get; set; }
    }
}
