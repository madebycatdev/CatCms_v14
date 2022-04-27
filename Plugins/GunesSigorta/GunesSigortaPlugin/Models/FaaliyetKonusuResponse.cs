using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class FaaliyetKonusuResponse
    {
        public List<Faaliyet> faaliyetKonulari { get; set; }

        public class Faaliyet
        {
            public int id { get; set; }
            public string ad { get; set; }
        }
    }
}