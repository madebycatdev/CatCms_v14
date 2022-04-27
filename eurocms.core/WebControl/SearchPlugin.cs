using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
[assembly: TagPrefix("EuroCMS.WebControl", "plugin")]
namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [ToolboxData("<{0}:Search  runat=server></{0}:Search>")]
    [DefaultProperty("Search Plugin")]
    public class Search : System.Web.UI.WebControls.WebControl
    {




        public override void RenderBeginTag(HtmlTextWriter writer)
        {

            //  writer.RenderBeginTag("h2");
            //base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //  writer.RenderEndTag();
            //   base.RenderEndTag(writer);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {

            writer.Write("This is Search Plugin");
        }
    }

}
