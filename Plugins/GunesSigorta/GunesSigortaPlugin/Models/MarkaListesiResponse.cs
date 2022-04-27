using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class MarkaListesiResponse
    {
        public List<Markalar> markalar { get; set; }
        public class Markalar
        {
            public int id { get; set; }
            public string ad { get; set; }
        }
    }
}