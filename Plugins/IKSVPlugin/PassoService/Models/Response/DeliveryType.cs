using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class DeliveryTypeResult
    {
        public List<DeliveryType> deliveryTypeList { get; set; }
        public string errorMessage { get; set; }
    }
    public class DeliveryType
    {
        public int id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
    }
}
