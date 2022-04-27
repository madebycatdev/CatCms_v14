using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PassoService.Helpers
{
    public class RecaptchaV2Helper
    {

        public bool Success { get; set; }
        public List<string> ErrorCodes { get; set; }

        public static bool Validate(string encodedResponse)
        {
            if (string.IsNullOrEmpty(encodedResponse)) return false;

            var client = new System.Net.WebClient();
            var secret = WebConfigurationManager.AppSettings["GoogleReCaptchaV2SecretKey"];

            if (string.IsNullOrEmpty(secret)) return false;

            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var reCaptcha = serializer.Deserialize<RecaptchaV2Helper>(googleReply);

            return reCaptcha.Success;
        }

        public static string Validatestr(string secret, string encodedResponse)
        {
            if (string.IsNullOrEmpty(encodedResponse)) return "boş response";

            var client = new System.Net.WebClient();
            //var secret = WebConfigurationManager.AppSettings["GoogleReCaptchaV2SecretKey"];

            if (string.IsNullOrEmpty(secret)) return "boş secret";

            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

            return googleReply;
        }

    }
}