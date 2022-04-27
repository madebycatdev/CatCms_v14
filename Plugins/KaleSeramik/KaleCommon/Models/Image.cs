using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Image
    {
        private string _Thumb = string.Empty;
        public string Thumb { get { return this._Thumb; } set { this._Thumb = value ?? ""; } }

        private string _Medium = string.Empty;
        public string Medium { get { return this._Medium; } set { this._Medium = value ?? ""; } }

        private string _Large = string.Empty;
        public string Large { get { return this._Large; } set { this._Large = value ?? ""; } }

        private string _Resolution = string.Empty;
        public string Resolution { get { return this._Resolution; } set { this._Resolution = value ?? ""; } }
    }
}