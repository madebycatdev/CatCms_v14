using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [ToolboxData("<{0}:EditableDate  runat=server></{0}:EditableDate>")]
    [DefaultProperty("DataValueField")]
    public class EditableDate : System.Web.UI.WebControls.WebControl
    {

        private string _DataValueField = string.Empty;
        [Bindable(true)]
        [DefaultValue("headline")]
        [Localizable(true)]
        public string DataValueField
        {
            get
            {
                return _DataValueField;
            }

            set
            {

                _DataValueField = value;
            }
        }
        private string _DisplayFormat = string.Empty;
        [Bindable(true)]
        [DefaultValue("MM:DD:YYYY")]
        [Localizable(true)]
        public string DisplayFormat
        {
            get
            {
                return _DisplayFormat;
            }

            set
            {

                _DisplayFormat = value;
            }
        }



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
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            if (!string.IsNullOrEmpty(DisplayFormat))
            {
                if (!string.IsNullOrEmpty(ArticleDetails[DataValueField].ToString()))
                {
                    writer.Write(Convert.ToDateTime(ArticleDetails[DataValueField]).ToString(DisplayFormat));
                }
            }
            else
            {
                writer.Write(ArticleDetails[DataValueField].ToString());
            }


        }
    }
}
