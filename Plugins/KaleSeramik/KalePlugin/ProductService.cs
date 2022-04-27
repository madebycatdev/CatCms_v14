using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using EuroCMS.Plugin.Kale.KaleCommon.Models;
using Newtonsoft.Json;

namespace EuroCMS.Plugin.Kale
{
    public static class ProductService
    {

        public const string DEFAULT_LANGUAGE = "TR";

        public static tr.com.kale.digitalkatalogws.KSWebContent ws = new tr.com.kale.digitalkatalogws.KSWebContent();

        private static tr.com.kale.digitalkatalogws.KDAuthenticationHeader authHeader = new tr.com.kale.digitalkatalogws.KDAuthenticationHeader();

        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public static Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>> seriFiltreleri = new Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>>();
        public static Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>> urunFiltreleri = new Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>>();


        private static object _wsUsername = ConfigurationManager.AppSettings["ws.username"];
        private static string WSUsername
        {
            get
            {
                if (_wsUsername != null)
                    return _wsUsername.ToString();
                return string.Empty;
            }
        }

        private static object _wsPassword = ConfigurationManager.AppSettings["ws.password"];
        private static string WSPassword
        {
            get
            {
                if (_wsPassword != null)
                    return _wsPassword.ToString();
                return string.Empty;
            }
        }

        private static object _wsEnvironment = ConfigurationManager.AppSettings["ws.environment"];
        private static string WSEnvironment
        {
            get
            {
                if (_wsEnvironment != null)
                    return _wsEnvironment.ToString();
                return string.Empty;
            }
        }

        public static void Authenticate()
        {
            ws.KDAuthenticationHeaderValue = authHeader;
            authHeader.UserName = WSUsername;
            authHeader.Password = WSPassword;
            authHeader.Environment = WSEnvironment;
        }

        public static void ReloadCache()
        {
            Language._LanguageItems = null;
            HttpContext.Current.Response.Write("reloaded!");
        }

        public static tr.com.kale.digitalkatalogws.BayiTuru BuildBayiTuru(string enume)
        {
            tr.com.kale.digitalkatalogws.BayiTuru tur = new tr.com.kale.digitalkatalogws.BayiTuru();
            tur.Tur = Convert.ToInt32(enume);
            return tur;
        }

        public static Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>> GetirFiltreler(tr.com.kale.digitalkatalogws.EnumTip tip, string dil)
        {
            string result = ws.GetirFiltreler(tip, dil);
            List<tr.com.kale.digitalkatalogws.Filtre> fList = null;
            Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>> retval = new Dictionary<string, List<tr.com.kale.digitalkatalogws.Filtre>>();
            if (!string.IsNullOrEmpty(result))
                fList = serializer.Deserialize<List<tr.com.kale.digitalkatalogws.Filtre>>(result);
            retval.Add(dil, fList);
            return retval;
        }

        #region Build Filters
        public static tr.com.kale.digitalkatalogws.Filtre[] BuildFilters(List<tr.com.kale.digitalkatalogws.Filtre> incomingFilters)
        {
            List<tr.com.kale.digitalkatalogws.Filtre> filters = new List<tr.com.kale.digitalkatalogws.Filtre>();
            if (incomingFilters != null)
            {
                // add to filters from Form Post
                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var value = HttpContext.Current.Request[key];
                        var result = incomingFilters.Where(item => item.FiltreKey.ToLower() == key.ToLower()).Take(1);
                        if (result.Count() > 0)
                        {
                            string baslik = String.Join(" ", result.Select(w => w.FiltreBaslik));

                            //Response.Write(string.Format("<b>key:</b> {0}, <b>value:</b> {1} <br />", key, value));
                            filters.Add(new tr.com.kale.digitalkatalogws.Filtre { FiltreBaslik = baslik, FiltreKey = key, FiltreDegeri = value });
                        }
                    }
                }

