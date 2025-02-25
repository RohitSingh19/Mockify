using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class User
    {
        [Description("Random User Id")]
        public int Id { get; set; }

        [Description("Random first name")]
        public string FirstName { get; set; }
        
        [Description("Random last name")]
        public string LastName { get; set; }

        [Description("Random gender")]
        public string Gender { get; set; }

        [Description("Random email")]
        public string Email { get; set; }
        
        [Description("Random password")]
        public string Password { get; set; }

        [Description("Random date")]
        public DateTime DateOfBirth { get; set; }
    }
}
