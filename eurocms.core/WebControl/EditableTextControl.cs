using EuroCMS.Core;
using EuroCMS.Data;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [ToolboxData("<{0}:EditableText runat=server></{0}:EditableText>")]
    [DefaultProperty("DataValueField")]
    public class EditableText : System.Web.UI.WebControls.WebControl
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

            try
            {
                Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
                string[] DataArr = new string[2];
                string StrRange = string.Empty;
                string outPutHtml = string.Empty;
                if (DataValueField.Contains(","))
                {
                    DataArr = DataValueField.Split(',');
                    DataValueField = DataArr[0].ToString();
                    StrRange = DataArr[1].ToString();
                }

                if (DataValueField == "article_detail_url")
                {

                    CmsDbContext dbContext = new CmsDbContext();
                    int articleId = Convert.ToInt32(ArticleDetails["article_id"]);
                    string azAlias = dbContext.ArticleZones.Where(az => az.ArticleID == articleId).FirstOrDefault().AzAlias;
                    string articleDetailUrl = string.Empty;
                    if (ArticleDetails["article_type"].ToString() == "2")
                    {
                        string[] typeDetail = ArticleDetails["article_type_detail"].ToString().Split('-').ToArray();
                        string redirectZoneId = typeDetail[0];
                        int redirectArticleId = Convert.ToInt32(typeDetail[1]);
                        articleDetailUrl = CmsHelper.GetArticleAliasOrURL(redirectArticleId, redirectZoneId);
                    }
                    else
                    {
                        articleDetailUrl = CmsHelper.getContentLinkAlias(ArticleDetails["zone_id"].ToString(), ArticleDetails["article_id"].ToString(), ArticleDetails["site_name"].ToString(), ArticleDetails["zone_group_name"].ToString(), ArticleDetails["zone_name"].ToString(), ArticleDetails["headline"].ToString(), "");
                    }
                    articleDetailUrl = (string.IsNullOrEmpty(azAlias) ? articleDetailUrl : azAlias);
                    articleDetailUrl = (articleDetailUrl.Substring(0, 1) == "/" ? articleDetailUrl.Substring(1, articleDetailUrl.Length - 1) : articleDetailUrl);
                    ArticleDetails["article_detail_url"] = articleDetailUrl;

                    outPutHtml = articleDetailUrl;

                    //if (HttpContext.Current.Session["newArticleLink"] != null)
                    //{
                    //    outPutHtml = HttpContext.Current.Session["newArticleLink"].ToString();
                    //}
                    //else
                    //{
                    //    outPutHtml = ArticleDetails["article_detail_url"] != null ? ArticleDetails["article_detail_url"].ToString() : "";
                    //}

                }
                else
                {
                    if (ArticleDetails == null)
                    {
                        ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
                    }
                    if (ArticleDetails != null)
                    {
                        if (ArticleDetails[DataValueField] != null)
                        {
                            outPutHtml = HttpUtility.HtmlEncode(ArticleDetails[DataValueField].ToString());
                        }
                    }
                }
                if (!string.IsNullOrEmpty(StrRange))
                {
                    if (outPutHtml.Length > Convert.ToInt32(StrRange))
                    {
                        outPutHtml = outPutHtml.Substring(0, Convert.ToInt32(StrRange)) + "...";
                    }
                }
                string sub_template = string.Empty;

                string EditableType = string.Empty;
                string EditableID = string.Empty;

                string EditableName = DataValueField;

                if (DataValueField == "headline" || DataValueField == "summary" || DataValueField == "menu_text")
                {
                    EditableType = "article";
                    EditableID = ArticleDetails["article_id"].ToString();
                }
                else if (DataValueField == "zone_name")
                {
                    EditableType = "zone";
                    EditableID = ArticleDetails["zone_id"].ToString();
                }
                else if (DataValueField == "zone_group_name")
                {
                    EditableType = "zonegroup";
                    EditableID = ArticleDetails["zone_group_id"].ToString();
                }
                else if (DataValueField.Contains("custom_"))
                {
                    EditableType = "article";
                    EditableID = ArticleDetails["article_id"].ToString();

                    DataTable Clsfs = DbHelper.ExecSQL("select * from cms_classification_combo_values where classification_id=" + ArticleDetails["clsf_id"].ToString() + " and column_no=" + DataValueField.Replace("custom_", "")).Tables[0];
                    if (Clsfs.Rows.Count > 0)
                    {
                        outPutHtml = "<select name=\"" + DataValueField + "\" id=\"" + DataValueField + "\" <option value=\"\">Please Select</option>";
                        for (int i = 0; i < Clsfs.Rows.Count; i++)
                        {
                            outPutHtml = outPutHtml + "<option value=\"" + Clsfs.Rows[i]["combo_value"].ToString() + "\">" + Clsfs.Rows[i]["combo_label"].ToString() + "</option>";
                        }

                        outPutHtml = outPutHtml + "</select>";
                    }
                    else
                    {
                        outPutHtml = ArticleDetails[DataValueField].ToString();
                    }
                }


                if (Page.User.Identity.IsAuthenticated && (Page.User.IsInRole("Administrator") || Page.User.IsInRole("Editor") || Page.User.IsInRole("Author") || Page.User.IsInRole("ContentManager") || Page.User.IsInRole("ContentEntry") || Page.User.IsInRole("UserCreator") ))
                {
                    //outPutHtml="<div data-type=\"" + EditableType + "\" data-id=\"" + EditableID + "\" data-name=\"" + EditableName + "\" contenteditable=\"true\">" + outPutHtml + "</div>";
                    //  outPutHtml = HttpUtility.HtmlEncode(ArticleDetails[DataValueField].ToString());
                }
                else
                {
                    //  outPutHtml = HttpUtility.HtmlEncode(ArticleDetails[DataValueField].ToString());
                }


                output.Write(outPutHtml);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect("/", true);

                //var st = new System.Diagnostics.StackTrace(ex, true);
                //var frame = st.GetFrame(0);
                //var line = frame.GetFileLineNumber();

                //HttpContext.Current.Response.Write(ex.Message + " - " + ex.InnerException + " - Line: " + line.ToString() + " - FullStackTrace: " + ex.StackTrace);


                //if (HttpContext.Current.Session["EditableTextError"] == null)
                //{
                //    HttpContext.Current.Session["EditableTextError"] = 1;
                //    HttpContext.Current.Response.Redirect(Page.Request.Url.ToString(), true);
                //}
                //else
                //{
                //    int errorCount = Convert.ToInt32(HttpContext.Current.Session["EditableTextError"]);

                //    if (errorCount >= 3)
                //    {
                //        HttpContext.Current.Session["EditableTextError"] = null;
                //        HttpContext.Current.Response.Redirect("/", true);
                //    }
                //    else
                //    {
                //        HttpContext.Current.Session["EditableTextError"] = errorCount++;
                //        HttpContext.Current.Response.Redirect(Page.Request.Url.ToString(), true);
                //    }
                //}

            }


        }

    }
}
