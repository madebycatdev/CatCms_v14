using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;

namespace EuroCMS.Core
{
    #region Send Mail
    public static class MailSender
    {
        public static void Send(string recipients, string subject, string message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(recipients);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
        }

        public static void Send(string from, string recipients, string subject, string message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(from, recipients, subject, message);
            }
        }


        public static string SendTest()
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
            return "";
        }


        public static MailResult SendMail(string to, string cc, string bcc, string subject, string body, Attachment attachment)
        {
            var result = new MailResult();
            try
            {
               //MailMessage mails = new MailMessage("web@kale.com.tr", "ufuk.ates@d-teknoloji.com.tr");
               //
               //
               //SmtpClient client = new SmtpClient();
               //client.Dispose();
               //client = new SmtpClient();
               //client.Port = 587;
               //
               //client.UseDefaultCredentials = false;
               //client.EnableSsl = true;
               //client.Credentials = new NetworkCredential("web@kale.com.tr", "kqdVd2DL");
               //client.DeliveryMethod = SmtpDeliveryMethod.Network;
               //client.Host = "smtp.gmail.com";
               //mails.Subject = "this is a test email.";
               //mails.Body = "this is my test email body";
               //client.Send(mails);





                #region Smtp
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                SmtpSection smtpInfo = new SmtpSection();
                smtpInfo = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");

                //SmtpClient smtpClient = new SmtpClient(smtpInfo.Network.Host, smtpInfo.Network.Port);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Port = smtpInfo.Network.Port;
                smtpClient.UseDefaultCredentials = smtpInfo.Network.DefaultCredentials;
                smtpClient.EnableSsl = smtpInfo.Network.EnableSsl;

                if (smtpInfo.Network.DefaultCredentials)
                {
                    smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else
                {
                    if (string.IsNullOrEmpty(smtpInfo.Network.ClientDomain))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                    }
                    else
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password, smtpInfo.Network.ClientDomain);
                    }
                }
                smtpClient.DeliveryMethod = smtpInfo.DeliveryMethod;
                smtpClient.Host = smtpInfo.Network.Host;

                #endregion

                #region mail
                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                #region cc,bcc,attachment

                if (cc != null && !string.IsNullOrEmpty(cc))
                {
                    mail.CC.Add(cc);

                }
                if (bcc != null && !string.IsNullOrEmpty(bcc))
                {
                    mail.Bcc.Add(bcc);

                }
                if (attachment != null)
                {
                    mail.Attachments.Add(attachment);

                }
                #endregion
                mail.From = new MailAddress(smtpInfo.From);
                mail.Subject = subject;
                mail.IsBodyHtml = true;

                mail.Body = (body.Contains("<html>") ? body : "<html><head></head><body>" + body + "</body></html>");
                smtpClient.Send(mail);
                #endregion

                result.status = true;
                result.message = "İşlem Başarılı.";
                return result;
            }
            catch (Exception ex)
            {
                result.status = false;
                result.message = ex.Message +" " + ex.InnerException;
                return result;
            }
        }
    }
    #endregion

    public class MailResult
    {
        public bool status { get; set; }

        public string message { get; set; }
    }
}
