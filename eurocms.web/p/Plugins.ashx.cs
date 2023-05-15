using EuroCMS.Core;
using EuroCMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace EuroCMS.FrontEnd.p
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string result = "", pluginName = "";

            if (!string.IsNullOrEmpty(context.Request.Form["plugin"]))
            {
                pluginName = context.Request.Form["plugin"].ToLower().Trim();
            }

            if (string.IsNullOrEmpty(pluginName))
            {
                context.Response.Write(JsonConvert.SerializeObject(""));
                context.Response.End();
            }

            switch (pluginName)
            {
                case "search":
                    result = SearchPlugin(context);
                    break;
                case "iscaptchavisible":
                    result = IsVisible(context);
                    break;
                case "captcha":
                    result = CaptchaPlugin(context);
                    break;
                case "captchawithcounter":
                    result = CaptchaWithCounter(context);
                    break;
                case "sendmail":
                case "sendmailwithcaptcha":
                    result = SendMail(context);
                    break;
                case "sendmailtouser":
                    result = SendMailToUser(context);
                    break;
                case "getsequentialurls":
                    result = GetSequentials(context);
                    break;
                case "splash":
                    result = SplashPlugin(context);
                    break;
                case "searchtags":
                    result = SearchTags(context);
                    break;
                case "galleryfile":
                    result = GalleryFilePlugin(context);
                    break;
                case "executetoscript":
                    result = ExecuteToScript(context);
                    break;
                case "getarticle":
                    result = getarticle(context);
                    break;
                case "inlinemail":
                    result = TestManuelSendMail(context);
                    break;
            }

            context.Response.Write(result);
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

        #region test

        public string TestManuelSendMail(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string result = "", host = "", from = "", to = "", username = "", password = "", ssl = "";

            int port = 0;
            bool enableSsl = false;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["port"]))
                {
                    port = Convert.ToInt32(context.Request.Form["port"].ToLower().Trim());
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "port alanı boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["host"]))
                {
                    host = context.Request.Form["host"].ToLower().Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "host alanı boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["from"]))
                {
                    from = context.Request.Form["from"].ToLower().Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "from alanı boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["to"]))
                {
                    to = context.Request.Form["to"].ToLower().Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "to alanı boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["username"]))
                {
                    username = context.Request.Form["username"].ToLower().Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "username alanı boş gönderilemez" });
                }


                if (!string.IsNullOrEmpty(context.Request.Form["password"]))
                {
                    password = context.Request.Form["password"].ToLower().Trim();
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "password alanı boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["ssl"]))
                {
                    ssl = context.Request.Form["ssl"].ToLower().Trim();

                    if (ssl == "1")
                    {
                        enableSsl = true;
                    }
                }
                //var credential = new NetworkCredential
                //{
                //    UserName = username,
                //    Password = password
                //};
                //var smtp = new SmtpClient(host);
                //smtp.Port = port;
                //smtp.Credentials = credential;
                //smtp.EnableSsl = enableSsl;
                //smtp.UseDefaultCredentials = true;

                //var mail = new MailMessage(from, to)
                //{
                //    Subject = "test",
                //    IsBodyHtml = true,
                //    Body = "test"
                //};
                //smtp.Send(mail);

                //var client = new SmtpClient(host, port)
                //{
                //    Credentials = new NetworkCredential(username, password),
                //    EnableSsl = enableSsl,
                //    UseDefaultCredentials = true
                //};

                //client.Send(from, to, "test", "testbody");
                var fromAddress = new MailAddress(from, "From Name");
                var toAddress = new MailAddress(to, "To Name");
                string fromUsername = username.ToString();
                string fromPassword = password;
                const string subject = "Subject";
                const string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromUsername, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                result = jss.Serialize(new { status = true, message = "Gönderim başarılı." });
            }
            catch (Exception ex)
            {
                string error = "";
                error = ex.Message;
                if (ex.InnerException != null)
                {
                    error = error + " inner exp" + ex.InnerException.Message;
                }
                result = jss.Serialize(new { status = false, message = error });
            }
            return result;
        }

        #endregion





        #region Get Sequentials
        private string GetSequentials(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            GetSequentialResult sq = new GetSequentialResult
            {
                Code = "10",
                Message = "Unknown Error!",
                Status = "NOK"
            };

            bool isLoop = !string.IsNullOrEmpty(
                context.Request.Form["isLoop"])
                && context.Request.Form["isLoop"].Trim().ToLower().Contains("true") ? true : false;

            try
            {
                #region Get Current Article
                int currentArticleId = -1, fileTypeId = -1;
                try
                {
                    fileTypeId = Convert.ToInt32(!string.IsNullOrEmpty(context.Request.Form["fileTypeId"]) ? context.Request.Form["fileTypeId"].Trim() : "-1");
                    currentArticleId = Convert.ToInt32(context.Request.Form["currentarticle"]);
                }
                catch (Exception ex)
                {
                    currentArticleId = -1;
                    sq = new GetSequentialResult { Code = "13", Message = "currentarticle is not in correct format", Status = "NOK" };
                    CmsHelper.SaveErrorLog(ex, "currentarticle or filetypeid is not in correct format", false);
                }
                #endregion
                if (currentArticleId == -1)
                {
                    //articleId gönderilmeli
                    sq = new GetSequentialResult { Code = "13", Message = "currentarticle is not in correct format", Status = "NOK" };
                }
                else
                {
                    //articleId geldi 
                    #region Order In Geldi
                    //go on
                    #region Get Zone Id
                    int zoneorClsfId = -1;
                    bool isZone = true;
                    try
                    {
                        if (!string.IsNullOrEmpty(context.Request.Form["zoneid"]))
                        {
                            isZone = true;
                            zoneorClsfId = Convert.ToInt32(context.Request.Form["zoneid"]);
                        }
                        else if (!string.IsNullOrEmpty(context.Request.Form["clsfid"]))
                        {
                            isZone = false;
                            zoneorClsfId = Convert.ToInt32(context.Request.Form["clsfid"]);
                        }
                        else
                        {
                            //hata
                            sq = new GetSequentialResult { Code = "14", Message = "zoneid is not in correct format", Status = "NOK" };
                            return jss.Serialize(sq);
                        }
                    }
                    catch (Exception ex)
                    {
                        zoneorClsfId = -1;
                        sq = new GetSequentialResult { Code = "14", Message = "zoneid is not in correct format", Status = "NOK" };
                        CmsHelper.SaveErrorLog(ex, "zoneid is not in correct format", false);
                    }
                    #endregion
                    if (zoneorClsfId == -1)
                    {
                        sq = new GetSequentialResult { Code = "14", Message = "zoneid is not in correct format", Status = "NOK" };
                    }
                    else
                    {

                        //filtercolumn
                        string filterColumn = (!string.IsNullOrEmpty(context.Request.Form["filtercolumn"])) ? context.Request.Form["filtercolumn"] : string.Empty;
                        filterColumn = filterColumn.Trim();

                        string filterColumnValue = (!string.IsNullOrEmpty(context.Request.Form["filtercolumnvalue"])) ? context.Request.Form["filtercolumnvalue"] : string.Empty;
                        filterColumnValue = filterColumnValue.Trim();

                        //zoneId alındı
                        #region Get OrderColumn & OrderType
                        string orderColumn = (!string.IsNullOrEmpty(context.Request.Form["ordercolumn"])) ? context.Request.Form["ordercolumn"] : string.Empty;
                        orderColumn = orderColumn.Trim();
                        if (string.IsNullOrEmpty(orderColumn))
                        {
                            orderColumn = "Headline";
                        }

                        string orderType = (!string.IsNullOrEmpty(context.Request.Form["ordertype"])) ? context.Request.Form["ordertype"] : string.Empty;
                        orderType = orderType.Trim().ToLower();
                        if (string.IsNullOrEmpty(orderType))
                        {
                            orderType = "desc";
                        }
                        else
                        {
                            if (orderType != "desc" && orderType != "asc")
                            {
                                orderType = "desc";
                            }
                        }
                        #endregion

                        CmsDbContext dbContext = new CmsDbContext();
                        vArticlesZonesFull vArticle = dbContext.vArticlesZonesFulls.Where(x => x.ArticleID == currentArticleId).FirstOrDefault();

                        if (vArticle == null)
                        {
                            sq = new GetSequentialResult { Code = "16", Message = "Current article not found", Status = "NOK" };
                        }
                        else
                        {
                            //article bulundu
                            List<vArticlesZonesFull> articleZonesList = new List<vArticlesZonesFull>();
                            byte val = Convert.ToByte("1");
                            if (isZone)
                            {
                                articleZonesList = dbContext.vArticlesZonesFulls.Where(x => x.ZoneID == zoneorClsfId && x.Status == val && x.IsPage).ToList();
                            }
                            else
                            {
                                articleZonesList = dbContext.vArticlesZonesFulls.Where(x => x.ClassificationID == zoneorClsfId && x.Status == val && x.IsPage).ToList();
                            }

                            if (!string.IsNullOrEmpty(context.Request.Form["classificationid"]))
                            {
                                var classification = Convert.ToInt32(context.Request.Form["classificationid"]);
                                if (classification > 0)
                                {
                                    articleZonesList = articleZonesList.Where(w => w.ClassificationID == classification).ToList();
                                }
                            }


                            string filterColumnName = filterColumn; //string.Empty;
                            try
                            {
                                //filterColumnName = vArticle.GetType().GetProperties().Where(x => x.GetCustomAttributesData()[0].ConstructorArguments[0].Value.ToString() == filterColumn).FirstOrDefault().Name;

                                if (!string.IsNullOrEmpty(filterColumnName) && !string.IsNullOrEmpty(filterColumnValue))
                                {
                                    articleZonesList = articleZonesList.Where(x => x.GetType().GetProperty(filterColumnName).GetValue(x, null).ToString() == filterColumnValue).ToList();
                                }
                                else
                                {
                                    sq = new GetSequentialResult { Code = "12", Message = "There is no such filter column and value.", Status = "NOK" };
                                }
                            }
                            catch (Exception ex)
                            {
                                //CmsHelper.SaveErrorLog(ex, "There is no such column", false);
                                sq = new GetSequentialResult { Code = "12", Message = "There is no such filter column", Status = "NOK" };
                            }






                            string columnName = orderColumn; //string.Empty;


                            try
                            {
                                columnName = vArticle.GetType().GetProperties().Where(x => x.GetCustomAttributesData()[0].ConstructorArguments[0].Value.ToString() == orderColumn).FirstOrDefault().Name;
                            }
                            catch (Exception ex)
                            {
                                //CmsHelper.SaveErrorLog(ex, "There is no such column", false);
                                sq = new GetSequentialResult { Code = "12", Message = "There is no such column", Status = "NOK" };
                            }
                            if (string.IsNullOrEmpty(columnName))
                            {
                                sq = new GetSequentialResult { Code = "12", Message = "There is no such column.", Status = "NOK" };
                            }
                            else
                            {
                                if (orderType == "asc")
                                {
                                    articleZonesList = articleZonesList.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                                }
                                else
                                {
                                    articleZonesList = articleZonesList.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                                }

                                #region Index
                                int index = articleZonesList.IndexOf(vArticle);
                                if (index < 0)
                                {
                                    if (isZone)
                                    {
                                        sq = new GetSequentialResult { Code = "17", Message = "Current article does not belong to the specified zone", Status = "NOK" };
                                    }
                                    else
                                    {
                                        sq = new GetSequentialResult { Code = "17", Message = "Current article does not belong to the specified classification", Status = "NOK" };
                                    }
                                }
                                else
                                {
                                    //article zone'a bağlı; index alındı
                                    if (index == 0)
                                    {
                                        //ilk item - previous yok
                                        sq = new GetSequentialResult();
                                        sq.Code = "100";
                                        sq.Message = "Success";
                                        sq.Status = "OK";
                                        sq.Next = CmsHelper.GetArticleAliasOrURL(articleZonesList[index + 1].ArticleID, zoneorClsfId.ToString());
                                        sq.NextHeadline = articleZonesList[index + 1].Headline;
                                        if (fileTypeId > 0)
                                        {
                                            int nextArticleId = articleZonesList[index + 1].ArticleID;
                                            ArticleFile nextArticleFile = new ArticleFile();
                                            nextArticleFile = dbContext.Files.Where(s => s.ArticleId == nextArticleId && s.FileTypeId == fileTypeId).FirstOrDefault();
                                            sq.NextFile1 = nextArticleFile != null ? ("i/content/" + nextArticleId.ToString() + "_" + nextArticleFile.File1) : "";
                                        }

                                        if (isLoop)
                                        {
                                            sq.Previous = CmsHelper.GetArticleAliasOrURL(articleZonesList.LastOrDefault().ArticleID, zoneorClsfId.ToString());
                                            sq.PrevHeadline = articleZonesList.LastOrDefault().Headline;
                                            if (fileTypeId > 0)
                                            {
                                                int prevArticleId = articleZonesList.LastOrDefault().ArticleID;
                                                ArticleFile prevArticleFile = new ArticleFile();
                                                prevArticleFile = dbContext.Files.Where(s => s.ArticleId == prevArticleId && s.FileTypeId == fileTypeId).FirstOrDefault();
                                                sq.PrevFile1 = prevArticleFile != null ? ("i/content/" + prevArticleId.ToString() + "_" + prevArticleFile.File1) : "";
                                            }
                                        }
                                        sq.Current = index + 1;
                                        sq.Total = articleZonesList.Count;

                                        //sq = new GetSequentialResult { Code = "100", Message = "Success", Status = "OK", Next = CmsHelper.GetArticleAliasOrURL(articleZonesList[index + 1].ArticleID, zoneorClsfId.ToString()) };
                                    }
                                    else if (index == articleZonesList.Count - 1)
                                    {
                                        //son item
                                        sq = new GetSequentialResult();
                                        sq.Code = "100";
                                        sq.Message = "Success";
                                        sq.Status = "OK";
                                        sq.Previous = CmsHelper.GetArticleAliasOrURL(articleZonesList[index - 1].ArticleID, zoneorClsfId.ToString());
                                        sq.PrevHeadline = articleZonesList[index - 1].Headline;
                                        if (fileTypeId > 0)
                                        {
                                            int prevArticleId = articleZonesList[index - 1].ArticleID;
                                            ArticleFile prevArticleFile = new ArticleFile();
                                            prevArticleFile = dbContext.Files.Where(s => s.ArticleId == prevArticleId && s.FileTypeId == fileTypeId).FirstOrDefault();
                                            sq.PrevFile1 = prevArticleFile != null ? ("i/content/" + prevArticleId.ToString() + "_" + prevArticleFile.File1) : "";
                                        }

                                        if (isLoop)
                                        {
                                            sq.Next = CmsHelper.GetArticleAliasOrURL(articleZonesList.FirstOrDefault().ArticleID, zoneorClsfId.ToString());
                                            sq.NextHeadline = articleZonesList.FirstOrDefault().Headline;
                                            if (fileTypeId > 0)
                                            {
                                                int nextArticleId = articleZonesList.LastOrDefault().ArticleID;
                                                ArticleFile nextArticleFile = new ArticleFile();
                                                nextArticleFile = dbContext.Files.Where(s => s.ArticleId == nextArticleId && s.FileTypeId == fileTypeId).FirstOrDefault();
                                                sq.NextFile1 = nextArticleFile != null ? ("i/content/" + nextArticleId.ToString() + "_" + nextArticleFile.File1) : "";
                                            }
                                        }
                                        sq.Current = index + 1;
                                        sq.Total = articleZonesList.Count;

                                        //sq = new GetSequentialResult { Code = "100", Message = "Success", Status = "OK", Previous = CmsHelper.GetArticleAliasOrURL(articleZonesList[index - 1].ArticleID, zoneorClsfId.ToString()) };
                                    }
                                    else
                                    {
                                        //diğer itemler
                                        sq = new GetSequentialResult();
                                        sq.Code = "100";
                                        sq.Message = "Success";
                                        sq.Status = "OK";
                                        sq.Next = CmsHelper.GetArticleAliasOrURL(articleZonesList[index + 1].ArticleID, zoneorClsfId.ToString());
                                        sq.NextHeadline = articleZonesList[index + 1].Headline;
                                        sq.Previous = CmsHelper.GetArticleAliasOrURL(articleZonesList[index - 1].ArticleID, zoneorClsfId.ToString());
                                        sq.PrevHeadline = articleZonesList[index - 1].Headline;
                                        sq.Current = index + 1;
                                        sq.Total = articleZonesList.Count;
                                        if (fileTypeId > 0)
                                        {
                                            int prevArticleId = articleZonesList[index - 1].ArticleID, nextArticleId = articleZonesList[index + 1].ArticleID;
                                            ArticleFile prevArticleFile = new ArticleFile();
                                            ArticleFile nextArticleFile = new ArticleFile();
                                            prevArticleFile = dbContext.Files.Where(s => s.ArticleId == prevArticleId && s.FileTypeId == fileTypeId).FirstOrDefault();
                                            nextArticleFile = dbContext.Files.Where(s => s.ArticleId == nextArticleId && s.FileTypeId == fileTypeId).FirstOrDefault();
                                            sq.PrevFile1 = prevArticleFile != null ? ("i/content/" + prevArticleId.ToString() + "_" + prevArticleFile.File1) : "";
                                            sq.NextFile1 = nextArticleFile != null ? ("i/content/" + nextArticleId.ToString() + "_" + nextArticleFile.File1) : "";
                                        }
                                        //sq = new GetSequentialResult { Code = "100", Message = "Success", Status = "OK", Next = CmsHelper.GetArticleAliasOrURL(articleZonesList[index + 1].ArticleID, zoneorClsfId.ToString()), Previous = CmsHelper.GetArticleAliasOrURL(articleZonesList[index - 1].ArticleID, zoneorClsfId.ToString()) };
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, false);
                sq = new GetSequentialResult { Code = "11", Message = "Process Failed! - GetSequentials exception ", Status = "NOK" };
            }

            return jss.Serialize(sq);
        }

        //10 -> Unknown Error!
        //11 -> Process Failed
        //12 -> There is no such column
        //13 -> currentarticle is not in correct format
        //14 -> zoneid is not in correct format 
        //100 -> Success

        public class GetSequentialResult
        {
            public string Code { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string Previous { get; set; }
            public string Next { get; set; }
            public string NextHeadline { get; set; }
            public string PrevHeadline { get; set; }
            public string NextFile1 { get; set; }
            public string PrevFile1 { get; set; }
            public int Current { get; set; }
            public int Total { get; set; }
        }
        #endregion

        #region SendMail Plugin
        public class SendMailResult
        {
            public string Code { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string ErrorMessage { get; set; }
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

        private string SendMail(HttpContext context)
        {
            try
            {
                string result = string.Empty;
                context.Response.ContentType = "text/json";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                result = JsonConvert.SerializeObject(new SendMailResult { Code = "13", Message = "Unknown Error", Status = "NOK" });

                string pluginName = context.Request.Form["plugin"].Trim().ToLower();
                if (pluginName == "sendmailwithcaptcha")
                {
                    #region Captcha Control
                    string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                    string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
                    if (string.IsNullOrEmpty(captchavalue))
                    {
                        return result = JsonConvert.SerializeObject(new SendMailResult { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                    }

                    if (context.Session["CaptchaImageText" + captchaid] == null)
                    {
                        return JsonConvert.SerializeObject(new SendMailResult { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                    }

                    if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                    {
                        return JsonConvert.SerializeObject(new SendMailResult { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                    }

                    context.Session["CaptchaImageText" + captchaid] = null;
                    context.Session.Remove("CaptchaImageText" + captchaid);
                    #endregion

                }


                #region Keys & Values
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                List<string> keywords = new List<string> { "plugin", "articleid", "filaname" };

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
                        bool hasAttachment = false;
                        HttpPostedFile file = null;
                        if (context.Request.Files["attachmentfiles"] == null)
                        {
                            hasAttachment = false;
                        }

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
                            file = context.Request.Files["Attachmentfiles"];
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

                        #region Mail
                        var mailResult = MailSender.SendMail(toAddress, string.Join(",", ccList), string.Join(",", bccList), subject, mailTemplate, attachmentFile);
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
                SendMailResult sr = new SendMailResult { Code = "14", Message = "Process Failed", ErrorMessage = "send mail exception", Status = "NOK" };// ErrorMessage = ex.Message + " - " + ex.InnerException.Message,
                CmsHelper.SaveErrorLog(ex, "Cannot send mail", false);
                string jsonResult = JsonConvert.SerializeObject(sr);
                return jsonResult;
            }
        }
        #endregion

        #region Search Plugin

        public class SearchDecode
        {
            public int ArticleId { get; set; }
            public string Headline { get; set; }
            public string Summary { get; set; }
            public string Article1 { get; set; }
            public string Article2 { get; set; }
            public string Article3 { get; set; }
            public string Article4 { get; set; }
            public string Article5 { get; set; }
        }

        public class SearchResult
        {
            public string Headline { get; set; }
            public string Summary { get; set; }
            public string Alias { get; set; }
            public int ZoneId { get; set; }
            public DateTime LastUpdateDate { get; set; }
            public DateTime CreateDate { get; set; }
            public int ItemCount { get; set; }
            public int PageCount { get; set; }
            public int CurrentPage { get; set; }
            public string Custom1 { get; set; }
            public string Custom2 { get; set; }
            public string Custom3 { get; set; }
            public string Custom4 { get; set; }
            public string Custom5 { get; set; }
            public List<SearchFileResult> Files { get; set; }

            public double Rating { get; set; }
        }

        public class SearchFileResult
        {
            public int FileType { get; set; }
            public string File1 { get; set; }
            public string File2 { get; set; }
            public string File3 { get; set; }
            public string File4 { get; set; }
            public string File5 { get; set; }
            public string File6 { get; set; }
            public string File7 { get; set; }
            public string File8 { get; set; }
            public string File9 { get; set; }
            public string File10 { get; set; }
            public string FileTitle { get; set; }
            public string FileComment { get; set; }
        }

        public string SearchTextExtension(string text, string lang)
        {
            if (text == "" || text == null)
            {
                return text;
            }
            string returnVal = "";
            try
            {
                text = HttpUtility.HtmlDecode(text);
                text = HttpUtility.UrlDecode(text);
                returnVal = text.ToLower().Trim();

                if (lang == "TR" || lang == "tr")
                {
                    return returnVal;
                }
                else
                {
                    string cleanText = "abcdefghijklmnoprstuvyzqwx0123456789/";

                    returnVal = returnVal.Replace("ı", "i");
                    returnVal = returnVal.Replace("ğ", "g");
                    returnVal = returnVal.Replace("ü", "u");
                    returnVal = returnVal.Replace("ş", "s");
                    returnVal = returnVal.Replace("ö", "o");
                    returnVal = returnVal.Replace("ç", "c");

                    foreach (char item in returnVal)
                    {
                        if (!cleanText.Contains(item))
                        {
                            returnVal = returnVal.Replace(item.ToString(), "-");
                        }
                    }


                    while (returnVal.Contains("--"))
                    {
                        returnVal = returnVal.Replace("--", "-");
                    }

                    while (returnVal.Contains("//"))
                    {
                        returnVal = returnVal.Replace("//", "/");
                    }

                    while (!cleanText.Contains(returnVal.Substring(returnVal.Length - 1, 1)))
                    {
                        returnVal = returnVal.Remove(returnVal.Length - 1, 1);
                    }

                    while (!cleanText.Contains(returnVal.Substring(0, 1)))
                    {
                        returnVal = returnVal.Remove(0, 1);
                    }
                }
            }
            catch (Exception ex)
            {

                return "";
            }

            return returnVal;
        }


        public string SearchPlugin(HttpContext context)
        {
            string result = "", lang = "", searchText = "", excludeZones = "", excludeArticles = "", pageSize = "", currentPage = "", zoneIds = "";
            int siteId = 6, file_type_id = 0, subStringCount = 0;
            lang = "TR";

            searchText = !string.IsNullOrEmpty(context.Request.Form["searchtext"]) ? context.Request.Form["searchtext"].ToString().Trim() : "";

            if (string.IsNullOrEmpty(searchText))
            {
                return "";
            }

            lang = !string.IsNullOrEmpty(context.Request.Form["lang"]) ? context.Request.Form["lang"].ToString().Trim() : "TR";
            siteId = !string.IsNullOrEmpty(context.Request.Form["siteid"]) ? Convert.ToInt32(context.Request.Form["siteid"].ToString()) : 6;
            pageSize = !string.IsNullOrEmpty(context.Request.Form["pagesize"]) ? context.Request.Form["pagesize"].ToString() : "12";
            currentPage = !string.IsNullOrEmpty(context.Request.Form["currentpage"]) ? context.Request.Form["currentpage"].ToString() : "";
            excludeZones = !string.IsNullOrEmpty(context.Request.Form["excludezones"]) ? context.Request.Form["excludezones"].ToString() : "";
            excludeArticles = !string.IsNullOrEmpty(context.Request.Form["excludearticles"]) ? context.Request.Form["excludearticles"].ToString() : "";

            zoneIds = !string.IsNullOrEmpty(context.Request.Form["zoneid"]) ? context.Request.Form["zoneid"].ToString() : "";
            file_type_id = !string.IsNullOrEmpty(context.Request.Form["filetypeid"]) ? Convert.ToInt32(context.Request.Form["filetypeid"].ToString()) : 0;

            subStringCount = !string.IsNullOrEmpty(context.Request.Form["substringcount"]) ? Convert.ToInt32(context.Request.Form["substringcount"]) : Convert.ToInt32(ConfigurationManager.AppSettings["SummaryMinCount"].ToString());

            int summaryMaxCount = Convert.ToInt32(ConfigurationManager.AppSettings["SummaryMaxCount"].ToString());
            if (subStringCount > summaryMaxCount || subStringCount < 0)
            {
                subStringCount = Convert.ToInt32(ConfigurationManager.AppSettings["SummaryMinCount"].ToString());
            }

            List<int> listExcludeZoneIds = new List<int>();
            List<int> listExcludeArticleIds = new List<int>();
            List<int> zoneIdList = new List<int>();

            List<vArticlesZonesFull> ListVArticleZones = new List<vArticlesZonesFull>();

            List<SearchResult> ListSearchResult = new List<SearchResult>();

            searchText = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(searchText));

            searchText = searchText.Trim();

            if (!string.IsNullOrEmpty(excludeZones))
            {
                if (excludeZones.Contains(","))
                {
                    listExcludeZoneIds = excludeZones.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
                }
                else
                {
                    listExcludeZoneIds.Add(Convert.ToInt32(excludeZones));
                }
            }

            if (!string.IsNullOrEmpty(excludeArticles))
            {
                if (excludeArticles.Contains(","))
                {
                    listExcludeArticleIds = excludeArticles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
                }
                else
                {
                    listExcludeArticleIds.Add(Convert.ToInt32(excludeArticles));
                }
            }

            // zoneId altındakileri arama
            if (!string.IsNullOrEmpty(zoneIds))
            {

                if (zoneIds.Contains(","))
                {
                    zoneIdList = zoneIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
                }
                else
                {
                    zoneIdList.Add(Convert.ToInt32(zoneIds));
                }

            }


            string lowSearchText = "";

            lowSearchText = SearchTextExtension(searchText, lang);

            //lowSearchText = searchText.ToLower();
            //if (lang == "EN" || lang == "en")
            //{
            //    lowSearchText = CmsHelper.StringToAlphaNumeric(lowSearchText, false);
            //}
            List<int> listArticleId = new List<int>();
            List<SearchDecode> listSearchDecode = new List<SearchDecode>();
            using (CmsDbContext dbContext = new CmsDbContext())
            {

                listSearchDecode = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ZoneStatus == "A" && vaz.Status == 1 && vaz.IsPage == true && vaz.LanguageID == lang && vaz.ZoneGroupSiteId == siteId && vaz.ArticleType != 2 && vaz.ArticleType != 3 && vaz.ArticleType != 4 && vaz.ArticleType != 7 && vaz.ArticleType != 8 && vaz.IsPage).Select(s => new SearchDecode { ArticleId = s.ArticleID, Headline = s.Headline, Summary = s.Summary, Article1 = s.Article1, Article2 = s.Article2, Article3 = s.Article3, Article4 = s.Article4, Article5 = s.Article5 }).ToList();
                listSearchDecode = listSearchDecode.Where(sd => SearchTextExtension(sd.Headline, lang).Contains(lowSearchText) ||
                                                                SearchTextExtension(sd.Summary, lang).Contains(lowSearchText) ||
                                                                SearchTextExtension(sd.Article1, lang).Contains(lowSearchText) ||
                                                                SearchTextExtension(sd.Article2, lang).Contains(lowSearchText) ||
                                                                SearchTextExtension(sd.Article3, lang).Contains(lowSearchText) ||
                                                                SearchTextExtension(sd.Article4, lang).Contains(lowSearchText) ||
                                                                SearchTextExtension(sd.Article5, lang).Contains(lowSearchText)).ToList();
                /*listSearchDecode = listSearchDecode.Where(sd => HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Headline)).ToLower().Contains(lowSearchText) ||
                                                HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Summary)).ToLower().Contains(lowSearchText) ||
                                                HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article1)).ToLower().Contains(lowSearchText) ||
                                                HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article2)).ToLower().Contains(lowSearchText) ||
                                                HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article3)).ToLower().Contains(lowSearchText) ||
                                                HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article4)).ToLower().Contains(lowSearchText) ||
                                                HttpUtility.UrlDecode(HttpUtility.HtmlDecode(sd.Article5)).ToLower().Contains(lowSearchText)).ToList();*/

                listArticleId = listSearchDecode.Select(sd => sd.ArticleId).ToList();
                listArticleId = listArticleId.Distinct().ToList();

                //data with Exclude Zones
                var list = dbContext.vArticlesZonesFulls.Where(vaz => listArticleId.Contains(vaz.ArticleID) && vaz.ZoneGroupSiteId == siteId && !listExcludeZoneIds.Contains(vaz.ZoneID)).ToList();

                ListVArticleZones = list.Distinct().ToList();

                if (zoneIdList.Count() > 0)
                {
                    ListVArticleZones = ListVArticleZones.Where(w => zoneIdList.Contains(w.ZoneID)).ToList();
                }

            }



            //// Exclude Zones
            //if (listExcludeZoneIds != null)
            //{
            //    if (listExcludeZoneIds.Count() > 0)
            //    {
            //        ListVArticleZones = ListVArticleZones.Where(v => !listExcludeZoneIds.Contains(v.ZoneID)).ToList();
            //    }
            //}

            // Exclude Articles
            if (listExcludeArticleIds != null)
            {
                if (listExcludeArticleIds.Count() > 0)
                {
                    ListVArticleZones = ListVArticleZones.Where(v => !listExcludeArticleIds.Contains(v.ArticleID)).ToList();
                }
            }



            int inCurrentPage = 0, totalItemCount = 0, pageCount = 0;
            inCurrentPage = string.IsNullOrEmpty(currentPage) ? 1 : Convert.ToInt32(currentPage);
            totalItemCount = ListVArticleZones.Count();

            double recordCount = 0;
            recordCount = Convert.ToDouble(ListVArticleZones.Count());
            pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(pageSize)));
            //ListSearchResult = ListSearchResult.Select(s => new SearchResult { PageCount = 0, Headline = s.Headline, Alias = s.Alias, CreateDate = s.CreateDate, CurrentPage = s.CurrentPage, ItemCount = s.ItemCount, LastUpdateDate = s.LastUpdateDate, Summary = s.Summary }).ToList();

            //if (!string.IsNullOrEmpty(pageSize))
            //{
            //    int inPageSize = 0;
            //    inPageSize = Convert.ToInt32(pageSize);

            //    if (ListVArticleZones.Count() > inPageSize)
            //    {
            //        double recordCount = 0;
            //        recordCount = Convert.ToDouble(ListVArticleZones.Count());
            //        pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(pageSize)));
            //        if (pageCount > 1)
            //        {

            //            if (inCurrentPage == 1)
            //            {
            //                ListVArticleZones = ListVArticleZones.Take(inPageSize).ToList();
            //            }
            //            else
            //            {
            //                int skipCount = ((inCurrentPage - 1) * inPageSize);
            //                ListVArticleZones = ListVArticleZones.Skip(skipCount).Take(inPageSize).ToList();
            //            }
            //        }

            //    }
            //}


            List<int> articleIds = ListVArticleZones.Select(x => x.ArticleID).ToList();
            List<ArticleFile> allFiles = new List<ArticleFile>();
            using (CmsDbContext dbContext = new CmsDbContext())
            {
                allFiles = dbContext.Files.Where(x => articleIds.Contains(x.ArticleId)).ToList();
            }
            ListSearchResult = ListVArticleZones.Select(s => new SearchResult
            {
                Headline = s.Headline,
                ZoneId = s.ZoneID,
                CreateDate = s.Created,
                LastUpdateDate = s.Updated,
                Summary = GetSummary(s.ArticleID, subStringCount), //string.IsNullOrEmpty(HttpUtility.HtmlDecode(s.Summary)) ? (HttpUtility.HtmlDecode(s.Article1).Length <= 250 ? HttpUtility.HtmlDecode(s.Article1) : HttpUtility.HtmlDecode(s.Article1).Substring(0, 250)) : HttpUtility.HtmlDecode(s.Summary),
                CurrentPage = inCurrentPage,
                ItemCount = totalItemCount,
                Alias = (s.ArticleType == 6) ? "../" + s.ArticleTypeDetail.Replace("href=\"/", "").Replace("\"", "") : GetAlias(s.ZoneID, s.ArticleID),     //eğer free form link varsa içeriğini bas
                PageCount = pageCount,
                Custom1 = s.Custom1,
                Custom2 = s.Custom2,
                Custom3 = s.Custom3,
                Custom4 = s.Custom4,
                Custom5 = s.Custom5,
                Files = GetArticleFiles(s.ArticleID, allFiles),
                Rating = CalculatePoint(s, searchText.ToLower())
            }).OrderByDescending(o => o.Rating).ToList();

            if (!string.IsNullOrEmpty(pageSize))
            {
                int inPageSize = 0;
                inPageSize = Convert.ToInt32(pageSize);

                if (ListSearchResult.Count() > inPageSize)
                {

                    if (pageCount > 1)
                    {

                        if (inCurrentPage == 1)
                        {
                            ListSearchResult = ListSearchResult.Take(inPageSize).ToList();
                        }
                        else
                        {
                            int skipCount = ((inCurrentPage - 1) * inPageSize);
                            ListSearchResult = ListSearchResult.Skip(skipCount).Take(inPageSize).ToList();
                        }
                    }

                }
            }

            result = JsonConvert.SerializeObject(ListSearchResult);
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //jss.MaxJsonLength = int.MaxValue;
            //result = jss.Serialize(ListSearchResult);

            return result;
        }


        //2017-01-11 Arama Rating Ekleme
        public double CalculatePoint(vArticlesZonesFull data, string searchText)
        {
            double firstPoint = 40;
            double firstPointExist = 25;
            double secondPoint = 20;
            double thirdPoint = 15;
            double calculate = 0;

            var decodeArticleContent = "";

            var indexOf = data.Headline.ToLower().IndexOf(searchText);
            if (indexOf != -1)
            {
                if (indexOf == 0)
                {
                    calculate = firstPoint;
                }
                else
                {
                    double indexOfPoint = ((indexOf * 100) / data.Headline.Length);
                    calculate = firstPoint - ((firstPoint / 100) * indexOfPoint);
                }

                calculate += firstPointExist;
            }
            //data.Summary = GetSummary(data.ArticleID, 1000);
            if (!string.IsNullOrEmpty(data.Summary))
            {
                if (data.Summary.ToLower().IndexOf(searchText) != -1)
                {
                    calculate += secondPoint;
                }
            }

            if (!string.IsNullOrEmpty(data.Article1))
            {
                decodeArticleContent = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(data.Article1.ToLower()));
                var count = CountWords(decodeArticleContent, searchText);
                if (count != 0)
                {
                    calculate += (thirdPoint / 5) - (((thirdPoint / 5) / 100) * (count / 100) * TotalWords(decodeArticleContent));
                }
            }

            if (!string.IsNullOrEmpty(data.Article2))
            {
                decodeArticleContent = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(data.Article2.ToLower()));
                var count = CountWords(decodeArticleContent, searchText);
                if (count != 0)
                {
                    calculate += (thirdPoint / 5) - (((thirdPoint / 5) / 100) * (count / 100) * TotalWords(decodeArticleContent));
                }
            }

            if (!string.IsNullOrEmpty(data.Article3))
            {
                decodeArticleContent = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(data.Article3.ToLower()));
                var count = CountWords(decodeArticleContent, searchText);
                if (count != 0)
                {
                    calculate += (thirdPoint / 5) - (((thirdPoint / 5) / 100) * (count / 100) * TotalWords(decodeArticleContent));
                }

            }

            if (!string.IsNullOrEmpty(data.Article4))
            {
                decodeArticleContent = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(data.Article4.ToLower()));
                var count = CountWords(decodeArticleContent, searchText);
                if (count != 0)
                {
                    calculate += (thirdPoint / 5) - (((thirdPoint / 5) / 100) * (count / 100) * TotalWords(decodeArticleContent));
                }
            }

            if (!string.IsNullOrEmpty(data.Article5))
            {
                decodeArticleContent = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(data.Article5.ToLower()));
                var count = CountWords(decodeArticleContent, searchText);
                if (count != 0)
                {
                    calculate += (thirdPoint / 5) - (((thirdPoint / 5) / 100) * (count / 100) * TotalWords(decodeArticleContent));
                }
            }

            return calculate;
        }

        public static int TotalWords(string text)
        {
            MatchCollection collection = Regex.Matches(text, @"[\S]+");
            return collection.Count;
        }

        public static int CountWords(string text, string searchText)
        {
            return Regex.Matches(Regex.Escape(text), searchText).Count;
        }
        //2017-01-11 Arama Rating Ekleme


        private List<SearchFileResult> GetArticleFiles(int ArticleID, List<ArticleFile> allFiles)
        {
            List<SearchFileResult> result = new List<SearchFileResult>();

            List<ArticleFile> files = allFiles.Where(x => x.ArticleId == ArticleID).ToList();
            foreach (ArticleFile af in files)
            {
                SearchFileResult sfr = new SearchFileResult();
                sfr.File1 = af.ArticleId + "_" + af.File1;
                sfr.File2 = af.ArticleId + "_" + af.File2;
                sfr.File3 = af.ArticleId + "_" + af.File3;
                sfr.File4 = af.ArticleId + "_" + af.File4;
                sfr.File5 = af.ArticleId + "_" + af.File5;
                sfr.File6 = af.ArticleId + "_" + af.File6;
                sfr.File7 = af.ArticleId + "_" + af.File7;
                sfr.File8 = af.ArticleId + "_" + af.File8;
                sfr.File9 = af.ArticleId + "_" + af.File9;
                sfr.File10 = af.ArticleId + "_" + af.File10;

                sfr.FileComment = af.Comment;
                sfr.FileTitle = af.Title;
                sfr.FileType = af.FileTypeId;

                result.Add(sfr);
            }

            return result;
        }

        string GetSummary(int articleId, int subStringCount)
        {
            string returnVal = "";

            CmsDbContext dbContext = new CmsDbContext();
            vArticlesZonesFull vArticleZone = new vArticlesZonesFull();

            vArticleZone = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ArticleID == articleId).FirstOrDefault();

            if (vArticleZone == null)
            {
                return "";
            }

            if (!string.IsNullOrEmpty(vArticleZone.Summary))
            {
                var summary = WebUtility.HtmlDecode(Regex.Replace(HttpUtility.HtmlDecode(vArticleZone.Summary), "<[^>]*(>|$)", string.Empty)).Trim();
                summary = summary.Replace("\r", "");
                summary = summary.Replace("\n", "");
                if (summary.Length <= subStringCount)
                {
                    returnVal = HttpUtility.HtmlDecode(summary);
                    return returnVal;
                }

                returnVal = summary.Substring(0, subStringCount).Trim();
                returnVal = HttpUtility.HtmlDecode(returnVal);
                //returnVal = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(vArticleZone.Summary));
                return returnVal;
            }

            if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(vArticleZone.Article1)))
            {
                string article1 = WebUtility.HtmlDecode(Regex.Replace(HttpUtility.HtmlDecode(vArticleZone.Article1), "<[^>]*(>|$)", string.Empty)).Trim();

                article1 = article1.Replace("\r", "");
                article1 = article1.Replace("\n", "");
                if (article1.Length <= subStringCount)
                {
                    returnVal = HttpUtility.HtmlDecode(article1);
                    return returnVal;
                }

                returnVal = article1.Substring(0, subStringCount).Trim();
                returnVal = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(returnVal));
            }


            return returnVal;
        }
        string GetAlias(int zoneId, int articleId)
        {
            string result = "";

            CmsDbContext dbContext = new CmsDbContext();
            vArticlesZonesFull vArticleZone = new vArticlesZonesFull();

            vArticleZone = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ZoneID == zoneId && vaz.ArticleID == articleId).FirstOrDefault();

            if (vArticleZone == null)
            {
                return "";
            }

            if (!string.IsNullOrEmpty(vArticleZone.ArticleZoneAlias))
            {
                result = vArticleZone.ArticleZoneAlias;
            }
            else
            {
                result = CmsHelper.getContentLinkAlias(vArticleZone.ZoneID.ToString(), vArticleZone.ArticleID.ToString(), HttpUtility.HtmlDecode(vArticleZone.SiteName), HttpUtility.HtmlDecode(vArticleZone.ZoneGroupName),
                HttpUtility.HtmlDecode(vArticleZone.ZoneName), HttpUtility.HtmlDecode(vArticleZone.Headline), "");
            }

            result = (result.Substring(0, 1) == "/" ? result.Substring(1, result.Length - 1) : result);


            return result;
        }

        #endregion

        #region Captcha Plugin
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


            string captchaText = GenerateRandomCode(length.ToString(), captchaisnumeric).Trim().ToLower();
            context.Session["CaptchaImageText" + captchaId] = captchaText;

            string imageBase64 = "";
            imageBase64 = CaptchaImageToBase64(context, captchaText, width, height);

            returnVal = JsonConvert.SerializeObject(imageBase64);

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

            string captchaText = GenerateRandomCode(length.ToString(), captchaisnumeric).Trim().ToLower();
            context.Session["CaptchaImageText" + captchaId] = captchaText;

            string imageBase64 = "";
            imageBase64 = CaptchaImageToBase64(context, captchaText, width, height);

            returnVal = JsonConvert.SerializeObject(imageBase64);

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

        #region Splash Plugin

        public class SplashResult
        {
            public Splash Splash { get; set; }
            public string Content { get; set; }

        }

        public string SplashPlugin(HttpContext context)
        {
            string returnVal = JsonConvert.SerializeObject("");

            CmsDbContext dbContext = new CmsDbContext();
            string splashIds = "";
            List<int> listSplashId = new List<int>();
            splashIds = !string.IsNullOrEmpty(context.Request.Form["splashids"]) ? context.Request.Form["splashids"].ToString().Trim() : "";

            if (!string.IsNullOrEmpty(splashIds))
            {
                if (splashIds.Contains(","))
                {
                    listSplashId = splashIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
                }
                else
                {
                    listSplashId.Add(Convert.ToInt32(splashIds));
                }
            }


            List<Splash> listSplash = new List<Splash>();
            listSplash = dbContext.Splashes.Where(s => listSplashId.Contains(s.ID) && s.Status == 1).ToList();

            Guid guid = new Guid();
            List<SplashResult> listSplashResult = new List<SplashResult>();

            foreach (Splash splash in listSplash)
            {
                string content = "";
                int articleId = 0, zoneId = 0;
                vArticlesZonesFull getArticle = new vArticlesZonesFull();
                SplashResult splashResult = new SplashResult();

                articleId = splash.ArticleID;
                zoneId = splash.ZoneID;
                splash.ArticleID = 0;
                splash.ZoneID = 0;
                splash.CreatedBy = guid;
                splash.CreateDate = DateTime.MinValue;
                splash.UpdateDate = DateTime.MinValue;

                getArticle = dbContext.vArticlesZonesFulls.Where(s => s.ArticleID == articleId && s.ZoneID == zoneId).FirstOrDefault();
                if (getArticle != null)
                {
                    content = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(getArticle.Article1));
                }

                splashResult.Splash = splash;
                splashResult.Content = content;

                listSplashResult.Add(splashResult);
            }

            returnVal = JsonConvert.SerializeObject(listSplashResult);

            return returnVal;
        }

        #endregion

        #region SearchTags Plugin
        private string SearchTags(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            CmsDbContext dbContext = new CmsDbContext();
            string result = string.Empty;
            string tagAlias = string.Empty;
            string tag = string.Empty; //context.Request.Form["tag"].ToString();

            tagAlias = context.Request.Form["tagalias"].ToString();
            tagAlias = HttpUtility.HtmlDecode(tagAlias.ToLower());

            Tag tCurrent = dbContext.Tags.Where(x => x.Alias == tagAlias).FirstOrDefault();
            if (tCurrent != null)
            {
                tag = tCurrent.Text;
            }

            string siteId = string.Empty;
            if (context.Request.Form["siteid"] != null)
            {
                siteId = context.Request.Form["siteid"].ToString();
            }

            if (!string.IsNullOrEmpty(tag))
            {
                tag = tag.ToLower();
                List<vArticlesZonesFull> aList = new List<vArticlesZonesFull>();
                if (string.IsNullOrEmpty(siteId))
                {
                    aList = dbContext.vArticlesZonesFulls.Where(x => x.TagContents.ToLower().Contains(tag)).ToList();
                }
                else
                {
                    int sId = Convert.ToInt32(siteId);
                    aList = dbContext.vArticlesZonesFulls.Where(x => x.TagContents.ToLower().Contains(tag) && x.ZoneGroupSiteId.Value == sId).ToList();
                }

                if (aList != null && aList.Count > 0)
                {
                    foreach (vArticlesZonesFull a in aList.ToList())
                    {
                        List<string> tagsOfArticle = a.TagContents.Split(',').ToList().Where(x => x.ToLower() == tag.ToLower()).ToList();
                        if (tagsOfArticle == null || tagsOfArticle.Count <= 0)
                        {
                            aList.Remove(a);
                        }
                    }

                    string pageSize = !string.IsNullOrEmpty(context.Request.Form["pagesize"]) ? context.Request.Form["pagesize"].ToString() : "";
                    string currentPage = !string.IsNullOrEmpty(context.Request.Form["currentpage"]) ? context.Request.Form["currentpage"].ToString() : "";

                    int inCurrentPage = 0, totalItemCount = 0, pageCount = 0;
                    inCurrentPage = string.IsNullOrEmpty(currentPage) ? 1 : Convert.ToInt32(currentPage);
                    totalItemCount = aList.Count();

                    //ListSearchResult = ListSearchResult.Select(s => new SearchResult { PageCount = 0, Headline = s.Headline, Alias = s.Alias, CreateDate = s.CreateDate, CurrentPage = s.CurrentPage, ItemCount = s.ItemCount, LastUpdateDate = s.LastUpdateDate, Summary = s.Summary }).ToList();

                    if (!string.IsNullOrEmpty(pageSize))
                    {
                        int inPageSize = 0;
                        inPageSize = Convert.ToInt32(pageSize);

                        if (aList.Count() > inPageSize)
                        {
                            double recordCount = 0;
                            recordCount = Convert.ToDouble(aList.Count());
                            pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(pageSize)));
                            if (pageCount > 1)
                            {

                                if (inCurrentPage == 1)
                                {
                                    aList = aList.Take(inPageSize).ToList();
                                }
                                else
                                {
                                    int skipCount = ((inCurrentPage - 1) * inPageSize);
                                    aList = aList.Skip(skipCount).Take(inPageSize).ToList();
                                }
                            }

                        }
                    }

                    //listeyi dön
                    List<SearchResult> ListSearchResult = new List<SearchResult>();
                    ListSearchResult = aList.Select(s => new SearchResult
                    {
                        Headline = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(s.Headline)),
                        CreateDate = s.Created,
                        LastUpdateDate = s.Updated,
                        Summary = GetSummary(s.ArticleID, 300), //string.IsNullOrEmpty(HttpUtility.HtmlDecode(s.Summary)) ? (HttpUtility.HtmlDecode(s.Article1).Length <= 250 ? HttpUtility.HtmlDecode(s.Article1) : HttpUtility.HtmlDecode(s.Article1).Substring(0, 250)) : HttpUtility.HtmlDecode(s.Summary),
                        CurrentPage = inCurrentPage,
                        ItemCount = totalItemCount,
                        Alias = (s.ArticleType == 6) ? "../" + s.ArticleTypeDetail.Replace("href=\"/", "").Replace("\"", "") : GetAlias(s.ZoneID, s.ArticleID),     //eğer free form link varsa içeriğini bas
                        PageCount = pageCount
                    }).ToList();

                    result = JsonConvert.SerializeObject(ListSearchResult);
                }
            }

            return result;
        }
        #endregion

        #region Gallery File Plugin

        public class GalleryFile
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string FullName { get; set; }
            public string Extension { get; set; }
            public string Size { get; set; }
            public int ItemCount { get; set; }
            public int PageCount { get; set; }
            public int CurrentPage { get; set; }
        }

        public string GalleryFilePlugin(HttpContext context)
        {
            string returnVal = JsonConvert.SerializeObject("NOK");

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = int.MaxValue;

            try
            {
                string fileExtension = !string.IsNullOrEmpty(context.Request.Form["extension"]) ? HtmlAndUrlDecode(context.Request.Form["extension"].ToString().Trim()) : "";
                string fileNameLike = !string.IsNullOrEmpty(context.Request.Form["filenamelike"]) ? HtmlAndUrlDecode(context.Request.Form["filenamelike"].ToString().Trim()) : "*";
                string targetPath = !string.IsNullOrEmpty(context.Request.Form["targetPath"]) ? HtmlAndUrlDecode(context.Request.Form["targetPath"].ToString().Trim()) : "";
                string order = !string.IsNullOrEmpty(context.Request.Form["order"]) ? context.Request.Form["order"].ToString().Trim() : "name";
                string orderBy = !string.IsNullOrEmpty(context.Request.Form["orderby"]) ? context.Request.Form["orderby"].ToString().Trim() : "asc";
                string pageSize = !string.IsNullOrEmpty(context.Request.Form["pagesize"]) ? context.Request.Form["pagesize"].ToString() : "";
                string currentPage = !string.IsNullOrEmpty(context.Request.Form["currentpage"]) ? context.Request.Form["currentpage"].ToString() : "";
                string defaultGalleryPath = "i/assets/Gallery";
                string fileSearchPattern = "";

                if (string.IsNullOrEmpty(targetPath))
                {
                    return returnVal;
                }

                targetPath = targetPath.Replace(defaultGalleryPath, "");
                targetPath = targetPath.StartsWith("/") ? targetPath.Substring(1, targetPath.Length - 1).Trim() : targetPath;
                targetPath = targetPath.EndsWith("/") ? targetPath.Substring(0, targetPath.Length - 1).Trim() : targetPath;
                targetPath = defaultGalleryPath + "/" + targetPath;

                string galleryPath = context.Server.MapPath("~/" + targetPath);

                fileSearchPattern = fileNameLike;
                if (fileNameLike == "*")
                {
                    fileSearchPattern = "*";
                }
                else if (fileNameLike.StartsWith("*") && !fileNameLike.EndsWith("*"))
                {
                    fileSearchPattern = fileNameLike + ".*";
                }

                List<string> listExtensions = new List<string>();
                if (!string.IsNullOrEmpty(fileExtension))
                {
                    fileExtension = fileExtension.Replace(".", "").Trim();
                    if (fileExtension.Contains(","))
                    {
                        listExtensions = fileExtension.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => "." + s.Trim()).ToList();
                    }
                    else
                    {
                        listExtensions.Add("." + fileExtension);
                    }
                }

                List<GalleryFile> listGalleryFile = new List<GalleryFile>();

                DirectoryInfo directoryInfo = new DirectoryInfo(galleryPath);
                IEnumerable<FileInfo> listFiles;
                listFiles = directoryInfo.EnumerateFiles(fileSearchPattern);//("*");

                if (listFiles == null)
                {
                    return jss.Serialize(listGalleryFile);
                }

                foreach (FileInfo fileInfo in listFiles)
                {
                    GalleryFile galleryFile = new GalleryFile();
                    galleryFile.FullName = fileInfo.Name;
                    galleryFile.Name = fileInfo.Name.Replace(fileInfo.Extension, "");
                    galleryFile.Path = Uri.EscapeUriString(targetPath + "/" + fileInfo.Name);
                    galleryFile.Size = fileInfo.Length.ToString();
                    galleryFile.Extension = fileInfo.Extension;
                    listGalleryFile.Add(galleryFile);
                }

                if (listExtensions.Count > 0)
                {
                    listGalleryFile = listGalleryFile.Where(s => listExtensions.Contains(s.Extension)).ToList();
                }

                #region Order
                if (order == "name")
                {
                    if (orderBy == "asc")
                    {
                        listGalleryFile = listGalleryFile.OrderBy(o => o.Name).ToList();
                    }
                    else
                    {
                        listGalleryFile = listGalleryFile.OrderByDescending(o => o.Name).ToList();
                    }
                }

                if (order == "size")
                {
                    if (orderBy == "asc")
                    {
                        listGalleryFile = listGalleryFile.OrderBy(o => o.Size).ToList();
                    }
                    else
                    {
                        listGalleryFile = listGalleryFile.OrderByDescending(o => o.Size).ToList();
                    }
                }
                #endregion

                #region Pager
                int inCurrentPage = 0, totalItemCount = 0, pageCount = 0;
                inCurrentPage = string.IsNullOrEmpty(currentPage) ? 1 : Convert.ToInt32(currentPage);
                totalItemCount = listGalleryFile.Count();

                if (!string.IsNullOrEmpty(pageSize) && totalItemCount > 0)
                {
                    int inPageSize = 0;
                    inPageSize = Convert.ToInt32(pageSize);

                    if (listGalleryFile.Count() > inPageSize)
                    {
                        double recordCount = 0;
                        recordCount = Convert.ToDouble(listGalleryFile.Count());
                        pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(pageSize)));
                        if (pageCount > 1)
                        {

                            if (inCurrentPage == 1)
                            {
                                listGalleryFile = listGalleryFile.Take(inPageSize).ToList();
                            }
                            else
                            {
                                int skipCount = ((inCurrentPage - 1) * inPageSize);
                                listGalleryFile = listGalleryFile.Skip(skipCount).Take(inPageSize).ToList();
                            }
                        }
                    }
                }

                listGalleryFile = listGalleryFile.Select(s => { s.ItemCount = totalItemCount; s.PageCount = pageCount; s.CurrentPage = inCurrentPage; return s; }).ToList();
                #endregion

                returnVal = jss.Serialize(listGalleryFile);
                returnVal = "{\"GalleryFiles\":" + returnVal + "}";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, ex.Message, false);
                returnVal = jss.Serialize("NOK - GalleryFilePlugin exception");
                //returnVal = jss.Serialize("NOK - " + ex.Message + " - " + ex.InnerException + " - " + ex.StackTrace);
            }

            return returnVal;
        }

        #endregion

        public string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
        }

        public string ExecuteToScript(HttpContext context)
        {
            string returnVal = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            returnVal = jss.Serialize("NOK");
            try
            {
                //string fileName = "";
                //fileName = !string.IsNullOrEmpty(context.Request.Form["fileName"]) ? HtmlAndUrlDecode(context.Request.Form["fileName"].Trim()) : "";
                //if (string.IsNullOrEmpty(fileName))
                //{
                //    return jss.Serialize("NOK - File Name");
                //}
                //string script = File.ReadAllText(context.Server.MapPath("/") + fileName);
                //CmsDbContext dbContext = new CmsDbContext();
                //dbContext.Database.ExecuteSqlCommand(script);
                //returnVal = jss.Serialize("OK");
            }
            catch (Exception ex)
            {
                returnVal = jss.Serialize("NOK - ExecuteToScript exception");
                //returnVal = jss.Serialize("NOK - " + ex.Message + " - " + ex.InnerException + " - " + ex.StackTrace);
            }
            return returnVal;
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