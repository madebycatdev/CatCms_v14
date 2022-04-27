using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Web;

namespace EuroCMS.Plugin.Tsa
{
    public class HttpRequestHelper
    {
        public static HttpClient ApiClient { get; set; }


        public static string Request(string url, string method, dynamic paramList, string schoolId, string seasonId, string apiKey)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient(url);

            var request = new RestRequest(method == "GET" ? Method.GET : Method.POST);
            request.Resource = string.Empty;

            if (!string.IsNullOrEmpty(apiKey))
            {
                var login_token = String.Format("Bearer {0}", apiKey);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", login_token);
                request.AddHeader("schoolId", schoolId);
                request.AddHeader("seasonId", seasonId);

            }


            if (method == "POST")
            {
                var body = JsonConvert.SerializeObject(paramList);
                request.AddJsonBody(body);
            }

            try
            {
                IRestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }



        }


        public static string MatrixToString(IDictionary<string, string> keyValueContent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('?');
            foreach (KeyValuePair<string, string> kvp in keyValueContent)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (sb.Length > 0) sb.Append('&');
                    sb.Append(HttpUtility.UrlEncode(kvp.Key));
                    sb.Append('=');
                    sb.Append(HttpUtility.UrlEncode(kvp.Value));
                }
            }
            return sb.ToString();
        }
    }
}