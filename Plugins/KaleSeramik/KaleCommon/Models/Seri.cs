using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Seri
    {
        private string _SeriID = string.Empty;
        public string SeriID { get { return this._SeriID; } set { this._SeriID = value ?? ""; } }

        private string _Tanim = string.Empty;
        public string Tanim { get { return this._Tanim; } set { this._Tanim = value ?? ""; } }

        private string _Adi = string.Empty;
        public string Adi { get { return this._Adi; } set { this._Adi = value ?? ""; } }

        private string _Facebook = string.Empty;
        public string Facebook { get { return this._Facebook; } set { this._Facebook = value ?? ""; } }

        private string _Twitter = string.Empty;
        public string Twitter { get { return this._Twitter; } set { this._Twitter = value ?? ""; } }

        private string _SEO = string.Empty;
        public string SEO { get { return this._SEO; } set { this._SEO = value ?? ""; } }

        public List<Image> ImageList { get; set; }

        public List<Image> Image { get; set; }

        public List<Cins> Cins { get; set; }

        public List<Dosya> DosyaList { get; set; }

        private string _MarkaAdi = string.Empty;
        public string MarkaAdi { get { return this._MarkaAdi; } set { this._MarkaAdi = value ?? ""; } }

        public List<Marka> MarkaList { get; set; }

        private string _UrunTipi = string.Empty;
        public string UrunTipi { get { return this._UrunTipi; } set { this._UrunTipi = value ?? ""; } }

        private string _Video = string.Empty;
        public string Video { get { return this._Video; } set { this._Video = value ?? ""; } }

        private string _WhiteBoxLink = string.Empty;
        public string WhiteBoxLink { get { return this._WhiteBoxLink; } set { this._WhiteBoxLink = value ?? ""; } }
    }
}