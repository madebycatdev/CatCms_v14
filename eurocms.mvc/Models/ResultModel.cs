using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eurocms.mvc
{
    public class ResultModel<T>
    {
        public string Message = string.Empty;
        public string Detail = string.Empty;
        public bool HasError = false;
        public List<T> ResultSet = new List<T>();
    }
}
