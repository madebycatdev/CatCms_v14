using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Helpers
{
    public static class StringHelper
    {
        public static string ClearString(this string param)
        {
            var newString = param != null ? param.ToString()
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("5_", "")
                .Replace("_", "") : "";

            return HttpUtility.HtmlDecode(newString);
        }


        public static string ClearSpaces(this string param)
        {
            var newString = param.ToString()
                .Replace(" ", "");

            return newString;
        }
    }
}