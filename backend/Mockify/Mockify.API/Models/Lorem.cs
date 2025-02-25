using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Lorem
    {
        [Description("Random word")]
        public string Word { get; set; }
        
        [Description("Random words")]
        public string [] Words { get; set; }
        
        [Description("Random letter")]
        public string Letter { get; set; }
        
        [Description("Random sentence")]
        public string Sentence { get; set; }
        
        [Description("Random text")]
        public string Text { get; set; }
        
        [Description("Random slug")]
        public string Slug { get; set; }
    }
}
