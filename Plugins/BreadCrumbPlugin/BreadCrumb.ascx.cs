using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("EuroCMS.Plugin", "plugin")]
namespace EuroCMS.Plugin
{
    public partial class BreadCrumb : System.Web.UI.UserControl
    {
        public string BreadCrumbResult { get; set; }

        public string FirstText { get; set; }

        public bool IsMenuText { get; set; }

        public string Url { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadModel()
        {

            CmsDbContext dbContext = new CmsDbContext();

            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];

            int articleId = 0; //199;
            int zoneId = 0; //76;
            string liFirstText = "", li1 = "", li2 = "", li3 = "", li4 = "", li5 = "", li6 = "";

            if (ArticleDetails == null)
            {
                return;
            }

            articleId = Convert.ToInt32(ArticleDetails["article_id"]);
            zoneId = Convert.ToInt32(ArticleDetails["zone_id"]);

            vArticlesZonesFull getCurrentArticleDetail = new vArticlesZonesFull();
            getCurrentArticleDetail = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ArticleID == articleId && vaz.ZoneID == zoneId).ToList().FirstOrDefault();

            if (getCurrentArticleDetail == null)
            {
                return;
            }


            li1 = "<li id=\"" + getCurrentArticleDetail.ArticleID.ToString() + "\" class=\"active\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";

            // Maksimum 7 parent
            vArticlesZonesFull parentArticleZone1 = new vArticlesZonesFull();
            vArticlesZonesFull parentArticleZone2 = new vArticlesZonesFull();
            vArticlesZonesFull parentArticleZone3 = new vArticlesZonesFull();
            vArticlesZonesFull parentArticleZone4 = new vArticlesZonesFull();
            vArticlesZonesFull parentArticleZone5 = new vArticlesZonesFull();
            vArticlesZonesFull parentArticleZone6 = new vArticlesZonesFull();

            parentArticleZone1 = dbContext.vArticlesZonesFulls.Where(vaz => vaz.NavigationZoneID == zoneId && vaz.Status == 1 && vaz.NavigationDisplay != 0).OrderBy(o => o.AzOrder).FirstOrDefault();

