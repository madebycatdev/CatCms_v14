using EuroCMS.Core;
using EuroCMS.Model;
using EuroCMS.Plugin.StandardProfil.Models;
using GTCaptchaSolution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.StandardProfil
{
    /// <summary>
    /// Summary description for investorplugin
    /// </summary>
    public class investorplugin : IHttpHandler, IRequiresSessionState
    {
        JavaScriptSerializer jss = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
        string lang = "en";
        public void ProcessRequest(HttpContext context)
        {
            string plugin = string.Empty;

            string result;

            if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
            {
                lang = context.Request.Form["lang"].ToLower().Trim();
            }

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
                case "sendmail":
                    result = sendMail(context);
                    break;
                case "captcha":
                    result = Captcha(context);
                    break;
                case "login":
                    result = login(context);
                    break;
                case "logout":
                    result = logout(context);
                    break;
                case "userislogged":
                    result = userislogged(context);
                    break;
                case "register":
                    result = register(context);
                    break;

                default:
                    result = jss.Serialize(new { status = false, message = "plugin geçersiz", data = "" });
                    break;

            }


            context.Response.Write(result);
        }


        public string sendMail(HttpContext context)
        {
            try
            {
                string result = string.Empty;
                context.Response.ContentType = "text/json";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                result = jss.Serialize(new { status = false, message = "UnknownError", data = "" });

                string isValidCaptcha = ValidateCaptcha(context);

                if (!string.IsNullOrEmpty(isValidCaptcha))
                    return isValidCaptcha;

                string pluginName = context.Request.Form["plugin"].Trim().ToLower();

                #region Keys & Values
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                List<string> keywords = new List<string> { "plugin", "articleid" };

                keys = context.Request.Form.AllKeys.Where(x => !keywords.Contains(x)).ToList();
                foreach (string s in keys)
                {
                    values.Add(HtmlAndUrlDecode(context.Request[s]));
                }
                #endregion

                string _articleid = (!string.IsNullOrEmpty(context.Request.Form["articleid"])) ? context.Request.Form["articleid"] : string.Empty;
                if (string.IsNullOrEmpty(_articleid))
                {
                    return jss.Serialize(new { status = false, message = "ArticleIdRequired", data = "" });
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
                        return jss.Serialize(new { status = false, message = "ArticleIdRequired", data = "" });
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

                    if (counter <= 0)
                    {
                        return jss.Serialize(new { status = false, message = "ArticleIsNotCorrect", data = "" });
                    }
                    else
                    {

                        #region Mail Values
                        string fromAddress = HtmlAndUrlDecode(arBase.Custom1.Trim());
                        string toAddress = HtmlAndUrlDecode(arBase.Custom2.Trim());
                        string ccAddress = HtmlAndUrlDecode(arBase.Custom3.Trim());
                        string bccAddress = HtmlAndUrlDecode(arBase.Custom4.Trim());
                        string subject = HtmlAndUrlDecode(arBase.Custom5.Trim());
                        string mailTemplate = HtmlAndUrlDecode(arBase.Article1.Trim());


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

                        #region Mail
                        var mailResult = MailSender.SendMail(toAddress, string.Join(",", ccList), string.Join(",", bccList), subject, mailTemplate, null);
                        if (!mailResult.status)
                        {
                            return jss.Serialize(new { status = false, message = mailResult.message, data = "" });
                        }

                        return jss.Serialize(new { status = true, message = "Success", data = "" });

                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Cannot send mail", false);
                return jss.Serialize(new { status = false, message = "send mail exception", data = "" }); //ex.Message + " " + ex.StackTrace
            }
        }
        public string userislogged(HttpContext context)
        {
            string email = "", token = "", pluginNotValidMessage = "";
            if (context.Request.Form["email"] != null)
            {
                email = context.Request.Form["email"].Trim();
                email = HtmlAndUrlDecode(email);
            }
            else
            {
                pluginNotValidMessage = string.Concat(pluginNotValidMessage, "NullEmail");
            }

            if (context.Request.Form["token"] != null)
            {
                token = context.Request.Form["token"].Trim();
                token = HtmlAndUrlDecode(token);
            }
            else
            {
                pluginNotValidMessage = string.Concat(pluginNotValidMessage, "NullEmail");
            }

            if (!string.IsNullOrEmpty(pluginNotValidMessage))
                return jss.Serialize(new { status = false, message = pluginNotValidMessage, data = "" });

            using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
            {
                var user = dbContext.InvestorUsers.FirstOrDefault(x => x.Email == email && x.Status == (int)UserStatus.Approved);
                if (user == null)
                    return jss.Serialize(new { status = false, message = "UserNotFound", data = "" });
                else if (user.LoginToken != token || user.LastLoginDate.Value.AddMinutes(30) < DateTime.Now)
                {
                    user.LoginToken = null;
                    dbContext.SaveChanges();

                    return jss.Serialize(new { status = false, message = "UserisNotLogin", data = "" });
                }

                return jss.Serialize(new { status = true, message = "UserisLogin", data = "" });
            }
        }
        public string logout(HttpContext context)
        {

            string email = "", token = "", pluginNotValidMessage = "";
            if (context.Request.Form["email"] != null)
            {
                email = context.Request.Form["email"].Trim();
                email = HtmlAndUrlDecode(email);
            }
            else
            {
                pluginNotValidMessage = string.Concat(pluginNotValidMessage, "NullEmail");
            }

            if (context.Request.Form["token"] != null)
            {
                token = context.Request.Form["token"].Trim();
                token = HtmlAndUrlDecode(token);
            }
            else
            {
                pluginNotValidMessage = string.Concat(pluginNotValidMessage, "NullEmail");
            }

            if (!string.IsNullOrEmpty(pluginNotValidMessage))
                return jss.Serialize(new { status = false, message = pluginNotValidMessage, data = "" });

            using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
            {
                var user = dbContext.InvestorUsers.FirstOrDefault(x => x.Email == email && x.Status == (int)UserStatus.Approved);
                if (user == null)
                    return jss.Serialize(new { status = false, message = "UserNotFound", data = "" });

                else if (user.LoginToken != token)
                {
                    return jss.Serialize(new { status = false, message = "UserisNotLogin", data = "" });
                }

                user.LoginToken = string.Empty;
                dbContext.SaveChanges();

                return jss.Serialize(new { status = true, message = "Success", data = "" });
            }
        }
        public string register(HttpContext context)
        {
            string email = "", name = "", surname = "", companyname = "";

            string pluginNotValidMessage = "";


            try
            {
                string isValidCaptcha = ValidateCaptcha(context);
                if (!string.IsNullOrEmpty(isValidCaptcha))
                {
                    return jss.Serialize(new { status = false, message = (lang == "en" ? "Invalid Captcha Code!" : "Lütfen güvenlik kodunu doğru girin."), data = "" });
                }

                if (context.Request.Form["email"] != null)
                {
                    email = context.Request.Form["email"].Trim();
                    email = HtmlAndUrlDecode(email);
                }
                else
                {
                    pluginNotValidMessage = string.Concat(pluginNotValidMessage, (lang == "en" ? "Enter your email!" : "Lütfen e-posta adresinizi girin."));
                }

                if (context.Request.Form["name"] != null)
                {
                    name = context.Request.Form["name"].Trim();
                    name = HtmlAndUrlDecode(name);
                }
                else
                {
                    pluginNotValidMessage = string.Concat(pluginNotValidMessage, (lang == "en" ? "Enter your name!" : "Lütfen adınızı girin."));
                }

                if (context.Request.Form["surname"] != null)
                {
                    surname = context.Request.Form["surname"].Trim();
                    surname = HtmlAndUrlDecode(surname);
                }
                else
                {
                    pluginNotValidMessage = string.Concat(pluginNotValidMessage, (lang == "en" ? "Enter your surname!" : "Lütfen soyadınızı girin."));
                }

                if (context.Request.Form["companyname"] != null)
                {
                    companyname = context.Request.Form["companyname"].Trim();
                    companyname = HtmlAndUrlDecode(companyname);
                }
                else
                {
                    pluginNotValidMessage = string.Concat(pluginNotValidMessage, (lang == "en" ? "Enter your company name!" : "Lütfen firma adını girin."));
                }

                if (!string.IsNullOrEmpty(pluginNotValidMessage))
                    return jss.Serialize(new { status = false, message = pluginNotValidMessage, data = "" });

                CmsDbContext cmsContext = new CmsDbContext();
                var mailingArticle = cmsContext.Articles.Where(x => x.Id == 395).FirstOrDefault();

                using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                {
                    int existUser = dbContext.InvestorUsers.Count(x => x.Email == email);
                    if (existUser > 0)
                        return jss.Serialize(new { status = false, message = (lang == "en" ? "Email address is exist" : "Bu e-posta adresi kayıtlı"), data = "" });

                    InvestorUser user = new InvestorUser();
                    user.Status = (int)UserStatus.Waiting;
                    user.Name = name;
                    user.Surname = surname;
                    user.Email = email;
                    user.CompanyName = companyname;
                    user.CreatedDate = DateTime.Now;
                    user.isExcelUser = false;

                    dbContext.InvestorUsers.Add(user);
                    dbContext.SaveChanges();

                    try
                    {
                        StringBuilder mailBody = new StringBuilder();
                        mailBody.AppendLine("<p>There is a new registeration by Investor Relations Form. Please check the register info at web site cms.</p>");
                        mailBody.AppendLine("<p><strong>Name : </strong>" + user.Name + "<br/>");
                        mailBody.AppendLine("<strong>Surname : </strong>" + user.Surname + "<br/>");
                        mailBody.AppendLine("<strong>Company Name : </strong>" + user.CompanyName + "<br/>");
                        mailBody.AppendLine("<strong>Email : </strong>" + user.Email + "</p>");

                        SendMail(mailingArticle, mailBody.ToString());
                    }
                    catch (Exception)
                    {
                    }


                    return jss.Serialize(new { status = true, message = "RegisterSuccess", data = "" });


                }
            }
            catch (Exception ex)
            {
                return jss.Serialize(new { status = false, message = "Register exception", data = "" });
                //return jss.Serialize(GetExceptionDetails(ex));
            }
        }
        public string login(HttpContext context)
        {
            string userName = "", password = "";

            try
            {

                string isValidCaptcha = ValidateCaptcha(context);
                if (!string.IsNullOrEmpty(isValidCaptcha))
                {
                    return jss.Serialize(new { status = false, message = (lang == "en" ? "Invalid Captcha Code!" : "Lütfen güvenlik kodunu doğru girin."), data = "" });
                }


                if (context.Request.Form["username"] != null)
                {
                    userName = context.Request.Form["username"].Trim();
                    userName = HtmlAndUrlDecode(userName);
                }
                else
                {
                    return jss.Serialize(new { status = false, message = (lang == "en" ? "Username is not valid!" : "Lütfen kullanıcı adınızı girin."), data = "" });
                }

                if (context.Request.Form["password"] != null)
                {
                    password = context.Request.Form["password"].Trim();
                    password = HtmlAndUrlDecode(password);

                }
                else
                {
                    return jss.Serialize(new { status = false, message = (lang == "en" ? "Password is not valid!" : "Lütfen şifrenizi adınızı girin."), data = "" });
                }

                string loginResult = string.Empty;
                using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                {
                    var encryptPassword = Utility.Crypt.Encrypt(password);
                    var user = dbContext.InvestorUsers.FirstOrDefault(x => x.Email == userName && x.Status == (int)UserStatus.Approved);
                    if (user == null)
                        loginResult = jss.Serialize(new { status = false, message = (lang == "en" ? "Username is not valid!" : "Hatalı kullancı adı girdiniz."), data = "" });

                    else
                    {
                        if (user.Password != encryptPassword)
                            loginResult = jss.Serialize(new { status = false, message = (lang == "en" ? "Password is not valid!" : "Hatalı şifre girdiniz."), data = "" });
                    }

                    if (string.IsNullOrEmpty(loginResult))
                    {
                        string token = Guid.NewGuid().ToString();
                        user.LastLoginDate = DateTime.Now;
                        user.LoginToken = token;
                        dbContext.SaveChanges();

                        var loggedUserInfo = new { Name = user.Name, Surname = user.Surname, Email = user.Email, CompanyName = user.CompanyName };

                        loginResult = jss.Serialize(new { status = true, message = "", data = loggedUserInfo, token = token });
                    }
                }

                return loginResult;
            }
            catch (Exception ex)
            {
                return jss.Serialize(new { status = false, message = "Login exception", data = "" });
                //return jss.Serialize(GetExceptionDetails(ex));
            }
        }


        string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.HtmlDecode(value).Trim();
            //value = HttpUtility.UrlDecode(value).Trim();
            return value;
        }

        bool ValidatePasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        string ValidateCaptcha(HttpContext context)
        {
            #region Captcha Control
            string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
            string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchaValue"])) ? context.Request.Form["captchaValue"] : string.Empty;
            captchavalue = HtmlAndUrlDecode(captchavalue);
            if (string.IsNullOrEmpty(captchavalue))
            {
                return jss.Serialize(new SendMailResult { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
            }

            if (context.Session == null)
                return jss.Serialize(new SendMailResult { Code = "12", Message = "Captcha Error", Status = "CNOK" });

            if (context.Session["CaptchaImageText" + captchaid] == null)
            {
                return jss.Serialize(new SendMailResult { Code = "14", Message = "Captcha Error - CaptchaImageText" + captchaid, Status = "CNOK" }); //jss.Serialize("CNOK");
            }

            if (context.Session["CaptchaImageText" + captchaid].ToString().Trim() != captchavalue.Trim())
            {
                return jss.Serialize(new SendMailResult { Code = "15", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
            }

            context.Session["CaptchaImageText" + captchaid] = null;
            context.Session.Remove("CaptchaImageText" + captchaid);
            return string.Empty;
            #endregion
        }

        public class SendMailResult
        {
            public string Code { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string ErrorMessage { get; set; }
        }


        #region Captcha Plugin
        
        public string Captcha(HttpContext context)
        {
            string captchaId = !string.IsNullOrEmpty(context.Request.Form["captchaid"]) ? context.Request.Form["captchaid"].ToString().Trim() : "1";

            GTCaptcha gtCaptcha = new GTCaptcha();
            GTCaptchaResult captchaResult = gtCaptcha.GenerateCaptcha(12, returnInputVariables: false);
            context.Session["CaptchaImageText" + captchaId] = captchaResult.text;
            return jss.Serialize(Convert.ToBase64String(captchaResult.bmp));
        }
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

            returnVal = jss.Serialize(imageBase64);

            return returnVal;
        }

        private string CaptchaWithCounter(HttpContext context)
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

            if (captchaId != "1" && captchaId != "2" && captchaId != "3" && captchaId != "4" && captchaId != "5")
            {
                captchaId = "1";
            }

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

            returnVal = jss.Serialize(imageBase64);

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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetExceptionDetails(Exception exception)
        {
            var properties = exception.GetType()
                                    .GetProperties();
            var fields = properties
                             .Select(property => new
                             {
                                 Name = property.Name,
                                 Value = property.GetValue(exception, null)
                             })
                             .Select(x => String.Format(
                                 "{0} = {1}",
                                 x.Name,
                                 x.Value != null ? x.Value.ToString() : String.Empty
                             ));
            return String.Join("\n", fields);
        }

        private int SendMail(Article mailingArticle, string message)
        {
            try
            {
                if (mailingArticle != null)
                {
                    string fromAddress = HtmlAndUrlDecode(mailingArticle.Custom1.Trim());
                    string toAddress = HtmlAndUrlDecode(mailingArticle.Custom2.Trim());
                    string ccAddress = HtmlAndUrlDecode(mailingArticle.Custom3.Trim());
                    string bccAddress = HtmlAndUrlDecode(mailingArticle.Custom4.Trim());
                    string subject = HtmlAndUrlDecode(mailingArticle.Custom5.Trim());
                    string mailTemplate = HtmlAndUrlDecode(mailingArticle.Article1.Trim());



                    mailTemplate = mailTemplate.Replace("##NameSuename##", "");
                    mailTemplate = mailTemplate.Replace("##Message##", message);


                    var mailResult = MailSender.SendMail(toAddress, ccAddress, bccAddress, subject, mailTemplate, null);
                    if (!mailResult.status)
                    {
                        return 505;
                    }
                }
            }
            catch (Exception ex)
            {
                return 505;
            }

            return 200;
        }

    }

    public enum UserStatus
    {
        Waiting = 0,
        Approved = 1,
        Rejected = -1,
        Deleted = -2
    }
}