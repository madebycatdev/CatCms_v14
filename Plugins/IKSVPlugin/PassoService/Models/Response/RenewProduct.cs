using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
  
    public class RenewProductResult
    {
        public string ErrorMessage { get; set; }
        public bool result { get; set; }
        public string mobilePhone { get; set; }
        public string orderId { get; set; }
        public string paymentUrl { get; set; }

    }
}