            if (parentArticleZone1 != null)
            {
                if (parentArticleZone1.Headline.Trim() != getCurrentArticleDetail.Headline.Trim())
                {
                    li1 += " itemprop=\"child\"";
                    li2 = "<li id=\"" + parentArticleZone1.ArticleID.ToString() + "\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";

                    parentArticleZone2 = dbContext.vArticlesZonesFulls.Where(vaz => vaz.NavigationZoneID == parentArticleZone1.ZoneID && vaz.Status == 1 && vaz.NavigationDisplay != 0).OrderBy(o => o.AzOrder).FirstOrDefault();
                    if (parentArticleZone2 != null)
                    {
                        if (parentArticleZone2.Headline.Trim() != parentArticleZone1.Headline.Trim())
                        {
                            li2 += " itemprop=\"child\"";
                            li3 = "<li id=\"" + parentArticleZone2.ArticleID.ToString() + "\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";
                            parentArticleZone3 = dbContext.vArticlesZonesFulls.Where(vaz => vaz.NavigationZoneID == parentArticleZone2.ZoneID && vaz.Status == 1 && vaz.NavigationDisplay != 0).OrderBy(o => o.AzOrder).FirstOrDefault();

                            if (parentArticleZone3 != null)
                            {
                                if (parentArticleZone2.Headline.Trim() != parentArticleZone3.Headline.Trim())
                                {
                                    li3 += " itemprop=\"child\"";
                                    li4 = "<li id=\"" + parentArticleZone3.ArticleID.ToString() + "\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";

                                    parentArticleZone4 = dbContext.vArticlesZonesFulls.Where(vaz => vaz.NavigationZoneID == parentArticleZone3.ZoneID && vaz.Status == 1 && vaz.NavigationDisplay != 0).OrderBy(o => o.AzOrder).FirstOrDefault();

                                    if (parentArticleZone4 != null)
                                    {
                                        if (parentArticleZone3.Headline.Trim() != parentArticleZone4.Headline.Trim())
                                        {
                                            li4 += " itemprop=\"child\"";
                                            li5 = "<li id=\"" + parentArticleZone4.ArticleID.ToString() + "\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";

                                            parentArticleZone5 = dbContext.vArticlesZonesFulls.Where(vaz => vaz.NavigationZoneID == parentArticleZone4.ZoneID && vaz.Status == 1 && vaz.NavigationDisplay != 0).OrderBy(o => o.AzOrder).FirstOrDefault();

                                            if (parentArticleZone5 != null)
                                            {
                                                if (parentArticleZone5.Headline.Trim() != parentArticleZone4.Headline.Trim())
                                                {
                                                    li5 += " itemprop=\"child\"";
                                                    li6 = "<li id=\"" + parentArticleZone5.ArticleID.ToString() + "\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";

                                                    parentArticleZone6 = dbContext.vArticlesZonesFulls.Where(vaz => vaz.NavigationZoneID == parentArticleZone5.ZoneID && vaz.Status == 1 && vaz.NavigationDisplay != 0).OrderBy(o => o.AzOrder).FirstOrDefault();

                                                    if (parentArticleZone6 != null)
                                                    {
                                                        if (parentArticleZone6.Headline.Trim() != parentArticleZone5.Headline.Trim())
                                                        {
                                                            li6 += " itemprop=\"child\"";
                                                        }
                                                    }

                                                    li6 += " itemref=\"" + parentArticleZone4.ArticleID.ToString() + "\">" + Environment.NewLine + "<a href='" + GetArticleDetailUrl(parentArticleZone5.ArticleID, parentArticleZone5.ZoneID) + "' itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + ((string.IsNullOrEmpty(parentArticleZone5.MenuText.Trim()) ? parentArticleZone5.Headline.Trim() : parentArticleZone5.MenuText.Trim())) + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;
                                                }
                                            }

                                            li5 += " itemref=\"" + parentArticleZone3.ArticleID.ToString() + "\">" + Environment.NewLine + "<a href='" + GetArticleDetailUrl(parentArticleZone4.ArticleID, parentArticleZone4.ZoneID) + "' itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + ((string.IsNullOrEmpty(parentArticleZone4.MenuText.Trim()) ? parentArticleZone4.Headline.Trim() : parentArticleZone4.MenuText.Trim())) + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;
                                        }
                                    }

                                    li4 += " itemref=\"" + parentArticleZone2.ArticleID.ToString() + "\">" + Environment.NewLine + "<a href='" + GetArticleDetailUrl(parentArticleZone3.ArticleID, parentArticleZone3.ZoneID) + "' itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + ((string.IsNullOrEmpty(parentArticleZone3.MenuText.Trim()) ? parentArticleZone3.Headline.Trim() : parentArticleZone3.MenuText.Trim())) + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;
                                }
                            }

                            li3 += " itemref=\"" + parentArticleZone1.ArticleID.ToString() + "\">" + Environment.NewLine + "<a href='" + GetArticleDetailUrl(parentArticleZone2.ArticleID, parentArticleZone2.ZoneID) + "' itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + ((string.IsNullOrEmpty(parentArticleZone2.MenuText.Trim()) ? parentArticleZone2.Headline.Trim() : parentArticleZone2.MenuText.Trim())) + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;
                        }
                    }

                    li2 += " itemref=\"" + getCurrentArticleDetail.ArticleID.ToString() + "\">" + Environment.NewLine + "<a href='" + GetArticleDetailUrl(parentArticleZone1.ArticleID, parentArticleZone1.ZoneID) + "' itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + ((string.IsNullOrEmpty(parentArticleZone1.MenuText.Trim()) ? parentArticleZone1.Headline.Trim() : parentArticleZone1.MenuText.Trim())) + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;
                }
            }

            li1 += ">" + Environment.NewLine + "<a href='" + GetArticleDetailUrl(getCurrentArticleDetail.ArticleID, getCurrentArticleDetail.ZoneID) + "' itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + ((string.IsNullOrEmpty(getCurrentArticleDetail.MenuText.Trim()) ? getCurrentArticleDetail.Headline.Trim() : getCurrentArticleDetail.MenuText.Trim())) + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;


