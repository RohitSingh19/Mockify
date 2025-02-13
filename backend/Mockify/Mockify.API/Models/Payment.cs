namespace Mockify.API.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId {  get; set; }
        public DateTime PaymentDate { get; set; }   
        public decimal Amount { get; set; }
    }
}
