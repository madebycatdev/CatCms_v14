using System.Collections.Generic;
using System.Web.Configuration;

namespace EuroCMS.Captcha
{
    public class GoogleReCaptchaV2
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

            var reCaptcha = serializer.Deserialize<GoogleReCaptchaV2>(googleReply);

            return reCaptcha.Success;
        }
    }
}
