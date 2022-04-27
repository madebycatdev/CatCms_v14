using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Pagination
    {
        public int TotalItemCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int TotalPageCount { get; set; }
        public int ItemsPerPage { get; set; }
    }
}