using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace EuroCMS.Web
{
    public class CmsTemplateFile : VirtualFile
    {
        string requestPath;

        public CmsTemplateFile(string virtualPath)
            : base(virtualPath)
        {
            requestPath = virtualPath;
 
            //SiteMapNode node = SiteMap.Provider.FindSiteMapNode(HttpContext.Current.Request.Url.PathAndQuery);
            
            //if(node !=null)
            //    path = node.Url;
        }

        public override System.IO.Stream Open()
        {

            string basePath = "~/App_Data/Template/Default/" + VirtualPathUtility.GetFileName(requestPath); //(VirtualPathUtility.GetFileName(requestPath).Trim() != "Page.aspx" && VirtualPathUtility.GetFileName(requestPath).Trim() != "Error.aspx" ? "Page.aspx" : VirtualPathUtility.GetFileName(requestPath));
            string file = HttpContext.Current.Server.MapPath(basePath);


            //HttpContext.Current.Response.Write(file);
            //HttpContext.Current.Response.End();

            //string aspx_html =   "<%@ Page Language=\"C#\" EnableViewState=\"true\" ValidateRequest=\"true\" AutoEventWireup=\"true\" CodeBehind=\"Page.aspx.cs\" Inherits=\"EuroCMS.FrontEnd.Page\" %>"+
            //"<%@ OutputCache CacheProfile=\"DefaultCacheProfile\" %>"+
            //"<cms:CachedControl ID=\"test1\" runat=\"server\" />";

            try
            {
                return System.IO.File.Open(file, FileMode.Open);
            }
            catch (Exception ex)
            {
                EuroCMS.Model.CmsDbContext dbContext = new EuroCMS.Model.CmsDbContext();
                string errorPageArticle = dbContext.Domains.FirstOrDefault().ErrorPageArticle;

                List<int> zoneIdArticleId = new List<int>();
                zoneIdArticleId = errorPageArticle.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();

                int zoneId = zoneIdArticleId[0];
                int articleId = zoneIdArticleId[1];

                EuroCMS.Model.vArticlesZonesFull getArticleZoneFull = new Model.vArticlesZonesFull();
                getArticleZoneFull = dbContext.vArticlesZonesFulls.Where(az => az.ZoneID == zoneId && az.ArticleID == articleId).FirstOrDefault();

                string redirectUrl = "";

                if (getArticleZoneFull != null)
                {
                    redirectUrl = getArticleZoneFull.ArticleZoneAlias;
                }

                if (string.IsNullOrEmpty(redirectUrl))
                {
                    redirectUrl = EuroCMS.Core.CmsHelper.getContentLinkAlias(getArticleZoneFull.ZoneID.ToString(), getArticleZoneFull.ArticleID.ToString(), HttpUtility.HtmlDecode(getArticleZoneFull.SiteName), HttpUtility.HtmlDecode(getArticleZoneFull.ZoneGroupName),
                   HttpUtility.HtmlDecode(getArticleZoneFull.ZoneName), HttpUtility.HtmlDecode(getArticleZoneFull.Headline), "");
                }

                redirectUrl = (redirectUrl.Substring(0, 1) == "/" ? redirectUrl.Substring(1, redirectUrl.Length - 1) : redirectUrl);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Status = "404 Not Found";
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.Redirect("/" + redirectUrl);
                return null;
            }
        }
    }
}