            if (!string.IsNullOrEmpty(FirstText))
            {
                liFirstText = "<li id=\"0\" class=\"active\" itemscope itemtype=\"http://data-vocabulary.org/Breadcrumb\"";
                liFirstText += ">" + Environment.NewLine + "<a href=\"" + Url + "\" itemprop=\"url\">" + Environment.NewLine + "<span itemprop=\"title\">" + FirstText + "</span>" + Environment.NewLine + "</a>" + Environment.NewLine + "</li>" + Environment.NewLine;
            }
            //BreadCrumbResult = "<ul>" + li7 + li6 + li5 + li4 + li3 + li2 + li1 + "</ul>";
            BreadCrumbResult = "<ul class=\"breadcrumb\">" + Environment.NewLine + liFirstText + li6 + li5 + li4 + li3 + li2 + li1 + "</ul>";
        }

        public string GetArticleDetailUrl(int articleId, int zoneId)
        {
            string returnVal = "";

            CmsDbContext dbContext = new CmsDbContext();
            vArticlesZonesFull getCurrentArticleDetail = new vArticlesZonesFull();
            getCurrentArticleDetail = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ArticleID == articleId && vaz.ZoneID == zoneId).ToList().FirstOrDefault();

            if (getCurrentArticleDetail == null)
            {
                return "";
            }
            string currentArticleLink = getCurrentArticleDetail.ArticleZoneAlias;
            if (string.IsNullOrEmpty(currentArticleLink))
            {
                currentArticleLink = "/" + CmsHelper.getContentLinkAlias(getCurrentArticleDetail.ZoneID.ToString(),
                                                     getCurrentArticleDetail.ArticleID.ToString(), getCurrentArticleDetail.SiteName,
                                                     getCurrentArticleDetail.ZoneGroupName, getCurrentArticleDetail.ZoneName, getCurrentArticleDetail.Headline, "");
            }


            //yenieklendi

            string article_link = string.Empty;
            if (getCurrentArticleDetail.ArticleType == 2)
            {
                string[] typeDetail = getCurrentArticleDetail.ArticleTypeDetail.Split('-').ToArray();

                currentArticleLink = CmsHelper.GetArticleAliasOrURL(getCurrentArticleDetail.ArticleID, getCurrentArticleDetail.ZoneID.ToString());
            }
            else
            {
                currentArticleLink = CmsHelper.getContentLinkAlias(getCurrentArticleDetail.ZoneID.ToString(),
                                                     getCurrentArticleDetail.ArticleID.ToString(), getCurrentArticleDetail.SiteName,
                                                     getCurrentArticleDetail.ZoneGroupName, getCurrentArticleDetail.ZoneName, getCurrentArticleDetail.Headline, getCurrentArticleDetail.ArticleZoneAlias);
            }


            if (getCurrentArticleDetail.ArticleType == 1 && (getCurrentArticleDetail.ArticleTypeDetail.StartsWith("http://") || getCurrentArticleDetail.ArticleTypeDetail.StartsWith("https://")))
            {
                currentArticleLink = getCurrentArticleDetail.ArticleTypeDetail;
                if (currentArticleLink.Contains(" "))
                    currentArticleLink = currentArticleLink.Replace(" ", "\" target=\"\"");
            }

            currentArticleLink = "href=\"" + currentArticleLink + "\"";

            if (getCurrentArticleDetail.ArticleType == 6)
                currentArticleLink = getCurrentArticleDetail.ArticleTypeDetail;

            currentArticleLink = currentArticleLink.Replace("href=\"", "").Replace("\"", "");


            //yeni eklendi.


            returnVal = (currentArticleLink.Substring(0, 1) == "/" ? currentArticleLink : "/" + currentArticleLink);
            returnVal = (currentArticleLink.Substring(0, 2) == "//" ? currentArticleLink.Substring(1) : currentArticleLink);

            return returnVal;
        }
    }
}