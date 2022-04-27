using EuroCMS.Core;
using EuroCMS.Plugin.GarantiLeasing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.Plugin.GarantiLeasing
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        //bytes
        int maxFileSize = 6000000;
        string fileExtensions = ".pdf,.docx,.doc";
        JavaScriptSerializer jss = new JavaScriptSerializer();
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
                result = jss.Serialize(new { status = false, message = "plugin alanı boş gönderilemez", data = "" });
            }



            switch (plugin)
            {
                case "callme":
                    result = callme(context);
                    break;

                case "complaint":
                    result = complaint(context);
                    break;

                case "contact":
                    result = contact(context);
                    break;

                case "hr":
                    result = hr(context);
                    break;

                case "newsletter":
                    result = newsletter(context);
                    break;

                case "leasing":
                    result = leasing(context);
                    break;
             
            }

            context.Response.Write(result);
        }

        public string leasing(HttpContext context)
        {
            string type = "", price = "", description = "", currency = "", company = "", person = "", phone = "", mail = "", city = "", town = "", address = "", source = "";

            int expiration = 0;
            bool isaccept = true;

            try
            {
                #region Captcha Control
                string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;

                if (string.IsNullOrEmpty(captchavalue))
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error 1" });
                }

                if (context.Session["CaptchaImageText" + captchaid] == null)
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error 2" });
                }

                if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error 3" });
                }

                context.Session["CaptchaImageText" + captchaid] = null;
                context.Session.Remove("CaptchaImageText" + captchaid);
                #endregion

                if (!string.IsNullOrEmpty(context.Request.QueryString["source"]))
                {
                    source = context.Request.QueryString["source"].ToString();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["type"]))
                {
                    type = context.Request.Form["type"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("type boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["price"]))
                {
                    price = context.Request.Form["price"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("price boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["expiration"]))
                {
                    expiration = Convert.ToInt32(context.Request.Form["expiration"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("expiration boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["description"]))
                {
                    description = context.Request.Form["description"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("description boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["currency"]))
                {
                    currency = context.Request.Form["currency"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("currency boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["company"]))
                {
                    company = context.Request.Form["company"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("company boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["person"]))
                {
                    person = context.Request.Form["person"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("person boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("phone boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["mail"]))
                {
                    mail = context.Request.Form["mail"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("mail boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["city"]))
                {
                    city = context.Request.Form["city"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("city boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["town"]))
                {
                    town = context.Request.Form["town"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("town boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["address"]))
                {
                    address = context.Request.Form["address"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("address boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["kvkk"]))
                {
                    isaccept = Convert.ToBoolean(context.Request.Form["kvkk"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("kvkk boş gönderilemez"));
                    context.Response.End();
                }


                using (GarantiLeasingDbContext dbContext = new GarantiLeasingDbContext())
                {
                    var leasing = new LeasingApplication();
                    leasing.Source= HttpUtility.UrlDecode(HttpUtility.HtmlDecode(source));
                    leasing.ProductType = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(type));
                    leasing.ProductPrice = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(price));
                    leasing.Expiration = expiration;
                    leasing.Description = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(description));
                    leasing.Currency = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(currency));
                    leasing.Company = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(company));
                    leasing.Person = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(person));
                    leasing.Phone = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(phone));
                    leasing.Mail = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(mail));
                    leasing.City = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(city));
                    leasing.Town = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(town));
                    leasing.Address = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(address));
                    leasing.IsAccept = isaccept;
                    leasing.CreateDt = DateTime.Now;
                    leasing.Status = 1;

                    dbContext.LeasingApplications.Add(leasing);
                    if (dbContext.SaveChanges() > 0)
                    {
                        return jss.Serialize(new { status = true, message = "Kayıt işlemi başarılı.", data = "" });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Kayıt işlemi sırasında hata oluştu.", data = "" });
                    }
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : leasing form", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string callme(HttpContext context)
        {
            string name = "", surname = "", city = "", town = "", email = "", phone = "";
            bool kvkk = false;

            try
            {
                #region Captcha Control
                string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
                if (string.IsNullOrEmpty(captchavalue))
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid] == null)
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                context.Session["CaptchaImageText" + captchaid] = null;
                context.Session.Remove("CaptchaImageText" + captchaid);
                #endregion

                if (!string.IsNullOrEmpty(context.Request.Form["name"]))
                {
                    name = context.Request.Form["name"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("name boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["surname"]))
                {
                    surname = context.Request.Form["surname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("surname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["city"]))
                {
                    city = context.Request.Form["city"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("city boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["town"]))
                {
                    town = context.Request.Form["town"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("town boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    email = context.Request.Form["email"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("email boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("phone boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["kvkk"]))
                {
                    kvkk = Convert.ToBoolean(context.Request.Form["kvkk"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("kvkk boş gönderilemez"));
                    context.Response.End();
                }


                using (GarantiLeasingDbContext dbContext = new GarantiLeasingDbContext())
                {
                    var callme = new CallMeForm();
                    callme.Name = name;
                    callme.Surname = surname;
                    callme.City = city;
                    callme.Town = town;
                    callme.Email = email;
                    callme.Phone = phone;
                    callme.Kvkk = kvkk;
                    callme.CreateDt = DateTime.Now;
                    callme.Status = 1;

                    dbContext.CallMeForms.Add(callme);
                    if (dbContext.SaveChanges() > 0)
                    {
                        return jss.Serialize(new { status = true, message = "Kayıt işlemi başarılı.", data = "" });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Kayıt işlemi sırasında hata oluştu.", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : callme form", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string complaint(HttpContext context)
        {
            string name = "", surname = "", companyname = "", email = "", phone = "", message = "", marsno = "";
            bool kvkk = false;

            try
            {

                #region Captcha Control
                string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
                if (string.IsNullOrEmpty(captchavalue))
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid] == null)
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                context.Session["CaptchaImageText" + captchaid] = null;
                context.Session.Remove("CaptchaImageText" + captchaid);
                #endregion

                if (!string.IsNullOrEmpty(context.Request.Form["name"]))
                {
                    name = context.Request.Form["name"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("name boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["surname"]))
                {
                    surname = context.Request.Form["surname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("surname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["companyname"]))
                {
                    companyname = context.Request.Form["companyname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("companyname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    email = context.Request.Form["email"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("email boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("phone boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["message"]))
                {
                    message = context.Request.Form["message"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("message boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["marsno"]))
                {
                    marsno = context.Request.Form["marsno"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("marsno boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["kvkk"]))
                {
                    kvkk = Convert.ToBoolean(context.Request.Form["kvkk"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("kvkk boş gönderilemez"));
                    context.Response.End();
                }


                using (GarantiLeasingDbContext dbContext = new GarantiLeasingDbContext())
                {
                    var complaint = new Complaint();
                    complaint.Name = name;
                    complaint.Surname = surname;
                    complaint.CompanyName = companyname;
                    complaint.Email = email;
                    complaint.Phone = phone;
                    complaint.Message = message;
                    complaint.MarsNo = marsno;
                    complaint.Kvkk = kvkk;
                    complaint.CreateDt = DateTime.Now;
                    complaint.Status = 1;

                    dbContext.Complaints.Add(complaint);
                    if (dbContext.SaveChanges() > 0)
                    {
                        return jss.Serialize(new { status = true, message = "Kayıt işlemi başarılı.", data = "" });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Kayıt işlemi sırasında hata oluştu.", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : complaint form", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string contact(HttpContext context)
        {
            string namesurname = "", companyname = "", citytown = "", email = "", phone = "", message = "";
            bool kvkk = false;

            try
            {
                #region Captcha Control
                string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
                if (string.IsNullOrEmpty(captchavalue))
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid] == null)
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                context.Session["CaptchaImageText" + captchaid] = null;
                context.Session.Remove("CaptchaImageText" + captchaid);
                #endregion

                if (!string.IsNullOrEmpty(context.Request.Form["namesurname"]))
                {
                    namesurname = context.Request.Form["namesurname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("namesurname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["companyname"]))
                {
                    companyname = context.Request.Form["companyname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("companyname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["citytown"]))
                {
                    citytown = context.Request.Form["citytown"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("citytown boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    email = context.Request.Form["email"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("email boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("phone boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["message"]))
                {
                    message = context.Request.Form["message"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("message boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["kvkk"]))
                {
                    kvkk = Convert.ToBoolean(context.Request.Form["kvkk"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("kvkk boş gönderilemez"));
                    context.Response.End();
                }


                using (GarantiLeasingDbContext dbContext = new GarantiLeasingDbContext())
                {
                    var contact = new ContactForm();
                    contact.NameSurname = namesurname;
                    contact.CompanyName = companyname;
                    contact.CityTown = citytown;
                    contact.Email = email;
                    contact.Phone = phone;
                    contact.Message = message;
                    contact.Kvkk = kvkk;
                    contact.CreateDt = DateTime.Now;
                    contact.Status = 1;

                    dbContext.ContactForms.Add(contact);
                    if (dbContext.SaveChanges() > 0)
                    {
                        return jss.Serialize(new { status = true, message = "Kayıt işlemi başarılı.", data = "" });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Kayıt işlemi sırasında hata oluştu.", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : contact form", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string hr(HttpContext context)
        {
            string namesurname = "", phone = "", email = "", position = "", address = "";
            bool kvkk = false;
            HttpPostedFile fileurl = null;

            try
            {

                #region Captcha Control
                string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
                if (string.IsNullOrEmpty(captchavalue))
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid] == null)
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                {
                    return jss.Serialize(new { status = false, message = "Captcha Error" });
                }

                context.Session["CaptchaImageText" + captchaid] = null;
                context.Session.Remove("CaptchaImageText" + captchaid);
                #endregion

                if (!string.IsNullOrEmpty(context.Request.Form["namesurname"]))
                {
                    namesurname = context.Request.Form["namesurname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("namesurname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("phone boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    email = context.Request.Form["email"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("email boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["position"]))
                {
                    position = context.Request.Form["position"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("position boş gönderilemez"));
                    context.Response.End();
                }

                if (context.Request.Files["fileurl"] != null)
                {
                    fileurl = context.Request.Files["fileurl"];
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "fileurl boş gönderilemez" });

                }

                if (!string.IsNullOrEmpty(context.Request.Form["address"]))
                {
                    address = context.Request.Form["address"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("address boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["kvkk"]))
                {
                    kvkk = Convert.ToBoolean(context.Request.Form["kvkk"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("kvkk boş gönderilemez"));
                    context.Response.End();
                }

                List<string> acceptedFileTypes = new List<string>();
                if (!string.IsNullOrEmpty(fileExtensions))
                {
                    if (fileExtensions.Contains(","))
                    {
                        acceptedFileTypes = fileExtensions.Split(',').ToList();
                    }
                    else
                    {
                        acceptedFileTypes.Add(fileExtensions);
                    }
                }

                if (maxFileSize > 0)
                {
                    if (fileurl.ContentLength > maxFileSize || fileurl.ContentLength <= 0)
                    {
                        return jss.Serialize(new { status = false, message = "Dosya boyutu 6 MB’den az olmalıdır." });
                    }
                }

                string extension = System.IO.Path.GetExtension(fileurl.FileName).Trim().ToLower();

                if (!acceptedFileTypes.Contains(extension))
                {
                    return jss.Serialize(new { status = false, message = "Dosya PDF, DOCX ve DOC formatında olmalıdır." });
                }

                string fileName = Guid.NewGuid().ToString();
                #region Save File
                string path = context.Server.MapPath("/i/content/hr") + "//" + fileName + extension;
                context.Request.Files["fileurl"].SaveAs(path);
                #endregion





                using (GarantiLeasingDbContext dbContext = new GarantiLeasingDbContext())
                {
                    var hr = new HrForm();
                    hr.NameSurname = namesurname;
                    hr.Phone = phone;
                    hr.Email = email;
                    hr.Position = position;
                    hr.FileUrl = fileName + extension;
                    hr.Address = address;
                    hr.Kvkk = kvkk;
                    hr.CreateDt = DateTime.Now;
                    hr.Status = 1;

                    dbContext.HrForms.Add(hr);
                    if (dbContext.SaveChanges() > 0)
                    {
                        return jss.Serialize(new { status = true, message = "Kayıt işlemi başarılı.", data = "" });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Kayıt işlemi sırasında hata oluştu.", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : HrForm form", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string newsletter(HttpContext context)
        {
            string email = "";
            bool kvkk = false;

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                {
                    email = context.Request.Form["email"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("email boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["kvkk"]))
                {
                    kvkk = Convert.ToBoolean(context.Request.Form["kvkk"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("kvkk boş gönderilemez"));
                    context.Response.End();
                }


                using (GarantiLeasingDbContext dbContext = new GarantiLeasingDbContext())
                {
                    var newsletter = new Newsletter();
                    newsletter.Email = email;
                    newsletter.Kvkk = kvkk;
                    newsletter.CreateDt = DateTime.Now;
                    newsletter.Status = 1;

                    dbContext.Newsletters.Add(newsletter);
                    if (dbContext.SaveChanges() > 0)
                    {
                        return jss.Serialize(new { status = true, message = "Kayıt işlemi başarılı.", data = "" });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Kayıt işlemi sırasında hata oluştu.", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : HrForm form", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
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