using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Cins
    {
        private string _CinsID = string.Empty;
        public string CinsID { get { return this._CinsID; } set { this._CinsID = value ?? ""; } }

        private string _CinsAdi = string.Empty;
        public string CinsAdi { get { return this._CinsAdi; } set { this._CinsAdi = value ?? ""; } }
    }
}