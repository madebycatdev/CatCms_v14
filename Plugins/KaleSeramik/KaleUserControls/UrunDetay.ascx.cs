using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using EuroCMS.Plugin.Kale.KaleCommon.Models;

namespace EuroCMS.Plugin.KaleUserControls
{
    public partial class UrunDetay : System.Web.UI.UserControl
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
                if (urlSplit[1] == "urun-list")
                {
                    StringBuilder html = new StringBuilder();

                    //int urun_id = Convert.ToInt32(urlSplit[3]);
                    string urun_seo_url = requestURLPath.Substring(13, requestURLPath.Length - 13);
                    dil = urlSplit[0];

                    ProductService.Authenticate();
                    //Urun urunDetay = ProductService.GetUrunDetay(urun_id, dil);
                    Urun urunSeoDetay = ProductService.GetUrunDetay(urun_seo_url, dil);



                    if (urunSeoDetay != null)
                    {
                        Urun urunDetay = ProductService.GetUrunDetay(Convert.ToInt32(urunSeoDetay.UrunID), dil);

                        HttpContext.Current.Session["NewHeadline"] = urunSeoDetay.UrunAdi;
                        HttpContext.Current.Session["NewMetaTitle"] = urunSeoDetay.UrunAdi;
                        html.Append("<script> var SERIID=" + urunSeoDetay.UrunID + " </script>");
                        html.Append("<div class=\"container-fluid\">");
                        html.Append(" <h1>" + urunSeoDetay.UrunAdi + "</h1>");
                        html.Append(" <div class=\"row\">");
                        html.Append(" <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">");
                        html.Append("	<div class=\"urunGallery\">");
                        html.Append("		<ul>");
                        foreach (EuroCMS.Plugin.Kale.KaleCommon.Models.Image image in urunSeoDetay.ImageList)
                        {
                            html.Append("			<li data-src=\"" + ProductService.WSImageUrl + image.Large + "\" data-sub-html=\"<h4>" + urunSeoDetay.UrunAdi + "</h4>\"><a href=\"\">");
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
                        foreach (EuroCMS.Plugin.Kale.KaleCommon.Models.Dosya dosya in urunSeoDetay.DosyaList)
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
                        if (!String.IsNullOrEmpty(urunSeoDetay.MarkaAdi))
                        {
                            html.Append("		<img src=\"i/assets/images/site/" + ProductService.GetValidAlias(urunSeoDetay.MarkaAdi) + "-logo.png\" alt=\"" + urunSeoDetay.MarkaAdi + "\">");
                        }
                        html.Append("	</div>");
                        html.Append("	<div class=\"productSpec\">");
                        html.Append("		<h2>" + Language.CurrentLanguage[dil].SelectLabel("lbl_upper_properties") + "</h2>");
                        html.Append("		<p>" + urunSeoDetay.UrunAciklama + "</p>");
                        if (string.IsNullOrEmpty(urunSeoDetay.UrunAciklama))
                        {
                            if (!string.IsNullOrEmpty(urunSeoDetay.UrunKodu))
                            {
                                html.Append("<p><strong>" + Language.CurrentLanguage[dil].SelectLabel("lbl_upper_product_code") + "</strong><br>" + urunSeoDetay.UrunKodu + "</p>");
                            }

                            if (!string.IsNullOrEmpty(urunSeoDetay.KutuIciMiktar))
                            {
                                html.Append("<p><strong>" + (dil == "tr" ? "KUTU BİLGİSİ" : "BOX INFO") + "</strong><br>" + urunSeoDetay.KutuIciMiktar + "</p>");
                            }

                            if (urunSeoDetay.EbatList != null)
                            {
                                foreach (var ebat in urunSeoDetay.EbatList)
                                {
                                    html.Append("<p><strong>" + (dil == "tr" ? "EBAT" : "SIZE") + "</strong><br>" + ebat.EbatAdi + "</p>");
                                }

                            }

                            if (!string.IsNullOrEmpty(urunSeoDetay.Ebat))
                            {
                                html.Append("<p><strong>" + (dil == "tr" ? "ÜRÜN EBAT" : "PRODUCT SIZE") + "</strong><br>" + urunSeoDetay.Ebat + "</p>");
                            }

                            if (!string.IsNullOrEmpty(urunDetay.UrunEbat))
                            {
                                html.Append("<p><strong>"+(dil == "tr" ? "ÜRÜN EBAT" : "PRODUCT SIZE")+"</strong><br>" + urunDetay.UrunEbat + "</p>");
                            }

                            if (!string.IsNullOrEmpty(urunSeoDetay.Yuzey))
                            {
                                html.Append("<p><strong>"+ (dil == "tr" ? "YÜZEY" : "SURFACE") + "</strong><br>" + urunSeoDetay.Yuzey + "</p>");
                            }

                            if (!string.IsNullOrEmpty(urunSeoDetay.KutuIciAdet))
                            {
                                html.Append("<p><strong>"+ (dil == "tr" ? "KUTU İÇİ ADET" : "PIECES IN BOX") + "</strong><br>" + urunSeoDetay.KutuIciAdet + "</p>");
                            }
                        }
                        html.Append("	</div>");
                        html.Append("   <div class=\"ozellikList\">");
                        html.Append("     <ul>");
                        foreach (var ozellik in urunSeoDetay.OzellikList)
                        {
                            html.Append("       <li><img src=\"" + ProductService.WSImageUrl + "/Images/" + ozellik.OImageURL + "\" alt=\"" + ozellik.ODesc + "\"></li>");
                        }
                        html.Append("     </ul>");
                        html.Append("   </div>");
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
                        html.Append("    <p>" + Language.CurrentLanguage[dil].SelectLabel("lbl_not_found_product") + "</p>");
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
                html.Append("<p>" + ex.Message + "</p>");
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    html.Append("<p>" + ex.InnerException.Message + "</p>");
                }
                html.Append("</div>");
                HttpContext.Current.Session["NewArticle_1"] = html.ToString();
            }
        }

    }
}