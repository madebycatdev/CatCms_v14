using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class IlResponse
    {
        public List<Iller> iller { get; set; }

        public class Iller
        {
            public int id { get; set; }
            public string ad { get; set; }
            public int ulkeId { get; set; }
        }
    }
}