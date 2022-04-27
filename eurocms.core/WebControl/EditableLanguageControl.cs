using EuroCMS.Core;
using EuroCMS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [ToolboxData("<{0}:EditableLanguage runat=server></{0}:EditableLanguage>")]
    [DefaultProperty("DataValueField")]
    public class EditableLanguage : System.Web.UI.WebControls.WebControl
    {
        private string _DataValueField = string.Empty;
        [Bindable(true)]
        //[DefaultValue("combo")]
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

            writer.Write("");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }


        protected override void RenderContents(HtmlTextWriter output)
        {
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];

            bool aPre = false;
            string revID = string.Empty;
            int articleID = 0;
            int zoneID = 0;
            string template = string.Empty;

            articleID = Convert.ToInt32(!string.IsNullOrEmpty(ArticleDetails["article_id"].ToString()) ? ArticleDetails["article_id"].ToString() : "0");
            zoneID = Convert.ToInt32(!string.IsNullOrEmpty(ArticleDetails["zone_id"].ToString()) ? ArticleDetails["zone_id"].ToString() : "0");
          
            output.Write(processLangRelations(aPre, revID, articleID, zoneID, template));
        }

        private string processLangRelations(bool aPre, string revID, int article_id, int zone_id, string template)
        {
            string result = template;
            string varText = string.Empty;
            string varCombo = string.Empty;

            if (!string.IsNullOrEmpty(DataValueField))
            {
                DataTable dt = Dal.Instance.SelectArticleLanguageRelations(zone_id, article_id);

                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string atdurl = CmsHelper.getContentLinkAlias(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][11].ToString());
                        if (DataValueField == "combo")
                        {
                            varCombo += "<option value=\"" + atdurl + "\">" + dt.Rows[i][7].ToString() + "</option>" + Environment.NewLine;
                        }
                        else if (DataValueField == "text")
                        {
                            varText += "<li><a href=\"" + atdurl + "\">" + dt.Rows[i][7].ToString() + "</a></li>" + Environment.NewLine;
                        }
                        

                    }

                }
            }

            if (!string.IsNullOrEmpty(varText))
                varText = "<ul>" + Environment.NewLine + varText + Environment.NewLine + "</ul>" + Environment.NewLine;

            
            //if (result.Contains("##article_languages_text##"))
            if (DataValueField=="text")
            {
                result = varText;
            }
            
            //if (result.Contains("##article_languages_combo##"))
            if (DataValueField=="combo")
            {
                varCombo = "<select>" + varCombo + "</select>";
                result = varCombo;
            }
            
            return result;
        }


    }
}
