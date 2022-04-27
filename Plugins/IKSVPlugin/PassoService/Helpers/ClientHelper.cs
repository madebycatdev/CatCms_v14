using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace PassoService.Helpers
{
    public class ClientHelper
    {
        public static string ClientSecret = "8cbrk95ZiDzs5F89VsRKt5Lv9K";
        public static string ClientId = "RKdega2KL6noP.com.aktifbank.Iksv";
        public static async System.Threading.Tasks.Task<string> RequestAsync(string accessToken, string method, string credentialString)
        {
            HttpClient client = new HttpClient();


            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["PassoApiUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                               | SecurityProtocolType.Tls11
                               | SecurityProtocolType.Tls12
                               | SecurityProtocolType.Ssl3;

            if (!string.IsNullOrEmpty(accessToken))
            {
                var login_token = String.Format("Bearer {0}", accessToken);
                client.DefaultRequestHeaders.Add("Authorization", login_token);
            }

            var stringContent = new StringContent(credentialString.ToString(), Encoding.UTF8, "application/json");

            using (var httpResonse = await client.PostAsync(method, stringContent).ConfigureAwait(false))
            {
                return await httpResonse.Content.ReadAsStringAsync();
            }


        }

      
    }
}