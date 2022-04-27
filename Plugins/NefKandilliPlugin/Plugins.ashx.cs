using EuroCMS.Model;
using EuroCMS.Plugin.NefKandilli.NEFWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace EuroCMS.Plugin.NefKandilli
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string result = "", plugin = "";

            if (!string.IsNullOrEmpty(context.Request.Form["plugin"]))
            {
                plugin = context.Request.Form["plugin"].ToLower().Trim();
            }
            else
            {
                result = jss.Serialize(new { status = false, message = "plugin alanı boş gönderilemez", data = "" });
            }

            switch (plugin)
            {
                case "contactform":
                    result = contactform(context);
                    break;
            }

            context.Response.Write(result);

        }

        public string contactform(HttpContext context)
        {
            MsCrmResult result = new MsCrmResult();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string resultJson = "";

            string name = "", surname = "", phone = "", email = "", awareness = "", message = "", projectCode = "", information = "", flatType = "", investmentRange = "", utmSource = "", utmCampaign = "", utmMedium = "", gaClientId = "";
            try
            {

                if (!string.IsNullOrEmpty(context.Request.Form["scc"]))
                {
                    result.Message = "Geçersiz İşlem";
                    result.Success = false;

                    resultJson = jss.Serialize(result); //jss.Serialize(result);
                    return resultJson;
                }
                if (!string.IsNullOrEmpty(context.Request.Form["firstname"]))
                {
                    name = context.Request.Form["firstname"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["lastname"]))
                {
                    surname = context.Request.Form["lastname"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["projectCode"]))
                {
                    projectCode = context.Request.Form["projectCode"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    email = context.Request.Form["email"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["how"]))
                {
                    awareness = context.Request.Form["how"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["msg"]))
                {
                    message = context.Request.Form["msg"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["information"]))
                {
                    information = context.Request.Form["information"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["flattype"]))
                {
                    flatType = context.Request.Form["flattype"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["investmentrange"]))
                {
                    investmentRange = context.Request.Form["investmentrange"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["utm_source"]))
                {
                    utmSource = context.Request.Form["utm_source"].ToString().Trim();
                }

                utmCampaign = string.IsNullOrEmpty(context.Request.Form["utm_campaign"]) ? "" : context.Request.Form["utm_campaign"].ToString().Trim();
                utmMedium = string.IsNullOrEmpty(context.Request.Form["utm_medium"]) ? "" : context.Request.Form["utm_medium"].ToString().Trim();

                gaClientId = string.IsNullOrEmpty(context.Request.Form["ga_clientid"]) ? "" : context.Request.Form["ga_clientid"].ToString().Trim();

                //if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(awareness) || awareness == "0" || string.IsNullOrEmpty(message))
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
                {
                    result.Message = "Lütfen tüm alanları eksiksiz doldurunuz";
                    result.Success = false;

                    resultJson = jss.Serialize(result); //jss.Serialize(result);
                    return resultJson;
                }

                name = HtmlAndUrlDecode(name);
                surname = HtmlAndUrlDecode(surname);
                //phone = HtmlAndUrlDecode(phone);
                email = HtmlAndUrlDecode(email);
                awareness = HtmlAndUrlDecode(awareness);
                message = HtmlAndUrlDecode(message);

                Regex r = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                Match m = r.Match(email);
                if (!m.Success)
                {
                    result.Message = "Geçersiz email adresi";
                    result.Success = false;

                    resultJson = jss.Serialize(result); //jss.Serialize(result);
                    return resultJson;
                }

                CmsDbContext dbContext = new CmsDbContext();

                //int mailTemplateArticleId = 3, classsificationId = 1;

                //Article getMailTemplateArticle = new Article();
                //getMailTemplateArticle = dbContext.Articles.Where(a => a.Id == mailTemplateArticleId && a.ClassificationId == classsificationId).FirstOrDefault();

                //if (getMailTemplateArticle == null)
                //{
                //    result.Message = "Geçersiz email template";
                //    result.Success = false;
                //    resultJson = jss.Serialize(result);
                //    return resultJson;
                //}

                InvestmentRange optionInvestMent = new InvestmentRange();
                FlatType optionFlat = new FlatType();

                WebFormClient service = new WebFormClient();
                Webform form = new Webform();
                //---------------------
                form.ChannelOfAwareness = awareness;
                form.CustomerEmail = email;
                form.CustomerMobilePhone = phone;
                form.CustomerName = name;
                form.CustomerSurname = surname;
                form.Message = message;

                switch (investmentRange)
                {
                    case "1":
                        form.CustomerInvestmentRange = InvestmentRange.optionOne;
                        break;
                    case "2":
                        form.CustomerInvestmentRange = InvestmentRange.optionTwo;
                        break;
                    case "3":
                        form.CustomerInvestmentRange = InvestmentRange.optionThree;
                        break;
                    case "4":
                        form.CustomerInvestmentRange = InvestmentRange.optionFour;
                        break;
                    case "5":
                        form.CustomerInvestmentRange = InvestmentRange.optionFive;
                        break;
                    case "6":
                        form.CustomerInvestmentRange = InvestmentRange.optionSix;
                        break;
                    default:
                        form.CustomerInvestmentRange = null;
                        break;
                }

                switch (flatType)
                {
                    case "1":
                        form.FlatTypeChoose = FlatType.optionOne;
                        break;
                    case "2":
                        form.FlatTypeChoose = FlatType.optionTwo;
                        break;
                    case "3":
                        form.FlatTypeChoose = FlatType.optionThree;
                        break;
                    case "4":
                        form.FlatTypeChoose = FlatType.optionFour;
                        break;
                    default:
                        form.FlatTypeChoose = null;
                        break;
                }

                //-----------------------

                if (!string.IsNullOrEmpty(utmSource))
                {
                    form.SubParticipationSource = utmSource;
                }

                if (!string.IsNullOrEmpty(utmCampaign))
                {
                    form.UTM_Campaign = utmCampaign;
                }

                if (!string.IsNullOrEmpty(utmMedium))
                {
                    form.UTM_Medium = utmMedium;
                }

                if (!string.IsNullOrEmpty(gaClientId))
                {
                    form.ClientId = gaClientId;
                }

                form.InterestOfProjectCode = projectCode; //"A1-829-34-900";
                form.NefInformation = (string.IsNullOrEmpty(information) || information == "0" ? false : true);

                form.ContactPreferences = ContactPreferences.Phone;
                result = service.CreateWebForm(form);

                resultJson = jss.Serialize(result); //jss.Serialize(result);


                // MAIL SEND START
                //if (result.Success)
                //{
                //    string smtpHostAddress = "", smtpPortNumber = "", fromMailAddress = "", fromMailPassword = "", mailToAddress = "", mailSubject = "", mailCCAddress = "", mailBody = "", howText = "";

                //    smtpHostAddress = getMailTemplateArticle.Custom1; //ConfigurationManager.AppSettings["smtpHostAddress"];
                //    smtpPortNumber = getMailTemplateArticle.Custom2; //ConfigurationManager.AppSettings["smtpPortNumber"];
                //    fromMailAddress = getMailTemplateArticle.Custom3; //ConfigurationManager.AppSettings["fromMailAddress"];
                //    fromMailPassword = getMailTemplateArticle.Custom4; //ConfigurationManager.AppSettings["fromMailPassword"];
                //    mailToAddress = getMailTemplateArticle.Custom5; //ConfigurationManager.AppSettings["mailToAddress"];
                //    mailSubject = getMailTemplateArticle.Custom6; //ConfigurationManager.AppSettings["mailSubject"];
                //    mailCCAddress = getMailTemplateArticle.Custom7; //ConfigurationManager.AppSettings["mailCCAddress"];

                //    howText = HowToText(awareness);


                //    System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                //    SmtpSection smtpInfo = new SmtpSection();
                //    smtpInfo = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");

                //    SmtpClient smtpClient = new SmtpClient(smtpInfo.Network.Host, smtpInfo.Network.Port);
                //    if (smtpInfo.Network.DefaultCredentials)
                //    {
                //        smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //    }
                //    else
                //    {
                //        if (string.IsNullOrEmpty(smtpInfo.Network.ClientDomain))
                //        {
                //            smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                //        }
                //        else
                //        {
                //            smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password, smtpInfo.Network.ClientDomain);
                //        }
                //    }

                //    smtpClient.UseDefaultCredentials = smtpInfo.Network.DefaultCredentials;
                //    smtpClient.EnableSsl = smtpInfo.Network.EnableSsl;
                //    smtpClient.DeliveryMethod = smtpInfo.DeliveryMethod;

                //    MailMessage mail = new MailMessage();
                //    mail.To.Add(new MailAddress(mailToAddress));
                //    if (!string.IsNullOrEmpty(mailCCAddress))
                //    {
                //        mail.CC.Add(mailCCAddress);
                //    }
                //    mail.Subject = mailSubject;
                //    mail.From = new MailAddress(fromMailAddress);
                //    mail.IsBodyHtml = true;

                //    string htmlTemplate = "";
                //    htmlTemplate = getMailTemplateArticle.Article1;
                //    htmlTemplate = HttpUtility.HtmlDecode(htmlTemplate);
                //    htmlTemplate = HttpUtility.UrlDecode(htmlTemplate);

                //    htmlTemplate = htmlTemplate.Replace("##firstname##", name).ToString();
                //    htmlTemplate = htmlTemplate.Replace("##lastname##", surname).ToString();
                //    htmlTemplate = htmlTemplate.Replace("##how##", howText).ToString();
                //    htmlTemplate = htmlTemplate.Replace("##phone##", phone).ToString();
                //    htmlTemplate = htmlTemplate.Replace("##email##", email).ToString();
                //    htmlTemplate = htmlTemplate.Replace("##msg##", message).ToString();

                //    mailBody = htmlTemplate;

                //    mail.Body = mailBody;
                //    smtpClient.Send(mail);

                //}
                ////MAIL SEND END

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
                return resultJson;
            }
            catch (Exception exp)
            {
                result.Message = exp.Message;
                result.Success = false;
                resultJson = jss.Serialize(result);
                return resultJson;
            }

        }

        public string HowToText(string howValue)
        {
            string returnVal = "";

            switch (howValue)
            {
                case "32":
                    returnVal = "TV";
                    break;
                case "4":
                    returnVal = "Dergi";
                    break;
                case "13":
                    returnVal = "İnternet";
                    break;
                case "21":
                    returnVal = "Referans";
                    break;
                case "11":
                    returnVal = "Gazete";
                    break;
                case "2":
                    returnVal = "Billboard";
                    break;
                case "27":
                    returnVal = "Sosyal medya";
                    break;
                case "8":
                    returnVal = "E-posta";
                    break;
                case "20":
                    returnVal = "Radyo";
                    break;
                case "5":
                    returnVal = "Diğer";
                    break;
            }
            return returnVal;
        }

        public string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}