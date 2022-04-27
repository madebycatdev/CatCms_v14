using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Varyasyon
    {
        private string _VaryasyonAdi = string.Empty;
        public string VaryasyonAdi { get { return this._VaryasyonAdi; } set { this._VaryasyonAdi = value ?? ""; } }

        public Renk Renk { get; set; }

        private string _Ebat = string.Empty;
        public string Ebat { get { return this._Ebat; } set { this._Ebat = value ?? ""; } }

        private string _UrunKodu = string.Empty;
        public string UrunKodu { get { return this._UrunKodu; } set { this._UrunKodu = value ?? ""; } }

        private string _Yuzey = string.Empty;
        public string Yuzey { get { return this._Yuzey; } set { this._Yuzey = value ?? ""; } }

    }
}