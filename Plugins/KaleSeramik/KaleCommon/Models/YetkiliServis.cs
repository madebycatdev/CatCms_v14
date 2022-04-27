using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class YetkiliServis
    {
        public string ServisAdi { get; set; }
        public string ServisAdresi { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string Koordinat { get; set; }
        public string Email { get; set; }
        public bool Bayi { get; set; }
        public string CepTel { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Semt { get; set; }
        public string CariKodu { get; set; }
        public string YetkinlikBilgisi { get; set; }
    }
}
