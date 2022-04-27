using EuroCMS.Plugin.Kale.KaleCommon;
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
    public partial class YeniKoleksiyonlar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadModel()
        {
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            string dil = ArticleDetails.FirstOrDefault(f => f.Key == "lang_id").Value.ToString();

            string requestURLPath = "";
            requestURLPath = HttpContext.Current.Request.RawUrl; //Request.Url.OriginalString;
            requestURLPath = requestURLPath.StartsWith("/") ? requestURLPath.Substring(1, requestURLPath.Length - 1).Trim() : requestURLPath;
            //      /tr/kale/yenikoleksiyonlar/123

            var urlSplit = requestURLPath.Split('/');
            string test = Language.CurrentLanguage[dil].SelectLabel("lbl_yeni_koleksiyonlar");



            try
            {
                if (urlSplit[2] == "yenikoleksiyonlar")
                {                    
                    StringBuilder html = new StringBuilder();

                    int marka_id = Convert.ToInt32(urlSplit[3]);
                    dil = urlSplit[0];

                    ProductService.Authenticate();
                    List<Seri> yeniseriler = ProductService.GetYeniSeriler(marka_id, dil);
                    

                    if (yeniseriler != null && yeniseriler.Count > 0)
                    {
                        Seri ilkSeri = ProductService.GetSeriDetay(Convert.ToInt32(yeniseriler[0].SeriID), dil);

                        html.Append("<div class=\"container-fluid\">");
                        html.Append("   <h2>" + Language.CurrentLanguage[dil].SelectLabel("lbl_yeni_koleksiyonlar") +  "</h2>");
                        html.Append("   <div class=\"row\">");
                        html.Append("      <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">");
                        html.Append("         <div class=\"urunGallery\">");
                        html.Append("            <ul>");

                        foreach (EuroCMS.Plugin.Kale.KaleCommon.Models.Image image in ilkSeri.Image)
                        {
                            html.Append("			<li data-src=\"" + ProductService.WSImageUrl + image.Large + "\" data-sub-html=\"<h4>" + ilkSeri.Adi + "</h4>\"><a href=\"\">");
                            html.Append("				<figcaption class=\"svghover\">");
                            html.Append("					<img src=\"" + ProductService.WSImageUrl + image.Large + "\" alt=\"\">");
                            html.Append("					<span class=\"plus\"><svg id=\"circle\"><circle cx=\"60\" cy=\"60\" r=\"35\" stroke=\"white\" stroke-width=\"1\" fill=\"transparent\"></circle></svg><em class=\"line1\"></em><em class=\"line2\"></em></span>");
                            html.Append("				</figcaption>");
                            html.Append("			</a></li>");
                        }
                        html.Append("            </ul>");
                        html.Append("         </div>");
                        html.Append("         <div class=\"pagetools\">");
                        html.Append("            <ul>");
                        html.Append("               <li>");
                        html.Append("                  <a href=\"javascript:;\" class=\"arrow ion-android-arrow-dropdown\">" + Language.CurrentLanguage[dil].SelectLabel("lbl_share") + "<span class=\"ion-share\"></span></a>");
                        html.Append("                  <ul class=\"hd\">");

                        int defaultPort = Request.IsSecureConnection ? 443 : 80;
                        string pageUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.Port != defaultPort ? ":" + Request.Url.Port : "") + Request.RawUrl;

                        html.Append("					<li><a class=\"socialLink\" href=\"http://www.facebook.com/sharer/sharer.php?u=" + pageUrl + "\"><span class=\"ion-social-facebook\"></span>Facebook</a></li>");
                        html.Append("					<li><a class=\"socialLink\" href=\"http://www.twitter.com/share?url=" + pageUrl + "\"><span class=\"ion-social-twitter\"></span>Twitter</a></li>");
                        html.Append("					<li><a class=\"socialLink\" href=\"http://pinterest.com/pin/create/button/?url=" + pageUrl + "\"><span class=\"ion-social-pinterest\"></span>Pinterest</a></li>");
                        html.Append("                  </ul>");
                        html.Append("               </li>");
                        html.Append("               <li><a href=\"\" onclick=\"window.print(); return false\">" + Language.CurrentLanguage[dil].SelectLabel("lbl_print") + "<span class=\"ion-printer\"></span></a></li>");
                        html.Append("               <li>");
                        html.Append("                  <a href=\"javascript:;\" class=\"ion-android-arrow-dropdown arrow\">" + Language.CurrentLanguage[dil].SelectLabel("lbl_docs") + "<span class=\"ion-document\"></span></a>");
                        html.Append("                  <ul class=\"hd\">");
                        foreach (EuroCMS.Plugin.Kale.KaleCommon.Models.Dosya dosya in ilkSeri.DosyaList)
                        {
                            html.Append("					<li><a href=\"" + ProductService.WSImageUrl + dosya.DosyaAdresi + "\">" + dosya.DokumanTipi + "</a></li>");
                        }
                        html.Append("                  </ul>");
                        html.Append("               </li>");
                        html.Append("            </ul>");
                        html.Append("         </div>");
                        html.Append("      </div>");
                        html.Append("      <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">");
                        html.Append("         <div class=\"productSpec\">");
                        html.Append("            <h1><a href=\"" + GenerateSeriDetayLink(ilkSeri, dil) + "\">" + ilkSeri.Adi + "</a></h1>");
                        html.Append("            <p><a href=\"" + GenerateSeriDetayLink(ilkSeri, dil) + "\">" + ilkSeri.Tanim + "</a></p>");
                        html.Append("         </div>");
                        html.Append("      </div>");
                        html.Append("      <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">");
                        html.Append("         <div class=\"gateway type6\">");
                        html.Append("            <div class=\"row\">");

                        foreach (Seri seri in yeniseriler)
                        {
                            html.Append("               <a class=\"gwitem col-lg-2 col-md-2 col-sm-4 col-xs-12\" href=\"" + GenerateSeriDetayLink(seri, dil) + "\">");
                            html.Append("                  <figcaption class=\"svghover\">");
                            html.Append("                     <img src=\"" + ProductService.WSImageUrl + seri.Image[0]?.Medium + "\" alt=\"" + seri.Adi + "\">");
                            html.Append("                     <span class=\"plus\">");
                            html.Append("                        <svg id=\"circle\">");
                            html.Append("                           <circle cx=\"60\" cy=\"60\" r=\"35\" stroke=\"white\" stroke-width=\"1\" fill=\"transparent\"></circle>");
                            html.Append("                        </svg>");
                            html.Append("                        <em class=\"line1\"></em><em class=\"line2\"></em>");
                            html.Append("                     </span>");
                            html.Append("                  </figcaption>");
                            html.Append("                  <figure>");
                            html.Append("                     <span>" + seri.Adi + "</span>");
                            html.Append("                     <small>" + seri.Tanim?.Truncate(50, "...") + "</small>");
                            html.Append("                  </figure>");
                            html.Append("               </a>");
                        }
                        html.Append("            </div>");
                        html.Append("         </div>");
                        html.Append("      </div>");
                        html.Append("   </div>");
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

        private string GenerateSeriDetayLink(Seri seri, string dil)
        {
            return "/" + dil + "/seri-list/" + seri.SEO;
        }

    }
}