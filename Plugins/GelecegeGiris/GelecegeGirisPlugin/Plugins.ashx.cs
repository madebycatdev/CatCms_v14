using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.GelecegeGiris
{
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        String PictureName = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond) + "_";

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
                case "gelecegebasvur":
                    result = GelecegeBasvur(context);
                    break;
            }
            context.Response.Write(result);
        }

        private string GelecegeBasvur(HttpContext context)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string applyChoice = "";
                    string department = "";

                    if (chckIs.Checked == true)
                    {
                        applyChoice = "İş";
                    }
                    else if (chckStaj.Checked == true)
                    {
                        foreach (var control in checkboxesStaj.Controls)
                        {
                            if (control is CheckBox)
                            {
                                var checkBox = (CheckBox)control;
                                if (checkBox.Checked)
                                {
                                    applyChoice += checkBox.Text + ",";
                                }
                            }
                        }
                        applyChoice = applyChoice.Substring(0, applyChoice.Length - 1);
                    }
                    else
                    {
                        applyChoice = "Belirtilmedi";
                    }

                    foreach (var controls in checkboxesDepartment.Controls)
                    {
                        if (controls is CheckBox)
                        {
                            var checkBox = (CheckBox)controls;
                            if (checkBox.Checked)
                            {
                                department += checkBox.Text + ",";
                            }
                        }
                    }
                    department = department.Substring(0, department.Length - 1);

                    string filePath = FileUpload();
                    SendEmail(txtmail.Value);
                    if (filePath != null)
                    {
                        Voter voter = new Voter();
                        int voterID = voter.AddVoter(814, 46113, txtAdSoyad.Value, Convert.ToString(context.Request.UserHostAddress));
                        voter.AddVoter(voterID, 814, 46115, txtmail.Value);
                        voter.AddVoter(voterID, 814, 46117, txtCepTel.Value);
                        voter.AddVoter(voterID, 814, 46118, ddlOkul.SelectedItem.Text);
                        voter.AddVoter(voterID, 814, 46119, txtBolum.Value);
                        voter.AddVoter(voterID, 814, 46114, ddlSinif.SelectedItem.Text);
                        voter.AddVoter(voterID, 814, 46116, nedenDT.Value);
                        voter.AddVoter(voterID, 814, 46120, applyChoice);
                        //voter.AddCoter(voterID, 814, 46112, StajYapmakİstediğiAlanlar);
                        voter.AddVoter(voterID, 814, 46122, referansAdSoyad.Value);
                        voter.AddVoter(voterID, 814, 46121, filePath);
                        //voter.AddVoter(voterID, 814, 42852, department);
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }

        private string FileUpload(HttpContext context)
        {
            string unaccentedText = String.Join("", FileUpload1.FileName.Normalize(NormalizationForm.FormD)
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace(" ", "_");

            string uploadedFilePath = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + "/files/" + PictureName + unaccentedText;
            string savedFilePath = context.Server.MapPath("/files/") + PictureName + unaccentedText;

            try
            {
                String ftpfullpath = "ftp://192.168.75.137/www.gelecegegiris.com/files/" + PictureName + unaccentedText;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpfullpath));
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                ftp.Timeout = 600000;
                ftp.ReadWriteTimeout = 600000;
                ftp.Credentials = new NetworkCredential("websites", "QwEdCxZaS!");
                ftp.UsePassive = true;
                ftp.UseBinary = true;
                ftp.KeepAlive = false;
                Stream requestStream = ftp.GetRequestStream();
                const int bufferLength = 5120;
                int readBytes = 0;
                byte[] buffer = new byte[bufferLength];
                do
                {
                    readBytes = FileUpload1.PostedFile.InputStream.Read(buffer, 0, bufferLength);
                    requestStream.Write(buffer, 0, readBytes);
                }
                while (readBytes != 0);
                requestStream.Close();
                return uploadedFilePath;
            }

            catch (Exception hata)
            {
                context.Response.Write(hata.Message);
            }
            return null;
        }

        public void SendEmail(string to)
        {
            try
            {
                SmtpClient _client = new SmtpClient("dtekedgev2.fw.dteknoloji.com.tr");
                MailMessage mail = new MailMessage();
                _client.Port = 587;
                mail.From = new MailAddress("Gelecege.Giris@d-teknoloji.com.tr");
                mail.To.Add(new MailAddress(to));
                mail.Subject = "Doğuş Teknoloji Geleceğe Giriş Programı - Başvurun Bize Ulaştı";
                mail.Body = "Merhaba, <br/><br/>";
                mail.Body = mail.Body + "Şirketimize göstermiş olduğun ilgiye teşekkür ederiz. <br/><br/>";
                mail.Body = mail.Body + "Geleceğe Giriş programımız için başvurun bize ulaştı. Program değerlendirme sürecimiz <strong>2 Mayıs 2019</strong> tarihi itibari ile başlayacaktır. Aranan niteliklerimizin örtüşmesi durumunda , değerlendirme süreci için seninle iletişime geçeceğimizi belirtir, şimdiden başarı dolu bir okul hayatı dileriz. <br/><br/>";
                mail.Body = mail.Body + "Doğuş Teknoloji İnsan Kaynakları";
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                _client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}