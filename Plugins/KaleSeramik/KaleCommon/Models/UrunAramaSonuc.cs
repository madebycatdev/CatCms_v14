using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class UrunAramaSonuc
    {
        public List<Filtre> FiltreList { get; set; }
        public List<Urun> UrunList { get; set; }
        public Pagination Pagination { get; set; }
    }
}