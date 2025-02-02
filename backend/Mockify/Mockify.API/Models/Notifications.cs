namespace Mockify.API.Models
{
    public class Notifications
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
