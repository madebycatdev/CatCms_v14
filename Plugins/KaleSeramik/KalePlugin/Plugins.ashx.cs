using EMWebApplicationLibrary;
using EuroCMS.Core;
using EuroCMS.Model;
using EuroCMS.Plugin.Kale.KaleCommon.Models;
using EuroCMS.Plugin.Kale.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.Kale
{
    /// <summary>
    /// Summary description for plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        public class RedirectionJsonReq
        {
            public string password { get; set; }
            public List<RedirectionJson> redirections { get; set; }
        }

        public class RedirectionJson
        {
            public string fromUrl { get; set; }
            public string toUrl { get; set; }
            public string type { get; set; }
            public string reqBy { get; set; }
        }

        public class InsertPageRedirectionResponse
        {
            public bool isProcess { get; set; }
            public bool isError { get; set; }
            public string message { get; set; }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            context.Response.ContentType = "text/json";


            var redirectionResponse = InsertPageRedirections(context);
            if (redirectionResponse.isProcess && !redirectionResponse.isError)
            {
                context.Response.Write("İşlem tamamlandı.");
            }
            else if (redirectionResponse.isProcess)
            {
                context.Response.Write(redirectionResponse.message);
            }

            string result = "", action = "";

            try
            {
                if (!string.IsNullOrEmpty(context.Request["action"]))
                {
                    action = context.Request["action"].ToLower().Trim();

                    string dil = context.Request["dil"] ?? "TR";
                    dil = dil.ToLowerInvariant();
                    string ulke_id = context.Request["ulke_id"] ?? string.Empty;
                    string bolge_id = context.Request["bolge_id"] ?? string.Empty;
                    string sehir_id = context.Request["sehir_id"] ?? string.Empty;
                    string kayit_sayisi = context.Request["kayit_sayisi"] ?? string.Empty;
                    string ilce_id = context.Request["ilce_id"] ?? string.Empty;
                    string marka_id = context.Request["marka_id"] ?? string.Empty;
                    string seri_id = context.Request["seri_id"] ?? string.Empty;
                    string seri_alias = context.Request["seri_alias"] ?? string.Empty;
                    if (seri_alias.EndsWith("/"))
                        seri_alias = seri_alias.Substring(0, seri_alias.Length - 1);
                    string urun_id = context.Request["urun_id"] ?? string.Empty;
                    string urun_alias = context.Request["urun_alias"] ?? string.Empty;
                    if (urun_alias.EndsWith("/"))
                        urun_alias = urun_alias.Substring(0, seri_alias.Length - 1);
                    string producttype = context.Request["producttype"] ?? string.Empty;
                    string subproducttype = context.Request["subproducttype"] ?? string.Empty;
                    string referans_turu_id = context.Request["referans_turu_id"] ?? string.Empty;
                    string cins_id = context.Request["cins_id"] ?? string.Empty;
                    string referans_id = context.Request["referans_id"] ?? string.Empty;
                    string kampanya_id = context.Request["kampanya_id"] ?? string.Empty;
                    string etkinlik_id = context.Request["etkinlik_id"] ?? string.Empty;
                    string reload_cache = context.Request["reload_cache"] ?? string.Empty;
                    string urun_ids = context.Request["urun_ids"] ?? string.Empty;
                    string pageindex = context.Request["pageindex"] ?? string.Empty;
                    string pagesize = context.Request["pagesize"] ?? string.Empty;
                    string keyword = context.Request["keyword"] ?? string.Empty;
                    string bayi_tipi = context.Request["bayi_tipi"] ?? string.Empty;
                    string urun_tipi_enum = context.Request["urun_tipi_enum"] ?? string.Empty;
                    string zone_id = context.Request["zone_id"] ?? string.Empty;
                    tr.com.kale.digitalkatalogws.EnumBayiTipi bayitip = tr.com.kale.digitalkatalogws.EnumBayiTipi.SatisNoktalari;
                    //iletisim sayfası
                    string country_id = context.Request["country_id"] ?? string.Empty;
                    string city_id = context.Request["city_id"] ?? string.Empty;
                    string town_id = context.Request["town_id"] ?? string.Empty;

                    if (!AppUtility.IsNumeric(urun_tipi_enum))
                        urun_tipi_enum = "0";

                    if (!AppUtility.IsNumeric(pageindex))
                        pageindex = "1";

                    if (!AppUtility.IsNumeric(bolge_id))
                        bolge_id = "0";

                    if (!AppUtility.IsNumeric(pagesize))
                        pagesize = "16";

                    if (!AppUtility.IsNumeric(marka_id))
                        marka_id = "0";

                    if (!AppUtility.IsNumeric(referans_turu_id))
                        referans_turu_id = "0";

                    if (!AppUtility.IsNumeric(producttype))
                        producttype = "0";

                    if (!AppUtility.IsNumeric(cins_id))
                        cins_id = "0";

                    if (!AppUtility.IsNumeric(seri_id))
                        seri_id = "0";

                    if (!AppUtility.IsNumeric(urun_id))
                        urun_id = "0";

                    if (!AppUtility.IsNumeric(kayit_sayisi))
                        kayit_sayisi = "0";

                    if (!AppUtility.IsNumeric(reload_cache))
                        reload_cache = "0";

                    if (!AppUtility.IsNumeric(bayi_tipi))
                        bayi_tipi = "1";

                    if (!AppUtility.IsNumeric(subproducttype))
                        subproducttype = "0";

                    if (!AppUtility.IsNumeric(zone_id))
                        zone_id = "0";

                    if (bayi_tipi == "1")
                        bayitip = tr.com.kale.digitalkatalogws.EnumBayiTipi.SatisNoktalari;

                    if (bayi_tipi == "2")
                        bayitip = tr.com.kale.digitalkatalogws.EnumBayiTipi.YetkiliServis;

                    if (bayi_tipi == "0")
                        bayitip = tr.com.kale.digitalkatalogws.EnumBayiTipi.Hicbiri;

                    ProductService.Authenticate();

                    switch (action)
                    {
                        case "reload":
                            ProductService.ReloadCache();
                            break;
                        case "uruntitle":
                            result = ProductService.ws.GetirUrunTitle(Convert.ToInt32(urun_id), dil);
                            break;
                        case "serititle":
                            result = ProductService.ws.GetirSeriTitle(Convert.ToInt32(seri_id), dil);
                            break;
                        case "yeniseriler":
                            result = ProductService.ws.GetirYeniSeriler(Convert.ToInt32(marka_id), dil);
                            break;
                        case "ulkeler":
                            result = ProductService.ws.GetirUlkeler(dil);
                            break;
                        case "sehirler":
                            result = ProductService.ws.GetirSehirler(Convert.ToInt32(ulke_id), dil, bayitip);
                            break;
                        case "ilceler":
                            result = ProductService.ws.GetirIlceler(Convert.ToInt32(sehir_id), dil, bayitip);
                            break;
                        case "markalar":
                            result = ProductService.ws.GetirMarkalar(dil);
                            break;
                        case "referansuruntipleri":
                            result = ProductService.ws.GetirUrunTipleri(dil, 0, Convert.ToInt32(marka_id), 9999, 2, Convert.ToInt32(seri_id));
                            break;
                        case "referansturleri":
                            result = ProductService.ws.GetirReferansTurleri(Convert.ToInt32(marka_id), dil);
                            break;
                        case "serifiltreleri":
                            if (!ProductService.seriFiltreleri.ContainsKey(dil))
                                ProductService.seriFiltreleri = ProductService.GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip.Seri, dil);

                            result = ProductService.ws.GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip.Seri, dil);
                            break;
                        case "urunfiltreleri":
                            if (!ProductService.urunFiltreleri.ContainsKey(dil))
                                ProductService.urunFiltreleri = ProductService.GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip.Urun, dil);

                            result = ProductService.ws.GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip.Urun, dil);
                            break;
                        case "referanslar":
                            result = JsonConvert.SerializeObject(ProductService.Referanslar(Convert.ToInt32(referans_turu_id), Convert.ToInt32(producttype), Convert.ToInt32(bolge_id), dil, Convert.ToInt32(pageindex) - 1, Convert.ToInt32(pagesize)));
                            break;
                        case "ebatlar":
                            result = ProductService.ws.GetirEbatlarUrunTipineGore(Convert.ToInt32(producttype));
                            break;
                        case "uruntipleri":
                            result = ProductService.ws.GetirUrunTipleri(dil, Convert.ToInt32(producttype), Convert.ToInt32(marka_id), kayit_sayisi == "" ? 10 : Convert.ToInt32(kayit_sayisi), 1, Convert.ToInt32(seri_id));
                            break;
                        case "seriyegoreilgiliurunler":     // Hep boş dönüyor. 
                            result = ProductService.ws.GetirIlgiliUrunlerSeriyeGore(Convert.ToInt32(seri_id), kayit_sayisi == "" ? 10 : Convert.ToInt32(kayit_sayisi), dil);
                            break;
                        case "urunegoreilgiliurunler":   // urun_id parametresini bilmediğim için istek yapamadım.
                            result = ProductService.ws.GetirIlgiliUrunlerUruneGore(Convert.ToInt32(urun_id), kayit_sayisi == "" ? 10 : Convert.ToInt32(kayit_sayisi), dil);
                            break;
                        case "yetkiliservisler":
                            result = JsonConvert.SerializeObject(ProductService.YetkiliServisler(Convert.ToInt32(sehir_id), Convert.ToInt32(ilce_id), urun_tipi_enum, Convert.ToInt32(pageindex) - 1, Convert.ToInt32(pagesize)));
                            break;
                        case "satisnoktalari":
                            result = JsonConvert.SerializeObject(ProductService.SatisNoktalari(urun_tipi_enum, Convert.ToInt32(ulke_id), Convert.ToInt32(sehir_id), Convert.ToInt32(ilce_id), dil, Convert.ToInt32(pageindex) - 1, Convert.ToInt32(pagesize)));
                            break;
                        case "cinsler":
                            result = ProductService.ws.GetirCinsler(Convert.ToInt32(seri_id), dil);
                            break;
                        case "seriurunleri":
                            result = ProductService.ws.GetirSeriUrunleri(Convert.ToInt32(seri_id), Convert.ToInt32(cins_id), kayit_sayisi == "" ? 10 : Convert.ToInt32(kayit_sayisi), dil);
                            break;
                        case "urunler":
                            if (urun_ids.EndsWith(","))
                                urun_ids = urun_ids.Substring(0, urun_ids.Length - 1);
                            int[] idsArray = new int[0];
                            if (urun_ids != "")
                                idsArray = urun_ids.Split(',').Select(n => int.Parse(n)).ToList().ToArray();

                            if (idsArray.Length <= 20)
                                result = ProductService.ws.GetirUrunler(idsArray, dil);
                            break;
                        case "etkinlikler":
                            result = ProductService.ws.GetirEtkinlikler(dil);
                            break;
                        case "ilgiliurunler":
                            result = ProductService.ws.GetirIlgiliUrunlerUruneGore(Convert.ToInt32(urun_id), kayit_sayisi == "" ? 10 : Convert.ToInt32(kayit_sayisi), dil);
                            break;
                        case "kampanyalar":
                            result = ProductService.ws.GetirKampanyalar(dil);
                            break;
                        case "renkler":
                            result = ProductService.ws.GetirRenklerUrunTipineGore(Convert.ToInt32(producttype).ToString(), dil);
                            break;
                        case "seridetay":
                            result = ProductService.ws.GetirSeriDetay(Convert.ToInt32(seri_id), dil);
                            break;
                        case "sericinsleri":
                            result = ProductService.ws.GetirCinsler(Convert.ToInt32(seri_id), dil);
                            break;
                        case "seridekiurunler":
                            result = ProductService.ws.GetirSeriUrunleri(Convert.ToInt32(seri_id), Convert.ToInt32(cins_id), 99999, dil);
                            break;
                        case "seriilgiliurunleri":
                            result = ProductService.ws.GetirIlgiliUrunlerSeriyeGore(Convert.ToInt32(seri_id), 99999, dil);
                            break;
                        case "urundetay":
                            result = ProductService.ws.GetirUrunDetay(Convert.ToInt32(urun_id), dil);
                            break;
                        case "kampanyadetay":
                            result = ProductService.ws.GetirKampanyaDetay(Convert.ToInt32(kampanya_id), dil);
                            break;
                        case "urunilgiliurunleri":
                            result = ProductService.ws.GetirIlgiliUrunlerUruneGore(Convert.ToInt32(urun_id), 99999, dil);
                            break;
                        case "language":
                            result = KaleCommon.Models.Language.CurrentLanguage.ToString();
                            break;
                        case "language_item":
                            result = KaleCommon.Models.Language.CurrentLanguage[dil].SelectLabel(context.Request["key"]).ToString();
                            break;
                        case "language_xml":
                            context.Response.ContentType = "text/xml";
                            result = KaleCommon.Models.Language.CurrentLanguage[dil].Xml.OuterXml;
                            break;
                        case "seriuruntipleri":
                            result = ProductService.ws.GetirUrunTipleri(dil, 0, Convert.ToInt32(marka_id), Convert.ToInt32(kayit_sayisi), 1, Convert.ToInt32(seri_id));
                            break;
                        case "seriarama":
                        case "searchserial":
                            if ((!ProductService.seriFiltreleri.ContainsKey(dil)) || (Convert.ToInt32(reload_cache) == 1))
                                ProductService.seriFiltreleri = ProductService.GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip.Seri, dil);
                            result = JsonConvert.SerializeObject(ProductService.SeriArama(dil, keyword, Convert.ToInt32(producttype), Convert.ToInt32(reload_cache), Convert.ToInt32(pageindex) - 1, Convert.ToInt32(pagesize)));
                            break;
                        case "urunarama":
                        case "searchproduct":
                            if ((!ProductService.urunFiltreleri.ContainsKey(dil)) || (Convert.ToInt32(reload_cache) == 1))
                                ProductService.urunFiltreleri = ProductService.GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip.Urun, dil);

                            result = JsonConvert.SerializeObject(ProductService.UrunArama(dil, keyword, Convert.ToInt32(seri_id), Convert.ToInt32(subproducttype), Convert.ToInt32(reload_cache), Convert.ToInt32(pageindex) - 1, Convert.ToInt32(pagesize)));
                            break;
                        case "zonearticlesfiles":
                            if (Convert.ToInt32(zone_id) > 0)
                            {
                                result = JsonConvert.SerializeObject(GetZoneArticleFiles(Convert.ToInt32(zone_id), dil));
                            }
                            else
                            {
                                result = JsonConvert.SerializeObject(Response<string>.CreateFailResponse(300, "'zone_id' required"));
                            }
                            break;

                        //iletisim
                        case "getcountries":
                            result = GetCountries();
                            break;
                        case "getcities":
                            result = GetCities(country_id);
                            break;
                        case "gettowns":
                            result = GetTowns(city_id);
                            break;
                            //case "getarticles":
                            //    result = GetArticles(context);
                            //    break;
                            //case "getredirections":
                            //    result = GetRedirections(context);
                            //    break;
                    }
                }
                else
                {
                    result = jsSerializer.Serialize(Response<string>.CreateFailResponse(300, "'action' required"));
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "", false);
                result = JsonConvert.SerializeObject(Response<string>.CreateFailResponse(500, ex.ToString()));
            }

            context.Response.Write(result);
        }

        //public string GetArticles(HttpContext context)
        //{
        //    string password = "jf83(GE{fn";
        //    List<int> results = new List<int>();

        //    if (context.Request.Form["ExecPassword"] != null && context.Request.Form["ExecPassword"].ToString() == password)
        //    {
        //        try
        //        {
        //            using (KaleDbContext db = new KaleDbContext())
        //            {

        //                var articles = from x in db.Articles
        //                               where x.Status == 1
        //                               orderby x.Id descending
        //                               select new { x.Headline, x.Summary, x.CustomBody, x.Article1, x.Article2, x.Article3, x.Article4, x.Article5, x.CanonicalUrl };

        //                return jsSerializer.Serialize(new { status = true, data = articles });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return jsSerializer.Serialize(new { status = false, message = ex.ToString() });
        //        }
        //    }
        //    else
        //        return jsSerializer.Serialize(new { status = false, message = "Password is null" });
        //}

        public string GetRedirections(HttpContext context)
        {
            string password = "jf83(GE{fn";
            List<int> results = new List<int>();

            if (context.Request.Form["ExecPassword"] != null && context.Request.Form["ExecPassword"].ToString() == password)
            {
                try
                {
                    using (KaleDbContext db = new KaleDbContext())
                    {
                        //var list = db.PageRedirections.Where(x => x.ID > 6690).ToList();
                        //db.PageRedirections.RemoveRange(list);
                        //db.SaveChanges();

                        var redirections = db.PageRedirections.OrderByDescending(x => x.ID).ToList();
                        return jsSerializer.Serialize(new { status = true, data = redirections });
                    }
                }
                catch (Exception ex)
                {
                    return jsSerializer.Serialize(new { status = false, message = ex.ToString() });
                }
            }
            else
                return jsSerializer.Serialize(new { status = false, message = "Password is null" });
        }

        public InsertPageRedirectionResponse InsertPageRedirections(HttpContext context)
        {
            string password = "jf83(GE{fn";

            try
            {
                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;

                System.IO.Stream body = context.Request.InputStream;
                System.Text.Encoding encoding = context.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

                string querytring = reader.ReadToEnd();

                var redirectionReq = JsonConvert.DeserializeObject<RedirectionJsonReq>(querytring);

                if (redirectionReq.password == password)
                {
                    using (KaleDbContext db = new KaleDbContext())
                    {
                        var userId = db.vAspNetMembershipUsers.FirstOrDefault().UserId;

                        foreach (var item in redirectionReq.redirections)
                        {
                            PageRedirection red = new PageRedirection();
                            red.RedirectFrom = item.fromUrl;
                            red.RedirectTo = item.toUrl;
                            red.RedirectType = item.type;
                            red.UpdateDate = DateTime.Now;
                            red.CreateDate = DateTime.Now;
                            red.CreatedBy = userId;
                            red.UpdatedBy = userId;

                            db.PageRedirections.Add(red);
                        }

                        db.SaveChanges();
                        return new InsertPageRedirectionResponse() { isProcess = true, isError = false, message = "" };
                    }
                }
                else
                {
                    return new InsertPageRedirectionResponse() { isProcess = true, isError = true, message = "Password invalid" };
                }
            }
            catch (Exception ex)
            {
                return new InsertPageRedirectionResponse() { isProcess = false, isError = true, message = ex.ToString() };
            }

        }



        #region iletisim

        public string GetCountries()
        {
            try
            {
                var jsonSerialiser = new JavaScriptSerializer();
                var json = "";
                List<Country> country = new List<Country>();
                KaleDbContext dbContext = new KaleDbContext();
                country = dbContext.Countries.ToList();
                json = jsSerializer.Serialize(country);

                if (string.IsNullOrEmpty(json))
                {
                    return jsSerializer.Serialize(new { status = false, message = "Json null" });
                }

                //return jsSerializer.Serialize(new { status = true, message = "İşlem başarılı", data = json });
                return json;
            }
            catch (Exception ex)
            {
                //return jsSerializer.Serialize(new { status = false, message = "Json null", ex.InnerException });
                throw ex;
            }

        }


        public string GetCities(string country_id)
        {
            if (string.IsNullOrEmpty(country_id))
            {
                return jsSerializer.Serialize(new { status = false, message = " This 'country_id' null or empty!" });
            }
            try
            {
                var jsonSerialiser = new JavaScriptSerializer();
                var json = "";
                List<City> cities = new List<City>();
                KaleDbContext dbContext = new KaleDbContext();
                cities = dbContext.Cities.Where(i => i.country_id == country_id).ToList();
                json = jsSerializer.Serialize(cities);

                if (string.IsNullOrEmpty(json))
                {
                    return jsSerializer.Serialize(new { status = false, message = "Json null" });
                }

                //return jsSerializer.Serialize(new { status = true, message = "İşlem başarılı", data = json });
                return json;
            }
            catch (Exception ex)
            {
                //return jsSerializer.Serialize(new { status = false, message = "Json null", ex.InnerException });
                throw ex;
            }

        }

        public string GetTowns(string city_id)
        {
            if (string.IsNullOrEmpty(city_id))
            {
                return jsSerializer.Serialize(new { status = false, message = " This 'city_id' null or empty!" });
            }
            try
            {
                var jsonSerialiser = new JavaScriptSerializer();
                var json = "";
                List<Town> towns = new List<Town>();
                KaleDbContext dbContext = new KaleDbContext();
                towns = dbContext.Towns.Where(i => i.city_id == city_id).ToList();
                json = jsSerializer.Serialize(towns);

                if (string.IsNullOrEmpty(json))
                {
                    return jsSerializer.Serialize(new { status = false, message = "Json null" });
                }

                //return jsSerializer.Serialize(new { status = true, message = "İşlem başarılı", data = json });
                return json;
            }
            catch (Exception ex)
            {
                //return jsSerializer.Serialize(new { status = false, message = "Json null", ex.InnerException });
                throw ex;
            }

        }

        #endregion


        private List<VArticlesZonesFullWithFiles> GetZoneArticleFiles(int zone_id, string dil)
        {
            var files = new List<ArticleFile>();
            var articles = new List<vArticlesZonesFull>();

            using (CmsDbContext dbContext = new CmsDbContext())
            {
                files = dbContext.Files.ToList();
                articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zone_id && w.Status == 1).ToList();
            }

            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(dil).DateTimeFormat;

            List<VArticlesZonesFullWithFiles> articlesWithFiles = new List<VArticlesZonesFullWithFiles>();

            foreach (var article in articles)
            {
                VArticlesZonesFullWithFiles articleWithFiles = JsonConvert.DeserializeObject<VArticlesZonesFullWithFiles>(JsonConvert.SerializeObject(article));
                articleWithFiles.files = files.Where(f => f.ArticleId == article.ArticleID).ToList();

                articlesWithFiles.Add(articleWithFiles);
            }

            return articlesWithFiles;
        }

    }

    class VArticlesZonesFullWithFiles : vArticlesZonesFull
    {
        public List<ArticleFile> files { get; set; }

    }
}