using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Notification
    {
        [Description("Random number as notification id")]
        public int NotificationId { get; set; }

        [Description("Random user id")]
        public string UserId { get; set; }
        
        [Description("Random notification message")]
        public string Message { get; set; }

        [Description("Random notification date")]
        public DateTime Date { get; set; }
    }
}
