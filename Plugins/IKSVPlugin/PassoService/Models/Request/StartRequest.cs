using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Request
{
    public class StartRequest
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public int gender { get; set; }
        public string mobilePhone { get; set; }
        public string workPhone { get; set; }
        public billingAddress billingAddress { get; set; }
        public deliveryAddress deliveryAddress { get; set; }
        public int productPriceId { get; set; }
        public string pinCode { get; set; }


    }
}