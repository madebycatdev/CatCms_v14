using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Ebat
    {
        private int _EbatID = 0;
        public int EbatID { get { return this._EbatID; } set { this._EbatID = value; } }

        private string _EbatAdi = string.Empty;
        public string EbatAdi { get { return this._EbatAdi; } set { this._EbatAdi = value ?? ""; } }
    }
}