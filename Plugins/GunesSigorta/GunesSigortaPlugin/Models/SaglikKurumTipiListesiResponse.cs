using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class SaglikKurumTipiListesiResponse
    {
        public List<SaglikKurumTipleri> saglikKurumTipleri { get; set; }

        public class SaglikKurumTipleri
        {
            public int id { get; set; }
            public string kod { get; set; }
            public string ad { get; set; }
            public object adIngilizce { get; set; }

        }
    }
}