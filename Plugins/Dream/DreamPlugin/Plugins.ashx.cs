using DreamPlugin.Data;
using DreamPlugin.Models;
using EuroCMS.Captcha;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.Dream
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

        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

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
                result = jsSerializer.Serialize(new { status = false, message = "plugin alanı boş gönderilemez", data = "" });
            }

            switch (plugin)
            {
                case "contactform":
                    result = ContactForm(context);
                    break;
                case "getcontactformdata":
                    result = GetContactFormData(context);
                    break;
                case "removecontactformdata":
                    result = RemoveContactFormData(context);
                    break;
            }
            context.Response.Write(result);
        }

        private string GetIpValue(HttpContext context)
        {
            var ipAdd = new WebClient().DownloadString("http://icanhazip.com");

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

        public string ContactForm(HttpContext context)
        {
            string result = string.Empty;

            try
            {
                var source = context.Request["source"] ?? string.Empty;
                var name = context.Request["namesurname"] ?? string.Empty;
                var email = context.Request["email"] ?? string.Empty;
                //var message = context.Request["message"] ?? string.Empty;
                var phone = context.Request["phone"] ?? string.Empty;
                var birthDay = context.Request["dt"] ?? string.Empty;
                var kvkkConfirm = context.Request["permission"] == "on" ? true : false;
                var promotionConfirm = context.Request["mailpermission"] == "on" ? true : false;
                var isAllowEmail = context.Request["isallowemail"] == "on" ? true : false;
                var isAllowSMS = context.Request["isallowsms"] == "on" ? true : false;

                #region GOOGLE CAPTCHA
                var encodedResponse = context.Request.Form["g-Recaptcha-Response"];
                var isCaptchaValid = GoogleReCaptchaV2.Validate(encodedResponse);

                if (!isCaptchaValid)
                    return jsSerializer.Serialize(new { status = false, message = "Captcha Error" });
                #endregion

                #region VALIDATION

                #endregion

                #region DATABASE
                using (DreamDbContext dbContext = new DreamDbContext())
                {
                    ContactForm contactForm = new ContactForm()
                    {
                        Email = WebUtility.HtmlDecode(email),
                        IsConfirmKVKK = kvkkConfirm,
                        IsConfirmPromotion = promotionConfirm,
                        //Message = message,
                        Name = WebUtility.HtmlDecode(name),
                        Phone = WebUtility.HtmlDecode(phone),
                        Source = WebUtility.HtmlDecode(source),
                        Birthday = WebUtility.HtmlDecode(birthDay),
                        IpAddress = WebUtility.HtmlDecode(GetIpValue(context)),
                        IsAllowEmail = isAllowEmail,
                        IsAllowSMS = isAllowSMS,
                        CreationDate = DateTime.UtcNow
                    };
                    dbContext.ContactFormDatas.Add(contactForm);
                    dbContext.SaveChanges();
                }
                return JsonConvert.SerializeObject(new { status = true, message = "OK" });
                #endregion
            }
            catch (Exception exception)
            {
                result = jsSerializer.Serialize(Response<string>.CreateFailResponse(500, exception.ToString()));
            }
            return result;
        }

        public string GetContactFormData(HttpContext context)
        {
            var loginValidation = LoginValidation(context);
            if (loginValidation.IsSuccess)
            {
                using (DreamDbContext dbContext = new DreamDbContext())
                {
                    var contactFormDatas = dbContext.ContactFormDatas.ToList();
                    return JsonConvert.SerializeObject(contactFormDatas);
                }
            }
            else
            {
                return loginValidation.Message;
            }
        }

        public string RemoveContactFormData(HttpContext context)
        {
            var loginValidation = LoginValidation(context);
            if (loginValidation.IsSuccess)
            {
                using (DreamDbContext dbContext = new DreamDbContext())
                {
                    foreach (var id in dbContext.ContactFormDatas.Select(e => e.Id))
                    {
                        var entity = new ContactForm { Id = id };
                        dbContext.ContactFormDatas.Attach(entity);
                        dbContext.ContactFormDatas.Remove(entity);
                    }
                    dbContext.SaveChanges();
                    return jsSerializer.Serialize("OK");
                }
            }
            else
            {
                return loginValidation.Message;
            }
        }
        
        private LoginResponse LoginValidation(HttpContext context)
        {
            LoginResponse response = new LoginResponse();
            var username = context.Request["username"] ?? string.Empty;
            var password = context.Request["password"] ?? string.Empty;
            var serviceUsername = WebConfigurationManager.AppSettings["DreamServiceUsername"];
            var servicePassword = WebConfigurationManager.AppSettings["DreamServicePassword"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Kullanıcı adı ve parola alanlarını boş bırakamazsınız.";
            }
            else if (string.IsNullOrEmpty(serviceUsername) || string.IsNullOrEmpty(servicePassword))
            {
                response.Message = "Servis kullanıcısı tanımlanmamış. Lütfen hizmet yetkilileri ile iletişime geçiniz.";
            }
            else if (serviceUsername != username || servicePassword != password)
            {
                response.Message = "Servis oturum bilgileri hatalı.";
            }
            else
            {
                response.IsSuccess = true;
            }
            return response;
        }
    }
}