                // add to filters from Querystring
                foreach (string key in HttpContext.Current.Request.QueryString.AllKeys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var value = HttpContext.Current.Request[key];
                        var result = incomingFilters.Where(item => item.FiltreKey.ToLower() == key.ToLower()).Take(1);
                        if (result.Count() > 0)
                        {
                            string baslik = String.Join(" ", result.Select(w => w.FiltreBaslik));

                            //Response.Write(string.Format("<b>key:</b> {0}, <b>value:</b> {1} <br />", key, value));
                            filters.Add(new tr.com.kale.digitalkatalogws.Filtre { FiltreBaslik = baslik, FiltreKey = key, FiltreDegeri = value });
                        }
                    }
                }
            }
            return filters.ToArray();
        }
        #endregion

        public static Response<SeriAramaSonuc> SeriArama(string dil, string keyword, int urunTipiID, int reloadcache, int currentPage, int pageSize)
        {
            try
            {
                tr.com.kale.digitalkatalogws.Filtre[] filters = ProductService.BuildFilters(ProductService.seriFiltreleri[dil]);

                SeriAramaSonuc seriAramaSonuc = ProductService.SeriArama(filters, keyword, urunTipiID, dil);

                if (seriAramaSonuc != null)
                {
                    seriAramaSonuc.Pagination = new Pagination()
                    {
                        CurrentPageIndex = currentPage + 1,
                        ItemsPerPage = pageSize,
                        TotalItemCount = seriAramaSonuc.SeriList.Count(),
                        TotalPageCount = (int)Math.Ceiling(seriAramaSonuc.SeriList.Count() / (1.0 * pageSize))
                    };

                    #region get range of seri list according to paging
                    int paginationStartIndex = pageSize * currentPage;
                    int paginationEndIndex = paginationStartIndex + pageSize - 1;

                    if (paginationStartIndex < 0 || seriAramaSonuc.SeriList.Count - 1 < paginationStartIndex || paginationEndIndex < 0)
                    {
                        seriAramaSonuc.SeriList = new List<Seri>();
                    }
                    else
                    {
                        if (paginationEndIndex > seriAramaSonuc.SeriList.Count - 1)
                        {
                            paginationEndIndex = seriAramaSonuc.SeriList.Count - 1;
                        }

                        seriAramaSonuc.SeriList = seriAramaSonuc.SeriList.GetRange(paginationStartIndex, paginationEndIndex - paginationStartIndex + 1);
                    }
                    #endregion
                }

                return Response<SeriAramaSonuc>.CreateSuccessResponse(seriAramaSonuc);

            }
            catch (Exception ex)
            {
                return Response<SeriAramaSonuc>.CreateInternalErrorResponse(ex.ToString());
            }
        }


        private static SeriAramaSonuc SeriArama(tr.com.kale.digitalkatalogws.Filtre[] fts, string keyword, int urunTipiID, string dil)
        {
            string result = ws.GetirSeriAramaSonuc(fts, keyword, urunTipiID, dil);
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<SeriAramaSonuc>(result) : null;
        }


        public static Response<UrunAramaSonuc> UrunArama(string dil, string keyword, int seriID, int subproducttype, int reloadcache, int currentPage, int pageSize)
        {
            try
            {
                tr.com.kale.digitalkatalogws.Filtre[] filters = ProductService.BuildFilters(ProductService.urunFiltreleri[dil]);

                UrunAramaSonuc urunAramaSonuc = UrunArama(filters, keyword, seriID, subproducttype, dil);

                if (urunAramaSonuc != null)
                {
                    urunAramaSonuc.Pagination = new Pagination()
                    {
                        CurrentPageIndex = currentPage + 1,
                        ItemsPerPage = pageSize,
                        TotalItemCount = urunAramaSonuc.UrunList.Count(),
                        TotalPageCount = (int)Math.Ceiling(urunAramaSonuc.UrunList.Count() / (1.0 * pageSize))
                    };

                    #region get range of urun list according to paging
                    int paginationStartIndex = pageSize * currentPage;
                    int paginationEndIndex = paginationStartIndex + pageSize - 1;

                    if (paginationStartIndex < 0 || urunAramaSonuc.UrunList.Count - 1 < paginationStartIndex || paginationEndIndex < 0)
                    {
                        urunAramaSonuc.UrunList = new List<Urun>();
                    }
                    else
                    {
                        if (paginationEndIndex > urunAramaSonuc.UrunList.Count - 1)
                        {
                            paginationEndIndex = urunAramaSonuc.UrunList.Count - 1;
                        }

                        urunAramaSonuc.UrunList = urunAramaSonuc.UrunList.GetRange(paginationStartIndex, paginationEndIndex - paginationStartIndex + 1);
                    }
                    #endregion
                }

                return Response<UrunAramaSonuc>.CreateSuccessResponse(urunAramaSonuc);

            }
            catch (Exception ex)
            {
                return Response<UrunAramaSonuc>.CreateInternalErrorResponse(ex.ToString());
            }
        }

        private static UrunAramaSonuc UrunArama(tr.com.kale.digitalkatalogws.Filtre[] fts, string keyword, int seriID, int subproducttype, string dil)
        {
            string result = ws.GetirUrunAramaSonuc(fts, keyword, seriID, subproducttype, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<UrunAramaSonuc>(result) : null;
        }

        public static Response<ReferansSonuc> Referanslar(int referans_turu_id, int producttype, int bolge_id, string dil, int currentPage, int pageSize, int marka_id = 0)
        {
            try
            {
                List<Referans> referanslar = Referanslar(referans_turu_id, producttype, bolge_id, dil, marka_id);

                Pagination page = new Pagination()
                {
                    CurrentPageIndex = currentPage + 1,
                    ItemsPerPage = pageSize,
                    TotalItemCount = referanslar.Count(),
                    TotalPageCount = (int)Math.Ceiling(referanslar.Count() / (1.0 * pageSize))
                };


                int paginationStartIndex = pageSize * currentPage;
                int paginationEndIndex = paginationStartIndex + pageSize - 1;

                if (paginationStartIndex < 0 || referanslar.Count - 1 < paginationStartIndex || paginationEndIndex < 0)
                {
                    referanslar = new List<Referans>();
                }
                else
                {
                    if (paginationEndIndex > referanslar.Count - 1)
                    {
                        paginationEndIndex = referanslar.Count - 1;
                    }

                    referanslar = referanslar.GetRange(paginationStartIndex, paginationEndIndex - paginationStartIndex + 1);
                }
                var referanslarSonuc = new ReferansSonuc();
                referanslarSonuc.ReferansList = referanslar;
                referanslarSonuc.Pagination = page;

                return Response<ReferansSonuc>.CreateSuccessResponse(referanslarSonuc);
            }
            catch (Exception ex)
            {
                return Response<ReferansSonuc>.CreateInternalErrorResponse(ex.ToString());
            }
        }

        private static List<Referans> Referanslar(int referans_turu_id, int producttype, int bolge_id, string dil, int marka_id = 0)
        {
            string result = ws.GetirReferanslar(referans_turu_id, producttype, marka_id, bolge_id, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<List<Referans>>(result) : null;
        }

        public static Response<YetkiliServisSonuc> YetkiliServisler(int sehir_id, int ilce_id, string urun_tipi_enum, int currentPage, int pageSize)
        {
            try
            {
                List<YetkiliServis> yetkiliServisler = YetkiliServisler(sehir_id, ilce_id, urun_tipi_enum);

                Pagination page = new Pagination()
                {
                    CurrentPageIndex = currentPage + 1,
                    ItemsPerPage = pageSize,
                    TotalItemCount = yetkiliServisler.Count(),
                    TotalPageCount = (int)Math.Ceiling(yetkiliServisler.Count() / (1.0 * pageSize))
                };

                int paginationStartIndex = pageSize * currentPage;
                int paginationEndIndex = paginationStartIndex + pageSize - 1;

                if (paginationStartIndex < 0 || yetkiliServisler.Count - 1 < paginationStartIndex || paginationEndIndex < 0)
                {
                    yetkiliServisler = new List<YetkiliServis>();
                }
                else
                {
                    if (paginationEndIndex > yetkiliServisler.Count - 1)
                    {
                        paginationEndIndex = yetkiliServisler.Count - 1;
                    }

                    yetkiliServisler = yetkiliServisler.GetRange(paginationStartIndex, paginationEndIndex - paginationStartIndex + 1);
                }
                var yetkiliServisSonuc = new YetkiliServisSonuc();
                yetkiliServisSonuc.ServisList = yetkiliServisler;
                yetkiliServisSonuc.Pagination = page;

                return Response<YetkiliServisSonuc>.CreateSuccessResponse(yetkiliServisSonuc);
            }
            catch (Exception ex)
            {
                return Response<YetkiliServisSonuc>.CreateInternalErrorResponse(ex.ToString());
            }
        }


        private static List<YetkiliServis> YetkiliServisler(int sehir_id, int ilce_id , string urun_tipi_enum)
        {
            string result = ws.GetirYetkiliServisler(sehir_id,ilce_id, ProductService.BuildBayiTuru(urun_tipi_enum));
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<List<YetkiliServis>>(result) : null;
        }


        public static Response<YetkiliServisSonuc> SatisNoktalari(string urun_tipi_enum, int ulke_id, int sehir_id, int ilce_id, string dil, int currentPage, int pageSize)
        {
            try
            {
                List<YetkiliServis> satisNoktalari = SatisNoktalari(urun_tipi_enum, ulke_id, sehir_id, ilce_id, dil);

                Pagination page = new Pagination()
                {
                    CurrentPageIndex = currentPage + 1,
                    ItemsPerPage = pageSize,
                    TotalItemCount = satisNoktalari.Count(),
                    TotalPageCount = (int)Math.Ceiling(satisNoktalari.Count() / (1.0 * pageSize))
                };

               
                int paginationStartIndex = pageSize * currentPage;
                int paginationEndIndex = paginationStartIndex + pageSize - 1;

                if (paginationStartIndex < 0 || satisNoktalari.Count - 1 < paginationStartIndex || paginationEndIndex < 0)
                {
                    satisNoktalari = new List<YetkiliServis>();
                }
                else
                {
                    if (paginationEndIndex > satisNoktalari.Count - 1)
                    {
                        paginationEndIndex = satisNoktalari.Count - 1;
                    }

                    satisNoktalari = satisNoktalari.GetRange(paginationStartIndex, paginationEndIndex - paginationStartIndex + 1);
                }
                var satisNoktalarisSonuc = new YetkiliServisSonuc();
                satisNoktalarisSonuc.ServisList = satisNoktalari;
                satisNoktalarisSonuc.Pagination = page;
                return Response<YetkiliServisSonuc>.CreateSuccessResponse(satisNoktalarisSonuc);
            }
            catch (Exception ex)
            {
                return Response<YetkiliServisSonuc>.CreateInternalErrorResponse(ex.ToString());
            }
        }

        private static List<YetkiliServis> SatisNoktalari(string urun_tipi_enum,int ulke_id,int sehir_id, int ilce_id,string dil)
        {
            string result = ws.GetirSatisNoktalari(ProductService.BuildBayiTuru(urun_tipi_enum), ulke_id, sehir_id,ilce_id, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<List<YetkiliServis>>(result) : null;
        }

    }
}