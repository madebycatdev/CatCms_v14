using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCMS.Plugin.Kale.KaleCommon
{
    public static class Extensions
    {
        public static string Truncate(this string value, int maxChars, string endingString)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + endingString;
        }
    }
}
