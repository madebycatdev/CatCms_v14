using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Dosya
    {
        private string _DosyaAdi = string.Empty;
        public string DosyaAdi { get { return this._DosyaAdi; } set { this._DosyaAdi = value ?? ""; } }

        private string _DosyaAdresi = string.Empty;
        public string DosyaAdresi { get { return this._DosyaAdresi; } set { this._DosyaAdresi = value ?? ""; } }

        private string _DosyaUzantisi = string.Empty;
        public string DosyaUzantisi { get { return this._DosyaUzantisi; } set { this._DosyaUzantisi = value ?? ""; } }

        private string _DokumanTipi = string.Empty;
        public string DokumanTipi { get { return this._DokumanTipi; } set { this._DokumanTipi = value ?? ""; } }

    }
}