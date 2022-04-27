using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class IlceResponse
    {
        public List<Ilceler> ilceler { get; set; }
        public class Ilceler
        {
            public int id { get; set; }
            public string ad { get; set; }
            public int ilId { get; set; }
        }
    }
}