using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [ToolboxData("<{0}:CachedControl runat=server></{0}:CachedControl>")]
    public class CachedControl : UserControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("This is Cached User Control. Current Time" + DateTime.Now.ToString());
            base.Render(writer);
        }
    }
}
