using EuroCMS.Plugin.Kale.KaleCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EuroCMS.Plugin.KaleUserControls
{
    public partial class SeriDetay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadModel()
        {
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];

            string requestURLPath = "";

            requestURLPath = HttpContext.Current.Request.RawUrl; //Request.Url.OriginalString;
            requestURLPath = requestURLPath.StartsWith("/") ? requestURLPath.Substring(1, requestURLPath.Length - 1).Trim() : requestURLPath;
            //      /tr/kale/seri-detay/123

            var urlSplit = requestURLPath.Split('/');
            string dil = urlSplit[0];

            try
            {
                if (urlSplit[1] == "seri-list")
                {
                    StringBuilder html = new StringBuilder();
                    //int seri_id = Convert.ToInt32(urlSplit[3]);
                    string seri_seo_url = requestURLPath.Substring(13, requestURLPath.Length - 13); 
                    dil = urlSplit[0];

                    ProductService.Authenticate();
                    //Seri seriDetay = ProductService.GetSeriDetay(seri_id, dil);
                    Seri seriDetay = ProductService.GetSeriDetay(seri_seo_url, dil);

                    if (seriDetay != null)
                    {
                        HttpContext.Current.Session["NewHeadline"] = seriDetay.Adi;
                        HttpContext.Current.Session["NewMetaTitle"] = seriDetay.Adi;
                        html.Append("<script> var SERIID=" + seriDetay.SeriID + " </script>");
                        html.Append("<div class=\"container-fluid\">");
                        html.Append(" <h1>" + seriDetay.Adi + "</h1>");
                        html.Append(" <div class=\"row\">");
                        html.Append("  <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">");
                        html.Append("	<div class=\"urunGallery\">");
                        html.Append("		<ul>");
                        foreach (EuroCMS.Plugin.Kale.KaleCommon.Models.Image image in seriDetay.Image)
                        {
                            html.Append("			<li data-src=\"" + ProductService.WSImageUrl + image.Large + "\" data-sub-html=\"<h4>" + seriDetay.Adi + "</h4>\"><a href=\"\">");
                            html.Append("				<figcaption class=\"svghover\">");
                            html.Append("					<img src=\"" + ProductService.WSImageUrl + image.Large + "\" alt=\"\">");
                            html.Append("					<span class=\"plus\"><svg id=\"circle\"><circle cx=\"60\" cy=\"60\" r=\"35\" stroke=\"white\" stroke-width=\"1\" fill=\"transparent\"></circle></svg><em class=\"line1\"></em><em class=\"line2\"></em></span>");
                            html.Append("				</figcaption>");
                            html.Append("			</a></li>");
                        }
                        html.Append("		</ul>");
                        html.Append("	</div>");
                        html.Append("	<div class=\"pagetools\">");
                        html.Append("		<ul>");
                        html.Append("			<li><a href=\"javascript:;\" class=\"arrow ion-android-arrow-dropdown\">" + Language.CurrentLanguage[dil].SelectLabel("lbl_share") + "<span class=\"ion-share\"></span></a>");
                        html.Append("				<ul class=\"hd\">");

                        int defaultPort = Request.IsSecureConnection ? 443 : 80;
                        string pageUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.Port != defaultPort ? ":" + Request.Url.Port : "") + Request.RawUrl;

                        html.Append("					<li><a class=\"socialLink\" href=\"http://www.facebook.com/sharer/sharer.php?u=" + pageUrl + "\"><span class=\"ion-social-facebook\"></span>Facebook</a></li>");                        
                        html.Append("					<li><a class=\"socialLink\" href=\"http://www.twitter.com/share?url=" + pageUrl + "\"><span class=\"ion-social-twitter\"></span>Twitter</a></li>");                                              
                        html.Append("					<li><a class=\"socialLink\" href=\"http://pinterest.com/pin/create/button/?url=" + pageUrl + "\"><span class=\"ion-social-pinterest\"></span>Pinterest</a></li>");
                        html.Append("				</ul>");
                        html.Append("			</li>");
                        html.Append("			<li><a href=\"\" onclick=\"window.print(); return false\">" + Language.CurrentLanguage[dil].SelectLabel("lbl_print") + "<span class=\"ion-printer\"></span></a></li>");
                        html.Append("			<li><a href=\"javascript:;\" class=\"ion-android-arrow-dropdown arrow\">" + Language.CurrentLanguage[dil].SelectLabel("lbl_docs") + "<span class=\"ion-document\"></span></a>");
                        html.Append("				<ul class=\"hd\">");
                        foreach (EuroCMS.Plugin.Kale.KaleCommon.Models.Dosya dosya in seriDetay.DosyaList)
                        {
                            html.Append("					<li><a href=\"" + ProductService.WSImageUrl + dosya.DosyaAdresi + "\">" + dosya.DokumanTipi + "</a></li>");
                        }
                        html.Append("				</ul>");
                        html.Append("			</li>");
                        html.Append("		</ul>");
                        html.Append("	</div>");
                        html.Append("");
                        html.Append("</div>");
                        html.Append("<div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">");
                        html.Append("	<div class=\"brandLogo\">");
                        if (!String.IsNullOrEmpty(seriDetay.MarkaAdi))
                        {
                            html.Append("		<img src=\"i/assets/images/site/" + ProductService.GetValidAlias(seriDetay.MarkaAdi) + "-logo.png\" alt=\"" + seriDetay.MarkaAdi + "\">");
                        }
                        html.Append("	</div>");
                        html.Append("	<div class=\"productSpec\">");
                        html.Append("		<h2>" + Language.CurrentLanguage[dil].SelectLabel("lbl_upper_properties") + "</h2>");
                        html.Append("		<p>" + seriDetay.Tanim + "</p>");
                        html.Append("	</div>");
                        html.Append("  </div>");
                        html.Append("  </div>");
                        html.Append("</div>");


                        HttpContext.Current.Session["NewArticle_1"] = html.ToString();
                    }
                    else
                    {
                        html.Append("<div id=\"productNotFound\">");
                        html.Append("<h2><i class=\"ion-alert-circled\"></i></h2>");
                        html.Append(" ");
                        html.Append("    <p>" + Language.CurrentLanguage[dil].SelectLabel("lbl_not_found_seri") + "</p>");
                        html.Append("</div>");

                        HttpContext.Current.Session["NewArticle_1"] = html.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder html = new StringBuilder();
                html.Append("<div id=\"productNotFound\">");
                html.Append("<h2><i class=\"ion-alert-circled\"></i></h2>");
                html.Append(" ");
                html.Append("    <p>Bir hata oluştu, lütfen daha sonra tekrar deneyiniz. / An error occured, please try again later</p>");
                html.Append("</div>");
                HttpContext.Current.Session["NewArticle_1"] = html.ToString();
            }
        }

    }
}