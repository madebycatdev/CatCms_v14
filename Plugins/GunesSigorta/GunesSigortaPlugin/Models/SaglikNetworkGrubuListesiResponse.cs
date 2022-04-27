using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class SaglikNetworkGrubuListesiResponse
    {
        public List<SaglikKurumsalSiteNetworkGrubu> saglikKurumsalSiteNetworkGrubu { get; set; }

        public class SaglikKurumsalSiteNetworkList
        {
            public int id { get; set; }
            public string gorunenNetworkAdi { get; set; }
            public object gorunenNetworkAdiIngilizce { get; set; }
            public int gunesSaglikNetworkId { get; set; }

        }

        public class SaglikKurumsalSiteNetworkGrubu
        {
            public int id { get; set; }
            public object kod { get; set; }
            public string ad { get; set; }
            public object adIngilizce { get; set; }
            public List<SaglikKurumsalSiteNetworkList> saglikKurumsalSiteNetworkList { get; set; }

        }
    }
}