using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Randomizer
    {
        [Description("Random even number")]
        public int Even { get; set; }
        
        [Description("Random odd number")]
        public int Odd { get; set; }

        [Description("Random double number")]
        public double Double { get; set; }

        [Description("Random decimal number")]
        public decimal Decimal { get; set; }

        [Description("Random boolean value")]
        public bool Boolean { get; set; }

        [Description("Random hash")]
        public string Hash { get; set; }

        [Description("Random guid")]
        public string Guid { get; set; }
        
        [Description("Random word")]
        public string Word { get; set; }

    }
}
