using EuroCMS.Core;
using EuroCMS.Data;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{

    [PartialCaching(5)]
    [DefaultProperty("DataValueField")]
    [ToolboxData("<{0}:EditableHtml runat=server></{0}:EditableHtml>")]
    public class EditableHtml : System.Web.UI.WebControls.WebControl
    {

        private string _DataValueField = string.Empty;
        [Bindable(true)]
        [DefaultValue("article_1")]
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
            // 
            // writer.RenderBeginTag("");
            //base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //  writer.RenderEndTag();
            //   base.RenderEndTag(writer);
        }



        protected override void RenderContents(HtmlTextWriter output)
        {
            string sub_template = string.Empty;
            string EditableType = string.Empty;
            string EditableID = string.Empty;
            string EditableName = string.Empty;
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];

            bool Active = true;
            //  if (Page.User.Identity.IsAuthenticated && (Page.User.IsInRole("Administrator") || Page.User.IsInRole("Editor") || Page.User.IsInRole("Author")) && Active)
            if (Active)
            {
                int i = 1;
                switch (DataValueField)
                {
                    case "article_1":
                        i = 1;
                        break;
                    case "article_2":
                        i = 2;
                        break;
                    case "article_3":
                        i = 3;
                        break;
                    case "article_4":
                        i = 4;
                        break;
                    case "article_5":
                        i = 5;
                        break;
                    default:
                        break;
                }

                EditableType = "";
                EditableID = "";
                if (Convert.ToInt32(ArticleDetails["cl_" + i] ?? "0") < 3)
                {
                    if (ArticleDetails["s_article_" + i].ToString() != "")
                    {
                        sub_template = ArticleDetails["s_article_" + i].ToString();
                        EditableType = "site";
                        EditableID = ArticleDetails["site_id"].ToString();
                    }

                    if (Convert.ToInt32(ArticleDetails["cl_" + i]) < 2)
                    {
                        switch (string.IsNullOrEmpty(ArticleDetails["zg_append_" + i].ToString()) ? 0 : Convert.ToInt32(ArticleDetails["zg_append_" + i]))
                        {
                            case 1:
                                sub_template = sub_template + Environment.NewLine + ArticleDetails["zg_article_" + i].ToString();
                                EditableType = "zone_group";
                                EditableID = ArticleDetails["zone_group_id"].ToString();
                                ArticleDetails["zg_append_" + i] = "0";
                                break;
                            case 2:
                                sub_template = ArticleDetails["zg_article_" + i].ToString() + Environment.NewLine + sub_template;
                                EditableType = "zone_group";
                                EditableID = ArticleDetails["zone_group_id"].ToString();
                                ArticleDetails["zg_append_" + i] = "0";
                                break;
                            case 3:
                                sub_template = ArticleDetails["zg_article_" + i].ToString();
                                EditableType = "zone_group";
                                EditableID = ArticleDetails["zone_group_id"].ToString();
                                ArticleDetails["zg_append_" + i] = "0";
                                // Owerwrite, can not contain site container
                                //  vars.eb[1] = vars.eb[1].Replace(",S", "");
                                break;
                        }

                        if (Convert.ToInt32(ArticleDetails["cl_" + i]) < 1)
                        {
                            switch (string.IsNullOrEmpty(ArticleDetails["append_" + i].ToString()) ? 0 : Convert.ToInt32(ArticleDetails["append_" + i]))
                            {
                                case 1:
                                    sub_template = sub_template + Environment.NewLine + ArticleDetails["zone_article_" + i];
                                    EditableType = "zone";
                                    EditableID = ArticleDetails["zone_id"].ToString();
                                    ArticleDetails["append_" + i] = "0";
                                    break;
                                case 2:
                                    sub_template = ArticleDetails["zone_article_" + i].ToString() + Environment.NewLine + sub_template;
                                    EditableType = "zone";
                                    EditableID = ArticleDetails["zone_id"].ToString();
                                    ArticleDetails["append_" + i] = "0";
                                    break;
                                case 3:
                                    sub_template = ArticleDetails["zone_article_" + i].ToString();
                                    EditableType = "zone";
                                    EditableID = ArticleDetails["zone_id"].ToString();
                                    ArticleDetails["append_" + i] = "0";
                                    // Owerwrite, can not contain site container
                                    //  vars.eb[1] = vars.eb[1].Replace(",S", "");
                                    //  vars.eb[1] = vars.eb[1].Replace(",G", "");
                                    break;
                            }
                        }
                        else
                        {
                            // Zone skipped, can not contain zone container
                            //   vars.eb[1] = vars.eb[1].Replace(",Z", "");
                        }
                    }
                    else
                    {
                        // Zone Group skipped, can not contain zone group or zone container
                        //  vars.eb[1] = vars.eb[1].Replace(",G", "");
                        //   vars.eb[1] = vars.eb[1].Replace(",Z", "");
                    }
                    if (EditableType == "")
                    {
                        EditableType = "article";
                        EditableID = ArticleDetails["article_id"].ToString();
                        sub_template = ArticleDetails[DataValueField].ToString();
                    }
                    EditableName = DataValueField.ToString();

                    //    sub_template = HttpUtility.HtmlDecode(ArticleDetails["article_" + i].ToString());
                    // Parsing control on the page
                    sub_template = ReplaceVariables(sub_template, ArticleDetails);
                    Control child = Page.ParseControl(HttpUtility.HtmlDecode(sub_template));

                    child.Page = Page;

                    if (!string.IsNullOrEmpty(sub_template))
                    {
                        //output.Write("<div id=\"" + EditableName + "\"  data-type=\"" + EditableType + "\" data-id=\"" + EditableID + "\" data-name=\"" + EditableName + "\" contenteditable=\"false\">" + RenderControl(child) + "</div>");
                        output.Write(CmsHelper.RenderControl(child));
                    }
                }
                else
                {
                    EditableName = DataValueField.ToString();
                    EditableType = "article";
                    EditableID = ArticleDetails["article_id"].ToString();
                    //data-stype="article" data-sid="100" data-sname="headline" contenteditable="true"
                    //  output.Write("<div   data-type=\"" + EditableType + "\" data-id=\"" + EditableID + "\" data-name=\"" + EditableName + "\" contenteditable=\"false\">" + HttpUtility.HtmlDecode(ArticleDetails[DataValueField].ToString()) + "</div>");
                    output.Write(HttpUtility.HtmlDecode(ArticleDetails[DataValueField].ToString()));
                    // Uses template directly or no content found, can not contain site, zone group or zone container
                    //   vars.eb[1] = vars.eb[1].Replace(",S", "");
                    //   vars.eb[1] = vars.eb[1].Replace(",G", "");
                    //   vars.eb[1] = vars.eb[1].Replace(",Z", "");
                }
            }
            else
            {
                output.Write(HttpUtility.HtmlDecode(ArticleDetails[DataValueField].ToString()));
            }
        }

        private string ReplaceVariables(string sub_template, Dictionary<string, object> ArticleDetails)
        {
            List<string> keys = ArticleDetails.Keys.ToList();
            CmsDbContext dbContext = new CmsDbContext();
            sub_template = HttpUtility.HtmlDecode(sub_template);

            #region From Page aspx
            sub_template = sub_template.Replace("Î", "&#916;");
            sub_template = sub_template.Replace("Î\"", "&#916;");
            sub_template = sub_template.Replace("##current_year##", DateTime.Now.Year.ToString());
            sub_template = sub_template.Replace("##current_month##", DateTime.Now.Month.ToString());
            sub_template = sub_template.Replace("##current_day##", DateTime.Now.Day.ToString());

            if (sub_template.IndexOf("[code]") != -1)
            {
                Regex reg = new Regex("[code](.|.*\n|.*\r\n)*?[/code]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                MatchCollection matches = reg.Matches(sub_template);
                foreach (Match m in matches)
                {
                    sub_template = sub_template.Replace(m.Value, HttpUtility.HtmlEncode(m.Value.Replace("[code]", "").Replace("[/code]", "")));
                }
            }

            if (sub_template.IndexOf("##omniture_") != -1)
            {
                sub_template = sub_template.Replace("##omniture_site_name##", CmsHelper.c2QS(ArticleDetails["site_name"].ToString()));
                sub_template = sub_template.Replace("##omniture_zone_group_name##", CmsHelper.c2QS(ArticleDetails["zone_group_name"].ToString()));
                sub_template = sub_template.Replace("##omniture_zone_group_name_display##", CmsHelper.c2QS(ArticleDetails["zone_group_name_display"].ToString()));
                sub_template = sub_template.Replace("##omniture_zone_name##", CmsHelper.c2QS(ArticleDetails["zone_name"].ToString()));
                sub_template = sub_template.Replace("##omniture_zone_name_display##", HttpUtility.UrlDecode(HttpUtility.HtmlDecode(CmsHelper.c2QS(ArticleDetails["zone_name_display"].ToString()))));
                sub_template = sub_template.Replace("##omniture_headline##", CmsHelper.c2QS(ArticleDetails["headline"].ToString()));
            }


            if (ArticleDetails["article_type"].ToString().Equals("1") && ArticleDetails["article_type_detail"].ToString().StartsWith("http")
                && sub_template.IndexOf("href=\"##article_detail_url##\"") != -1)
            {
                sub_template = sub_template.Replace("href=\"##article_detail_url##\"", "href=\"" + ArticleDetails["article_type_detail"].ToString() + "\" target=\"_blank\"");
            }
            else if (ArticleDetails["article_type"].ToString().Equals("1") && ArticleDetails["article_type_detail"].ToString().StartsWith("http")
               && sub_template.IndexOf("value=\"##article_detail_url##\"") != -1)
            {
                sub_template = sub_template.Replace("value=\"##article_detail_url##\"", "value=\"" + ArticleDetails["article_type_detail"].ToString() + "\"");
            }
            else if (ArticleDetails["article_type"].ToString().Equals("6"))
            {
                sub_template = sub_template.Replace("##article_detail_url##", "" + ArticleDetails["article_type_detail"].ToString() + "");
            }
            else
            {
                if (!string.IsNullOrEmpty(ArticleDetails["mapped_article_url"].ToString()))
                {
                    sub_template = sub_template.Replace("##article_detail_url##", "" + ArticleDetails["mapped_article_url"].ToString() + "");
                }
                else
                {
                    sub_template = sub_template.Replace("##article_detail_url##", getContentLinkAlias(ArticleDetails["zone_id"].ToString(), ArticleDetails["article_id"].ToString(), ArticleDetails["site_name"].ToString(), ArticleDetails["zone_group_name"].ToString(), ArticleDetails["zone_name"].ToString(), ArticleDetails["headline"].ToString(), ArticleDetails["az_alias"].ToString()));
                }
            }
            Page.Items["article_detail_url"] = getContentLinkAlias(ArticleDetails["zone_id"].ToString(), ArticleDetails["article_id"].ToString(), ArticleDetails["site_name"].ToString(), ArticleDetails["zone_group_name"].ToString(), ArticleDetails["zone_name"].ToString(), ArticleDetails["headline"].ToString(), ArticleDetails["az_alias"].ToString());

            if (sub_template.IndexOf("##site_url") != -1)
            {
                sub_template = sub_template.Replace("##site_url##", (HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://") + HttpContext.Current.Request.Url.Host);
            }

            if (sub_template.IndexOf("##menu_text_headline") != -1)
            {
                if (ArticleDetails["menu_text_headline"].ToString() != "")
                {
                    sub_template = sub_template.Replace("##menu_text_headline##", ArticleDetails["menu_text"].ToString());
                }
                else
                {
                    sub_template = sub_template.Replace("##menu_text_headline##", ArticleDetails["headline"].ToString());
                }
            }
            #endregion

            sub_template = CmsHelper.processAFiles(sub_template, Convert.ToInt32(ArticleDetails["article_id"].ToString()));

            sub_template = CmsHelper.replacePortlets(sub_template, Convert.ToInt32(ArticleDetails["article_id"].ToString()), Convert.ToInt32(ArticleDetails["zone_id"].ToString()));

            #region Article Owner
            if (sub_template.IndexOf("##article_owner_") != -1 && !string.IsNullOrEmpty(ArticleDetails["publisher_id"].ToString()))
            {
                vAspNetMembershipUser publisher = new vAspNetMembershipUser();
                Guid publisherId = new Guid(ArticleDetails["publisher_id"].ToString());
                publisher = dbContext.vAspNetMembershipUsers.Where(v => v.IsApproved == true && v.UserId == publisherId).FirstOrDefault();

                var userProfile = System.Web.Profile.ProfileBase.Create(publisher.UserName, false);
                string userFullName = userProfile.GetPropertyValue("System.FullName").ToString().Trim();

                if (publisher != null)
                {
                    sub_template = sub_template.Replace("##article_owner_name##", "" + userFullName);
                    sub_template = sub_template.Replace("##article_owner_email##", "" + publisher.Email);
                    //sub_template = sub_template.Replace("##article_owner_dept##", "" + publisher.);
                }
            }
            #endregion

            sub_template = CmsHelper.processLangRelations(Convert.ToInt32(ArticleDetails["article_id"].ToString()), Convert.ToInt32(ArticleDetails["zone_id"].ToString()), sub_template);

            while (sub_template.Contains("##"))
            {
                int firstIndex = sub_template.IndexOf("##");
                int nextIndex = sub_template.Substring(firstIndex + 1).IndexOf("##");
                string variable = sub_template.Substring(firstIndex, nextIndex + 1);
                variable = variable.Replace("#", "");

                if (ArticleDetails.Keys.Contains(variable))
                {
                    sub_template = sub_template.Replace("##" + variable + "##", ArticleDetails[variable].ToString()); 
                }
                else
                {
                    //sub_template = sub_template.Replace("##" + variable + "##", "||" + variable + "||");
                    sub_template = sub_template.Replace("##" + variable + "##", "||" + variable + "||");
                }
            }

            sub_template = sub_template.Replace("||", "##");

            return sub_template;
        }
        #region getContentLinkAlias
        public string getContentLinkAlias(string zoneId, string articleId, string siteName, string zoneGroupName, string zoneName, string iheadline, string alias)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(alias))
            {
                str = "/" + alias;
            }
            else
            {

                string site_name = "", zone_group_name = "", zone_name = "", headline = "";

                if (siteName != "") site_name = "/" + CmsHelper.c2QS(siteName);
                if (zoneGroupName != "") zone_group_name = "/" + CmsHelper.c2QS(zoneGroupName);
                if (zoneName != "") zone_name = "/" + CmsHelper.c2QS(zoneName);
                if (iheadline != "") headline = "/" + CmsHelper.c2QS(iheadline);

                str = "/" + Constansts.ALIAS_WEB + "/" + zoneId.ToString() + "-" + articleId.ToString() + "-1-1" + site_name + zone_group_name + zone_name + headline;

                str = str.Replace(",", "-");
                str = str.Replace("--", "-");
                str = str.Replace("--", "-");
            }
            return str;
        }
        #endregion
    }
}

