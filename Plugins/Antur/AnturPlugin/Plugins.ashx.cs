using EuroCMS.Captcha;
using EuroCMS.Core;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.Antur
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
                var name = context.Request["name"] ?? string.Empty;
                var surname = context.Request["surname"] ?? string.Empty;
                var email = context.Request["email"] ?? string.Empty;
                var phone = context.Request["phone"] ?? string.Empty;
                var message = context.Request["message"] ?? string.Empty;
                var kvkkConfirm = context.Request["kvkkconfirm"] == "on" ? true : false;
                var confirmEnlightenment = context.Request["enlightenmentconfirm"] == "on" ? true : false;
                var isAllowEmail = context.Request["isallowemail"] == "on" ? true : false;
                var isAllowSMS = context.Request["isallowsms"] == "on" ? true : false;

                //#region GOOGLE CAPTCHA
                //var encodedResponse = context.Request.Form["g-Recaptcha-Response"];
                //var isCaptchaValid = GoogleReCaptchaV2.Validate(encodedResponse);
                //
                //if (!isCaptchaValid)
                //    return jsSerializer.Serialize(new { status = false, message = "Captcha Error" });
                //#endregion



                #region DATABASE
                using (AnturDbContext dbContext = new AnturDbContext())
                {
                    ContactForm contactForm = new ContactForm()
                    {
                        Email = WebUtility.HtmlDecode(email),
                        ConfirmKVKK = kvkkConfirm,
                        ConfirmEnlightenment = confirmEnlightenment,
                        //Message = message,
                        Name = WebUtility.HtmlDecode(name),
                        Surname = WebUtility.HtmlDecode(surname),
                        Phone = WebUtility.HtmlDecode(phone),
                        IpAddress = GetIpValue(context),
                        IsAllowEmail = isAllowEmail,
                        IsAllowSMS = isAllowSMS,
                        CreationDate = DateTime.UtcNow
                    };
                    dbContext.ContactFormDatas.Add(contactForm);
                    dbContext.SaveChanges();
                }
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>Adı: </strong>{WebUtility.HtmlDecode(name)}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>Soyadı: </strong>{WebUtility.HtmlDecode(surname)}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>E-Posta Adresi: </strong>{WebUtility.HtmlDecode(email)}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>Telefon: </strong>{WebUtility.HtmlDecode(phone)}</div>");
                stringBuilder.Append("<div style='font-family:arial;font-size:14px'><h4>İletişim Tercihi</h3></div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>E-Posta: </strong>{(isAllowEmail ? "Evet" : "Hayır")}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>SMS: </strong>{(isAllowSMS ? "Evet" : "Hayır")}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px;margin-top:20px'><strong>Aydınlatma Metni: </strong>{(kvkkConfirm ? "Evet" : "Hayır")}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>Paylaşım İzni: </strong>{(confirmEnlightenment ? "Evet" : "Hayır")}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px;margin-top:20px'><strong>IP Adresi: </strong>{GetIpValue(context)}</div>");
                stringBuilder.Append($"<div style='font-family:arial;font-size:14px'><strong>Oluşturulma Tarihi: </strong>{DateTime.UtcNow.ToLongDateString()}</div>");

                var emailTo = WebConfigurationManager.AppSettings["AnturContactFormEmailTo"];
                MailSender.SendMail(emailTo, null, null, "Antur İletişim Formu", stringBuilder.ToString(), null);

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
                using (AnturDbContext dbContext = new AnturDbContext())
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
                using (AnturDbContext dbContext = new AnturDbContext())
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
            var serviceUsername = WebConfigurationManager.AppSettings["AnturServiceUsername"];
            var servicePassword = WebConfigurationManager.AppSettings["AnturServicePassword"];
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