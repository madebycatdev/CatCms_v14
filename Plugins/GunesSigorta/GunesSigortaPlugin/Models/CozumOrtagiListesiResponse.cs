using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class CozumOrtagiListesiResponse
    {
        public List<CozumOrtaklari> cozumOrtaklari { get; set; }

        public class CozumOrtaklari
        {
            public int cozumOrtagiId { get; set; }
            public string cozumOrtagiAdi { get; set; }
            public string sirketAdi { get; set; }
            public int ilceId { get; set; }
            public List<Markalar> markalar { get; set; }
            public List<KullanimTarzlari> kullanimTarzlari { get; set; }
            public string adres { get; set; }
            public string telefon { get; set; }
            public string faks { get; set; }
            public int cozumOrtagiTipi { get; set; }
            public int servisTuru { get; set; }
            public bool gunesDost { get; set; }
            public int kaskoEkstra { get; set; }
            public List<CalisilanBranslar> calisilanBranslar { get; set; }
        }

        public class Markalar
        {
            public int id { get; set; }
            public string ad { get; set; }
        }

        public class FaaliyetKonulari
        {
            public int id { get; set; }
            public string ad { get; set; }
        }

        public class KullanimTarzlari
        {
            public int id { get; set; }
            public string ad { get; set; }
            public List<FaaliyetKonulari> faaliyetKonulari { get; set; }
        }

        public class CalisilanBranslar
        {
            public object id { get; set; }
            public string kod { get; set; }
            public string ad { get; set; }
        }
    }
}