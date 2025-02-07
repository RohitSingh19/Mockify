namespace Mockify.API.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }    
        public string Country { get; set; } 
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string GetEndPoint()
        {
            return "getLocationMock";
        }
    }
}
