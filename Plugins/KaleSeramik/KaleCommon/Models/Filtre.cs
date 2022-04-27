using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class FiltreSecenekList
    {
        public string Label { get; set; }
        public string ImageURL { get; set; }
        public string Value { get; set; }
    }

    public class Filtre
    {
        public string FiltreBaslik { get; set; }
        public string FiltreTipi { get; set; }
        public List<FiltreSecenekList> FiltreSecenekList { get; set; }
        public string FiltreKey { get; set; }
        public string FiltreDegeri { get; set; }
    }
}
