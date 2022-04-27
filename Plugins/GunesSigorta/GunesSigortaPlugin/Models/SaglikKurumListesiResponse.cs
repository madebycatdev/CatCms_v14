using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class SaglikKurumListesiResponse
    {
        public List<SaglikKurumlari> saglikKurumlari { get; set; }
        public class SaglikKurumlari
        {
            public int id { get; set; }
            public string kod { get; set; }
            public string ad { get; set; }
            public object adIngilizce { get; set; }
            public int kurumTipi { get; set; }
            public object bransIdList { get; set; }
            public string adres { get; set; }
            public int ilce { get; set; }
            public int il { get; set; }
            public string telefon { get; set; }
            public object konum { get; set; }
            public List<int> kurumsalSiteNetworkIdList { get; set; }

        }
    }
}