using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Request
{

    public class billingAddress
    {
        public string countryCode { get; set; }
        public string cityCode { get; set; }
        public string townCode { get; set; }
        public string address { get; set; }
    }
    public class deliveryAddress
    {
        public string countryCode { get; set; }
        public string cityCode { get; set; }
        public string townCode { get; set; }
        public string address { get; set; }
    }
}