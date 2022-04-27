using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Referans
    {
        public string ReferansID { get; set; }
        public string ReferansAdi { get; set; }
        public string Lokasyon { get; set; }
        public string ProjeMimari { get; set; }
        public string Aciklama { get; set; }
        public List<Image> ImageList { get; set; }
    }
}
