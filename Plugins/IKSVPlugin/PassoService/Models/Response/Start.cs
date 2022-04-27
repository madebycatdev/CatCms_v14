using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
   
    public class StartResult
    {
        public string errorMessage { get; set; }
        public bool result { get; set; }
        public int userId { get; set; }
        public string orderId { get; set; }
    }
}
