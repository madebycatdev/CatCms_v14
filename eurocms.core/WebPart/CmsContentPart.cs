using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace eurocms.controls.WebPart
{
    public class CmsContentPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        public string Html
        {
            get;
            set;
        }
    }
}
