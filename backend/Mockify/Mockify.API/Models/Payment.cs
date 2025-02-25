using Mockify.API.Helper;
using System.ComponentModel;

namespace Mockify.API.Models
{
    public class Payment
    {
        [Description("Random payment id")]
        public int PaymentId { get; set; }
        
        [Description("Random order id")]
        public int OrderId {  get; set; }
        
        [Description("Random payment date")]
        public DateTime PaymentDate { get; set; }

        [Description("Random amount")]
        public decimal Amount { get; set; }
    }
}
