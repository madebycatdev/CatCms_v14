using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Configuration;
using EuroCMS.Plugin.Tsa;
using EuroCMS.Core;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Configuration;
using EuroCMS.Model;
using System.Web.Script.Serialization;

namespace EuroCMS.Plugin.TsaPlugin
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {

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
                case "admissioninterview":
                    result = admissionInterview(context);
                    break;

                case "sendmailtouser":
                    result = SendMailToUser(context);
                    break;


            }

            context.Response.Write(result);
        }
        private string SendMailToUser(HttpContext context)
        {
            try
            {
                string result = string.Empty;
                context.Response.ContentType = "text/json";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                result = JsonConvert.SerializeObject(new SendMailResult { Code = "13", Message = "Unknown Error", Status = "NOK" });

                #region Keys & Values
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                List<string> keywords = new List<string> { "plugin", "articleid", "filename", "email" };

                keys = context.Request.Form.AllKeys.Where(x => !keywords.Contains(x)).ToList();
                foreach (string s in keys)
                {
                    values.Add(HtmlAndUrlDecode(context.Request[s]));
                }
                #endregion

                string fileName = string.Empty;
                try
                {
                    fileName = (!string.IsNullOrEmpty(context.Request.Form["filename"])) ? context.Request.Form["filename"] : string.Empty;
                }
                catch
                {
                    fileName = string.Empty;
                }

                string email = string.Empty;
                try
                {
                    email = (!string.IsNullOrEmpty(context.Request.Form["email"])) ? context.Request.Form["email"] : string.Empty;
                }
                catch
                {
                    email = string.Empty;
                }

                if (string.IsNullOrEmpty(email))
                {
                    SendMailResult sr = new SendMailResult { Code = "15", Message = "email Is Required", Status = "NOK" };
                    string jsonResult = JsonConvert.SerializeObject(sr);
                    return jsonResult;
                }

                string _articleid = (!string.IsNullOrEmpty(context.Request.Form["articleid"])) ? context.Request.Form["articleid"] : string.Empty;
                if (string.IsNullOrEmpty(_articleid))
                {
                    SendMailResult sr = new SendMailResult { Code = "10", Message = "articleid Is Required", Status = "NOK" };
                    string jsonResult = JsonConvert.SerializeObject(sr);
                    return jsonResult;
                }
                else
                {
                    int articleId = -1;
                    try
                    {
                        articleId = Convert.ToInt32(_articleid);
                    }
                    catch (Exception ex)
                    {
                        SendMailResult sr = new SendMailResult { Code = "11", Message = "articleid Is Not In The Correct Format", ErrorMessage = ex.Message, Status = "NOK" };
                        string jsonResult = JsonConvert.SerializeObject(sr);
                        return jsonResult;
                    }

                    CmsDbContext dbContext = new CmsDbContext();
                    Article arBase = dbContext.Articles.Where(x => x.Id == articleId).FirstOrDefault();
                    #region Check if correct article
                    int counter = 0;    // eğer ##key## sayısı 3 veya daha fazlaysa işlem devam edebilir :

                    foreach (string s in keys)
                    {
                        if (HtmlAndUrlDecode(arBase.Article1).Contains("##" + s + "##"))
                        {
                            counter++;
                        }
                    }

                    if (counter == 0)
                    {
                        SendMailResult sr = new SendMailResult { Code = "12", Message = "Article Is Not Correct", Status = "NOK" };
                        string jsonResult = JsonConvert.SerializeObject(sr);
                        return jsonResult;
                    }
                    else
                    {
                        //article is correct, ready to send
                        bool hasAttachment = false;
                        HttpPostedFile file = null;
                        if (context.Request.Files["attachmentfiles"] == null)
                        {
                            hasAttachment = false;
                        }

                        #region Mail Values
                        string fromAddress = HtmlAndUrlDecode(arBase.Custom1.Trim());
                        string toAddress = HtmlAndUrlDecode(email.Trim());
                        string ccAddress = HtmlAndUrlDecode(arBase.Custom3.Trim());
                        string bccAddress = HtmlAndUrlDecode(arBase.Custom4.Trim());
                        string subject = HtmlAndUrlDecode(arBase.Custom5.Trim());
                        string mailTemplate = HtmlAndUrlDecode(arBase.Article1.Trim());
                        string fromName = HtmlAndUrlDecode(arBase.Custom6.Trim());
                        #region MaxFileSize
                        int maxFileSize = -1;
                        try
                        {
                            maxFileSize = Convert.ToInt32(HtmlAndUrlDecode(arBase.Custom6.Trim()));
                        }
                        catch (Exception ex)
                        {
                            maxFileSize = -1;
                        }
                        #endregion
                        #region Accepted File Types
                        string fileTypes = HtmlAndUrlDecode(arBase.Custom7.Trim());
                        fileTypes = fileTypes.ToLower();
                        List<string> acceptedFileTypes = new List<string>();
                        if (!string.IsNullOrEmpty(fileTypes))
                        {
                            if (fileTypes.Contains(","))
                            {
                                acceptedFileTypes = fileTypes.Split(',').ToList();
                            }
                            else
                            {
                                acceptedFileTypes.Add(fileTypes);
                            }
                        }
                        #endregion

                        try
                        {
                            file = context.Request.Files["attachmentfiles"];
                            hasAttachment = !(file == null);
                        }
                        catch (Exception)
                        {
                            hasAttachment = false;
                        }

                        if (hasAttachment)
                        {
                            if (maxFileSize > 0)
                            {
                                if (file.ContentLength > maxFileSize || file.ContentLength <= 0)
                                {
                                    hasAttachment = false;
                                    SendMailResult sr = new SendMailResult { Code = "15", Message = "File size is not accepted", Status = "NOK" };
                                    string jsonResult = JsonConvert.SerializeObject(sr);
                                    return jsonResult;
                                }
                            }

                            string extension = System.IO.Path.GetExtension(file.FileName).Trim().ToLower();
                            if (!string.IsNullOrEmpty(fileTypes))
                            {
                                if (!acceptedFileTypes.Contains(extension))
                                {
                                    hasAttachment = false;
                                    SendMailResult sr = new SendMailResult { Code = "16", Message = "File extension is not accepted", Status = "NOK" };
                                    string jsonResult = JsonConvert.SerializeObject(sr);
                                    return jsonResult;
                                }
                            }

                            //still hasAttachment
                        }

                        if (hasAttachment && string.IsNullOrEmpty(fileName))
                        {
                            fileName = file.FileName;
                        }

                        Attachment attachmentFile = null;
                        if (hasAttachment)
                        {
                            attachmentFile = new Attachment(file.InputStream, file.FileName);
                        }

                        List<string> ccList = new List<string>();
                        List<string> bccList = new List<string>();
                        #region CC
                        if (ccAddress.Contains(","))
                        {
                            ccList = ccAddress.Split(',').ToList();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ccAddress))
                            {
                                ccList.Add(ccAddress);
                            }
                        }
                        #endregion
                        #region Bcc
                        if (bccAddress.Contains(","))
                        {
                            bccList = bccAddress.Split(',').ToList();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(bccAddress))
                            {
                                bccList.Add(bccAddress);
                            }
                        }
                        #endregion
                        for (int i = 0; i < keys.Count; i++)
                        {
                            if (mailTemplate.Contains("##" + keys[i] + "##"))
                            {
                                mailTemplate = mailTemplate.Replace("##" + keys[i] + "##", values[i]);
                            }
                        }
                        #endregion

                        #region Smtp
                        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                        SmtpSection smtpInfo = new SmtpSection();
                        smtpInfo = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");

                        SmtpClient smtpClient = new SmtpClient(smtpInfo.Network.Host, smtpInfo.Network.Port);
                        if (smtpInfo.Network.DefaultCredentials)
                        {
                            smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(smtpInfo.Network.ClientDomain))
                            {
                                smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                            }
                            else
                            {
                                smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password, smtpInfo.Network.ClientDomain);
                            }
                        }

                        #region Basic Authentication
                        //if (!smtpInfo.Network.DefaultCredentials)
                        //{
                        //NetworkCredential basicCredential = new NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                        //smtpClient.UseDefaultCredentials = false;// = basicCredential;
                        //}

                        #endregion
                        //smtpClient.UseDefaultCredentials = smtpInfo.Network.DefaultCredentials;
                        smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                        smtpClient.EnableSsl = smtpInfo.Network.EnableSsl;
                        smtpClient.DeliveryMethod = smtpInfo.DeliveryMethod;
                        #endregion

                        #region Mail
                        MailMessage mail = new MailMessage();
                        mail.To.Add(new MailAddress(toAddress));
                        for (int i = 0; i < ccList.Count; i++)
                        {
                            mail.CC.Add(ccList[i]);
                        }
                        for (int i = 0; i < bccList.Count; i++)
                        {
                            mail.Bcc.Add(bccList[i]);
                        }
                        mail.From = new MailAddress(fromAddress, (string.IsNullOrEmpty(fromName) ? fromAddress : fromName));
                        mail.Subject = subject;
                        if (hasAttachment && attachmentFile != null)
                        {
                            mail.Attachments.Add(attachmentFile);
                        }
                        mail.IsBodyHtml = true;

                        mail.Body = (mailTemplate.Contains("<html>")) ? mailTemplate : "<html><head></head><body>" + mailTemplate + "</body></html>";
                        smtpClient.Send(mail);
                        #region Mail Sent
                        SendMailResult srFinal = new SendMailResult { Code = "100", Message = "Success", Status = "OK" };
                        string jsonResultFinal = JsonConvert.SerializeObject(srFinal);
                        return jsonResultFinal;
                        #endregion

                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                //, ErrorMessage = ex.Message + " - " + ex.StackTrace
                SendMailResult sr = new SendMailResult { Code = "14", Message = "Process Failed", ErrorMessage = "Send mail exception", Status = "NOK" };
                CmsHelper.SaveErrorLog(ex, "Cannot send mail", false);
                string jsonResult = JsonConvert.SerializeObject(sr);
                return jsonResult;
            }
        }
        public string admissionInterview(HttpContext context)
        {
            string apiKey = ConfigurationManager.AppSettings["SmartClassApi-Key"];
            string apiUrl = ConfigurationManager.AppSettings["SmartClassApi-Url"];
            string schoolId = "", name = "", lastName = "", phone = "", email = "", course = "", birthDate = "", seasonId = "tsa_2021_2022";


            if (!string.IsNullOrEmpty(context.Request.Form["seasonId"]))
            {
                seasonId = context.Request.Form["seasonId"].Trim();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["schoolid"]))
            {
                schoolId = context.Request.Form["schoolid"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "schoolid alanı boş gönderilemez", data = "" });
            }

            if (!string.IsNullOrEmpty(context.Request.Form["ad"]))
            {
                name = context.Request.Form["ad"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "ad alanı boş gönderilemez", data = "" });
            }

            if (!string.IsNullOrEmpty(context.Request.Form["soyad"]))
            {
                lastName = context.Request.Form["soyad"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "soyad alanı boş gönderilemez", data = "" });
            }
            if (!string.IsNullOrEmpty(context.Request.Form["telefon"]))
            {
                phone = context.Request.Form["telefon"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "telefon alanı boş gönderilemez", data = "" });
            }
            if (!string.IsNullOrEmpty(context.Request.Form["eposta"]))
            {
                email = context.Request.Form["eposta"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "eposta alanı boş gönderilemez", data = "" });
            }
            if (!string.IsNullOrEmpty(context.Request.Form["mesaj"]))
            {
                course = context.Request.Form["mesaj"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "course alanı boş gönderilemez", data = "" });
            }
            if (!string.IsNullOrEmpty(context.Request.Form["mesaj"]))
            {
                course = context.Request.Form["mesaj"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "course alanı boş gönderilemez", data = "" });
            }
            if (!string.IsNullOrEmpty(context.Request.Form["dogum"]))
            {
                birthDate = context.Request.Form["dogum"].Trim();
            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "birthDate alanı boş gönderilemez", data = "" });
            }


            dynamic parammeters = new
            {
                schoolId = schoolId,
                cellPhone = phone,
                emailAddress = email,
                courseName = course,
                school = Convert.ToInt32(schoolId)
            };


            var response = HttpRequestHelper.Request(apiUrl + "/admission_interviews", "POST", parammeters, schoolId, seasonId, apiKey);
            dynamic interviewInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);


            if (interviewInfo.id != null)
            {
                dynamic stdParammeters = new
                {
                    interview = Convert.ToInt32(interviewInfo.id.ToString()),
                    nameLastname = name + " " + lastName,
                    cellPhone = phone,
                    emailAddress = email,
                    birthDate = birthDate + "-01-01",//DateTime.Now.AddYears(-3).ToString("yyyy-MM-dd"),
                    school = Convert.ToInt32(schoolId),
                    schoolId = schoolId,
                    registeredAt = DateTime.Now
                };

                var responseStd = HttpRequestHelper.Request(apiUrl + "/admission_interview_students", "POST", stdParammeters, schoolId, seasonId, apiKey);
                dynamic interviewInfoStd = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseStd);

                if (interviewInfoStd.id != null)
                {
                    dynamic stNotedParammeters = new
                    {
                        interviewId = Convert.ToInt32(interviewInfo.id.ToString()),
                        processType = 0,
                        studentInterviewId = 0,
                        notes = course,
                        studentId = interviewInfoStd.id,
                        schoolId = Convert.ToInt32(schoolId),
                    };

                    var responseStdNote = HttpRequestHelper.Request(apiUrl + "/admission_interview_notes", "POST", stNotedParammeters, schoolId, seasonId, apiKey);
                    dynamic interviewInfoStdNote = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseStdNote);

                    return JsonConvert.SerializeObject(new { status = true, message = "success", data = response, data2 = responseStd, data3 = responseStdNote });
                }
            }

            return JsonConvert.SerializeObject(new { status = false, message = "failed", data = response });
            //return apiUrl + "/admission_interviews" + "-" +response;//JsonConvert.SerializeObject(new { status = false, message = "failed", data  = response });
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
        }
        public class SendMailResult
        {
            public string Code { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}