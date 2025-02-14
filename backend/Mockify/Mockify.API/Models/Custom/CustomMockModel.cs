using Mockify.API.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mockify.API.Models.Custom
{
    public class CustomMockModel
    {
        [Description("A random generated number")]
        [Label("Number")]
        [MinLength(1)]
        [MaxLength(1000)]
        public int Number { get; set; }

        [Description("Combination of first name and last name")]
        [Label("User Name")]
        [MinLength(3)]
        [MaxLength(30)]
        public string UserName {get; set;}

        [Description("Male or Female")]
        [Label("Gender")]
        [MinLength(4)]
        [MaxLength(6)]
        public string Gender { get; set; }

        [Description("An alphanumeric string")]
        [Label("Password")]
        [MinLength(5)]
        [MaxLength(10)]
        public string Password { get; set;}

        [Description("A random valid email address")]
        [Label("Email")]
        [EmailAddress]
        [MinLength(4)]
        [MaxLength(30)]
        public string Email { get; set;}

        [Description("DateTime")]
        [Label("Date Time")]
        
        public DateTime DateTime { get; set; }
        
        [Description("Random address")]
        [Label("Address")]
        [MinLength(2)]
        [MaxLength(300)]
        public string Address { get; set; }

        [Description("Random city")]
        [Label("City")]
        [MinLength(2)]
        [MaxLength(300)]
        public string City { get; set; }

        [Description("Random country")]
        [Label("Country")]
        [MinLength(2)]
        [MaxLength(300)]
        public string Country { get; set; }
        
        [Description("Random zipcode")]
        [Label("Zip Code")]
        [MinLength(2)]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Description("Random latitude")]
        [Label("Latitude")]
        
        public double Latitude { get; set; }

        [Description("Random Longitude")]
        [Label("Longitude")]
        public double Longitude { get; set; }

        [Description("True or False")]
        [Label("Boolean")]
        public bool Boolean { get; set; }

        [Description("An alphnumeric string")]
        [Label("Hash")]
        public string Hash { get; set; }

        [Description("Random GUID")]
        [Label("GUID")]
        public Guid Guid { get; set; }

    }
}
