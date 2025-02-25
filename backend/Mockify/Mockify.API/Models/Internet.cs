using Mockify.API.Helper;
using System.ComponentModel;
using System.Globalization;

namespace Mockify.API.Models
{
    public class Internet
    {
        [Description("Random avatar url")]
        public string Avatar { get; set; }
        
        [Description("Random email id")]
        public string Email { get; set; }

        [Description("Random user name")]
        public string UserName { get; set; }

        [Description("Random IP address")]
        public string IP { get; set; }

        [Description("Random password")]
        public string Password { get; set; }
        
        [Description("Random protocol")]
        public string Protocol { get; set; }

    }
}
