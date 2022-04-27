using PassoService.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Response
{
    public class OrderDetail
    {
        public int userId { get; set; }
        public string orderId { get; set; }
        public bool isNewUser { get; set; }
        public Product product { get; set; }
        public StartRequest userInfo { get; set; }

    }
}