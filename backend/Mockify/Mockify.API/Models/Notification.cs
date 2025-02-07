namespace Mockify.API.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string GetEndPoint()
        {
            return "getNotificationMock";
        }
    }
}
