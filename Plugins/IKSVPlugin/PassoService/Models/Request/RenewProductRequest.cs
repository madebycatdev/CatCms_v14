using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Request
{
    public class RenewProductRequest
    {
        public string cardNo { get; set; }
        public billingAddress deliveryAddress { get; set; }
        public int[] contractIdList { get; set; }
        public int deliveryTypeId { get; set; }
        public int productPriceId { get; set; }

    }

   
}