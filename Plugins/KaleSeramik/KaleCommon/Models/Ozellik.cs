using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Ozellik
    {
        private int _OID = 0;
        public int OID { get { return this._OID; } set { this._OID = value; } }

        private string _ODesc = string.Empty;
        public string ODesc { get { return this._ODesc; } set { this._ODesc = value ?? ""; } }

        private int _FID = 0;
        public int FID { get { return this._FID; } set { this._FID = value; } }

        private string _OImageURL = string.Empty;
        public string OImageURL { get { return this._OImageURL; } set { this._OImageURL = value ?? ""; } }

        private int _OImageSekil = 0;
        public int OImageSekil { get { return this._OImageSekil; } set { this._OImageSekil = value; } }
    }
}