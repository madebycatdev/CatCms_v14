using EuroCMS.Plugin.Kale.KaleCommon.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EuroCMS.Plugin.KaleUserControls
{
    public static class ProductService
    {

        public const string DEFAULT_LANGUAGE = "TR";

        public static tr.com.kale.digitalkatalogws.KSWebContent ws = new tr.com.kale.digitalkatalogws.KSWebContent();

        private static tr.com.kale.digitalkatalogws.KDAuthenticationHeader authHeader = new tr.com.kale.digitalkatalogws.KDAuthenticationHeader();

        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

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

        private static object _wsImageUrl = ConfigurationManager.AppSettings["ws.imageurl"];

        public static string WSImageUrl
        {
            get
            {
                if (_wsImageUrl != null)
                    return _wsImageUrl.ToString();
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

        
        public static Urun GetUrunDetay(string urun_seo_url, string dil)
        {
            string result = ws.GetirSeoUrunDetay(urun_seo_url, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<Urun>(result) : null;
        }

        public static Urun GetUrunDetay(int urunId, string dil)
        {
            string result = ws.GetirUrunDetay(urunId, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<Urun>(result) : null;
        }

        public static Seri GetSeriDetay(string seri_seo_url, string dil)
        {
            string result = ws.GetirSeoSeriDetay(seri_seo_url, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<Seri>(result) : null;
        }

        public static Seri GetSeriDetay(int seriId, string dil)
        {
            string result = ws.GetirSeriDetay(seriId, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<Seri>(result) : null;
        }

        public static List<Seri> GetYeniSeriler(int markaId, string dil)
        {
            string result = ws.GetirYeniSeriler(markaId, dil);
            return !string.IsNullOrEmpty(result) ? serializer.Deserialize<List<Seri>>(result) : null;
        }


        public static string GetValidAlias(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return string.Empty;

            return string.Join("-", filename.Split(Path.GetInvalidFileNameChars()))
                .Replace("ç", "c")
                .Replace("Ç", "c")
                .Replace("ş", "s")
                .Replace("Ş", "s")
                .Replace("İ", "i")
                .Replace("I", "i")
                .Replace("ı", "i")
                .Replace("Ü", "u")
                .Replace("ü", "u")
                .Replace("Ö", "o")
                .Replace("ö", "o")
                .Replace("Ğ", "g")
                .Replace("ğ", "g")
                .Replace(" ", "-").ToLower();
        }


    }
}
