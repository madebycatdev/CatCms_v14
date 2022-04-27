using EuroCMS.Core;
using EuroCMS.Model;
using EuroCMS.Plugin.Akbati.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Web.UI;

namespace EuroCMS.Plugin.Akbati
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        //bytes
        int maxFileSize = 6000000;
        private int duration = 600;
        
        string fileExtensions = ".jpg,.png,.jpeg";
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
                case "campaigndatainsert":
                    result = CampaignDataInsert(context);
                    break;
                case "login":
                    result = login(context);
                    break;
                case "getfilms":
                    result = GetFilms(context);
                    break;
                case "membershipinsert":
                    result = InsertMembership(context);
                    break;
                case "getarticle":
                    result = getarticle(context);
                    break;
            }

            context.Response.Write(result);
        }

        public string InsertMembership(HttpContext context)
        {
            try
            {
                string result = string.Empty;
                context.Response.ContentType = "text/json";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                result = JsonConvert.SerializeObject(new SendMailResult { Code = "13", Message = "Unknown Error", Status = "NOK" });

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

                    if (counter <= 0)
                    {
                        SendMailResult sr = new SendMailResult { Code = "12", Message = "Article Is Not Correct", Status = "NOK" };
                        string jsonResult = JsonConvert.SerializeObject(sr);
                        return jsonResult;
                    }
                    else
                    {
                        //article is correct, ready to send
                        string emailAddress = context.Request.Form["email"];

                        var existEmail = dbContext.NewsletterEmails.FirstOrDefault(x => x.Email == emailAddress);
                        if (existEmail != null)
                        {
                            SendMailResult sr = new SendMailResult { Code = "15", Message = "E-posta adresi kayıtlı.", Status = "NOK" };
                            string jsonResult = JsonConvert.SerializeObject(sr);
                            return jsonResult;
                        }

                        NewsletterEmail email = new NewsletterEmail();
                        email.Email = emailAddress;
                        email.CreatedDate = DateTime.Now;
                        email.MembershipPermission = context.Request.Form["membershipPermission"] != null && context.Request.Form["membershipPermission"] == "true" ? true : false;
                        email.eBulletinPermission = context.Request.Form["eBulletinPermission"] != null && context.Request.Form["eBulletinPermission"] == "true" ? true : false;
                        email.IpAddress = GetIpValue(context);

                        dbContext.NewsletterEmails.Add(email);
                        dbContext.SaveChanges();

                        #region Mail Values
                        string fromAddress = HtmlAndUrlDecode(arBase.Custom1.Trim());
                        string toAddress = HtmlAndUrlDecode(arBase.Custom2.Trim());
                        string ccAddress = HtmlAndUrlDecode(arBase.Custom3.Trim());
                        string bccAddress = HtmlAndUrlDecode(arBase.Custom4.Trim());
                        string subject = HtmlAndUrlDecode(arBase.Custom5.Trim());
                        string mailTemplate = HtmlAndUrlDecode(arBase.Article1.Trim());

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
                            SendMailResult mailResultFinal = new SendMailResult { Code = "", Message = mailResult.message, Status = "NOK" };
                            string mailResultFinalJson = JsonConvert.SerializeObject(mailResultFinal);
                            return mailResultFinalJson;
                        }

                        //form dolduran kullanıcıya mail 
                        string isSendMailToUser = (!string.IsNullOrEmpty(context.Request.Form["isSendMailToUser"]) ? context.Request.Form["isSendMailToUser"].Trim().ToLower() : "0");
                        try
                        {
                            if (isSendMailToUser == "1")
                            {
                                string userMail = context.Request.Form["email"].Trim().ToLower();
                                string userMailTemplate = HtmlAndUrlDecode(arBase.Article2.Trim());

                                for (int i = 0; i < keys.Count; i++)
                                {
                                    if (userMailTemplate.Contains("##" + keys[i] + "##"))
                                    {
                                        userMailTemplate = userMailTemplate.Replace("##" + keys[i] + "##", values[i]);
                                    }
                                }

                                MailSender.SendMail(userMail, null, null, subject, userMailTemplate, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            SendMailResult sr = new SendMailResult { Code = "14", Message = "User Mail Process Failed", ErrorMessage = ex.Message, Status = "NOK" };
                            CmsHelper.SaveErrorLog(ex, "Cannot send mail", false);
                            string jsonResult = JsonConvert.SerializeObject(sr);
                            return jsonResult;
                        }

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
                SendMailResult sr = new SendMailResult { Code = "14", Message = "Process Failed", ErrorMessage = ex.Message + " - " + ex.InnerException.Message, Status = "NOK" };
                CmsHelper.SaveErrorLog(ex, "Cannot send mail", false);
                string jsonResult = JsonConvert.SerializeObject(sr);
                return jsonResult;
            }
        }
        public string CampaignDataInsert(HttpContext context)
        {
            string name = "", surname = "", phone = "", mail = "", town = "";
            HttpPostedFile photo = null;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["name"]))
                {
                    name = context.Request.Form["name"].Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Ad boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["surname"]))
                {
                    surname = context.Request.Form["surname"].Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Soyad boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["phone"]))
                {
                    phone = context.Request.Form["phone"].Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Telefon boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["mail"]))
                {
                    mail = context.Request.Form["mail"].Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Mail boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["town"]))
                {
                    town = context.Request.Form["town"].Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Semt boş gönderilemez" });
                }

                if (context.Request.Files["photo"] != null)
                {
                    photo = context.Request.Files["photo"];
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Fotoğraf boş gönderilemez" });

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
                    if (photo.ContentLength > maxFileSize || photo.ContentLength <= 0)
                    {
                        return jss.Serialize(new { status = false, message = "Fotoğraf boyutu 6 MB’den az olmalıdır." });
                    }
                }

                string extension = System.IO.Path.GetExtension(photo.FileName).Trim().ToLower();

                if (!acceptedFileTypes.Contains(extension))
                {
                    return jss.Serialize(new { status = false, message = "Fotoğraflar JPEG ve PNG formatında olmalıdır." });
                }
                string fileName = Guid.NewGuid().ToString();
                #region Save File
                string path = context.Server.MapPath("/i/content/campaign") + "//" + fileName + extension;
                context.Request.Files["photo"].SaveAs(path);
                #endregion

                using (AkbatiCampaignDbContext dbContext = new AkbatiCampaignDbContext())
                {
                    var data = new AkbatiCampaign();
                    data.Name = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(name));
                    data.Surname = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(surname));
                    data.Phone = phone;
                    data.Mail = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(mail));
                    data.Town = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(town));
                    data.Photo = fileName + extension;
                    data.CreateDt = DateTime.Now;
                    data.Status = (int)AkbatiStatusType.Pending;
                    dbContext.AkbatiCampaigns.Add(data);
                    dbContext.SaveChanges();
                }

                return jss.Serialize(new { status = true, message = "İşlem başarıyla gerçekleştirildi." });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : CampaignDataInsert", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string login(HttpContext context)
        {
            try
            {
                var client = new RestClient("https://api.cinemaximum.com.tr/oauth/token");
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest(Method.POST);
                request.AddParameter("username", "info@akbati.com"); // adds to POST or URL querystring based on Method
                request.AddParameter("password", "7IFXqWE"); // adds to POST or URL querystring based on Method
                request.AddParameter("grant_type", "password"); // adds to POST or URL querystring based on Method
                //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

                // easily add HTTP Headers
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                // or automatically deserialize result
                // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                var response = client.Execute<LoginModel>(request).Data;

                return jss.Serialize(new { status = true, message = "İşlem başarıyla gerçekleştirildi.", data = response });
            }
            catch (Exception ex)
            {
                return jss.Serialize(new { status = false, message = ex.Message });
            }
        }

        public string GetFilms(HttpContext context)
        {
            string date = "", token = "";

            try
            {

                if (!string.IsNullOrEmpty(context.Request.Form["date"]))
                {
                    date = context.Request.Form["date"].Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "date boş gönderilemez" });
                }

                OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
                {
                    Duration = duration, // saniye
                    Location = OutputCacheLocation.Server,
                    VaryByParam = "plugin;date"
                });


                page.ProcessRequest(HttpContext.Current);
                context.Response.Charset = "utf-8";
                context.Response.ContentType = "text/json";

                if (context.Session["endDate"] == null || string.IsNullOrEmpty(context.Session["endDate"].ToString()) || DateTime.Now > Convert.ToDateTime(context.Session["endDate"].ToString()))
                {
                    var clientAuth = new RestClient("https://api.cinemaximum.com.tr/oauth/token");
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);

                    var requestAuth = new RestRequest(Method.POST);
                    requestAuth.AddParameter("username", "info@akbati.com"); // adds to POST or URL querystring based on Method
                    requestAuth.AddParameter("password", "7IFXqWE"); // adds to POST or URL querystring based on Method
                    requestAuth.AddParameter("grant_type", "password"); // adds to POST or URL querystring based on Method
                                                                        //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

                    // easily add HTTP Headers
                    requestAuth.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    // or automatically deserialize result
                    // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                    var responseAuth = clientAuth.Execute<LoginModel>(requestAuth).Data;

                    token = "bearer " + responseAuth.access_token;
                    context.Session["token"] = token;
                    context.Session["endDate"] = DateTime.Now.AddSeconds(responseAuth.expires_in);

                }
                else
                {
                    token = context.Session["token"].ToString();
                }


                var client = new RestClient("https://api.cinemaximum.com.tr/api/Film/GetFilms");
                var request = new RestRequest(Method.POST);

                //request.AddParameter("undefined", "{\r\n\r\n\"CinemaIds\": [\"e81b5aed-cd27-4b7b-b664-f580d56a7bd1\"],\r\n\"Dates\": [\"2018-11-28\"],\r\n\"Start\": 0\r\n}\r\n\t\t");
                request.AddParameter("CinemaIds", "e81b5aed-cd27-4b7b-b664-f580d56a7bd1"); // adds to POST or URL querystring based on Method
                request.AddParameter("Dates", date); // adds to POST or URL querystring based on Method
                request.AddParameter("Start", 0); // adds to POST or URL querystring based on Method
                request.AddHeader("Authorization", token);
                request.AddHeader("Content-Type", "application/json; charset=UTF-8");

                var response = client.Execute<GetFilms>(request).Data;


                var clientSessions = new RestClient("https://api.cinemaximum.com.tr/api/Film/GetFilmSessionsByFilter");
                var requestSessions = new RestRequest(Method.POST);

                requestSessions.AddParameter("CinemaIds", "e81b5aed-cd27-4b7b-b664-f580d56a7bd1"); // adds to POST or URL querystring based on Method
                requestSessions.AddParameter("Dates", date); // adds to POST or URL querystring based on Method
                requestSessions.AddHeader("Authorization", token);
                requestSessions.AddHeader("Content-Type", "application/json; charset=UTF-8");

                var responseSessios = clientSessions.Execute<GetFilms>(requestSessions).Data;

                var filmList = new List<FilmContent>();

                foreach (var film in response.Films)
                {
                    var filmContent = new FilmContent();
                    filmContent.Id = film.Id;
                    filmContent.Title = film.Title;
                    filmContent.Slug = film.Slug;
                    filmContent.Synopsis = film.Synopsis;
                    filmContent.ImageUrl = film.ImageUrl;
                    filmContent.DetailUrl = film.DetailUrl;
                    filmContent.Trailers = film.Trailers;

                    var sessionData = responseSessios.Films.FirstOrDefault(f => f.Id == film.Id);
                    var sessionList = new List<Session>();
                    var typeList = new List<string>();

                    foreach (var sessionCinema in sessionData.Cinemas)
                    {
                        foreach (var screen in sessionCinema.Screens)
                        {
                            foreach (var session in screen.Sessions)
                            {
                                foreach (var attribute in session.SessionAttributes)
                                {
                                    typeList.Add(attribute.Name);
                                }

                                var newSes = new Session();
                                newSes.Time = session.DateTime.Split('T')[1].Substring(0, 5);
                                sessionList.Add(newSes);
                            }
                            //sessionList.AddRange(screen.Sessions.Distinct().ToList());



                        }
                    }
                    filmContent.Types = typeList.Distinct().ToList();
                    filmContent.Sessions = sessionList.OrderBy(s => s.Time).ToList();
                    filmList.Add(filmContent);
                }

                return jss.Serialize(new { status = true, message = "İşlem başarıyla gerçekleştirildi.", data = filmList });
            }
            catch (Exception ex)
            {
                return jss.Serialize(new { status = false, message = ex.Message });
            }
        }


        public string getarticle(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = int.MaxValue;

            string text = "", lang = "TR", custom1 = "", custom2 = "", custom3 = "", custom4 = "", custom5 = "", custom6 = "", custom7 = "", custom8 = "", custom9 = "", custom10 = "";
            List<string> zone_ids = new List<string>();
            int pageCount = 0, itemCount = 0, currentPage = 0, clsf_id = 0, fileType = 0;
            var files = new List<ArticleFile>();
            var classificationValues = new List<ClassificationComboValue>();
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').ToList();
                }
                else
                {
                    context.Response.Write(jss.Serialize("zone_id boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["itemCount"]))
                {
                    itemCount = Convert.ToInt32(context.Request.Form["itemCount"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("itemCount boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["currentPage"]))
                {
                    currentPage = Convert.ToInt32(context.Request.Form["currentPage"].Trim());

                }

                List<articleReturn> list = new List<articleReturn>();
                var articles = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID.ToString()) && w.Status == 1).ToList();


                if (!string.IsNullOrEmpty(context.Request.Form["clsf_id"]))
                {
                    clsf_id = Convert.ToInt32(context.Request.Form["clsf_id"].Trim());
                    articles = articles.Where(w => w.ClassificationID == clsf_id).ToList();
                    classificationValues = dbContext.ClassificationComboValues.Where(w => w.ClassificationId == clsf_id).ToList();
                }

                #region customs
                if (!string.IsNullOrEmpty(context.Request.Form["custom1"]))
                {
                    custom1 = context.Request.Form["custom1"].Trim();
                    articles = articles.Where(w => w.Custom1 == custom1).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom2"]))
                {
                    custom2 = context.Request.Form["custom2"].Trim();
                    articles = articles.Where(w => w.Custom2 == custom2).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom3"]))
                {
                    custom3 = context.Request.Form["custom3"].Trim();
                    articles = articles.Where(w => w.Custom3 == custom3).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom4"]))
                {
                    custom4 = context.Request.Form["custom4"].Trim();
                    articles = articles.Where(w => w.Custom4 == custom4).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom5"]))
                {
                    custom5 = context.Request.Form["custom5"].Trim();
                    articles = articles.Where(w => w.Custom5 == custom5).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom6"]))
                {
                    custom6 = context.Request.Form["custom6"].Trim();
                    articles = articles.Where(w => w.Custom6 == custom6).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom7"]))
                {
                    custom7 = context.Request.Form["custom7"].Trim();
                    articles = articles.Where(w => w.Custom7 == custom7).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom8"]))
                {
                    custom8 = context.Request.Form["custom8"].Trim();
                    articles = articles.Where(w => w.Custom8 == custom8).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom9"]))
                {
                    custom9 = context.Request.Form["custom9"].Trim();
                    articles = articles.Where(w => w.Custom9 == custom9).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["custom10"]))
                {
                    custom10 = context.Request.Form["custom10"].Trim();
                    articles = articles.Where(w => w.Custom10 == custom10).ToList();
                }
                #endregion

                if (!string.IsNullOrEmpty(context.Request.Form["text"]))
                {
                    text = context.Request.Form["text"].Trim();
                    text = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(text));

                    if (lang == "TR")
                    {
                        text = text.ToLower();
                    }
                    else
                    {
                        text = text.ToLower(System.Globalization.CultureInfo.InvariantCulture);
                    }

                    articles = articles.Where(sd => HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Headline)).ToLower().Contains(text) ||
                                                             HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Summary)).ToLower().Contains(text) ||
                                                             HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article1)).ToLower().Contains(text) ||
                                                             HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article2)).ToLower().Contains(text) ||
                                                             HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article3)).ToLower().Contains(text) ||
                                                             HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article4)).ToLower().Contains(text) ||
                                                             HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article5)).ToLower().Contains(text)).ToList();
                }

                pageCount = articles.Count / itemCount;

                var articleIds = articles.Select(s => s.ArticleID).ToList();
                files = dbContext.Files.Where(w => articleIds.Contains(w.ArticleId)).ToList();


                if (!string.IsNullOrEmpty(context.Request.Form["fileType"]))
                {
                    fileType = Convert.ToInt32(context.Request.Form["fileType"].Trim());
                    files = files.Where(w => w.FileTypeId == fileType).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["dateorder"]))
                {
                    var dateOrder = context.Request.Form["dateorder"].Trim();
                    if (dateOrder == "asc")
                    {
                        articles = articles.OrderBy(m => m.Date1).ToList();
                    }
                    else
                    {
                        articles = articles.OrderByDescending(m => m.Date1).ToList();
                    }
                }

                foreach (var article in articles)
                {
                    var fileList = files.Where(f => f.ArticleId == article.ArticleID).ToList();
                    var listItem = new articleReturn();
                    listItem.articleId = article.ArticleID;
                    listItem.headline = article.Headline;
                    listItem.summary = article.Summary;
                    listItem.article1 = HttpUtility.HtmlDecode(article.Article1);
                    listItem.custom1 = article.Custom1;
                    listItem.custom1_text = (classificationValues == null ? "" : classificationValues.FirstOrDefault(f => f.ComboValue == article.Custom1) == null ? "" : classificationValues.FirstOrDefault(f => f.ComboValue == article.Custom1).ComboLabel);
                    listItem.custom2 = article.Custom2;
                    listItem.custom2_text = (classificationValues == null ? "" : classificationValues.FirstOrDefault(f => f.ComboValue == article.Custom2) == null ? "" : classificationValues.FirstOrDefault(f => f.ComboValue == article.Custom2).ComboLabel);
                    listItem.custom3 = article.Custom3;
                    listItem.custom3_text = (classificationValues == null ? "" : classificationValues.FirstOrDefault(f => f.ComboValue == article.Custom3) == null ? "" : classificationValues.FirstOrDefault(f => f.ComboValue == article.Custom3).ComboLabel);
                    if (article.Date1.HasValue)
                    {
                        listItem.date_1 = article.Date1.Value.ToString();
                    }
                    listItem.created = article.Created.ToString();
                    listItem.order = article.AzOrder;
                    listItem.alias = article.ArticleZoneAlias;
                    listItem.files = fileList.Select(s => new fileReturn { title = s.Title, comment = s.Comment, order = s.FileOrder, file1 = s.File1, file2 = s.File2, file3 = s.File3, file4 = s.File4 }).ToList();
                    list.Add(listItem);
                }

                if (list.Count > itemCount)
                {
                    list = list.Skip(((currentPage - 1) > 0 ? (currentPage - 1) * itemCount : 0)).Take(itemCount).ToList();
                }
                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount, text = text });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : basın bultenleri", false);
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
        private sealed class OutputCachedPage : Page
        {
            private OutputCacheParameters _cacheSettings;

            public OutputCachedPage(OutputCacheParameters cacheSettings)
            {
                // Tracing requires Page IDs to be unique.
                ID = Guid.NewGuid().ToString();
                _cacheSettings = cacheSettings;
            }

            protected override void FrameworkInitialize()
            {
                base.FrameworkInitialize();
                InitOutputCache(_cacheSettings);
            }
        }

        public string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
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
    }

    public class articleReturn
    {
        public string created { get; set; }
        public int articleId { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }
        public string article1 { get; set; }
        public string custom1 { get; set; }
        public string custom1_text { get; set; }
        public string custom2 { get; set; }
        public string custom2_text { get; set; }
        public string custom3 { get; set; }
        public string custom3_text { get; set; }
        public string date_1 { get; set; }
        public int order { get; set; }
        public string alias { get; set; }
        public List<fileReturn> files { get; set; }
    }

    public class fileReturn
    {
        public string title { get; set; }
        public string comment { get; set; }
        public int order { get; set; }
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string file4 { get; set; }
    }
    public class SendMailResult
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }

}