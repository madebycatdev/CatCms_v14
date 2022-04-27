using Newtonsoft.Json;
using RestSharp;
using System;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string result = "", plugin = "";

            if (!string.IsNullOrEmpty(context.Request.Form["plugin"]))
            {
                plugin = context.Request.Form["plugin"].ToLower().Trim();
            }
            else
            {
                result = JsonConvert.SerializeObject(new { status = false, message = "plugin alanı boş gönderilemez", data = "" });
            }

            switch (plugin)
            {
                case "cozumortagi":
                    result = GetCozumOrtagi();
                    break;
                case "faaliyetkonusu":
                    result = GetFaaliyetKonusu();
                    break;
                case "il":
                    result = GetIl();
                    break;
                case "ilce":
                    result = GetIlce();
                    break;
                case "markalar":
                    result = GetMarkalar();
                    break;
                case "doktorbranslistesi":
                    result = GetSaglikDoktorBransListesi();
                    break;
                case "saglikkurumlistesi":
                    result = GetSaglikKurumListesi();
                    break;
                case "sagliknetworkgrubulistesi":
                    result = GetSaglikNetworkGrubuListesi();
                    break;
                case "saglikkurumtipilistesi":
                    result = GetSaglikKurumTipiListesi();
                    break;
            }
            context.Response.Write(result);
        }

        private string GetToken()
        {
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaTokenServiceEndpoint"];
            var username = WebConfigurationManager.AppSettings["GunesSigortaServiceUsername"];
            var password = WebConfigurationManager.AppSettings["GunesSigortaServicePassword"];
            var clientId = WebConfigurationManager.AppSettings["GunesSigortaServiceClientId"];
            var clientSecret = WebConfigurationManager.AppSettings["GunesSigortaServiceClientSecret"];

            var tokenRequest = new TokenRequestModel()
            {
                grant_type = "password",
                username = username,
                password = password,
                scope = "token",
                client_id = clientId,
                client_secret = clientSecret
            };

            var response = Request<TokenResponse>(baseUrl, "token", Method.POST, tokenRequest);
            if (response.Status == "SUCCESS")
            {
                return response.Result.access_token;
            }else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        private ServiceResponseModel<T> Request<T>(string endpoint, string resource, Method method, object body = null, string token = null) where T : class, new()
        {
            try
            {
                RestClient restClient = new RestClient(endpoint);
                RestRequest restRequest = new RestRequest(resource, method);

                if (!string.IsNullOrEmpty(token))
                    restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", token));

                if (body != null)
                {
                    restRequest.RequestFormat = DataFormat.Json;
                    restRequest.AddBody(body);
                }

                var response = restClient.Execute<ServiceResponseModel<T>>(restRequest);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception("Servis isteği başarısız oldu");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetCozumOrtagi()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<CozumOrtagiListesiResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/cozumOrtagiListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetFaaliyetKonusu()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<FaaliyetKonusuResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/faaliyetKonusuListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetIl()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<IlResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/ilListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetIlce()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<IlceResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/ilceListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetMarkalar()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<MarkaListesiResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/markaListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetSaglikDoktorBransListesi()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<SaglikDoktorBransListesiResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/saglikDoktorBransListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetSaglikKurumListesi()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<SaglikKurumListesiResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/saglikKurumListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetSaglikNetworkGrubuListesi()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<SaglikNetworkGrubuListesiResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/saglikKurumsalSiteNetworkGrubuListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }

        public string GetSaglikKurumTipiListesi()
        {
            var token = GetToken();
            var baseUrl = WebConfigurationManager.AppSettings["GunesSigortaRestServiceEndpoint"];
            var response = Request<SaglikKurumTipiListesiResponse>(baseUrl, "gunessigorta/rest/GunesWebRestService/saglikKurumTipiListesi", Method.POST, null, token);
            if (response.Status == "SUCCESS")
            {
                return JsonConvert.SerializeObject(response.Result);
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(response.Messages));
            }
        }        
    }
}