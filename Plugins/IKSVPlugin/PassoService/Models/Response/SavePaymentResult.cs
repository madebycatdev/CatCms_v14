using System;
namespace PassoService.Models.Response
{
   
    public class SavePaymentResult
    {
        public string errorMessage { get; set; }
        public bool result { get; set; }
        public string paymentUrl { get; set; }
        public long totalPrice { get; set; }
    }
}
