using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [DefaultProperty("DataValueField")]
    [ToolboxData("<{0}:EditableDatetime runat=server></{0}:EditableDatetime>")]
    public class EditableDatetime : System.Web.UI.WebControls.WebControl
    {
        string EditableType = string.Empty;
        string EditableID = string.Empty;
        string EditableName = string.Empty;
        private string _DataValueField = string.Empty;

        [Bindable(true)]
        [DefaultValue("flag_1")]
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



        protected override void RenderContents(HtmlTextWriter output)
        {
            EditableType = "article";
            EditableID = Page.Items["article_id"].ToString();
            EditableName = DataValueField;
            string sub_template = string.Empty;
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            if (Page.User.Identity.IsAuthenticated && (Page.User.IsInRole("Administrator") || Page.User.IsInRole("Editor") || Page.User.IsInRole("Author") || Page.User.IsInRole("ContentManager") || Page.User.IsInRole("ContentEntry") || Page.User.IsInRole("UserCreator") ))
            {
                
               // output.Write("<div   data-type=\"" + EditableType + "\" data-id=\"" + EditableID + "\" data-name=\"" + EditableName + "\" contenteditable=\"true\">" + "<input onclick=\"new Calendar(this.name);\" value=\""+ArticleDetails[DataValueField].ToString()+"\"  size=\"9\" maxlength=\"10\"  style=\"cursor:pointer; width:90px;\"  type=\"text\" name=\"" + DataValueField + "\" id=\"" + DataValueField + "\" value=\"" + HttpUtility.HtmlDecode(sub_template) + "\">" + "</div>");
                output.Write(HttpUtility.HtmlDecode(sub_template));

            }
            else
            {
                output.Write(HttpUtility.HtmlDecode(ArticleDetails[DataValueField].ToString()));
            }


        }

    }
}
