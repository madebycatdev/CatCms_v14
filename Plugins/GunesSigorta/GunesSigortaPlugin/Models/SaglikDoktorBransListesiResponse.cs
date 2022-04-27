using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class SaglikDoktorBransListesiResponse
    {
        public List<SaglikDoktorBranslari> saglikDoktorBranslari { get; set; }

        public class SaglikDoktorBranslari
        {
            public int id { get; set; }
            public string kod { get; set; }
            public string ad { get; set; }
            public string adIngilizce { get; set; }
        }
    }
}