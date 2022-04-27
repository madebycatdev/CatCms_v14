using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace EuroCMS.FrontEnd.p
{
    /// <summary>
    /// Summary description for WebMethods
    /// </summary>
    public class WebMethods : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string methodName = "";

            object result = "";

            if (!string.IsNullOrEmpty(context.Request.Form["methodname"]))
            {
                methodName = context.Request.Form["methodname"].ToLower().Trim();
            }

            if (string.IsNullOrEmpty(methodName))
            {
                context.Response.Write("");
                context.Response.End();
            }

            switch (methodName)
            {
                case "ebulten":
                    result = EBulten(context);
                    break;
                case "contactform":
                    result = ContactForm(context);
                    break;
                default:
                    break;
            }

            context.Response.Write(result.ToString());
            context.Response.End();
        }


        public string EBulten(HttpContext context)
        {
            string emailAddress = "";
            string resultMailInfo = "", resultMailClass = "";
            StringBuilder sb = new StringBuilder();


            if (!string.IsNullOrEmpty(context.Request.Form["scc"]))
            {
                //return ContactFormStringToJson("Geçersiz işlem.", "errorMailResult");
                resultMailInfo = "Geçersiz işlem.";
                resultMailClass = "errorMailResult";
            }
            else
            {
                if (string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    resultMailInfo = "E-Posta adresi geçersiz.";
                    resultMailClass = "errorMailResult";
                }
                else
                {
                    emailAddress = context.Request.Form["email"].Trim();
                    emailAddress = HttpUtility.HtmlDecode(emailAddress);
                    emailAddress = HttpUtility.UrlDecode(emailAddress);
                    if (!CmsHelper.IsValidEmail(emailAddress))
                    {
                        resultMailInfo = "E-Posta adresi geçersiz.";
                        resultMailClass = "errorMailResult";
                    }
                    else
                    {
                        emailAddress = CmsHelper.NoInj(emailAddress).ToLower().Trim();
                        CmsDbContext dbContext = new CmsDbContext();
                        STFEmail getEmail = new STFEmail();
                        getEmail = dbContext.STFEmails.Where(s => s.FromEmail == emailAddress).FirstOrDefault();

                        if (getEmail != null)
                        {
                            resultMailInfo = "E-Posta adresiniz sistemde bulunmaktadır.";
                            resultMailClass = "errorMailResult";
                        }
                        else
                        {
                            STFEmail insertMail = new STFEmail();
                            insertMail.FromEmail = emailAddress;
                            dbContext.STFEmails.Add(insertMail);
                            dbContext.SaveChanges();
                            resultMailInfo = "E-Posta adresiniz başarıyla eklendi.";
                            resultMailClass = "successMailResult";
                        }
                    }
                }
            }

            sb.Append("[{");
            sb.Append("\"");
            sb.Append("mailInfo");
            sb.Append("\"");
            sb.Append(":");
            sb.Append("\"");
            sb.Append(resultMailInfo);
            sb.Append("\"");
            sb.Append(",");
            sb.Append("\"");
            sb.Append("mailClass");
            sb.Append("\"");
            sb.Append(":");
            sb.Append("\"");
            sb.Append(resultMailClass);
            sb.Append("\"");
            sb.Append("}]");

            return sb.ToString();
        }


        public string ContactForm(HttpContext context)
        {
            string errorMessage = "";

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["scc"]))
                {
                    return ContactFormStringToJson("Geçersiz işlem.", "errorContactForm");
                }

                string name = "", email = "", phone = "", fax = "", address = "", information = "", isCustomer = "", customerNumber = "", foundationTitle = "", reason = "", subject = "", message = "";
                string resultInfo = "", resultClass = "";
                int articleId = 24;

                name = context.Request.Form["adSoyad"] == null ? "" : context.Request.Form["adSoyad"].ToString().Trim();
                email = context.Request.Form["email"] == null ? "" : context.Request.Form["email"].ToString().Trim();
                phone = context.Request.Form["telefon"] == null ? "" : context.Request.Form["telefon"].ToString().Trim();
                fax = context.Request.Form["fax"] == null ? "" : context.Request.Form["fax"].ToString().Trim();
                address = context.Request.Form["adres"] == null ? "" : context.Request.Form["adres"].ToString().Trim();
                information = context.Request.Form["guncel-bilgi-alma"] == null ? "" : context.Request.Form["guncel-bilgi-alma"].ToString().Trim();
                isCustomer = context.Request.Form["musteri"] == null ? "" : context.Request.Form["musteri"].ToString().Trim();
                customerNumber = context.Request.Form["musteriNumarasi"] == null ? "" : context.Request.Form["musteriNumarasi"].ToString().Trim();
                foundationTitle = context.Request.Form["kurumUnvani"] == null ? "" : context.Request.Form["kurumUnvani"].ToString().Trim();
                reason = context.Request.Form["yazmaNedeni"] == null ? "" : context.Request.Form["yazmaNedeni"].ToString().Trim();
                subject = context.Request.Form["yazmaKonusu"] == null ? "" : context.Request.Form["yazmaKonusu"].ToString().Trim();
                message = context.Request.Form["mesaj"] == null ? "" : context.Request.Form["mesaj"].ToString().Trim();

                name = EuroCMS.Core.CmsHelper.NoInj(name);
                email = EuroCMS.Core.CmsHelper.NoInj(email);
                phone = EuroCMS.Core.CmsHelper.NoInj(phone);
                fax = EuroCMS.Core.CmsHelper.NoInj(fax);
                address = EuroCMS.Core.CmsHelper.NoInj(address);
                information = EuroCMS.Core.CmsHelper.NoInj(information);
                isCustomer = EuroCMS.Core.CmsHelper.NoInj(isCustomer);
                customerNumber = EuroCMS.Core.CmsHelper.NoInj(customerNumber);
                foundationTitle = EuroCMS.Core.CmsHelper.NoInj(foundationTitle);
                reason = EuroCMS.Core.CmsHelper.NoInj(reason);
                subject = EuroCMS.Core.CmsHelper.NoInj(subject);
                message = EuroCMS.Core.CmsHelper.NoInj(message);

                name = HtmlAndUrlDecode(name);
                email = HtmlAndUrlDecode(email);
                phone = HtmlAndUrlDecode(phone);
                fax = HtmlAndUrlDecode(fax);
                address = HtmlAndUrlDecode(address);
                information = HtmlAndUrlDecode(information);
                isCustomer = HtmlAndUrlDecode(isCustomer);
                customerNumber = HtmlAndUrlDecode(customerNumber);
                foundationTitle = HtmlAndUrlDecode(foundationTitle);
                reason = HtmlAndUrlDecode(reason);
                subject = HtmlAndUrlDecode(subject);
                message = HtmlAndUrlDecode(message);

                errorMessage = "Buraya geldi 0";

                string subjectText = "";
                string reasonText = "";

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(isCustomer) || string.IsNullOrEmpty(foundationTitle) || string.IsNullOrEmpty(message))
                {
                    return ContactFormStringToJson("Lütfen gerekli alanları doldurunuz.", "errorContactForm");
                }

                if (!isCustomer.Contains("no"))
                {
                    if (string.IsNullOrEmpty(customerNumber))
                    {
                        return ContactFormStringToJson("Lütfen müşteri numaranızı giriniz.", "errorContactForm");
                    }
                }

                if (!EuroCMS.Core.CmsHelper.IsValidEmail(email))
                {
                    return ContactFormStringToJson("Lütfen geçerli bir e-posta adresi giriniz.", "errorContactForm"); ;
                }

                errorMessage = "Buraya geldi -1";

                List<int> listReasonType = new List<int>();
                listReasonType.Add(1);
                listReasonType.Add(2);
                listReasonType.Add(3);

                if (!listReasonType.Contains(Convert.ToInt32(reason)))
                {
                    return ContactFormStringToJson("Lütfen yazma nedeninizi giriniz.", "errorContactForm");
                }

                reasonText = ContactFormGetReasonText(reason);

                CmsDbContext dbContext = new CmsDbContext();

                Article getArticle = new Article();
                getArticle = dbContext.Articles.Where(a => a.Id == articleId).FirstOrDefault();

                if (getArticle == null)
                {
                    return ContactFormStringToJson("Article bulunamadı.", "errorContactForm");
                }

                int mailTemplateArticleId;

                string mailFrom = "", mailToTSKBKredileri = "", mailToHalkaArz = "", mailToSurdurulebilirlik = "", mailToYatirimciIliskileri = "", mailToArastirma = "", mailToSatilikGayrimenkul = "", mailToDiger = "", mailToSikayet = "", mailCC = "", mailBCC = "";

                mailTemplateArticleId = Convert.ToInt32(getArticle.Custom1);

                mailFrom = getArticle.Custom2.Trim();
                mailToTSKBKredileri = getArticle.Custom3.Trim();
                mailToHalkaArz = getArticle.Custom4.Trim();
                mailToSurdurulebilirlik = getArticle.Custom5.Trim();
                mailToYatirimciIliskileri = getArticle.Custom6.Trim();
                mailToArastirma = getArticle.Custom7.Trim();
                mailToSatilikGayrimenkul = getArticle.Custom8.Trim();
                mailToDiger = getArticle.Custom9.Trim();
                mailToSikayet = getArticle.Custom10.Trim();
                mailCC = getArticle.Custom11.Trim();
                mailBCC = getArticle.Custom12.Trim();

                //Article getMailTemplateArticle = new Article();
                //getArticle = dbContext.Articles.Where(a => a.Id == articleId).FirstAllDefault();

                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                SmtpSection smtpInfo = new SmtpSection();
                smtpInfo = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");




                SmtpClient smtpClient = new SmtpClient(smtpInfo.Network.Host, smtpInfo.Network.Port);
                if (smtpInfo.Network.DefaultCredentials)
                {

                    smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials; //new System.Net.NetworkCredential("", "");
                }
                else
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                }

                smtpClient.UseDefaultCredentials = smtpInfo.Network.DefaultCredentials;
                smtpClient.EnableSsl = smtpInfo.Network.EnableSsl;
                smtpClient.DeliveryMethod = smtpInfo.DeliveryMethod;


                //smtpClient.SendCompleted += new SendCompletedEventHandler(ContactFormSendCompletedCallback);
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();



                string mailTo = "";
                List<string> listMailTo = new List<string>();

                errorMessage = "Buraya geldi 1";

                int intSubject = Convert.ToInt32(subject);

                if (reason == "3")
                {
                    if (mailToSikayet.Contains(","))
                    {
                        List<string> listMailToSikayet = mailToSikayet.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        for (int i = 0; i < listMailToSikayet.Count(); i++)
                        {
                            //mail.To.Add(new MailAddress(listMailToSikayet[i]));
                            mail.To.Add(listMailToSikayet[i].Trim());
                        }
                    }
                    else
                    {
                        //mail.To.Add(new MailAddress(mailToSikayet));
                        mail.To.Add(mailToSikayet.Trim());
                    }
                }
                else
                {
                    switch (intSubject)
                    {

                        case 1:
                            mailTo = mailToTSKBKredileri;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("kurumsalpazmd@tskb.com.tr"));
                            break;

                        case 2:
                            mailTo = mailToHalkaArz;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("kurumsal.finansman@tskb.com.tr"));
                            break;

                        case 3:
                            mailTo = mailToSurdurulebilirlik;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("SURDURULEBILIRLIK_KOMITESI@tskb.com.tr@tskb.com.tr"));
                            break;

                        case 4:
                            mailTo = mailToYatirimciIliskileri;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("ir@tskb.com.tr"));
                            break;

                        case 5:
                            mailTo = mailToArastirma;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("ekonomikarastirmalar@tskb.com.tr"));
                            break;

                        case 6:
                            mailTo = mailToSatilikGayrimenkul;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("krediizleme@tskb.com.tr"));
                            //mail.To.Add(new MailAddress("krediler@tskb.com.tr"));
                            break;

                        case 7:
                            mailTo = mailToDiger;
                            if (mailTo.Contains(","))
                            {
                                listMailTo = mailTo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < listMailTo.Count(); i++)
                                {
                                    //mail.To.Add(new MailAddress(listMailTo[i]));
                                    mail.To.Add(listMailTo[i].Trim());
                                }
                            }
                            else
                            {
                                //mail.To.Add(new MailAddress(mailTo));
                                mail.To.Add(mailTo.Trim());
                            }
                            //mail.To.Add(new MailAddress("info@tskb.com.tr"));
                            break;
                        default:
                            mailTo = "tahsin.sarikaya2a@hotmail.com,tahsin.sarikaya@hotmail.com";
                            break;
                    }
                }
                if (mailCC.Contains(","))
                {
                    List<string> listMailCC = mailCC.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    for (int i = 0; i < listMailCC.Count(); i++)
                    {
                        mail.CC.Add(listMailCC[i].Trim());
                    }
                }
                else
                {
                    mail.CC.Add(mailCC.Trim());
                }


                subjectText = ContactFormGetSubjectText(subject);

                if (string.IsNullOrEmpty(subjectText))
                {
                    return ContactFormStringToJson("Yazma konusu bulanamadı. Lütfen tekrar deneyiniz.", "errorContactForm");
                }



                // Body Render Start

                Article getMailTemplateArticle = new Article();
                getMailTemplateArticle = dbContext.Articles.Where(a => a.Id == mailTemplateArticleId).FirstOrDefault();

                if (getMailTemplateArticle == null)
                {
                    return ContactFormStringToJson("Template article bulunamadı.", "errorContactForm"); ;
                }

                errorMessage = "Buraya geldi 2 Mail: " + mailFrom;
                reasonText = ContactFormGetReasonText(reason.Trim());
                string body = "";

                body = "<html><head></head><body>" + HttpUtility.HtmlDecode(getMailTemplateArticle.Article1) + "</body></html>";
                body = body.Replace("##adSoyad##", name);
                body = body.Replace("##email##", email);
                body = body.Replace("##telefon##", phone);
                body = body.Replace("##fax##", fax);
                body = body.Replace("##adres##", address);
                body = body.Replace("##guncel-bilgi-alma##", information);
                body = body.Replace("##musteri##", (isCustomer.Contains("no") ? "Hayır" : "Evet"));
                body = body.Replace("##musteriNumarasi##", customerNumber);
                body = body.Replace("##kurumUnvani##", foundationTitle);
                body = body.Replace("##yazmaNedeni##", reasonText);
                body = body.Replace("##yazmaKonusu##", subjectText);
                body = body.Replace("##mesaj##", message);
                // Body Render End

                if (string.IsNullOrEmpty(mailTo))
                {
                    mail.To.Add(mailTo);
                }

                if (!string.IsNullOrEmpty(mailBCC))
                {
                    mail.Bcc.Add(mailBCC);
                }

                if (!string.IsNullOrEmpty(mailCC))
                {
                    mail.CC.Add(mailCC);
                }

                mail.Subject = subjectText;
                mail.Body = body;
                mail.From = new MailAddress(mailFrom);
                mail.IsBodyHtml = true;
                errorMessage = "Buraya geldi 3";

                smtpClient.Send(mail);

                errorMessage = "Buraya geldi 4";

                // DB Insert start
                CustomForm customForm = new CustomForm();
                customForm.Ip = EuroCMS.Core.CmsHelper.GetCurrentIP();
                customForm.ToMail = mailFrom; //string.Join(",", mail.To.Select(x => x.Address.ToString()).ToArray());
                customForm.Opinion = HttpUtility.HtmlEncode(body);
                customForm.Subject = subjectText;
                customForm.SenderMail = email;

                string surname = "";
                string cacheName = name.Trim().Replace(" ", ";");
                if (cacheName.Contains(";"))
                {
                    string[] nameSurname = cacheName.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (nameSurname.Count() > 1)
                    {
                        for (int i = 0; i < nameSurname.Count(); i++)
                        {
                            if (i == 0)
                            {
                                customForm.SenderName = nameSurname[i];
                            }
                            else
                            {
                                surname += nameSurname[i] + " ";
                            }
                        }
                        surname = surname.Trim();
                        customForm.SenderSurname = surname;
                    }
                    else
                    {
                        customForm.SenderName = name;
                    }
                }
                else
                {
                    customForm.SenderName = name;
                }

                customForm.SenderPhone = phone;

                dbContext.CustomForms.Add(customForm);
                dbContext.SaveChanges();
                // DB Insert end

                return ContactFormStringToJson("Form başarıyla gönderildi.", "successContactForm");
            }
            catch (Exception ex)
            {
                return ContactFormStringToJson("Form gönderilirken bir hata oluştu. Lütfen tekrar deneyin. Hata: " + ex.Message + " Detay: " + errorMessage, "errorContactForm");
            }

        }
        string ContactFormGetSubjectText(string subjectId)
        {
            string returnVal = "";

            switch (subjectId)
            {
                case "1":
                    returnVal = "TSKB Kredileri";
                    break;
                case "2":
                    returnVal = "Halka arz/birleşme/satın alma danışmanlığı";
                    break;
                case "3":
                    returnVal = "Sürdürülebilirlik ve çevre";
                    break;
                case "4":
                    returnVal = "Yatırımcı İlişkileri/TSKB Pay Senedi";
                    break;
                case "5":
                    returnVal = "Araştırma raporları";
                    break;
                case "6":
                    returnVal = "Satılık Gayrimenkuller";
                    break;
                case "7":
                    returnVal = "Diğer";
                    break;
                default:
                    returnVal = "";
                    break;
            }


            return returnVal;
        }
        string ContactFormGetReasonText(string reasonId)
        {
            string returnVal = "";

            switch (reasonId)
            {
                case "1":
                    returnVal = "Soru/Bilgi Talebi";
                    break;
                case "2":
                    returnVal = "Öneri/Görüş";
                    break;
                case "3":
                    returnVal = "Şikayet (*)";
                    break;
                default:
                    returnVal = "";
                    break;
            }
            return returnVal;
        }

        private static void ContactFormSendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                HttpContext.Current.Session["ContactFormSendCompleteResult"] = "Cancelled";
            }
            if (e.Error != null)
            {
                HttpContext.Current.Session["ContactFormSendCompleteResult"] = "Hata: " + e.Error.ToString();
            }
            else
            {
                HttpContext.Current.Session["ContactFormSendCompleteResult"] = "Success";
            }
        }
        
        string ContactFormStringToJson(string infoText, string classText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[{");
            sb.Append("\"");
            sb.Append("resultText");
            sb.Append("\"");
            sb.Append(":");
            sb.Append("\"");
            sb.Append(infoText);
            sb.Append("\"");
            sb.Append(",");
            sb.Append("\"");
            sb.Append("resultClass");
            sb.Append("\"");
            sb.Append(":");
            sb.Append("\"");
            sb.Append(classText);
            sb.Append("\"");
            sb.Append("}]");
            return sb.ToString();
        }

        string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.HtmlDecode(value).Trim();
            value = HttpUtility.UrlDecode(value).Trim();
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