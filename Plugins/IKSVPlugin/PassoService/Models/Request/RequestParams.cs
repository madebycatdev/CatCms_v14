using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models.Request
{
    public class RequestParams<T> where T : class
    {
        public string accessToken { get; set; }
        public T objectParams { get; set; }
    }
}