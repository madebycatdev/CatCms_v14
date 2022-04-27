using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Marka
    {
        private string _MarkaID = string.Empty;
        public string MarkaID { get { return this._MarkaID; } set { this._MarkaID = value ?? ""; } }

        private string _MarkaAdi = string.Empty;
        public string MarkaAdi { get { return this._MarkaAdi; } set { this._MarkaAdi = value ?? ""; } }
    }
}