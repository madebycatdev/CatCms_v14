using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Request
{
    public class SavePaymentRequest
    {
        public deliveryAddress deliveryAddress { get; set; }
        public int deliveryTypeId { get; set; }
        public string orderId { get; set; }
        public int[] contractIdList { get; set; }

    }
}