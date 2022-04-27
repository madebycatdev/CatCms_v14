using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Request
{
    public class ForgotPinRequest
    {
        public string cardNumber { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }
        
    }
}