using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Renk
    {
        private string _RenkAdi = string.Empty;
        public string RenkAdi { get { return this._RenkAdi; } set { this._RenkAdi = value ?? ""; } }
    }
}