using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class SeriAramaSonuc
    {
        public List<Filtre> FiltreList { get; set; }
        public List<Seri> SeriList { get; set; }
        public Pagination Pagination { get; set; }
    }
}