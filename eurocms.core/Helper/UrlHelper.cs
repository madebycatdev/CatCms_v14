using System.Configuration;
using System.Linq;
using System.Web;

namespace EuroCMS.Helper
{
    public class UrlHelper
    {
        public static string GetCanonicalUrl(HttpContext context)
        {
            var canonicalUrlPref = ConfigurationManager.AppSettings["CanonicalUrlScheme"];
            string prefix = string.Empty;
            string path = string.Empty;

            var rawUrl = context.Request.Url;
            path = rawUrl.Query.Replace(rawUrl.LocalPath, "").Replace("?404;", "").Replace($":{rawUrl.Port}/", "/");

            path = path.ToLower();

            if (path.Count(c => c == '/') > 3)
            {
                path = path.TrimEnd('/');
            }

            if (path.EndsWith("/index"))
            {
                path = path.Substring(0, path.Length - 6);
            }

            if (string.IsNullOrEmpty(path))
            {
                path = rawUrl.Scheme + "://" + rawUrl.Authority;
            }

            if (!string.IsNullOrEmpty(canonicalUrlPref) && canonicalUrlPref != "auto")
            {
                prefix = canonicalUrlPref;
                if (rawUrl.Scheme == "http")
                    path = path.Replace("http://", prefix + "://");
                else
                    path = path.Replace("https://", prefix + "://");
            }

            return path;
        }

        public static string GetUriScheme()
        {
            var scheme = ConfigurationManager.AppSettings["UrlScheme"];
            if (!string.IsNullOrEmpty(scheme))
            {
                if (scheme == "https")
                {
                    return "https://";
                }
                else if (scheme == "http")
                {
                    return "http://";
                }
            }
            return HttpContext.Current.Request.Url.Scheme + "://";
        }

        public static string UriReplacer(string text)
        {
            var newText = text.Replace("https://", "").Replace("http://", "");
            return GetUriScheme() + newText;
        }

        public static bool IsSitemapOnlyURL()
        {
            if (ConfigurationManager.AppSettings["SitemapOnlyURL"] == null)
                return false;

            bool.TryParse(ConfigurationManager.AppSettings["SitemapOnlyURL"], out bool onlyUrl);
            return onlyUrl;
        }
    }
}