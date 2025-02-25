using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Location
    {
        [Description("Random location id")]
        public int LocationId { get; set; }
        
        [Description("Random full address")]
        public string Address { get; set; }
        
        [Description("Random city name")]
        public string City { get; set; }
        
        [Description("Random country name")]
        public string Country { get; set; }

        [Description("Random zip Code")]
        public string ZipCode { get; set; }
        
        [Description("Random latitude coordinate")]
        public string Latitude { get; set; }
        
        [Description("Random longitude coordinate")]
        public string Longitude { get; set; }
    }
}
