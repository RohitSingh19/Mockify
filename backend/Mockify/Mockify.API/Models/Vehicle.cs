using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Vehicle
    {
        [Description("Random vehicle Id")]
        public int VehicleId { get; set; }
        
        [Description("Random vehicle model")]
        public string Model { get; set; }
        
        [Description("Random vehicle type")]
        public string Type { get; set; }
        
        [Description("Random fuel")]
        public string Fuel { get; set; }
        
        [Description("Random manufacturer")]
        public string Manufacturer { get; set; }
    }

}
