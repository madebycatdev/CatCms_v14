using EuroCMS.Core;
using EuroCMS.Plugin.Kale.KaleCommon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.Kale
{
    public class SalesForce : IHttpHandler, IRequiresSessionState
    {
       
        public class AccountObject
        {
            public string Name { get; set; }
            public string Mobile__c { get; set; }
            public string E_mail__c { get; set; }
            public string Billing_Country__c { get; set; }
            public string City__c { get; set; }
            public string Billing_Region__c { get; set; }
            public string RCA_Billing_Street_1__c { get; set; }
            public string RCA_Billing_Street_2__c { get; set; }
            public string AccountSource { get; set; }
            public string TypeKS__c { get; set; }
            //public string RC_KVKK_KS__c { get; set; }
            //public string RC_Marketing_SMS_KS__c { get; set; }
            //public string RC_Marketing_Email_KS__c { get; set; }
            //public string RC_KVKK_Approve_Channel__c { get; set; }
            //public string RC_KVKK_Document_KS__c { get; set; }
        }

        public class CaseObject
        {
            public string Subject { get; set; } // Max 255 Character
            public string Description { get; set; }
            public string Origin { get; set; } //"kale.com.tr";
            public string AccountId { get; set; }
            public string RecordTypeId { get; set; }
            public string Type { get; set; }
            public string OwnerId { get; set; }
            public string Customer_Request__c { get; set; }
        }

        public class KvkkRequest
        {
            public string Musteri__c { get; set; }
            public string Approve_Channel__c { get; set; }
            public string Company__c { get; set; }
            public string KVKK__c { get; set; }
            public string KVKK_Document__c { get; set; }
            public DateTime KVKK_Created_Date__c { get; set; }
            public DateTime KVKK_Last_Modified_Date__c { get; set; }
            public string Marketing_Email__c { get; set; }
            public string Marketing_Email_Document__c { get; set; }
            public DateTime Marketing_Email_Created_Date__c { get; set; }
            public DateTime Marketing_Email_Last_Modified_Date__c { get; set; }
            public string Marketing_SMS__c { get; set; }
            public string Marketing_SMS_Document__c { get; set; }
            public DateTime Marketing_SMS_Created_Date__c { get; set; }
            public DateTime Marketing_SMS_Last_Modified_Date__c { get; set; }
            public string Last_Activity_IP__c { get; set; }
        }

        public class KvkkResponse
        {
            public string id { get; set; }
            public bool success { get; set; }
            public List<object> errors { get; set; }

        }

        public class SalesForceTokenResponse
        {
            public string access_token { get; set; }
            public string instance_url { get; set; }
            public string id { get; set; }
            public string token_type { get; set; }
            public string issued_at { get; set; }
            public string signature { get; set; }
        }

        public class SalesForceInsertResponse
        {
            public string id { get; set; }
            public bool success { get; set; }
            public string[] errors { get; set; }
        }

        public class FormViewModel
        {
            public int CaptchaId { get; set; }
            public string Captcha { get; set; }

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string District { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Job { get; set; }
            public string MessageType { get; set; }
            public string Message { get; set; }
            public bool IsAllowedPrivacy { get; set; }
            public bool IsAllowedCampaign { get; set; }
        }



        public class SendSalesForceResult
        {
            public string Code { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class SalesForceAccountCheckResponse
        {
            [JsonProperty("totalSize")]
            public long TotalSize { get; set; }

            [JsonProperty("done")]
            public bool Done { get; set; }

            [JsonProperty("records")]
            public List<Record> Records { get; set; }

        }

        public partial class Record
        {
            [JsonProperty("attributes")]
            public Attributes Attributes { get; set; }

            [JsonProperty("Id")]
            public string Id { get; set; }

            [JsonProperty("Mobile__c")]
            public string MobileC { get; set; }
        }

        public partial class Attributes
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        string SalesForceServiceURL = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            SalesForceServiceURL = ConfigurationManager.AppSettings["KaleSalesForceURL"];
            string result = "", action = "";
            try
            {
                if (!string.IsNullOrEmpty(context.Request["plugin"]))
                {
                    action = context.Request["plugin"].ToLower().Trim();
                    switch (action)
                    {
                        case "salesforce":
                            result = SendSalesForce(context);
                            break;
                        case "captcha":
                            result = CaptchaPlugin(context);
                            break;
                    }
                }
                else
                {
                    result = jsSerializer.Serialize(Response<string>.CreateFailResponse(300, "'plugin' required"));
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "", false);
                result = JsonConvert.SerializeObject(Response<string>.CreateFailResponse(500, ex.ToString()));
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(result);
        }

        private string GetIpValue(HttpContext context)
        {
            var ipAdd = new WebClient().DownloadString("http://icanhazip.com");
            ipAdd = ipAdd.Replace("\n", String.Empty);

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = context.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ipAdd;
        }

        private string SendSalesForce(HttpContext context)
        {
            string result = string.Empty;
            var formData = GetFormModel(context.Request.Form);
            string token = string.Empty;
            string checkAccountResultStr = string.Empty;
            try
            {
                #region CAPTCHA
                if (string.IsNullOrEmpty(formData.Captcha))
                {
                    return result = JsonConvert.SerializeObject(new SendSalesForceResult { Code = "11", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                }

                if (context.Session["CaptchaImageText" + formData.CaptchaId] == null)
                {
                    return JsonConvert.SerializeObject(new SendSalesForceResult { Code = "12", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                }

                if (context.Session["CaptchaImageText" + formData.CaptchaId].ToString().Trim().ToLower() != formData.Captcha.Trim().ToLower())
                {
                    return JsonConvert.SerializeObject(new SendSalesForceResult { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                }

                context.Session["CaptchaImageText" + formData.CaptchaId] = null;
                context.Session.Remove("CaptchaImageText" + formData.CaptchaId);
                #endregion

                #region TOKEN
                var grantType = ConfigurationManager.AppSettings["KaleSalesForceGrantType"];
                var clientId = ConfigurationManager.AppSettings["KaleSalesForceClientId"];
                var clientSecret = ConfigurationManager.AppSettings["KaleSalesForceClientSecret"];
                var username = ConfigurationManager.AppSettings["KaleSalesForceUsername"];
                var password = ConfigurationManager.AppSettings["KaleSalesForcePassword"];
                string tokenParameters = string.Format("grant_type={0}&client_id={1}&client_secret={2}&username={3}&password={4}", grantType, clientId, clientSecret, username, password);


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var tokenResult = SalesForceTokenRequest("oauth2/token", tokenParameters);
                token = tokenResult.access_token;
                #endregion

                #region CHECK_ACCOUNT
                var phoneNumber = WebUtility.UrlEncode(formData.Phone);
                var checkAccountResult = SalesForceRequest<SalesForceAccountCheckResponse>($"data/v45.0/query/?q=SELECT+Id%2CMobile__c+FROM+Account+where+Mobile__c+like+'%25{phoneNumber}%25'", tokenResult.access_token);
                checkAccountResultStr = JsonConvert.SerializeObject(checkAccountResult);
                #endregion

                string accountId = string.Empty;

                if (checkAccountResult.Records.Count() > 0)
                {
                    accountId = checkAccountResult.Records[0].Id;

                }
                else
                {
                    #region INSERT_ACCOUNT
                    AccountObject accountObject = new AccountObject()
                    {
                        AccountSource = "www.kale.com.tr",
                        Billing_Country__c = formData.Country,
                        Billing_Region__c = formData.District, //Test Ortamı : "a083N000000DMWFQA4"
                        City__c = formData.City, //Test Ortamı: "a083N000000DMN4QAO"
                        E_mail__c = formData.Email,
                        Mobile__c = formData.Phone,
                        Name = formData.Name + " " + formData.Surname,
                        RCA_Billing_Street_1__c = formData.Address1,
                        RCA_Billing_Street_2__c = formData.Address2,
                        TypeKS__c = "End User" //formData.Job
                        //RC_KVKK_Approve_Channel__c = "www.kale.com.tr",
                        //RC_KVKK_Document_KS__c = "??",
                        //RC_KVKK_KS__c = formData.IsAllowedPrivacy.ToString().ToLower(),
                        //RC_Marketing_Email_KS__c = formData.IsAllowedCampaign.ToString().ToLower(),
                        //RC_Marketing_SMS_KS__c = formData.IsAllowedCampaign.ToString().ToLower(),
                    };
                    token = tokenResult.access_token;
                    var accountResult = SalesForceRequest<SalesForceInsertResponse>("data/v45.0/sobjects/Account", accountObject, tokenResult.access_token);
                    accountId = accountResult.id;
                    #endregion

                }

                #region INSERT_CASE
                try
                {
                    CaseObject caseObject = new CaseObject()
                    {
                        AccountId = accountId,
                        Description = formData.Message,
                        Origin = "kale.com.tr",
                        Subject = "Web Sitesi İletişim Formu",
                        Type = "BİLGİ ALMA", // formData.MessageType
                        RecordTypeId = "0120Y000000KklEQAS",
                        Customer_Request__c = "Talebi Yok",
                        OwnerId = "00G0Y000004aCvpUAE"
                    };


                    var caseResult = SalesForceRequest<SalesForceInsertResponse>("data/v45.0/sobjects/Case", caseObject, tokenResult.access_token);
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, "Cannot send salesforce insert case bilgisi", false);
                }
                #endregion

                //try
                //{
                //KVKK REQUEST
                //KvkkRequest kvkkRequest = new KvkkRequest()
                //{
                //    Musteri__c = accountId,
                //    Approve_Channel__c = "www.kale.com.tr",
                //    Company__c = "KS",
                //    KVKK__c = formData.IsAllowedPrivacy.ToString().ToLower(),
                //    KVKK_Document__c = "versiyon 1",
                //    KVKK_Created_Date__c = DateTime.Now,
                //    KVKK_Last_Modified_Date__c = DateTime.Now,
                //    Marketing_Email__c = formData.IsAllowedCampaign.ToString().ToLower(),
                //    Marketing_Email_Document__c = "versiyon 1",
                //    Marketing_Email_Created_Date__c = DateTime.Now,
                //    Marketing_Email_Last_Modified_Date__c = DateTime.Now,
                //    Marketing_SMS__c = formData.IsAllowedCampaign.ToString().ToLower(),
                //    Marketing_SMS_Document__c = "versiyon 1",
                //    Marketing_SMS_Created_Date__c = DateTime.Now,
                //    Marketing_SMS_Last_Modified_Date__c = DateTime.Now,
                //    Last_Activity_IP__c = GetIpValue(context)
                //};
                //var kvkkResult = SalesForceRequest<KvkkResponse>("data/v45.0/sobjects/KVKK_Bilgisi__c/", kvkkRequest, tokenResult.access_token);
                //}
                //catch (Exception ex)
                //{
                //CmsHelper.SaveErrorLog(ex, "Cannot send salesforce kvkk bilgisi", false);
                //}

                var srFinal = new SendSalesForceResult { Code = "100", Message = "Success", Status = "OK" };
                string jsonResultFinal = JsonConvert.SerializeObject(srFinal);
                return jsonResultFinal;
            }
            catch (Exception exception)
            {
                var exMessage = exception.Message;
                if (exception.InnerException != null)
                    exMessage = exMessage + " - " + exception.InnerException;

                var sr = new SendSalesForceResult { Code = "14", Message = "Process Failed", ErrorMessage = exception.Message, Status = "NOK" };
                //CmsHelper.SaveErrorLog(exception, "Cannot send salesforce", false);
                string jsonResult = JsonConvert.SerializeObject(sr);
                return jsonResult;
            }
        }

        private FormViewModel GetFormModel(NameValueCollection collection)
        {
            FormViewModel formData = new FormViewModel
            {
                CaptchaId = collection["captchaid"] != null ? Convert.ToInt32(collection["captchaid"]) : 0,
                Captcha = collection["captchaValue"] != null ? collection["captchaValue"] : string.Empty,

                Name = HttpUtility.HtmlDecode(collection["ad"] != null ? collection["ad"] : string.Empty),
                Surname = HttpUtility.HtmlDecode(collection["soyad"] != null ? collection["soyad"] : string.Empty),
                Email = collection["email"] != null ? collection["email"] : string.Empty,
                Phone = collection["telefon"] != null ? collection["telefon"] : "+905555555555",
                Job = collection["meslek"] != null ? collection["meslek"] : string.Empty,

                Country = collection["ulke"] != null ? collection["ulke"] : string.Empty,
                City = collection["sehir"] != null ? collection["sehir"] : string.Empty,
                District = collection["ilceler"] != null ? collection["ilceler"] : string.Empty,
                Address1 = HttpUtility.HtmlDecode(collection["adres1"] != null ? collection["adres1"] : string.Empty),
                Address2 = HttpUtility.HtmlDecode(collection["adres2"] != null ? collection["adres2"] : string.Empty),

                MessageType = HttpUtility.HtmlDecode(collection["mesajturu"] != null ? collection["mesajturu"] : string.Empty),
                Message = HttpUtility.HtmlDecode(collection["mesaj"] != null ? collection["mesaj"] : string.Empty),

                IsAllowedCampaign = collection["eileti"] != null ? (collection["eileti"] == "on" ? true : false) : false,
                IsAllowedPrivacy = collection["onay"] != null ? (collection["onay"] == "on" ? true : false) : false,
            };
            return formData;
        }

        private SalesForceTokenResponse SalesForceTokenRequest(string uri, string parameters)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(SalesForceServiceURL + uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                request.Proxy = WebRequest.DefaultWebProxy;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                byte[] tokenParameterBytes = Encoding.ASCII.GetBytes(parameters);
                request.ContentLength = tokenParameterBytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(tokenParameterBytes, 0, tokenParameterBytes.Length);
                requestStream.Close();

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return JsonConvert.DeserializeObject<SalesForceTokenResponse>(responseString);
                }
                else
                {
                    //CmsHelper.SaveErrorLog(new Exception("Token alınırken hata oluştu."), "Cannot take salesforce token", false);
                    throw new Exception("Token alınırken hata oluştu.");
                }

            }
            catch (Exception exception)
            {
                //CmsHelper.SaveErrorLog(exception, "SalesForce token alınırken hata oluştu", false);
                throw exception;
            }
        }

        private T SalesForceRequest<T>(string uri, object body, string accessToken)
        {
            var bodyString = JsonConvert.SerializeObject(body);
            string req = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(SalesForceServiceURL + uri);
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);
                request.ContentType = "application/json";
                request.Method = "POST";

                request.Proxy = WebRequest.DefaultWebProxy;
                request.Credentials = CredentialCache.DefaultCredentials; ;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                using (var streamWriter = new StreamWriter(request.GetRequestStream(), Encoding.UTF8))
                {
                    streamWriter.Write(bodyString);
                }
                req = JsonConvert.SerializeObject(req);

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = string.Empty;
                    result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK || httpResponse.StatusCode == HttpStatusCode.Created)
                    {
                        //var ex=new Exception("SalesForce isteği sonucu : " + result + " Package: " + bodyString + "   Req: " + req);
                        // CmsHelper.SaveErrorLog(ex, "SalesForce isteği sonucu", false);
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    else
                    {
                        //CmsHelper.SaveErrorLog(new Exception("SalesForce isteği esnasında hata oluştu. Hata: " + result + " Package: " + bodyString + "   Req: " + req), "SalesForce isteği esnasında hata oluştu", false);
                        throw new Exception("SalesForce isteği esnasında hata oluştu. Hata: " + result + " Package: ");
                    }

                }
            }
            catch (Exception ex)
            {
                //CmsHelper.SaveErrorLog(new Exception("Hata: " + ex.Message + " Data: " + bodyString), "SalesForce isteğiHata", false);
                throw new Exception("Hata: " + ex.Message);
            }
        }

        private T SalesForceRequest<T>(string uri, string accessToken)
        {
            string req = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(SalesForceServiceURL + uri);
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);
                request.Method = "GET";

                request.Proxy = WebRequest.DefaultWebProxy;
                request.Credentials = CredentialCache.DefaultCredentials; ;
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                req = JsonConvert.SerializeObject(request);
                var httpResponse = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = string.Empty;
                    result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK || httpResponse.StatusCode == HttpStatusCode.Created)
                    {
                       // CmsHelper.SaveErrorLog(new Exception("SalesForce query isteği sonucu : " + result + " Package: " + uri + "   Req: " + req), "SalesForce query isteği sonucu", false);

                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    else
                    {
                        //CmsHelper.SaveErrorLog(new Exception("SalesForce accountCheck isteği sonucu : " + result + " Package: accountCheck uri: " + uri + "   Req: " + req), "SalesForce accountCheck isteği esnasında hata oluştu.", false);
                        throw new Exception("SalesForce isteği esnasında hata oluştu. Hata: " + result + " Package: accountCheck");
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "SalesForce accountCheck isteği esnasında hata oluştu.", false);
                throw new Exception("Hata: " + ex.Message + " Data: accountCheck");
            }
        }

        #region Captcha Plugin
        public string CaptchaPlugin(HttpContext context)
        {
            string returnVal = "";

            string captchaWidth = "300", captchaHeight = "75", captchaLength = "5", captchaId = "1";
            bool captchaisnumeric = false;

            //captchaValue = !string.IsNullOrEmpty(context.Request.Form["value"]) ? context.Request.Form["value"].ToString().Trim() : "";
            captchaWidth = !string.IsNullOrEmpty(context.Request.Form["width"]) ? context.Request.Form["width"].ToString().Trim() : "300";
            captchaHeight = !string.IsNullOrEmpty(context.Request.Form["height"]) ? context.Request.Form["height"].ToString().Trim() : "75";
            captchaId = !string.IsNullOrEmpty(context.Request.Form["captchaid"]) ? context.Request.Form["captchaid"].ToString().Trim() : "1";
            captchaLength = !string.IsNullOrEmpty(context.Request.Form["length"]) ? context.Request.Form["length"].ToString().Trim() : "5";
            captchaisnumeric = !string.IsNullOrEmpty(context.Request.Form["captchaisnumeric"]) ? (context.Request.Form["captchaisnumeric"].ToString().Trim() == "1" ? true : false) : false;

            //if (captchaId != "1" && captchaId != "2" && captchaId != "3" && captchaId != "4" && captchaId != "5")
            //{
            //    captchaId = "1";
            //}

            int width = 300;
            int height = 75;
            int length = 5;

            width = Int32.TryParse(captchaWidth, out width) ? Convert.ToInt32(captchaWidth) : 300;
            height = Int32.TryParse(captchaHeight, out height) ? Convert.ToInt32(captchaHeight) : 75;
            length = Int32.TryParse(captchaLength, out length) ? Convert.ToInt32(captchaLength) : 5;

            if (length < 5 || length > 8)
            {
                length = 5;
            }


            string captchaText = GenerateRandomCode(length.ToString(), captchaisnumeric).Trim();
            context.Session["CaptchaImageText" + captchaId] = captchaText;

            string imageBase64 = "";
            imageBase64 = CaptchaImageToBase64(context, captchaText, width, height);

            returnVal = JsonConvert.SerializeObject(imageBase64);

            return returnVal;
        }


        public string CaptchaImageToBase64(HttpContext context, string word, int width, int height)
        {
            string base64String = "";

            RandomImage ci = new RandomImage(word, width, height);

            //Bitmap resim = new Bitmap(120, 50);
            //string harfler = "AB12345CDEFGHJKMNPRSTYUVYZWX6789";
            //harfler = harfler.ToLower();
            //Graphics grafik = Graphics.FromImage(resim);
            //grafik.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), 0, 0, 240, 50);

            //Color fontColor = ColorTranslator.FromHtml("#CCCCCC");

            //Font yazi_tipi = new Font("Tahoma", 15, FontStyle.Bold);
            //Random rastgele = new Random();
            //SolidBrush firca = new SolidBrush(fontColor);
            //string karakter = "";
            //string kod = "";

            //for (int i = 0; i < 5; i++)
            //{
            //    karakter = harfler[rastgele.Next(0, harfler.Length - 1)].ToString();
            //    grafik.DrawString(karakter, yazi_tipi, firca, i * 25 + 5, 0);
            //    kod += karakter;
            //}


            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            //resim.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ci.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = stream.ToArray();
            base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }

        private string GenerateRandomCode(string characterLength, bool captchaisnumeric)
        {
            int characterCount = 5;

            if (Int32.TryParse(characterLength, out characterCount))
            {
                characterCount = Convert.ToInt32(characterLength);
            }

            Random r = new Random();
            string s = "";
            for (int j = 0; j < characterCount; j++)
            {
                int i = r.Next(3);
                int ch;

                if (captchaisnumeric)
                {
                    ch = r.Next(48, 57);
                    s = s + Convert.ToChar(ch).ToString();
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            ch = r.Next(0, 9);
                            s = s + ch.ToString();
                            break;
                        case 2:
                            ch = r.Next(65, 90);
                            s = s + Convert.ToChar(ch).ToString();
                            break;
                        case 3:
                            ch = r.Next(97, 122);
                            s = s + Convert.ToChar(ch).ToString();
                            break;
                        default:
                            ch = r.Next(97, 122);
                            s = s + Convert.ToChar(ch).ToString();
                            break;
                    }
                }

                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }

        private string IsVisible(HttpContext context)
        {
            string result = string.Empty;
            //captchaid alınacak. sessiona eklenecek
            int captchaCounter = 0;
            if (HttpContext.Current.Session["CaptchaCounter"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["CaptchaCounter"].ToString()))
            {
                captchaCounter = Convert.ToInt32(HttpContext.Current.Session["CaptchaCounter"].ToString());
            }

            int maxCaptchaCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxCaptchaCount"]);

            if (captchaCounter < maxCaptchaCount)
            {
                result = "false";
                HttpContext.Current.Session["CaptchaCounter"] = captchaCounter + 1;
            }
            else
            {
                result = "true";
                HttpContext.Current.Session["CaptchaCounter"] = captchaCounter + 1;
            }

            return result;
        }

        public class RandomImage
        {
            //Default Constructor 
            public RandomImage() { }
            //property
            public string Text
            {
                get { return this.text; }
            }
            public Bitmap Image
            {
                get { return this.image; }
            }
            public int Width
            {
                get { return this.width; }
            }
            public int Height
            {
                get { return this.height; }
            }
            //Private variable
            private string text;
            private int width;
            private int height;
            private Bitmap image;
            private Random random = new Random();
            //Methods declaration
            public RandomImage(string s, int width, int height)
            {
                this.text = s;
                this.SetDimensions(width, height);
                this.GenerateImage();
            }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                this.Dispose(true);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                    this.image.Dispose();
            }
            private void SetDimensions(int width, int height)
            {
                if (width <= 0)
                    throw new ArgumentOutOfRangeException("width", width,
                        "Argument out of range, must be greater than zero.");
                if (height <= 0)
                    throw new ArgumentOutOfRangeException("height", height,
                        "Argument out of range, must be greater than zero.");
                this.width = width;
                this.height = height;
            }


            private void GenerateImage()
            {
                // Create a new 32-bit bitmap image.
                Bitmap bitmap = new Bitmap(
                  this.width,
                  this.height,
                  PixelFormat.Format32bppArgb);

                // Create a graphics object for drawing.
                Graphics g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, this.width, this.height);

                // Fill in the background.
                HatchBrush hatchBrush = new HatchBrush(
                  HatchStyle.SmallConfetti,
                  Color.LightGray,
                  Color.White);
                g.FillRectangle(hatchBrush, rect);

                // Set up the text font.
                SizeF size;
                float fontSize = rect.Height + 1;
                Font font;
                // Adjust the font size until the text fits within the image.
                do
                {
                    fontSize--;
                    font = new Font(
                      FontFamily.GenericSansSerif,
                      fontSize,
                      FontStyle.Bold);
                    size = g.MeasureString(this.text, font);
                } while (size.Width > rect.Width);

                // Set up the text format.
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                // Create a path using the text and warp it randomly.
                GraphicsPath path = new GraphicsPath();
                path.AddString(
                  this.text,
                  font.FontFamily,
                  (int)font.Style,
                  font.Size, rect,
                  format);
                float v = 4F;
                PointF[] points =
      {
        new PointF(
          this.random.Next(rect.Width) / v,
          this.random.Next(rect.Height) / v),
        new PointF(
          rect.Width - this.random.Next(rect.Width) / v,
          this.random.Next(rect.Height) / v),
        new PointF(
          this.random.Next(rect.Width) / v,
          rect.Height - this.random.Next(rect.Height) / v),
        new PointF(
          rect.Width - this.random.Next(rect.Width) / v,
          rect.Height - this.random.Next(rect.Height) / v)
      };
                Matrix matrix = new Matrix();
                matrix.Translate(0F, 0F);
                path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

                // Draw the text.
                hatchBrush = new HatchBrush(
                  HatchStyle.LargeConfetti,
                  Color.Black,
                  Color.Black);
                g.FillPath(hatchBrush, path);

                // Add some random noise.
                int m = Math.Max(rect.Width, rect.Height);
                for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
                {
                    int x = this.random.Next(rect.Width);
                    int y = this.random.Next(rect.Height);
                    int w = this.random.Next(m / 50);
                    int h = this.random.Next(m / 50);
                    g.FillEllipse(hatchBrush, x, y, w, h);
                }

                // Clean up.
                font.Dispose();
                hatchBrush.Dispose();
                g.Dispose();

                // Set the image.
                this.image = bitmap;
            }


            private void GenerateImage2()
            {
                Bitmap bitmap = new Bitmap
                  (this.width, this.height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, this.width, this.height);
                HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
                g.FillRectangle(hatchBrush, rect);
                SizeF size;
                float fontSize = rect.Height + 1;
                Font font;

                do
                {
                    fontSize--;
                    font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                    size = g.MeasureString(this.text, font);
                } while (size.Width > rect.Width);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                GraphicsPath path = new GraphicsPath();
                path.AddString(this.text, font.FontFamily, (int)font.Style, 75, rect, format);
                float v = 4F;
                PointF[] points =
          {
                new PointF(this.random.Next(rect.Width) / v, this.random.Next(
                   rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v,
                    this.random.Next(rect.Height) / v),
                new PointF(this.random.Next(rect.Width) / v,
                    rect.Height - this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v,
                    rect.Height - this.random.Next(rect.Height) / v)
          };
                Matrix matrix = new Matrix();
                matrix.Translate(0F, 0F);
                path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
                hatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.SkyBlue);
                g.FillPath(hatchBrush, path);
                int m = Math.Max(rect.Width, rect.Height);
                for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
                {
                    int x = this.random.Next(rect.Width);
                    int y = this.random.Next(rect.Height);
                    int w = this.random.Next(m / 50);
                    int h = this.random.Next(m / 50);
                    g.FillEllipse(hatchBrush, x, y, w, h);
                }
                font.Dispose();
                hatchBrush.Dispose();
                g.Dispose();
                this.image = bitmap;
            }


        }

        #endregion
    }
}