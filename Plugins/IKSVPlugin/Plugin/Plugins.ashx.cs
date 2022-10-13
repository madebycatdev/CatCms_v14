using EuroCMS.Core;
using EuroCMS.Model;
using EuroCMS.Plugin.IKSV.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace EuroCMS.Plugin.IKSV
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler
    {
        int etkinlikClassification = 4;
        int etkinlikProgramClassification = 5;
        int etkinlikKategoriClassification = 6;
        int salonEtkinlikClassification = 9;
        private int duration = 600;
        JavaScriptSerializer jss = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetMaxAge(new TimeSpan(1, 0, 0));
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
                case "lalekartindirimlimekandetay":
                    result = lalekartindirimlimekandetay(context);
                    break;
                case "lalekartindirimlimekanlar":
                    result = lalekartIndirimliMekanlar(context);
                    break;
                case "lalekartnews":
                    result = lalekartNews(context);
                    break;
                case "lalekartevents":
                    result = lalekartEvents(context);
                    break;
                case "events":
                    result = events(context);
                    break;
                case "eventprogram":
                    result = eventProgram(context);
                    break;
                case "eventprogramchart":
                    result = eventprogramchart(context);
                    break;
                case "getarticle":
                    result = getarticle(context);
                    break;
                case "filters":
                    result = filters(context);
                    break;
                case "chart":
                    result = chart(context);
                    break;
                case "iksvchart":
                    result = iksvChart(context);
                    break;
                case "iksvarchive":
                    result = iksvarchive(context);
                    break;
                case "festivalarchive":
                    result = festivalarchive(context);
                    break;
                case "tags":
                    result = tags(context);
                    break;
                case "tagrelatedevents":
                    result = tagRelatedEvents(context);
                    break;
                case "subarticles":
                    result = subArticles(context);
                    break;
                case "tagrelatedeventssalon":
                    result = tagRelatedEventsSalon(context);
                    break;
                case "reservation":
                    result = reservation(context);
                    break;
                case "reservationcheck":
                    result = reservationcheck(context);
                    break;
            }

            context.Response.Write(result);
        }
        public string tagRelatedEventsSalon(HttpContext context)
        {
            //CmsDbContext dbContext = new CmsDbContext();
            List<EventDataModel> list = new List<EventDataModel>();
            int articleid = 0;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["articleid"]))
                {
                    articleid = Convert.ToInt32(context.Request.Form["articleid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("articleid boş gönderilemez"));
                    context.Response.End();
                }

                OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
                {
                    Duration = duration, // saniye
                    Location = OutputCacheLocation.Server,
                    VaryByParam = "plugin;articleid"
                });


                page.ProcessRequest(HttpContext.Current);
                context.Response.Charset = "utf-8";
                context.Response.ContentType = "text/json";

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    var article = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == articleid && f.Status == 1);
                    if (article != null)
                    {
                        var articlesWithCat = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.LanguageID == article.LanguageID && w.ClassificationID == salonEtkinlikClassification && !string.IsNullOrEmpty(w.TagIds)).ToList();
                        List<int> articleTags = new List<int>();
                        if (!string.IsNullOrEmpty(article.TagIds))
                        {
                            articleTags = article.TagIds.Split(',').Select(Int32.Parse).ToList();
                        }

                        List<vArticlesZonesFull> articles = new List<vArticlesZonesFull>();


                        foreach (var tagId in articleTags)
                        {
                            articles.AddRange(articlesWithCat.Where(w => w.TagIds.Split(',').ToList().Contains(tagId.ToString())).ToList());
                        }

                        articles = articles.Distinct().ToList();

                        foreach (var relatedarticle in articles)
                        {
                            var listItem = new EventDataModel();

                            listItem.articleId = relatedarticle.ArticleID;
                            listItem.headline = relatedarticle.Headline;
                            listItem.zone = relatedarticle.ZoneName;
                            listItem.zoneId = relatedarticle.ZoneID;
                            listItem.order = relatedarticle.AzOrder;
                            listItem.alias = relatedarticle.ArticleZoneAlias;
                            listItem.category = relatedarticle.ZoneName;
                            listItem.thumb = relatedarticle.Custom1;
                            listItem.activity = relatedarticle.Flag1;
                            listItem.tags = relatedarticle.TagContents;
                            listItem.section = relatedarticle.Custom10;
                            listItem.director = relatedarticle.Custom2;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = dbContext.Files.Where(w => w.ArticleId == relatedarticle.ArticleID && w.FileTypeId == 21).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.articleid = file.ArticleId;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }

                            listItem.files = fileList;
                            listItem.alias = relatedarticle.DomainName.Trim() + "/" + relatedarticle.ArticleZoneAlias;

                            list.Add(listItem);
                        }

                        list = list.Where(w => w.articleId != article.ArticleID).Distinct().ToList();

                        if (list.Count > 3)
                        {
                            List<int> articleIdList = new List<int>();
                            Random rand = new Random();
                            while (articleIdList.Count < 3)
                            {
                                int toSkip = rand.Next(0, list.Count);
                                var articleId = list.Skip(toSkip).Take(1).FirstOrDefault().articleId;
                                if (!articleIdList.Contains(articleId))
                                {
                                    articleIdList.Add(articleId);
                                }
                            }

                            list = list.Where(w => articleIdList.Contains(w.articleId)).Distinct().ToList();
                        }

                        return jss.Serialize(new { status = true, message = "İşlem başarılı.", data = list });
                    }
                }
                return jss.Serialize(new { status = false, message = "article bulunamadı.", data = "" });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Tag Related Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }


        public string salonevents(HttpContext context)
        {
            string lang = "TR";
            int file_type = 0, zone_id = 0, itemCount = 0, currentPage = 0;

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    zone_id = Convert.ToInt16(context);
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
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
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

                if (!string.IsNullOrEmpty(context.Request.Form["file_type"]))
                {
                    file_type = Convert.ToInt32(context.Request.Form["file_type"].Trim());
                }

                var files = new List<ArticleFile>();
                var articles = new List<vArticlesZonesFull>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    files = dbContext.Files.Where(w => w.FileTypeId == file_type).ToList();
                    articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zone_id && (w.StartDate.Value < DateTime.Now && w.EndDate.Value > DateTime.Now) && w.Status == 1).ToList();
                }

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;

                var events = new List<salonevent>();

                foreach (var article in articles)
                {
                    var singleEvent = new salonevent();
                    singleEvent.articleid = article.ArticleID;
                    singleEvent.headline = article.Headline;
                    singleEvent.headline = article.Headline;
                    singleEvent.alias = article.ArticleZoneAlias;

                    singleEvent.custom_1 = article.Custom1;
                    singleEvent.custom_2 = article.Custom2;
                    singleEvent.custom_3 = article.Custom3;
                    singleEvent.custom_4 = article.Custom4;

                    var file = files.FirstOrDefault(f => f.ArticleId == article.ArticleID);
                    if (file != null)
                    {
                        singleEvent.thumbnail = article.ArticleID + "_" + file.File1;
                    }

                    if (article.Date1.HasValue)
                    {
                        singleEvent.date_1 = article.Date1.Value;
                        singleEvent.day = dtfi.GetDayName(article.Date1.Value.DayOfWeek);
                        singleEvent.dayNo = article.Date1.Value.Day;
                        singleEvent.month = dtfi.GetMonthName(article.Date1.Value.Month);
                        singleEvent.monthNo = article.Date1.Value.Month;
                        singleEvent.year = article.Date1.Value.Year;
                        singleEvent.time = article.Date1.Value.TimeOfDay.ToString();
                    }

                    events.Add(singleEvent);
                }




            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : salon iksv events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }

            return "";
        }


        public string eventprogramchart(HttpContext context)
        {
            string zone = "", lang = "TR", title = "", summary = "";
            int program = 0, zoneid = 0;
            bool activity = false;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }

                //festivaller datası
                if (!string.IsNullOrEmpty(context.Request.Form["program"]))
                {
                    program = Convert.ToInt32(context.Request.Form["program"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("program boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["activity"]))
                {
                    activity = Convert.ToBoolean(context.Request.Form["activity"].Trim());

                }

                OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
                {
                    Duration = duration, // saniye
                    Location = OutputCacheLocation.Server,
                    VaryByParam = "plugin;program;lang;activity"
                });


                page.ProcessRequest(HttpContext.Current);
                context.Response.Charset = "utf-8";
                context.Response.ContentType = "text/json";

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    var programArticle = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == program && f.Status == 1 && f.StartDate < DateTime.Now && (f.EndDate.Value > DateTime.Now || !f.EndDate.HasValue));
                    if (programArticle != null)
                    {
                        zone = programArticle.Article2.Trim();
                        zoneid = Convert.ToInt32(zone);
                        var festivalArticle = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.NavigationZoneID == zoneid && f.Status == 1);
                        title = (festivalArticle != null ? festivalArticle.Headline : "");
                        summary = (festivalArticle != null ? festivalArticle.Summary : "");
                        if (!string.IsNullOrEmpty(zone))
                        {
                            var programs = new List<vArticlesZonesFull>();
                            var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zoneid && w.Status == 1 && w.LanguageID == lang).ToList();
                            var articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();
                            foreach (var article in articles)
                            {
                                var eventPrograms = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                                eventPrograms = eventPrograms.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                                if (eventPrograms != null)
                                {
                                    programs.AddRange(eventPrograms);
                                }
                            }

                            programs = programs.OrderBy(o => o.Date1).ToList();
                            if (programs != null)
                            {
                                List<programchart> charts = new List<programchart>();
                                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                                var firstDay = programs.FirstOrDefault().Date1.Value;
                                var lastDay = programs.LastOrDefault().Date1.Value;
                                var dayCount = (lastDay - firstDay).TotalDays;
                                var selected = false;
                                var sessions = programs.Select(s => s.Date1.Value.ToShortTimeString()).Distinct().OrderBy(o => o).ToList();


                                for (int i = 0; i <= dayCount; i++)
                                {
                                    var chart = new programchart();
                                    List<programchartItem> events = new List<programchartItem>();

                                    var day = firstDay.AddDays(i);
                                    var dayEvents = programs.Where(w => w.Date1.Value.ToShortDateString() == day.ToShortDateString()).ToList();
                                    var places = dayEvents.Select(s => s.Custom1).Distinct().OrderBy(o => o).ToList();
                                    foreach (var place in places)
                                    {
                                        programchartItem chartItem = new programchartItem();
                                        chartItem.place = place;
                                        chartItem.details = new List<programchartdetail>();
                                        foreach (var session in sessions)
                                        {
                                            programchartdetail detail = new programchartdetail();
                                            detail.status = false;
                                            var eventSession = dayEvents.FirstOrDefault(f => f.Custom1 == place && f.Date1.Value.ToShortTimeString() == session);
                                            if (eventSession != null)
                                            {
                                                var eventarticle = articles.FirstOrDefault(f => f.ArticleID.ToString() == eventSession.Custom10 && f.Status == 1);
                                                if (eventarticle != null)
                                                {
                                                    detail.articleId = eventarticle.ArticleID;
                                                    detail.headline = eventarticle.Headline.Trim();
                                                    detail.ticket = eventSession.Custom2;
                                                    detail.alias = eventarticle.ArticleZoneAlias;
                                                    detail.status = true;
                                                }
                                            }
                                            chartItem.details.Add(detail);
                                        }
                                        events.Add(chartItem);
                                    }

                                    if (day.ToShortDateString() == DateTime.Now.ToShortDateString())
                                    {
                                        selected = true;
                                    }

                                    chart.selected = selected;
                                    chart.day = String.Format("{0:dd}", day);
                                    chart.month = day.Month.ToString();
                                    chart.monthString = dtfi.GetMonthName(day.Month);
                                    chart.year = day.Year.ToString();
                                    chart.date = dtfi.GetMonthName(day.Month) + " " + dtfi.GetDayName(day.DayOfWeek);
                                    //chart.sessions = sessions;
                                    chart.events = events;
                                    chart.status = (events.Count > 0 ? true : false);
                                    charts.Add(chart);

                                }
                                return jss.Serialize(new { status = true, message = "İşlem Başarılı.", data = new { sessions = sessions, days = charts }, title = title, summary = summary });
                            }
                            else
                            {
                                return jss.Serialize(new { status = false, message = "Program data bulunamadı.", data = "" });
                            }
                        }
                        else
                        {
                            return jss.Serialize(new { status = false, message = "Program article alanına aktif dönem zoneid girilmemiş.", data = "" });
                        }
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Program article aktif değil.", data = "" });
                    }
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : eventprogramchart", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }


        public string festivalarchive(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            string alias = "";
            int pageCount = 0, itemCount = 0, currentPage = 0, filetype = 0;

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["alias"]))
                {
                    alias = context.Request.Form["alias"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("alias boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["filetype"]))
                {
                    filetype = Convert.ToInt32(context.Request.Form["filetype"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("filetype boş gönderilemez"));
                    context.Response.End();
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

                var festival = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleZoneAlias == alias && f.Status == 1 && f.StartDate < DateTime.Now && (f.EndDate.Value > DateTime.Now || !f.EndDate.HasValue));
                if (festival != null)
                {
                    var zoneid = festival.NavigationZoneID;
                    var zone = dbContext.Zones.FirstOrDefault(f => f.Id == zoneid && f.Status == "A");
                    if (zone != null)
                    {
                        List<EventDataModel> list = new List<EventDataModel>();
                        var articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zone.Id && w.Status == 1 && w.ClassificationID == 4).ToList();
                        foreach (var article in articles)
                        {

                            var listItem = new EventDataModel();
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.order = article.AzOrder;
                            listItem.alias = article.ArticleZoneAlias;
                            listItem.category = article.ZoneName;
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;

                            var files = dbContext.Files.Where(f => f.ArticleId == article.ArticleID && f.FileTypeId == filetype).ToList();
                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();
                            foreach (var file in files)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }
                            listItem.files = fileList;

                            list.Add(listItem);
                        }

                        double recordCount = 0;
                        recordCount = Convert.ToDouble(list.Count());
                        pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));
                        if (list.Count > itemCount)
                        {
                            list = list.Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                        }

                        return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount });
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "festival article subzone bulunamadı yada pasif", data = "" });
                    }

                }
                return jss.Serialize(new { status = false, message = "festival article aktif veya yayında değil.", data = "" });

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : festivalarchive", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string iksvarchive(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            List<string> zone_ids = new List<string>();
            string lang = "", category = "";
            int pageCount = 0, itemCount = 0, currentPage = 0;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
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

                if (!string.IsNullOrEmpty(context.Request.Form["category"]))
                {
                    category = context.Request.Form["category"].Trim();

                }

                List<EventDataModel> list = new List<EventDataModel>();
                var articles = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID.ToString()) && w.Status == 1 && w.ClassificationID == 4 && !(w.Date1.Value < DateTime.Now && w.Date2.Value > DateTime.Now)).ToList();

                foreach (var article in articles)
                {
                    List<Program> programList = new List<Program>();

                    var programs = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == 5).OrderBy(o => o.Date1).ToList();
                    programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                    //foreach (var program in programs)
                    //{
                    //    var programListItem = new Program();
                    //    programListItem.description = program.Article1;
                    //    programListItem.date = program.Date1.Value;
                    //    programListItem.dateString = program.Date1.Value.ToString();
                    //    programListItem.place = program.Custom1.ToString();
                    //    programListItem.ticketUrl = program.Custom2.ToString();
                    //    programListItem.longitude = program.Custom3.ToString();
                    //    programListItem.latitude = program.Custom4.ToString();
                    //    programListItem.flag1 = program.Flag1;
                    //    programList.Add(programListItem);
                    //}

                    var listItem = new EventDataModel();
                    listItem.articleId = article.ArticleID;
                    listItem.headline = article.Headline;
                    listItem.zone = article.ZoneName;
                    listItem.zoneId = article.ZoneID;

                    listItem.order = article.AzOrder;
                    listItem.alias = article.ArticleZoneAlias;
                    listItem.category = article.ZoneName;
                    listItem.thumb = article.Custom1;

                    var files = dbContext.Files.Where(f => f.ArticleId == article.ArticleID).ToList();
                    List<ArticleFileItem> fileList = new List<ArticleFileItem>();
                    foreach (var file in files)
                    {
                        var eventFile = new ArticleFileItem();
                        eventFile.id = file.FileId;
                        eventFile.type = file.FileTypeId;
                        eventFile.title = file.Title;
                        eventFile.commnent = file.Comment;
                        eventFile.file1 = file.File1;
                        eventFile.file2 = file.File2;
                        eventFile.file3 = file.File3;
                        eventFile.file4 = file.File4;
                        eventFile.file5 = file.File5;
                        eventFile.file6 = file.File6;
                        eventFile.file7 = file.File7;
                        eventFile.file8 = file.File8;
                        eventFile.file9 = file.File9;
                        eventFile.file10 = file.File10;
                        eventFile.order = file.FileOrder;
                        fileList.Add(eventFile);
                    }
                    listItem.files = fileList;

                    var programNow = programs.OrderBy(o => o.Date1).ToList().FirstOrDefault();
                    if (programNow != null)
                    {
                        listItem.date = programNow.Date1.ToString();
                        listItem.place = programNow.Custom1.ToString();
                        listItem.dateFormatted = programNow.Date1.Value;
                        DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;

                        listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                    }

                    list.Add(listItem);
                }


                if (!string.IsNullOrEmpty(category))
                {
                    category = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(category));
                    list = list.Where(w => w.category == category).ToList();
                }
                list = list.OrderByDescending(o => o.dateFormatted).ToList();


                double recordCount = 0;
                recordCount = Convert.ToDouble(list.Count());
                pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));
                if (list.Count > itemCount)
                {
                    list = list.Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                }

                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : iksv archive", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public List<chart> getFilms(CmsDbContext dbContext, int program, bool mainPage,
            bool paralel, string place, int month, int year, string session, HttpContext context, int dayF, string lang
            )
        {
            var charts = new List<chart>();

            var programArticle = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == program && f.Status == 1 && f.StartDate < DateTime.Now && (f.EndDate.Value > DateTime.Now || !f.EndDate.HasValue));

            if (programArticle != null)
            {
                string zone = programArticle.Article2.Trim();

                if (!string.IsNullOrEmpty(zone))
                {
                    var programs = new List<vArticlesZonesFull>();
                    var articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == zone && w.Status == 1 && w.ClassificationID == etkinlikClassification).ToList();

                    if (mainPage)
                    {
                        articles = articles.Where(w => w.Flag1 == false).ToList();
                    }

                    if (paralel)
                    {
                        articles = articles.Where(w => w.Flag3 == false).ToList();
                    }

                    var subArticles = articles.Where(w => !string.IsNullOrEmpty(w.Custom9)).ToList();
                    if (subArticles != null)
                    {
                        var subArticlesIds = subArticles.Select(s => s.ArticleID).Distinct().ToList();
                        articles = articles.Where(w => !subArticlesIds.Contains(w.ArticleID)).ToList();
                    }

                    foreach (var article in articles)
                    {
                        var eventPrograms = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification && w.LanguageID == lang).OrderBy(o => o.Date1).ToList();
                        eventPrograms = eventPrograms.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();


                        if (eventPrograms != null)
                        {
                            programs.AddRange(eventPrograms);
                        }
                    }

                    //filtre alanı
                    if (!string.IsNullOrEmpty(place))
                    {
                        place = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(place));
                        //programs = programs.Where(w => w.events.Any(p => p.place == place)).ToList();
                        programs = programs.Where(w => w.Custom1.Equals(place)).ToList();
                    }

                    if (!string.IsNullOrEmpty(session))
                    {
                        programs = programs.Where(w => w.Date1.Value.ToShortTimeString() == session).ToList();
                    }

                    if (!string.IsNullOrEmpty(context.Request.Form["day"]))
                    {
                        dayF = Convert.ToInt32(context.Request.Form["day"].Trim());
                        programs = programs.Where(w => (int)w.Date1.Value.DayOfWeek == (dayF - 1)).ToList();
                    }

                    if (month > 0)
                    {
                        programs = programs.Where(w => (int)w.Date1.Value.Month == month).ToList();
                    }

                    if (year > 0)
                    {
                        programs = programs.Where(w => (int)w.Date1.Value.Year == year).ToList();
                    }

                    //filtre alanı end

                    programs = programs.OrderBy(o => o.Date1).ToList();

                    if (programs != null)
                    {
                        DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                        var firstDay = programs.FirstOrDefault().Date1.Value;
                        var lastDay = programs.LastOrDefault().Date1.Value;
                        var dayCount = Math.Ceiling((lastDay - firstDay).TotalDays);
                        if (firstDay.ToShortDateString() == lastDay.ToShortDateString() && dayCount == 0)
                        {
                            dayCount = 1;
                        }
                        for (int i = 0; i <= dayCount; i++)
                        {
                            var chart = new chart();
                            List<chartItem> events = new List<chartItem>();

                            var day = firstDay.AddDays(i);
                            var dayEvents = programs.Where(w => w.Date1.Value.ToShortDateString() == day.ToShortDateString()).ToList();
                            foreach (var item in dayEvents)
                            {
                                var eventarticle = articles.FirstOrDefault(f => f.ArticleID.ToString() == item.Custom10 && f.Status == 1);
                                if (eventarticle != null)
                                {
                                    var file = dbContext.Files.FirstOrDefault(f => f.ArticleId == eventarticle.ArticleID && f.FileTypeId == 15);
                                    var eventItem = new chartItem();
                                    eventItem.headline = eventarticle.Headline;
                                    eventItem.alias = eventarticle.ArticleZoneAlias;
                                    eventItem.thumb = (file != null ? file.ArticleId + "_" + file.File1 : "");
                                    eventItem.place = item.Custom1;
                                    eventItem.ticket = item.Custom2;
                                    eventItem.flag1 = item.Flag1;
                                    eventItem.activity = eventarticle.Flag1;
                                    eventItem.date = item.Date1.Value.Day + " " + dtfi.GetMonthName(item.Date1.Value.Month) + " " + dtfi.GetDayName(item.Date1.Value.DayOfWeek) + " " + item.Date1.Value.ToShortTimeString();
                                    events.Add(eventItem);
                                }
                            }

                            chart.day = String.Format("{0:dd}", day);
                            chart.month = day.Month.ToString();
                            chart.monthString = dtfi.GetMonthName(day.Month);
                            chart.year = day.Year.ToString();
                            chart.date = dtfi.GetMonthName(day.Month) + " " + dtfi.GetDayName(day.DayOfWeek);
                            chart.events = events;
                            chart.status = (events.Count > 0 ? true : false);
                            chart.fullDate = day;

                            charts.Add(chart);
                        }
                    }
                }
            }

            return charts;
        }

        public string iksvChart(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            string
                lang = "TR",
                title = "",
                summary = "",
                place = "",
                category = "",
                session = "";

            var idList = new List<int>();

            bool mainPage = false, paralel = false;

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["programFilm"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programFilm"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programAltkat"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programAltkat"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programBienal"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programBienal"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programBienalFilm"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programBienalFilm"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programCaz"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programCaz"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programTiyatro"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programTiyatro"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programMusic"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programMusic"].Trim()));

                if (!string.IsNullOrEmpty(context.Request.Form["programSalon"]))
                    idList.Add(Convert.ToInt32(context.Request.Form["programSalon"].Trim()));


                return jss.Serialize("");
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : chart", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }


        public string chart(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            string zone = "", lang = "TR", title = "", summary = "", place = "", category = "", section = "", tag = "", session = "";
            int program = 0, month = 0, year = 0, dayF = 0;
            bool mainPage = false, paralel = false;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }

                //festivaller datası
                if (!string.IsNullOrEmpty(context.Request.Form["program"]))
                {
                    program = Convert.ToInt32(context.Request.Form["program"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("program boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["month"]))
                {
                    month = Convert.ToInt32(context.Request.Form["month"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["year"]))
                {
                    year = Convert.ToInt32(context.Request.Form["year"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["place"]))
                {
                    place = context.Request.Form["place"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["category"]))
                {
                    category = context.Request.Form["category"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["mainPage"]))
                {
                    mainPage = Convert.ToBoolean(context.Request.Form["mainPage"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["paralel"]))
                {
                    paralel = Convert.ToBoolean(context.Request.Form["paralel"].Trim());

                }

                var programArticle = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == program && f.Status == 1 && f.StartDate < DateTime.Now && (f.EndDate.Value > DateTime.Now || !f.EndDate.HasValue));
                if (programArticle != null)
                {
                    zone = programArticle.Article2.Trim();
                    var festivalArticle = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.NavigationZoneID.ToString() == zone && f.Status == 1);
                    title = (festivalArticle != null ? festivalArticle.Headline : "");
                    summary = (festivalArticle != null ? festivalArticle.Summary : "");
                    if (!string.IsNullOrEmpty(zone))
                    {
                        var programs = new List<vArticlesZonesFull>();
                        var articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == zone && w.Status == 1 && w.ClassificationID == etkinlikClassification).ToList();

                        if (mainPage)
                        {
                            articles = articles.Where(w => w.Flag1 == false).ToList();
                        }

                        if (paralel)
                        {
                            articles = articles.Where(w => w.Flag3 == false).ToList();
                        }

                        var subArticles = articles.Where(w => !string.IsNullOrEmpty(w.Custom9)).ToList();
                        if (subArticles != null)
                        {
                            var subArticlesIds = subArticles.Select(s => s.ArticleID).Distinct().ToList();
                            articles = articles.Where(w => !subArticlesIds.Contains(w.ArticleID)).ToList();
                        }

                        foreach (var article in articles)
                        {
                            var eventPrograms = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification && w.LanguageID == lang).OrderBy(o => o.Date1).ToList();
                            eventPrograms = eventPrograms.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();


                            if (eventPrograms != null)
                            {
                                programs.AddRange(eventPrograms);
                            }
                        }

                        //filtre alanı
                        if (!string.IsNullOrEmpty(place))
                        {
                            place = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(place));
                            //programs = programs.Where(w => w.events.Any(p => p.place == place)).ToList();
                            programs = programs.Where(w => w.Custom1.Equals(place)).ToList();
                        }

                        if (!string.IsNullOrEmpty(session))
                        {
                            programs = programs.Where(w => w.Date1.Value.ToShortTimeString() == session).ToList();
                        }

                        if (!string.IsNullOrEmpty(context.Request.Form["day"]))
                        {
                            dayF = Convert.ToInt32(context.Request.Form["day"].Trim());
                            programs = programs.Where(w => (int)w.Date1.Value.DayOfWeek == (dayF - 1)).ToList();
                        }

                        if (month > 0)
                        {
                            programs = programs.Where(w => (int)w.Date1.Value.Month == month).ToList();
                        }

                        if (year > 0)
                        {
                            programs = programs.Where(w => (int)w.Date1.Value.Year == year).ToList();
                        }

                        //filtre alanı end

                        programs = programs.OrderBy(o => o.Date1).ToList();
                        if (programs != null)
                        {
                            List<chart> charts = new List<chart>();
                            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                            var firstDay = programs.FirstOrDefault().Date1.Value;
                            var lastDay = programs.LastOrDefault().Date1.Value;
                            var dayCount = Math.Ceiling((lastDay - firstDay).TotalDays);
                            if (firstDay.ToShortDateString() == lastDay.ToShortDateString() && dayCount == 0)
                            {
                                dayCount = 1;
                            }
                            for (int i = 0; i <= dayCount; i++)
                            {
                                var chart = new chart();
                                List<chartItem> events = new List<chartItem>();

                                var day = firstDay.AddDays(i);
                                var dayEvents = programs.Where(w => w.Date1.Value.ToShortDateString() == day.ToShortDateString()).ToList();
                                foreach (var item in dayEvents)
                                {
                                    var eventarticle = articles.FirstOrDefault(f => f.ArticleID.ToString() == item.Custom10 && f.Status == 1);
                                    if (eventarticle != null)
                                    {
                                        var file = dbContext.Files.FirstOrDefault(f => f.ArticleId == eventarticle.ArticleID && f.FileTypeId == 15);
                                        var eventItem = new chartItem();
                                        eventItem.headline = eventarticle.Headline;
                                        eventItem.alias = eventarticle.ArticleZoneAlias;
                                        eventItem.thumb = (file != null ? file.ArticleId + "_" + file.File1 : "");
                                        eventItem.place = item.Custom1;
                                        eventItem.ticket = item.Custom2;
                                        eventItem.flag1 = item.Flag1;
                                        eventItem.activity = eventarticle.Flag1;
                                        eventItem.date = item.Date1.Value.Day + " " + dtfi.GetMonthName(item.Date1.Value.Month) + " " + dtfi.GetDayName(item.Date1.Value.DayOfWeek) + " " + item.Date1.Value.ToShortTimeString();
                                        events.Add(eventItem);
                                    }
                                }

                                chart.day = String.Format("{0:dd}", day);
                                chart.month = day.Month.ToString();
                                chart.monthString = dtfi.GetMonthName(day.Month);
                                chart.year = day.Year.ToString();
                                chart.date = dtfi.GetMonthName(day.Month) + " " + dtfi.GetDayName(day.DayOfWeek);
                                chart.events = events;
                                chart.status = (events.Count > 0 ? true : false);
                                chart.fullDate = day;

                                charts.Add(chart);

                            }

                            string nearestDay = "", nearestMonth = "";
                            int indexOfnearest = 0;
                            var nearestDate = charts.FirstOrDefault(f => f.fullDate >= DateTime.Now && f.status == true);
                            if (nearestDate != null)
                            {
                                nearestDay = nearestDate.day;
                                nearestMonth = nearestDate.month;
                                indexOfnearest = charts.IndexOf(nearestDate);
                            }

                            return jss.Serialize(new { status = true, message = "İşlem Başarılı.", data = charts, title = title, summary = summary, nearestDay = nearestDay, nearestMonth = nearestMonth, indexOfnearest = indexOfnearest });
                        }
                        else
                        {
                            return jss.Serialize(new { status = false, message = "Program data bulunamadı.", data = "" });
                        }
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Program article alanına aktif dönem zoneid girilmemiş.", data = "" });
                    }
                }
                else
                {
                    return jss.Serialize(new { status = false, message = "Program article aktif değil.", data = "" });
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : chart", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string filters(HttpContext context)
        {
            List<int> zone_ids = new List<int>();
            List<string> places = new List<string>();
            List<string> categories = new List<string>();
            List<string> sessions = new List<string>();
            List<List<Int32>> activeDates = new List<List<Int32>>();
            Dictionary<int, string> days = new Dictionary<int, string>();
            Dictionary<int, string> months = new Dictionary<int, string>();
            List<string> years = new List<string>();
            List<SectionDataModel> sections = new List<SectionDataModel>();
            List<vArticlesZonesFull> articles = null;
            bool activity = false, paralel = false;
            string musicZone = "", filmZone = "", cazZone = "", tiyatroZone = "", bienalZone = "", salonZone = "", tasarimZone = "", altkatZone = "", lang = "";

            int programMusic = 0, programFilm = 0, programCaz = 0, programTiyatro = 0, programBienal = 0, programSalon = 0, programTasarim = 0, programAltkat = 0; ;

            OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
            {
                Duration = duration, // saniye
                Location = OutputCacheLocation.Server,
                VaryByParam = "plugin;zone_id;lang;activity;paralel;programMusic;programFilm;programCaz;programTiyatro;programBienal;programSalon;programTasarim"
            });

            page.ProcessRequest(HttpContext.Current);
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "text/json";
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["activity"]))
                {
                    activity = Convert.ToBoolean(context.Request.Form["activity"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["paralel"]))
                {
                    paralel = Convert.ToBoolean(context.Request.Form["paralel"].Trim());

                }

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;

                //festivaller datası
                //muzik
                if (!string.IsNullOrEmpty(context.Request.Form["programMusic"]))
                {
                    programMusic = Convert.ToInt32(context.Request.Form["programMusic"].Trim());

                }

                //film
                if (!string.IsNullOrEmpty(context.Request.Form["programFilm"]))
                {
                    programFilm = Convert.ToInt32(context.Request.Form["programFilm"].Trim());

                }

                //caz
                if (!string.IsNullOrEmpty(context.Request.Form["programCaz"]))
                {
                    programCaz = Convert.ToInt32(context.Request.Form["programCaz"].Trim());

                }

                //tiyatro
                if (!string.IsNullOrEmpty(context.Request.Form["programTiyatro"]))
                {
                    programTiyatro = Convert.ToInt32(context.Request.Form["programTiyatro"].Trim());

                }

                //bienal
                if (!string.IsNullOrEmpty(context.Request.Form["programBienal"]))
                {
                    programBienal = Convert.ToInt32(context.Request.Form["programBienal"].Trim());

                }

                //salon
                if (!string.IsNullOrEmpty(context.Request.Form["programSalon"]))
                {
                    programSalon = Convert.ToInt32(context.Request.Form["programSalon"].Trim());

                }

                //tasarım
                if (!string.IsNullOrEmpty(context.Request.Form["programTasarim"]))
                {
                    programTasarim = Convert.ToInt32(context.Request.Form["programTasarim"].Trim());
                }

                //altkat
                if (!string.IsNullOrEmpty(context.Request.Form["programAltkat"]))
                {
                    programAltkat = Convert.ToInt32(context.Request.Form["programAltkat"]);
                }

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    if (programCaz > 0)
                    {
                        var cazData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programCaz);
                        if (cazData != null)
                        {
                            cazZone = cazData.Article2.Trim();
                        }
                    }

                    if (programMusic > 0)
                    {
                        var musicData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programMusic);
                        if (musicData != null)
                        {
                            musicZone = musicData.Article2.Trim();
                        }
                    }

                    if (programFilm > 0)
                    {
                        var filmData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programFilm);
                        if (filmData != null)
                        {
                            filmZone = filmData.Article2.Trim();
                        }
                    }

                    if (programBienal > 0)
                    {
                        var bienalData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programBienal);
                        if (bienalData != null)
                        {
                            bienalZone = bienalData.Article2.Trim();
                        }
                    }

                    if (programTiyatro > 0)
                    {
                        var tiyatroData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programTiyatro);
                        if (tiyatroData != null)
                        {
                            tiyatroZone = tiyatroData.Article2.Trim();
                        }
                    }

                    if (programSalon > 0)
                    {
                        var salonData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programSalon);
                        if (salonData != null)
                        {
                            salonZone = salonData.Article2.Trim();
                        }
                    }

                    if (programTasarim > 0)
                    {
                        var tasarimData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programTasarim);
                        if (tasarimData != null)
                        {
                            tasarimZone = tasarimData.Article2.Trim();
                        }
                    }

                    if (programAltkat > 0)
                    {
                        var altkatData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programAltkat);
                        if (altkatData != null)
                        {
                            altkatZone = altkatData.Article2.Trim();
                        }
                    }

                    if (zone_ids.Count > 0)
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID) && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification && w.LanguageID == lang).ToList();
                        //etkinlik - gala
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification).ToList();

                        if (articles.Count > 0)
                        {
                            var zones = articles.Select(s => s.ZoneName).ToList().Distinct().ToList();
                            categories.AddRange(zones);
                        }

                        foreach (var article in articles)
                        {
                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(musicZone))
                    {
                        var musicZoneId = Convert.ToInt32(musicZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == musicZoneId && w.Status == 1 && w.LanguageID == lang).ToList();
                        //muzik festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Müzik Yan Etkinlik" : "Music Side Event") : (lang == "tr" ? "Müzik" : "Music"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //muzik bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }

                    if (!string.IsNullOrEmpty(filmZone))
                    {
                        var filmZoneId = Convert.ToInt32(filmZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == filmZoneId && w.Status == 1 && w.LanguageID == lang).ToList();
                        //film festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();

                        sessions = eventDatas.Where(w => w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).Select(s => s.Date1.Value.ToShortTimeString()).Distinct().OrderBy(o => o).ToList();
                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Film Yan Etkinlik" : "Film Side Event") : (lang == "tr" ? "Film" : "Film"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }

                        //film bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }

                    if (!string.IsNullOrEmpty(cazZone))
                    {
                        var cazZoneId = Convert.ToInt32(cazZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == cazZone && w.Status == 1 && w.LanguageID == lang).ToList();
                        //caz festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Caz Yan Etkinlik" : "Jazz Side Event") : (lang == "tr" ? "Caz" : "Jazz"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //caz bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }


                    if (!string.IsNullOrEmpty(tiyatroZone))
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == tiyatroZone && w.Status == 1 && w.LanguageID == lang).ToList();
                        //tiyatro festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification).ToList();

                        if (activity)
                        {
                            articles.RemoveAll(r => r.Flag1 == !activity);
                        }
                        else
                        {
                            articles.RemoveAll(r => r.Flag1 == !activity);
                        }


                        if (paralel)
                        {
                            articles.RemoveAll(r => r.Flag3 == !paralel);
                        }
                        else
                        {
                            articles.RemoveAll(r => r.Flag3 == !paralel);
                        }

                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Tiyatro Yan Etkinlik" : "Theater Side Event") : (article.Flag3 ? (lang == "tr" ? "Paralel Etkinlik" : "Parallel Event") : (lang == "tr" ? "Tiyatro" : "Theater")));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //tiyatro bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }



                    if (!string.IsNullOrEmpty(bienalZone))
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == bienalZone && w.Status == 1 && w.LanguageID == lang).ToList();
                        //bienal festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Bienal Yan Etkinlik" : "Biennial Side Event") : (lang == "tr" ? "Bienal" : "Biennial"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //bineal bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }



                    if (!string.IsNullOrEmpty(salonZone))
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == salonZone && w.Status == 1 && w.LanguageID == lang).ToList();
                        //salon festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Salon Yan Etkinlik" : "Salon Side Event") : (lang == "tr" ? "Salon" : "Saloon"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString() && w.Date1.Value >= DateTime.Now).ToList();
                            foreach (var program in programs)
                            {
                                List<Int32> dates = new List<Int32>();
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    dates.Add(program.Date1.Value.Month);
                                    dates.Add(program.Date1.Value.Day);
                                    dates.Add(program.Date1.Value.Year);
                                    activeDates.Add(dates);

                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //salon bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }

                    if (!string.IsNullOrEmpty(tasarimZone))
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == tasarimZone && w.Status == 1 && w.LanguageID == lang).ToList();
                        //Tasarım data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Tasarım" : "Tasarım"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //Tasarım bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }

                    if (!string.IsNullOrEmpty(altkatZone))
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == altkatZone && w.Status == 1 && w.LanguageID == lang).ToList();
                        //altkat festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                        foreach (var article in articles)
                        {
                            string categoryName = (article.Flag1 ? (lang == "tr" ? "Alt Kat Yan Etkinlik" : "Alt Kat Side Event") : (lang == "tr" ? "Alt Kat" : "Alt Kat"));
                            categories.Add(categoryName);

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                            foreach (var program in programs)
                            {
                                //mekanlar filtresi
                                if (!places.Contains(program.Custom1.Trim()))
                                {
                                    places.Add(program.Custom1.Trim());
                                }

                                if (program.Date1.HasValue)
                                {
                                    //gün
                                    if (!days.ContainsKey((int)program.Date1.Value.DayOfWeek + 1))
                                    {
                                        days.Add((int)program.Date1.Value.DayOfWeek + 1, dtfi.GetDayName(program.Date1.Value.DayOfWeek));
                                    }

                                    //ay
                                    if (!months.ContainsKey(program.Date1.Value.Month))
                                    {
                                        months.Add(program.Date1.Value.Month, dtfi.GetMonthName(program.Date1.Value.Month));
                                    }

                                    //yıl
                                    string year = program.Date1.Value.Year.ToString();
                                    if (years.FirstOrDefault(f => f == year) == null)
                                    {
                                        years.Add(year);
                                    }
                                }
                            }
                        }
                        //alt kat bölümler filtre
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikKategoriClassification && w.Flag1 == activity).OrderBy(o => o.AzOrder).ToList();
                        var mainSections = articles.Where(w => string.IsNullOrEmpty(w.Custom1)).OrderBy(o => o.AzOrder).ToList();
                        foreach (var article in mainSections)
                        {
                            var section = new SectionDataModel();
                            section.id = article.ArticleID;
                            section.name = article.Headline;
                            section.alias = CmsHelper.StringToAlphaNumeric(article.Headline, true);
                            section.subSection = new List<SectionDataModel>();
                            var subSections = articles.Where(f => f.Custom1 == article.ArticleID.ToString()).ToList();
                            if (subSections.Count > 0)
                            {
                                section.isMain = true;
                                foreach (var sub in subSections)
                                {
                                    var subSection = new SectionDataModel();
                                    subSection.id = sub.ArticleID;
                                    subSection.name = sub.Headline;
                                    subSection.alias = CmsHelper.StringToAlphaNumeric(sub.Headline, true);
                                    section.subSection.Add(subSection);
                                }
                            }
                            sections.Add(section);
                        }
                    }
                }

                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = new { bolumler = sections, seanslar = sessions, kategoriler = categories.Distinct().ToList(), mekanlar = places.Distinct().ToList().OrderBy(o => o).ToList(), gunler = days.ToList(), aylar = months.ToList(), yillar = years, aktifTarihler = activeDates } });

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Filters", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string events(HttpContext context)
        {
            List<int> zone_ids = new List<int>();
            string musicZone = "",
                filmZone = "",
                cazzZone = "",
                tiyatroZone = "",
                bienalZone = "",
                bienalFilmZone = "",
                salonZone = "",
                tasarimZone = "",
                altkatZone = "",
                lang = "",
                place = "",
                category = "",
                section = "",
                tag = "",
                order = "",
                session = "";

            int pageCount = 0;
            int itemCount = 0,
                currentPage = 0,
                programMusic = 0,
                programFilm = 0,
                programTiyatro = 0,
                programCazz = 0,
                programSalon = 0,
                programBienal = 0,
                programBienalFilm = 0,
                programTasarim = 0,
                programAltkat = 0,
                month = 0,
                year = 0,
                dayNo = 0,
                day = 0;

            bool activity = false,
                paralel = false,
                getAll = true,
                highlight = false,
                getNew = false;

            OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
            {
                Duration = duration, // saniye
                Location = OutputCacheLocation.Server,
                VaryByParam = "plugin;zone_id;lang;itemCount;currentPage;dayno;month;year;place;category;tag;activity;paralel;getall;section;session;order;highlight;getnew;programMusic;programFilm;programCaz;programBienal;programTiyatro;programSalon;programTasarim"
            });

            page.ProcessRequest(HttpContext.Current);
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "text/json";

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                }


                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
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

                if (!string.IsNullOrEmpty(context.Request.Form["dayno"]))
                {
                    dayNo = Convert.ToInt32(context.Request.Form["dayno"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["day"]))
                {
                    day = Convert.ToInt32(context.Request.Form["day"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["month"]))
                {
                    month = Convert.ToInt32(context.Request.Form["month"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["year"]))
                {
                    year = Convert.ToInt32(context.Request.Form["year"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["place"]))
                {
                    place = context.Request.Form["place"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["category"]))
                {
                    category = context.Request.Form["category"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["tag"]))
                {
                    tag = context.Request.Form["tag"].Trim();
                    //tag = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(tag));
                }

                if (!string.IsNullOrEmpty(context.Request.Form["activity"]))
                {
                    activity = Convert.ToBoolean(context.Request.Form["activity"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["paralel"]))
                {
                    paralel = Convert.ToBoolean(context.Request.Form["paralel"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["getall"]))
                {
                    getAll = Convert.ToBoolean(context.Request.Form["getall"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["section"]))
                {
                    section = context.Request.Form["section"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["session"]))
                {
                    session = context.Request.Form["session"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["order"]))
                {
                    order = context.Request.Form["order"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["highlight"]))
                {
                    highlight = Convert.ToBoolean(context.Request.Form["highlight"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["getnew"]))
                {
                    getNew = Convert.ToBoolean(context.Request.Form["getnew"].Trim());

                }

                //festivaller datası
                //muzik
                if (!string.IsNullOrEmpty(context.Request.Form["programMusic"]))
                {
                    programMusic = Convert.ToInt32(context.Request.Form["programMusic"].Trim());
                }

                //film
                if (!string.IsNullOrEmpty(context.Request.Form["programFilm"]))
                {
                    programFilm = Convert.ToInt32(context.Request.Form["programFilm"].Trim());
                }

                //cazz
                if (!string.IsNullOrEmpty(context.Request.Form["programCaz"]))
                {
                    programCazz = Convert.ToInt32(context.Request.Form["programCaz"].Trim());
                }

                //bienal
                if (!string.IsNullOrEmpty(context.Request.Form["programBienal"]))
                {
                    programBienal = Convert.ToInt32(context.Request.Form["programBienal"].Trim());
                }

                //bienal
                if (!string.IsNullOrEmpty(context.Request.Form["programBienalFilm"]))
                {
                    programBienalFilm = Convert.ToInt32(context.Request.Form["programBienalFilm"].Trim());
                }

                //tiyatro
                if (!string.IsNullOrEmpty(context.Request.Form["programTiyatro"]))
                {
                    programTiyatro = Convert.ToInt32(context.Request.Form["programTiyatro"].Trim());
                }

                //salon
                if (!string.IsNullOrEmpty(context.Request.Form["programSalon"]))
                {
                    programSalon = Convert.ToInt32(context.Request.Form["programSalon"].Trim());
                }

                //tasarım
                if (!string.IsNullOrEmpty(context.Request.Form["programTasarim"]))
                {
                    programTasarim = Convert.ToInt32(context.Request.Form["programTasarim"].Trim());
                }

                //altkat
                if (!string.IsNullOrEmpty(context.Request.Form["programAltkat"]))
                {
                    programAltkat = Convert.ToInt32(context.Request.Form["programAltkat"]);
                }

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<EventDataModel> list = new List<EventDataModel>();
                Tag tagData = null;

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    tagData = dbContext.Tags.FirstOrDefault(f => f.Alias == tag);

                    //festivaller datası
                    //muzik
                    if (programMusic > 0)
                    {
                        vArticlesZonesFull musicData = null;
                        musicData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programMusic);

                        if (musicData != null)
                        {
                            musicZone = musicData.Article2.Trim();
                        }
                    }

                    //film
                    if (programFilm > 0)
                    {
                        vArticlesZonesFull filmData = null;
                        filmData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programFilm);

                        if (filmData != null)
                        {
                            filmZone = filmData.Article2.Trim();
                        }
                    }

                    //cazz
                    if (programCazz > 0)
                    {
                        vArticlesZonesFull cazzData = null;
                        cazzData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programCazz);

                        if (cazzData != null)
                        {
                            cazzZone = cazzData.Article2.Trim();
                        }
                    }

                    //bienal
                    if (programBienal > 0)
                    {
                        vArticlesZonesFull bienalData = null;
                        bienalData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programBienal);

                        if (bienalData != null)
                        {
                            bienalZone = bienalData.Article2.Trim();
                        }
                    }

                    //bienal film
                    if (programBienalFilm > 0)
                    {
                        vArticlesZonesFull bienalFilmData = null;
                        bienalFilmData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programBienalFilm);

                        if (bienalFilmData != null)
                        {
                            bienalFilmZone = bienalFilmData.Article2.Trim();
                        }
                    }

                    //bienal
                    if (programTiyatro > 0)
                    {
                        vArticlesZonesFull tiyatroData = null;
                        tiyatroData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programTiyatro);

                        if (tiyatroData != null)
                        {
                            tiyatroZone = tiyatroData.Article2.Trim();
                        }
                    }


                    //salon
                    if (programSalon > 0)
                    {
                        vArticlesZonesFull salonData = null;
                        salonData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programSalon);

                        if (salonData != null)
                        {
                            salonZone = salonData.Article2.Trim();
                        }
                    }

                    //tasarım
                    if (programTasarim > 0)
                    {
                        vArticlesZonesFull tasarimData = null;
                        tasarimData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programTasarim);

                        if (tasarimData != null)
                        {
                            tasarimZone = tasarimData.Article2.Trim();
                        }
                    }

                    //altkat
                    if (programAltkat > 0)
                    {
                        vArticlesZonesFull altkatData = null;
                        altkatData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programAltkat);

                        if (altkatData != null)
                        {
                            altkatZone = altkatData.Article2.Trim();
                        }
                    }

                    List<vArticlesZonesFull> articles = null;
                    #region iksv gala - etkinlik

                    if (zone_ids.Count > 0)
                    {
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID) && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity && w.Date2 != null && w.Date2.Value > DateTime.Now && string.IsNullOrEmpty(w.Custom9)).ToList();

                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();

                            List<vArticlesZonesFull> programs = null;
                            var listItem = new EventDataModel();

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.place = program.Custom1.Trim().ToString();
                                programListItem.ticketUrl = program.Custom2.ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;
                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.alias = article.ArticleZoneAlias;
                            listItem.category = article.ZoneName;
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.articleid = file.ArticleId;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }
                    #endregion

                    #region müzik

                    if (!string.IsNullOrEmpty(musicZone))
                    {
                        var musicZoneId = Convert.ToInt32(musicZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == musicZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //muzik festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programListItem.flag2 = program.Flag2;
                                programListItem.flag3 = program.Flag3;
                                programListItem.flag4 = program.Flag4;
                                programListItem.flag5 = program.Flag5;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();

                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }
                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Müzik" : "Music"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;



                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);

                        }
                    }

                    #endregion

                    #region film

                    if (!string.IsNullOrEmpty(filmZone))
                    {
                        var filmZoneId = Convert.ToInt32(filmZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == filmZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //film festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity && string.IsNullOrEmpty(w.Custom9)).ToList();

                        //var articleIds = articles.Select(s => s.ArticleID).ToList();
                        //var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();

                            var programs = eventDatas.Where(w => w.Status == 1 && w.ClassificationID == etkinlikProgramClassification);
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString());
                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programList.Add(programListItem);
                            }

                            programList = programList.OrderBy(o => o.date).ToList();
                            var listItem = new EventDataModel();
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Etkinlik" : "Event") : (lang == "tr" ? "Film" : "Film"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = dbContext.Files.Where(w => w.ArticleId == article.ArticleID);
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }
                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            list.Add(listItem);
                        }

                    }

                    #endregion

                    #region cazz

                    if (!string.IsNullOrEmpty(cazzZone))
                    {
                        var cazzZoneId = Convert.ToInt32(cazzZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == cazzZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //muzik festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity && string.IsNullOrEmpty(w.Custom9)).ToList();
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;
                                listItem.time = programNow.Date1.Value.ToShortTimeString();

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Caz" : "Jazz"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }

                    #endregion

                    #region bienal film
                    if (!string.IsNullOrEmpty(bienalFilmZone))
                    {
                        var bienalFilmZoneId = Convert.ToInt32(bienalFilmZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == bienalFilmZoneId
                        && w.Status == 1
                        && w.ClassificationID != etkinlikKategoriClassification)
                            .ToList();

                        //bienalZone festival data
                        articles = eventDatas
                            .Where(w => w.ClassificationID == etkinlikClassification &&
                             w.Flag1 == activity)
                            .ToList();

                        var articleIds = articles
                            .Select(s => s.ArticleID)
                            .ToList();

                        var files = dbContext
                            .Files
                            .Where(f => articleIds.Contains(f.ArticleId))
                            .ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas
                                .Where(w => w.ZoneID == article.ZoneID &&
                                w.Status == 1 &&
                                w.ClassificationID == etkinlikProgramClassification)
                                .OrderBy(o => o.Date1)
                                .ToList();

                            programs = programs
                                .Where(w => w.Custom10 == article.ArticleID
                                .ToString())
                                .ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programList.Add(programListItem);
                            }

                            var programNow = programs
                                .Where(w => w.Date1 > DateTime.Now)
                                .ToList()
                                .FirstOrDefault();

                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;
                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Bienal" : "Biennial"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }
                    #endregion
                    #region bienal

                    if (!string.IsNullOrEmpty(bienalZone))
                    {
                        var bienalZoneId = Convert.ToInt32(bienalZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == bienalZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //bienalZone festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Bienal" : "Biennial"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }

                    #endregion

                    #region tiyatro

                    if (!string.IsNullOrEmpty(tiyatroZone))
                    {
                        var tiyatroZoneId = Convert.ToInt32(tiyatroZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == tiyatroZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //tiyatro festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification).ToList();

                        if (activity)
                        {
                            articles.RemoveAll(r => r.Flag1 == !activity);
                        }
                        else
                        {
                            articles.RemoveAll(r => r.Flag1 == !activity);
                        }


                        if (paralel)
                        {
                            articles.RemoveAll(r => r.Flag3 == !paralel);
                        }
                        else
                        {
                            articles.RemoveAll(r => r.Flag3 == !paralel);
                        }
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;
                                listItem.time = programNow.Date1.Value.ToShortTimeString();

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Tiyatro Yan Etkinlik" : "Theater Side Event") : (article.Flag3 ? (lang == "tr" ? "Paralel Etkinlik" : "Parallel Event") : (lang == "tr" ? "Tiyatro" : "Theater")));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }

                    #endregion

                    #region salon

                    if (!string.IsNullOrEmpty(salonZone))
                    {
                        var salonZoneId = Convert.ToInt32(salonZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == salonZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //salon festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity && w.Flag3 == paralel && string.IsNullOrEmpty(w.Custom9)).ToList();

                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();

                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification && w.Date1.Value >= DateTime.Now).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var listItem = new EventDataModel();
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programListItem.day = program.Date1.Value.ToString("dd");
                                programListItem.month = program.Date1.Value.ToString("MM");
                                programListItem.year = program.Date1.Value.Year;
                                programList.Add(programListItem);

                                listItem.date = program.Date1.ToString();
                                listItem.place = program.Custom1.Trim().ToString();
                                listItem.dateFormatted = program.Date1.Value;
                                listItem.time = program.Date1.Value.ToShortTimeString();

                                listItem.date = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                listItem.day = program.Date1.Value.ToString("dd");
                                listItem.dayString = dtfi.GetDayName(program.Date1.Value.DayOfWeek);
                                listItem.month = program.Date1.Value.ToString("MM");
                                listItem.monthString = dtfi.GetMonthName(program.Date1.Value.Month);
                                listItem.year = program.Date1.Value.Year;


                                listItem.articleId = article.ArticleID;
                                listItem.headline = article.Headline;
                                listItem.zone = article.ZoneName;
                                listItem.zoneId = article.ZoneID;

                                listItem.programs = programList;
                                listItem.order = article.AzOrder;
                                listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Salon" : "Salon"));
                                listItem.thumb = article.Custom1;
                                listItem.activity = article.Flag1;
                                listItem.tags = article.TagContents;
                                listItem.tagIds = article.TagIds;
                                listItem.section = article.Custom10;
                                listItem.director = article.Custom2;
                                listItem.flag1 = article.Flag1;
                                listItem.flag2 = article.Flag2;
                                listItem.flag3 = article.Flag3;
                                listItem.flag4 = article.Flag4;
                                listItem.flag5 = article.Flag5;

                                List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                                var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                                foreach (var file in articleFiles)
                                {
                                    var eventFile = new ArticleFileItem();
                                    eventFile.id = file.FileId;
                                    eventFile.articleid = file.ArticleId;
                                    eventFile.type = file.FileTypeId;
                                    eventFile.title = file.Title;
                                    eventFile.commnent = file.Comment;
                                    eventFile.file1 = file.File1;
                                    eventFile.file2 = file.File2;
                                    eventFile.file3 = file.File3;
                                    eventFile.file4 = file.File4;
                                    eventFile.file5 = file.File5;
                                    eventFile.file6 = file.File6;
                                    eventFile.file7 = file.File7;
                                    eventFile.file8 = file.File8;
                                    eventFile.file9 = file.File9;
                                    eventFile.file10 = file.File10;
                                    eventFile.order = file.FileOrder;
                                    fileList.Add(eventFile);
                                }

                                listItem.files = fileList;
                                listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                                list.Add(listItem);
                            }
                        }
                    }
                    #endregion

                    #region tasarım

                    if (!string.IsNullOrEmpty(tasarimZone))
                    {
                        var tasarimZoneId = Convert.ToInt32(tasarimZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == tasarimZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //tasarım data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.flag1 = program.Flag1;
                                programListItem.flag2 = program.Flag2;
                                programListItem.flag3 = program.Flag3;
                                programListItem.flag4 = program.Flag4;
                                programListItem.flag5 = program.Flag5;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                            }

                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Tasarım" : "Tasarım"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;



                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }

                    #endregion

                    #region altkat

                    if (!string.IsNullOrEmpty(altkatZone))
                    {
                        var altkatZoneId = Convert.ToInt32(altkatZone);
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == altkatZoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                        //altkat festival data
                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();
                            var listItem = new EventDataModel();
                            List<vArticlesZonesFull> programs = null;

                            programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                            foreach (var program in programs)
                            {
                                var programListItem = new Program();
                                programListItem.description = program.Article1;
                                programListItem.date = program.Date1.Value;
                                programListItem.dateString = program.Date1.Value.ToString();
                                programListItem.time = program.Date1.Value.ToShortTimeString();
                                programListItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programListItem.place = program.Custom1.ToString();
                                programListItem.ticketUrl = program.Custom2.Trim().ToString();
                                programListItem.longitude = program.Custom3.ToString();
                                programListItem.latitude = program.Custom4.ToString();
                                programListItem.endDate = program.Date2;
                                programListItem.flag1 = program.Flag1;
                                programListItem.flag2 = program.Flag2;
                                programListItem.flag3 = program.Flag3;
                                programListItem.flag4 = program.Flag4;
                                programListItem.flag5 = program.Flag5;
                                programList.Add(programListItem);
                            }

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();

                            if (programNow != null)
                            {
                                listItem.date = programNow.Date1.ToString();
                                listItem.place = programNow.Custom1.Trim().ToString();
                                listItem.dateFormatted = programNow.Date1.Value;
                                listItem.endDateFormatted = programNow.Date2;

                                listItem.date = programNow.Date1.Value.Day + " " + dtfi.GetMonthName(programNow.Date1.Value.Month) + " " + programNow.Date1.Value.Year;
                                if (programNow.Date2.HasValue)
                                {
                                    listItem.endDate = programNow.Date2.Value.Day + " " + dtfi.GetMonthName(programNow.Date2.Value.Month) + " " + programNow.Date2.Value.Year;
                                }
                            }
                            //sonradan eklenen kısım
                            else
                            {
                                programNow = programs.FirstOrDefault();
                                listItem.dateFormatted = programNow.Date1.Value;
                                listItem.endDateFormatted = programNow.Date2;
                            }
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;

                            listItem.programs = programList;
                            listItem.order = article.AzOrder;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Alt Kat" : "Alt Kat"));
                            listItem.thumb = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.flag1 = article.Flag1;
                            listItem.flag2 = article.Flag2;
                            listItem.flag3 = article.Flag3;
                            listItem.flag4 = article.Flag4;
                            listItem.flag5 = article.Flag5;



                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);

                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }

                    #endregion
                }

                //list = list.OrderBy(o => o.dateFormatted).ThenBy(t => t.headline).ToList();
                list = list.OrderBy(o => o.dateFormatted).ToList();

                if (!string.IsNullOrEmpty(session))
                {
                    list = list.Where(w => w.programs.Where(p => p.date.ToShortTimeString() == session).ToList().Count() != 0).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["dayNo"]))
                {
                    dayNo = Convert.ToInt32(context.Request.Form["dayNo"].Trim());
                    list = list.Where(w => w.programs.Where(p => (int)p.date.DayOfWeek == (dayNo - 1)).ToList().Count() != 0).ToList();
                }

                if (day > 0)
                {
                    list = list.Where(w => w.programs.Where(p => p.date.Day == day).ToList().Count() != 0).ToList();
                }

                if (month > 0)
                {
                    list = list.Where(w => w.programs.Where(p => p.date.Month == month).ToList().Count() != 0).ToList();
                }

                if (year > 0)
                {
                    list = list.Where(w => w.programs.Where(p => p.date.Year == year).ToList().Count() != 0).ToList();
                }

                if (!string.IsNullOrEmpty(tag))
                {
                    if (tagData != null)
                    {
                        list = list.Where(w => w.tagIds.Split(',').Contains(tagData.ID.ToString())).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(place))
                {
                    place = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(place));
                    list = list.Where(w => w.programs.Where(p => p.place == place).ToList().Count() != 0).ToList();
                }

                if (!string.IsNullOrEmpty(category))
                {
                    category = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(category));
                    list = list.Where(w => w.category == category).ToList();
                }

                if (!string.IsNullOrEmpty(section))
                {
                    section = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(section));
                    list = list.Where(w => w.section.Replace(" ", "").Split(',').Contains(section)).ToList();
                }

                if (order == "headline")
                {
                    list = list.OrderBy(o => o.headline).ToList();
                }

                if (getAll)
                {
                    list = list.Where(w => !string.IsNullOrEmpty(w.date)).ToList();
                }

                if (highlight)
                {
                    list = list.Where(w => w.flag4 == highlight).Distinct().ToList();
                }

                if (getNew)
                {
                    list = list.Where(w => w.dateFormatted >= DateTime.Now).ToList();
                }

                pageCount = list.Count / itemCount;

                double recordCount = 0;
                recordCount = Convert.ToDouble(list.Count());
                pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));

                if (list.Count > itemCount)
                {
                    list = list.Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                }

                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string lalekartEvents(HttpContext context)
        {
            List<int> zoneGroupIds = new List<int>() { 28, 38, 53, 63, 103, 119, 156, 177, 599 };
            etkinlikClassification = 4;
            int zone_id = 599, pageCount = 0, itemCount = 0, currentPage = 0, month = 0, year = 0;
            string lang = string.Empty, order = string.Empty, session = string.Empty, eventType = string.Empty;
            OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
            {
                Duration = duration, // saniye
                Location = OutputCacheLocation.Server,
                VaryByParam = "plugin;zone_id;lang;itemCount;currentPage;month;year;order;eventType;session"
            });

            page.ProcessRequest(HttpContext.Current);
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "text/json";

            try
            {

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
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

                if (!string.IsNullOrEmpty(context.Request.Form["month"]))
                {
                    month = Convert.ToInt32(context.Request.Form["month"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["year"]))
                {
                    year = Convert.ToInt32(context.Request.Form["year"].Trim());
                }

                if (!string.IsNullOrEmpty(context.Request.Form["session"]))
                {
                    session = context.Request.Form["session"].Trim();

                }

                if (!string.IsNullOrEmpty(context.Request.Form["order"]))
                {
                    order = context.Request.Form["order"].Trim();

                }
                if (!string.IsNullOrEmpty(context.Request.Form["eventType"]))
                {
                    eventType = context.Request.Form["eventType"].Trim();

                }

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<EventDataModel> list = new List<EventDataModel>();
                List<vArticlesZonesFull> articles = null;

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    //&& w.Date2.Value > DateTime.Now 

                    var queryArticles = dbContext.vArticlesZonesFulls.Where(w => zoneGroupIds.Contains(w.ZoneGroupID) && w.Custom6.Contains("1") && w.Status == 1 && w.LanguageAlias == lang && w.ClassificationID == etkinlikClassification);

                    if (eventType == "future")
                    {
                        articles = queryArticles.Where(w => w.Date1.HasValue && w.Date1.Value >= DateTime.Now).OrderByDescending(x => x.Date1).ToList();
                    }
                    else if (eventType == "past")
                    {
                        articles = queryArticles.Where(w => w.Date1.HasValue && w.Date1.Value < DateTime.Now).OrderByDescending(x => x.Date1).ToList();
                    }


                    if (!string.IsNullOrEmpty(session))
                    {
                        articles = articles.Where(w => w.Date1.Value.ToShortTimeString() == session).ToList();
                    }

                    if (month > 0)
                    {
                        articles = articles.Where(w => w.Date1.Value.Month == month).ToList();
                    }

                    if (year > 0)
                    {
                        articles = articles.Where(w => w.Date1.Value.Year == year).ToList();
                    }


                    pageCount = articles.Count / itemCount;

                    double recordCount = 0;
                    recordCount = Convert.ToDouble(articles.Count());
                    pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));

                    if (articles.Count > itemCount)
                    {
                        articles = articles.Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                    }

                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                    foreach (var article in articles)
                    {
                        List<Program> programList = new List<Program>();
                        var listItem = new EventDataModel();
                        List<vArticlesZonesFull> programs = null;

                        programs = articles.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();
                        if (programs.Count > 0)
                        {
                            foreach (var program in programs)
                            {
                                var programItem = new Program();
                                programItem.description = HttpUtility.HtmlDecode(program.Article1);
                                programItem.date = program.Date1.Value;
                                programItem.dateString = program.Date1.Value.ToString();
                                programItem.time = program.Date1.Value.ToShortTimeString();
                                programItem.dateFormettedString = program.Date1.Value.Day + " " + dtfi.GetMonthName(program.Date1.Value.Month) + " " + program.Date1.Value.Year;
                                programItem.place = program.Custom1.ToString();
                                programItem.ticketUrl = program.Custom2.Trim().ToString();
                                programItem.longitude = program.Custom3.ToString();
                                programItem.latitude = program.Custom4.ToString();
                                programItem.flag1 = program.Flag1;
                                programList.Add(programItem);
                            }
                        }
                        else
                        {
                            var programListItem = new Program();
                            programListItem.description = HttpUtility.HtmlDecode(article.Article1);
                            programListItem.date = article.Date1.Value;
                            programListItem.dateString = article.Date1.Value.ToString();
                            programListItem.dateFormettedString = article.Date1.HasValue ? article.Date1.Value.Day + " " + dtfi.GetMonthName(article.Date1.Value.Month) + " " + article.Date1.Value.Year : string.Empty;
                            programListItem.time = article.Date1.HasValue ? article.Date1.Value.ToShortTimeString() : string.Empty;
                            programListItem.place = article.Custom1.Trim().ToString();
                            programListItem.ticketUrl = article.Custom2.ToString();
                            programListItem.longitude = article.Custom3.ToString();
                            programListItem.latitude = article.Custom4.ToString();
                            programListItem.flag1 = article.Flag1;
                            programList.Add(programListItem);
                        }



                        listItem.programs = programList;

                        listItem.dateFormatted = article.Date1.HasValue ? article.Date1.Value : DateTime.MinValue;
                        listItem.articleId = article.ArticleID;
                        listItem.headline = HttpUtility.HtmlDecode(article.Headline);
                        listItem.zone = article.ZoneName;
                        listItem.zoneId = article.ZoneID;
                        listItem.order = article.AzOrder;
                        listItem.alias = article.ArticleZoneAlias;
                        listItem.category = article.ZoneName;
                        listItem.activity = article.Flag1;
                        listItem.tags = article.TagContents;
                        listItem.tagIds = article.TagIds;
                        listItem.section = article.Custom10;
                        listItem.director = article.Custom2;

                        List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                        var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                        foreach (var file in articleFiles)
                        {
                            var eventFile = new ArticleFileItem();
                            eventFile.id = file.FileId;
                            eventFile.type = file.FileTypeId;
                            eventFile.title = file.Title;
                            eventFile.commnent = file.Comment;
                            eventFile.file1 = file.File1;
                            eventFile.file2 = file.File2;
                            eventFile.file3 = file.File3;
                            eventFile.file4 = file.File4;
                            eventFile.file5 = file.File5;
                            eventFile.file6 = file.File6;
                            eventFile.file7 = file.File7;
                            eventFile.file8 = file.File8;
                            eventFile.file9 = file.File9;
                            eventFile.file10 = file.File10;
                            eventFile.articleid = file.ArticleId;
                            eventFile.order = file.FileOrder;
                            fileList.Add(eventFile);
                        }

                        listItem.files = fileList;
                        listItem.alias = !article.DomainName.Contains("lalekart") ? "https://" + article.DomainName.Trim() + "/" + article.ArticleZoneAlias : "/" + article.ArticleZoneAlias;

                        list.Add(listItem);
                    }


                }

                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount, order = order, eventType = eventType });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası :Lalekart Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.ToString(), data = "" });
            }
        }

        public string lalekartNews(HttpContext context)
        {
            List<int> zoneIds = new List<int>() { 598, 433, 355, 315, 303, 189, 95, 59 };

            //int classificationId = 3;  //3 is news Classification
            int pageCount = 0, itemCount = 0, currentPage = 0, month = 0, year = 0;
            string lang = string.Empty, session = string.Empty;
            OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
            {
                Duration = duration, // saniye
                Location = OutputCacheLocation.Server,
                VaryByParam = "plugin;lang;itemCount;currentPage;year;month;session"
            });

            page.ProcessRequest(HttpContext.Current);
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "text/json";

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
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

                if (!string.IsNullOrEmpty(context.Request.Form["month"]))
                {
                    month = Convert.ToInt32(context.Request.Form["month"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["year"]))
                {
                    year = Convert.ToInt32(context.Request.Form["year"].Trim());
                }

                if (!string.IsNullOrEmpty(context.Request.Form["session"]))
                {
                    session = context.Request.Form["session"].Trim();

                }

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<NewsDetail> list = new List<NewsDetail>();
                List<vArticlesZonesFull> articles = null;

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    //&& w.Date2.Value > DateTime.Now 
                    //todo: flag1 is show on lalekart news page
                    //classificationId == w.ClassificationID &&
                    var queryArticles = dbContext.vArticlesZonesFulls.Where(w => zoneIds.Count(x => x == w.ZoneID) > 0 && w.Flag1 && w.Status == 1 && w.LanguageAlias == lang);

                    double recordCount = queryArticles.Count();
                    pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));


                    if (recordCount > itemCount)
                    {
                        articles = queryArticles.OrderBy(x => x.Headline).Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                    }
                    else
                    {
                        articles = queryArticles.OrderBy(x => x.Headline).Take(itemCount).ToList();
                    }

                    if (month > 0)
                    {
                        articles = articles.Where(w => w.Date1.Value.Month == month).ToList();
                    }

                    if (year > 0)
                    {
                        articles = articles.Where(w => w.Date1.Value.Year == year).ToList();
                    }

                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                    foreach (var article in articles)
                    {

                        var listItem = new NewsDetail();
                        listItem.articleId = article.ArticleID;
                        listItem.headline = HttpUtility.HtmlDecode(article.Headline);
                        listItem.zone = article.ZoneName;
                        listItem.zoneId = article.ZoneID;
                        listItem.zoneId = article.ZoneID;
                        listItem.order = article.AzOrder;
                        listItem.alias = article.ArticleZoneAlias;
                        listItem.date = article.Date1.Value;
                        listItem.dateString = article.Date1.Value.ToShortDateString();


                        List<ArticleFileItem> fileList = new List<ArticleFileItem>();
                        var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                        foreach (var file in articleFiles)
                        {
                            var venueFile = new ArticleFileItem();
                            venueFile.id = file.FileId;
                            venueFile.type = file.FileTypeId;
                            venueFile.title = file.Title;
                            venueFile.commnent = file.Comment;
                            venueFile.file1 = file.File1;
                            venueFile.file2 = file.File2;
                            venueFile.file3 = file.File3;
                            venueFile.file4 = file.File4;
                            venueFile.file5 = file.File5;
                            venueFile.file6 = file.File6;
                            venueFile.file7 = file.File7;
                            venueFile.file8 = file.File8;
                            venueFile.file9 = file.File9;
                            venueFile.file10 = file.File10;
                            venueFile.articleid = file.ArticleId;
                            venueFile.order = file.FileOrder;
                            fileList.Add(venueFile);
                        }

                        listItem.files = fileList;
                        listItem.alias = !article.DomainName.Contains("lalekart") ? "https://" + article.DomainName.Trim() + "/" + article.ArticleZoneAlias : "/" + article.ArticleZoneAlias;

                        list.Add(listItem);
                    }

                }
                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount, month = month, year = year });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası :Lalekart Haberler", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.ToString(), data = "" });
            }

        }

        public string lalekartIndirimliMekanlar(HttpContext context)
        {
            int zone_id = 597, pageCount = 0, itemCount = 0, currentPage = 0;
            string lang = string.Empty, session = string.Empty, name = string.Empty, category = string.Empty,
                district = string.Empty, latitude = string.Empty, longitude = string.Empty, paging = string.Empty; ;

            OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
            {
                Duration = duration, // saniye
                Location = OutputCacheLocation.Server,
                VaryByParam = "plugin;lang;itemCount;currentPage;name;category;district;latitude;longitude;paging;session"
            });

            page.ProcessRequest(HttpContext.Current);
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "text/json";

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
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

                if (!string.IsNullOrEmpty(context.Request.Form["category"]))
                {
                    category = context.Request.Form["category"].Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["district"]))
                {
                    district = HttpUtility.HtmlDecode(context.Request.Form["district"].Trim());
                }

                if (!string.IsNullOrEmpty(context.Request.Form["name"]))
                {
                    name = HttpUtility.HtmlDecode(context.Request.Form["name"].Trim());
                }

                if (!string.IsNullOrEmpty(context.Request.Form["paging"]))
                {
                    paging = context.Request.Form["paging"].Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["latitude"]) &&
                    !string.IsNullOrEmpty(context.Request.Form["longitude"]))
                {
                    latitude = context.Request.Form["latitude"].Trim();
                    longitude = context.Request.Form["longitude"].Trim();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["session"]))
                {
                    session = context.Request.Form["session"].Trim();

                }

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<VenueDetail> list = new List<VenueDetail>();
                List<vArticlesZonesFull> articles = null;


                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    //&& w.Date2.Value > DateTime.Now 
                    var queryArticles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zone_id && w.Status == 1 && w.LanguageAlias == lang);



                    if (!string.IsNullOrEmpty(category))
                    {
                        queryArticles = queryArticles.Where(x => x.Custom5.Contains(category));
                    }

                    if (!string.IsNullOrEmpty(name))
                    {
                        queryArticles = queryArticles.Where(x => x.Headline.Contains(name));
                    }

                    if (!string.IsNullOrEmpty(district))
                    {
                        queryArticles = queryArticles.Where(x => x.Custom1.Contains(district));
                    }



                    if (string.IsNullOrEmpty(paging) || paging == "1")
                    {
                        double recordCount = queryArticles.Count();
                        pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));

                        if (recordCount > itemCount)
                        {
                            articles = queryArticles.OrderBy(x => x.Headline).Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                        }
                        else
                        {
                            articles = queryArticles.OrderBy(x => x.Headline).Take(itemCount).ToList();
                        }
                    }
                    else
                    {
                        articles = queryArticles.OrderBy(x => x.Headline).ToList();
                    }


                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                    foreach (var article in articles)
                    {
                        bool isValidResult = true;
                        var listItem = new VenueDetail();


                        if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                        {
                            decimal longi = Convert.ToDecimal(longitude);
                            decimal lati = Convert.ToDecimal(latitude);

                            decimal venueLongitude = !string.IsNullOrEmpty(article.Custom7) ? Convert.ToDecimal(article.Custom7) : 0;
                            decimal venueLatitude = !string.IsNullOrEmpty(article.Custom6) ? Convert.ToDecimal(article.Custom6) : 0;

                            if (venueLatitude >= lati && venueLongitude <= longi)
                            {
                                isValidResult = true;
                            }
                            else isValidResult = false;

                        }


                        if (isValidResult)
                        {
                            listItem.articleId = article.ArticleID;
                            listItem.headline = HttpUtility.HtmlDecode(article.Headline);
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;
                            listItem.zoneId = article.ZoneID;
                            listItem.order = article.AzOrder;
                            listItem.alias = article.ArticleZoneAlias;
                            listItem.category = article.ZoneName;
                            listItem.date = article.StartDate.Value;
                            listItem.dateString = article.StartDate.Value.ToShortDateString();
                            listItem.district = article.Custom1.Trim().ToString();
                            listItem.address = article.Custom2.ToString();
                            listItem.phone = article.Custom3.ToString();
                            listItem.discount = article.Custom4.ToString();
                            listItem.longitude = article.Custom7;
                            listItem.latitude = article.Custom6;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();
                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var venueFile = new ArticleFileItem();
                                venueFile.id = file.FileId;
                                venueFile.type = file.FileTypeId;
                                venueFile.title = file.Title;
                                venueFile.commnent = file.Comment;
                                venueFile.file1 = file.File1;
                                venueFile.file2 = file.File2;
                                venueFile.file3 = file.File3;
                                venueFile.file4 = file.File4;
                                venueFile.file5 = file.File5;
                                venueFile.file6 = file.File6;
                                venueFile.file7 = file.File7;
                                venueFile.file8 = file.File8;
                                venueFile.file9 = file.File9;
                                venueFile.file10 = file.File10;
                                venueFile.articleid = file.ArticleId;
                                venueFile.order = file.FileOrder;
                                fileList.Add(venueFile);
                            }

                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                            list.Add(listItem);
                        }
                    }

                }
                if (string.IsNullOrEmpty(paging) || paging == "1")
                    return jss.Serialize(new { status = true, message = "İşlem başarılı.. ", data = list, currentPage = currentPage, pageCount = pageCount, category = category, name = name, district = district, longitude = longitude, latitude = latitude, paging = paging });
                else
                    return jss.Serialize(new { status = true, message = "İşlem başarılı.. ", data = list, category = category, name = name, district = district, longitude = longitude, latitude = latitude, paging = paging });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası :Lalekart İndirimli Mekanlar", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.ToString(), data = "" });
            }
        }

        public string lalekartindirimlimekandetay(HttpContext context)
        {
            int articleId = 0;
            string lang = string.Empty, session = string.Empty;

            OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
            {
                Duration = duration, // saniye
                Location = OutputCacheLocation.Server,
                VaryByParam = "plugin;lang;articleid;session"
            });

            page.ProcessRequest(HttpContext.Current);
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "text/json";

            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["articleid"]))
                {
                    articleId = Convert.ToInt32(context.Request.Form["articleid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("articleid boş gönderilemez"));
                    context.Response.End();
                }
                if (!string.IsNullOrEmpty(context.Request.Form["session"]))
                {
                    session = context.Request.Form["session"].Trim();
                }

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    //&& w.Date2.Value > DateTime.Now 
                    var article = dbContext.vArticlesZonesFulls.Where(w => w.ArticleID == articleId && w.Status == 1 && w.LanguageAlias == lang).FirstOrDefault();

                    if (article != null)
                    {
                        var files = dbContext.Files.Where(f => f.ArticleId == article.ArticleID).ToList();

                        VenueDetail venueDetail = new VenueDetail();

                        venueDetail.articleId = article.ArticleID;
                        venueDetail.headline = HttpUtility.HtmlDecode(article.Headline);
                        venueDetail.zone = article.ZoneName;
                        venueDetail.zoneId = article.ZoneID;
                        venueDetail.zoneId = article.ZoneID;
                        venueDetail.order = article.AzOrder;
                        venueDetail.alias = article.ArticleZoneAlias;
                        venueDetail.category = article.ZoneName;
                        venueDetail.date = article.StartDate.Value;
                        venueDetail.dateString = article.StartDate.Value.ToShortDateString();
                        venueDetail.district = article.Custom1.Trim().ToString();
                        venueDetail.address = article.Custom2.ToString();
                        venueDetail.phone = article.Custom3.ToString();
                        venueDetail.discount = article.Custom4.ToString();
                        venueDetail.longitude = article.Custom7;
                        venueDetail.latitude = article.Custom6;

                        List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                        foreach (var file in files)
                        {
                            var venueFile = new ArticleFileItem();
                            venueFile.id = file.FileId;
                            venueFile.type = file.FileTypeId;
                            venueFile.title = file.Title;
                            venueFile.commnent = file.Comment;
                            venueFile.file1 = file.File1;
                            venueFile.file2 = file.File2;
                            venueFile.file3 = file.File3;
                            venueFile.file4 = file.File4;
                            venueFile.file5 = file.File5;
                            venueFile.file6 = file.File6;
                            venueFile.file7 = file.File7;
                            venueFile.file8 = file.File8;
                            venueFile.file9 = file.File9;
                            venueFile.file10 = file.File10;
                            venueFile.articleid = file.ArticleId;
                            venueFile.order = file.FileOrder;
                            fileList.Add(venueFile);
                        }

                        venueDetail.files = fileList;
                        venueDetail.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                        return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = venueDetail });
                    }
                }

                return jss.Serialize(new { status = true, message = "Başarısız işlem. ", data = string.Empty });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası :Lalekart İndirimli Mekan Detayı", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.ToString(), data = "" });
            }
        }

        public string eventProgram(HttpContext context)
        {
            IksvDbContext dbContext = new IksvDbContext();
            string lang = "tr";
            int articleid = 0;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["articleid"]))
                {
                    articleid = Convert.ToInt32(context.Request.Form["articleid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("articleid boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }

                OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
                {
                    Duration = duration, // saniye
                    Location = OutputCacheLocation.Server,
                    VaryByParam = "plugin;articleid;lang"
                });


                page.ProcessRequest(HttpContext.Current);
                context.Response.Charset = "utf-8";
                context.Response.ContentType = "text/json";

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                DateTimeFormatInfo dtfien = CultureInfo.CreateSpecificCulture("en").DateTimeFormat;

                var article = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == articleid && f.Status == 1);

                if (article != null)
                {
                    var programs = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == 5 && w.StartDate < DateTime.Now && (w.EndDate.Value > DateTime.Now || !w.EndDate.HasValue)).OrderBy(o => o.Date1).ToList();
                    programs = programs.Where(w => w.Custom10 == article.ArticleID.ToString()).ToList();

                    if (programs != null)
                    {
                        return jss.Serialize(
                            new
                            {
                                status = true,
                                message = "İşlem başarılı!",
                                data = programs.Select(s =>
                                            new
                                            {
                                                programid = s.ArticleID,
                                                mekan = s.Custom1,
                                                bilet = s.Custom2,
                                                latitude = s.Custom3,
                                                longitude = s.Custom4,
                                                tarihFormatted = (s.Date1.HasValue ? s.Date1.Value.ToString() : ""),
                                                tarihCalendar = (s.Date1.HasValue ? (dtfien.GetMonthName(s.Date1.Value.Month) + " " + s.Date1.Value.Day + ", " + s.Date1.Value.Year + " " + s.Date1.Value.ToShortTimeString()) : ""),
                                                tarih = (s.Date1.HasValue ? (s.Date1.Value.Day + " " + dtfi.GetMonthName(s.Date1.Value.Month) + " " + dtfi.GetDayName(s.Date1.Value.DayOfWeek)) + " " + s.Date1.Value.ToShortTimeString() : ""),
                                                flag1 = s.Flag1,
                                                flag2 = s.Flag2,
                                                flag3 = s.Flag3,
                                                flag4 = s.Flag4,
                                                flag5 = s.Flag5,
                                                ucret = s.Custom5,
                                                day = s.Date1.Value.ToString("dd"),
                                                dayString = dtfi.GetDayName(s.Date1.Value.DayOfWeek),
                                                month = s.Date1.Value.ToString("MM"),
                                                monthString = dtfi.GetMonthName(s.Date1.Value.Month),
                                                year = s.Date1.Value.Year,
                                                time = s.Date1.Value.ToShortTimeString(),
                                                usedQuota = dbContext.Reservations.Where(w => w.EventId == s.ArticleID).ToList().Count(),
                                                quota = (string.IsNullOrEmpty(s.Custom9) ? 0 : Convert.ToInt32(s.Custom9))
                                            }).ToList()
                            });
                    }
                }

                return jss.Serialize(new { status = false, message = "article bulunamadı.", data = "" });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Event Program", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string tags(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            List<string> zone_ids = new List<string>();
            List<string> tagString = new List<string>();
            Dictionary<string, string> tags = new Dictionary<string, string>();
            string musicZone = "", filmZone = "", lang = "";
            int pageCount = 0;
            int currentPage = 0, programMusic = 0, programFilm = 0;
            bool activity = false;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("lang boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["activity"]))
                {
                    activity = Convert.ToBoolean(context.Request.Form["activity"].Trim());

                }

                //festivaller datası
                if (!string.IsNullOrEmpty(context.Request.Form["programMusic"]))
                {
                    programMusic = Convert.ToInt32(context.Request.Form["programMusic"].Trim());
                    var musicData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programMusic);
                    if (musicData != null)
                    {
                        musicZone = musicData.Article2.Trim();
                    }
                }

                if (!string.IsNullOrEmpty(context.Request.Form["programFilm"]))
                {
                    programFilm = Convert.ToInt32(context.Request.Form["programFilm"].Trim());
                    var filmData = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == programFilm);
                    if (filmData != null)
                    {
                        filmZone = filmData.Article2.Trim();
                    }
                }

                List<EventDataModel> list = new List<EventDataModel>();
                var articles = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID.ToString()) && w.Status == 1 && w.ClassificationID == 4 && (w.Date1.Value < DateTime.Now && w.Date2.Value > DateTime.Now) && w.Flag1 == activity).ToList();

                if (articles != null)
                {
                    var articleTagList = articles.Where(w => !string.IsNullOrEmpty(w.TagContents)).Select(s => s.TagContents).Distinct().ToList();
                    var tagStringList = string.Join(",", articleTagList);
                    tagString.AddRange(tagStringList.Split(',').ToList().Distinct().ToList());
                }

                //muzik festival data
                articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == musicZone && w.Status == 1 && w.ClassificationID == 4 && w.Flag1 == activity).ToList();

                if (articles != null)
                {
                    var articleTagList = articles.Where(w => !string.IsNullOrEmpty(w.TagContents)).Select(s => s.TagContents).Distinct().ToList();
                    var tagStringList = string.Join(",", articleTagList);
                    tagString.AddRange(tagStringList.Split(',').ToList().Distinct().ToList());
                }

                //film festival data
                articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID.ToString() == filmZone && w.Status == 1 && w.ClassificationID == 4 && w.Flag1 == activity).ToList();

                if (articles != null)
                {
                    var articleTagList = articles.Where(w => !string.IsNullOrEmpty(w.TagContents)).Select(s => s.TagContents).Distinct().ToList();
                    var tagStringList = string.Join(",", articleTagList);
                    tagString.AddRange(tagStringList.Split(',').ToList().Distinct().ToList());
                }

                var tagDatas = dbContext.Tags.Where(w => tagString.Contains(w.Text) && w.IsActive).ToList();
                foreach (var tag in tagDatas)
                {
                    tags.Add(tag.Alias, tag.Text);
                }


                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = tags.ToList() });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Tags", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string tagRelatedEvents(HttpContext context)
        {
            //CmsDbContext dbContext = new CmsDbContext();
            List<EventDataModel> list = new List<EventDataModel>();
            int articleid = 0;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["articleid"]))
                {
                    articleid = Convert.ToInt32(context.Request.Form["articleid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("articleid boş gönderilemez"));
                    context.Response.End();
                }

                OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
                {
                    Duration = duration, // saniye
                    Location = OutputCacheLocation.Server,
                    VaryByParam = "plugin;articleid"
                });


                page.ProcessRequest(HttpContext.Current);
                context.Response.Charset = "utf-8";
                context.Response.ContentType = "text/json";

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    var article = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == articleid && f.Status == 1);
                    if (article != null)
                    {
                        var articlesWithCat = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.LanguageID == article.LanguageID && w.ClassificationID == etkinlikClassification && !string.IsNullOrEmpty(w.TagIds)).ToList();
                        List<int> articleTags = new List<int>();
                        if (!string.IsNullOrEmpty(article.TagIds))
                        {
                            articleTags = article.TagIds.Split(',').Select(Int32.Parse).ToList();
                        }

                        List<vArticlesZonesFull> articles = new List<vArticlesZonesFull>();

                        articles = articles.Distinct().ToList();

                        foreach (var relatedarticle in articles)
                        {
                            var listItem = new EventDataModel();
                            var programs = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == relatedarticle.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
                            programs = programs.Where(w => w.Custom10 == relatedarticle.ArticleID.ToString()).ToList();

                            var programNow = programs.Where(w => w.Date1 > DateTime.Now).ToList().FirstOrDefault();
                            if (programNow != null)
                            {
                                listItem.day = programNow.Date1.Value.ToString("dd");
                                listItem.month = programNow.Date1.Value.ToString("MM");
                                listItem.year = programNow.Date1.Value.Year;
                                listItem.time = programNow.Date1.Value.ToShortTimeString();
                                listItem.dateFormatted = programNow.Date1.Value;
                            }

                            listItem.articleId = relatedarticle.ArticleID;
                            listItem.headline = relatedarticle.Headline;
                            listItem.zone = relatedarticle.ZoneName;
                            listItem.zoneId = relatedarticle.ZoneID;
                            listItem.order = relatedarticle.AzOrder;
                            listItem.alias = relatedarticle.ArticleZoneAlias;
                            listItem.category = relatedarticle.ZoneName;
                            listItem.thumb = relatedarticle.Custom1;
                            listItem.activity = relatedarticle.Flag1;
                            listItem.tags = relatedarticle.TagContents;
                            listItem.section = relatedarticle.Custom10;
                            listItem.director = relatedarticle.Custom2;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = dbContext.Files.Where(w => w.ArticleId == relatedarticle.ArticleID && w.FileTypeId == 15).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.articleid = file.ArticleId;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }

                            listItem.files = fileList;
                            listItem.alias = relatedarticle.DomainName.Trim() + "/" + relatedarticle.ArticleZoneAlias;

                            list.Add(listItem);
                        }

                        list = list.Where(w => w.articleId != article.ArticleID).Distinct().ToList();

                        list = list.Where(w => w.dateFormatted >= DateTime.Now).ToList();

                        if (list.Count > 3)
                        {
                            List<int> articleIdList = new List<int>();
                            Random rand = new Random();
                            while (articleIdList.Count < 3)
                            {
                                int toSkip = rand.Next(0, list.Count);
                                var articleId = list.Skip(toSkip).Take(1).FirstOrDefault().articleId;
                                if (!articleIdList.Contains(articleId))
                                {
                                    articleIdList.Add(articleId);
                                }
                            }

                            list = list.Where(w => articleIdList.Contains(w.articleId)).Distinct().ToList();
                        }

                        return jss.Serialize(new { status = true, message = "İşlem başarılı.", data = list });
                    }
                }
                return jss.Serialize(new { status = false, message = "article bulunamadı.", data = "" });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Tag Related Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string subArticles(HttpContext context)
        {
            //CmsDbContext dbContext = new CmsDbContext();
            List<EventDataModel> list = new List<EventDataModel>();
            int articleid = 0;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["articleid"]))
                {
                    articleid = Convert.ToInt32(context.Request.Form["articleid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("articleid boş gönderilemez"));
                    context.Response.End();
                }

                //OutputCachedPage page = new OutputCachedPage(new OutputCacheParameters
                //{
                //    Duration = duration, // saniye
                //    Location = OutputCacheLocation.Server,
                //    VaryByParam = "plugin;articleid"
                //});


                //page.ProcessRequest(HttpContext.Current);
                context.Response.Charset = "utf-8";
                context.Response.ContentType = "text/json";

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    var article = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ArticleID == articleid && f.Status == 1);
                    if (article != null)
                    {
                        var articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.LanguageID == article.LanguageID && w.ClassificationID == etkinlikClassification).ToList();

                        articles = articles.Where(w => w.Custom9 == articleid.ToString()).ToList();
                        //List<vArticlesZonesFull> articles = new List<vArticlesZonesFull>();
                        //articles = articles.Distinct().ToList();

                        foreach (var relatedarticle in articles)
                        {
                            var listItem = new EventDataModel();

                            listItem.articleId = relatedarticle.ArticleID;
                            listItem.headline = relatedarticle.Headline;
                            listItem.zone = relatedarticle.ZoneName;
                            listItem.zoneId = relatedarticle.ZoneID;
                            listItem.order = relatedarticle.AzOrder;
                            listItem.alias = relatedarticle.ArticleZoneAlias;
                            listItem.category = relatedarticle.ZoneName;
                            listItem.thumb = relatedarticle.Custom1;
                            listItem.activity = relatedarticle.Flag1;
                            listItem.tags = relatedarticle.TagContents;
                            listItem.section = relatedarticle.Custom10;
                            listItem.director = relatedarticle.Custom2;

                            List<ArticleFileItem> fileList = new List<ArticleFileItem>();

                            var articleFiles = dbContext.Files.Where(w => w.ArticleId == relatedarticle.ArticleID && w.FileTypeId == 15).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new ArticleFileItem();
                                eventFile.id = file.FileId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = file.File1;
                                eventFile.file2 = file.File2;
                                eventFile.file3 = file.File3;
                                eventFile.file4 = file.File4;
                                eventFile.file5 = file.File5;
                                eventFile.file6 = file.File6;
                                eventFile.file7 = file.File7;
                                eventFile.file8 = file.File8;
                                eventFile.file9 = file.File9;
                                eventFile.file10 = file.File10;
                                eventFile.articleid = file.ArticleId;
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }

                            listItem.files = fileList;
                            listItem.alias = relatedarticle.DomainName.Trim() + "/" + relatedarticle.ArticleZoneAlias;

                            list.Add(listItem);
                        }

                        return jss.Serialize(new { status = true, message = "İşlem başarılı.", data = list });
                    }
                }
                return jss.Serialize(new { status = false, message = "article bulunamadı.", data = "" });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Sub Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }
        public string getarticle(HttpContext context)
        {
            CmsDbContext dbContext = new CmsDbContext();
            List<string> zone_ids = new List<string>();
            int pageCount = 0, itemCount = 0, currentPage = 0;
            string orderType = "";
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').ToList();
                }
                else
                {
                    return jss.Serialize("zone_id boş gönderilemez");
                }

                if (!string.IsNullOrEmpty(context.Request.Form["itemCount"]))
                {
                    itemCount = Convert.ToInt32(context.Request.Form["itemCount"].Trim());
                }
                else
                {
                    return jss.Serialize("itemCount boş gönderilemez");
                }

                if (!string.IsNullOrEmpty(context.Request.Form["currentPage"]))
                {
                    currentPage = Convert.ToInt32(context.Request.Form["currentPage"].Trim());
                }



                if (!string.IsNullOrEmpty(context.Request.Form["orderType"]))
                {
                    orderType = context.Request.Form["orderType"].Trim();
                }
                else
                {
                    orderType = "desc";
                }

                List<GetArticleModel> list = new List<GetArticleModel>();
                var articles = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID.ToString()) && w.StartDate < DateTime.Now && (w.EndDate.Value > DateTime.Now || !w.EndDate.HasValue) && w.Status == 1).ToList();

                if (orderType == "asc")
                {
                    articles = articles.OrderBy(o => o.Date1.Value).ToList();
                }
                else if (orderType == "desc")
                {
                    articles = articles.OrderByDescending(o => o.Date1.Value).ToList();
                }


                foreach (var article in articles)
                {
                    var files = dbContext.Files.Where(f => f.ArticleId == article.ArticleID).ToList();

                    var listItem = new GetArticleModel();
                    listItem.articleid = article.ArticleID;
                    listItem.headline = article.Headline;
                    listItem.summary = article.Summary;
                    listItem.zone = article.ZoneName;
                    listItem.zoneid = article.ZoneID;
                    listItem.custom1 = article.Custom1;
                    listItem.custom2 = article.Custom2;
                    listItem.custom3 = article.Custom3;
                    listItem.custom4 = article.Custom4;
                    listItem.custom5 = article.Custom5;

                    listItem.files = files.Select(s => new fileReturn { filetypeid = s.FileTypeId, title = s.Title, comment = s.Comment, order = s.FileOrder, file1 = article.ArticleID + "_" + s.File1, file2 = article.ArticleID + "_" + s.File2, file3 = article.ArticleID + "_" + s.File3, file4 = article.ArticleID + "_" + s.File4 }).ToList();
                    listItem.order = article.AzOrder;
                    listItem.alias = article.ArticleZoneAlias;
                    listItem.activity = article.Flag1;

                    //var zone = dbContext.Zones.FirstOrDefault(f => f.Id == article.ZoneID);
                    //if (zone != null)
                    //{
                    //    var zoneGroup = dbContext.ZoneGroups.FirstOrDefault(f => f.Id == zone.ZoneGroupId);
                    //    if (zoneGroup != null)
                    //    {
                    //        var sites = dbContext.Sites.FirstOrDefault(f => f.Id == zoneGroup.SiteId);
                    //        if (sites != null)
                    //        {
                    //            var domain = dbContext.Domains.FirstOrDefault(f => f.Id == sites.DomainId);
                    //            if (domain != null)
                    //            {
                    //                var domainNamesUrls = domain.Names.Split('\r').FirstOrDefault();
                    //                if (!string.IsNullOrEmpty(article.ArticleZoneAlias.Trim()))
                    //                {
                    //                    listItem.alias = domainNamesUrls.Trim() + "/" + article.ArticleZoneAlias;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    if (article.Date1.HasValue)
                    {
                        listItem.date1 = article.Date1.Value.ToString();
                        listItem.year = article.Date1.Value.Year.ToString();
                    }

                    list.Add(listItem);
                }




                double recordCount = 0;
                recordCount = Convert.ToDouble(list.Count());
                pageCount = Convert.ToInt32(Math.Ceiling(recordCount / Convert.ToDouble(itemCount)));
                if (list.Count > itemCount)
                {
                    list = list.Skip((currentPage - 1) * itemCount).Take(itemCount).ToList();
                }


                return jss.Serialize(new { status = true, message = "İşlem başarılı. ", data = list, currentPage = currentPage, pageCount = pageCount });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Getarticle", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }
        }

        public string reservationcheck(HttpContext context)
        {
            string programid = "";
            int _programid = 0;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Form["programid"]))
                {
                    programid = context.Request.Form["programid"].Trim();
                    _programid = Convert.ToInt32(programid);
                }
                else
                {
                    context.Response.Write(jss.Serialize("programid boş gönderilemez"));
                    context.Response.End();
                }

                using (IksvDbContext dbContext = new IksvDbContext())
                {
                    var eventCheck = dbContext.Articles.FirstOrDefault(f => f.ClassificationId == etkinlikProgramClassification && f.Status == 1 && f.Id == _programid);
                    //var eventCheck = eventCheckList.FirstOrDefault(f => f.Custom10 == programid);
                    if (eventCheck != null)
                    {

                        if (eventCheck.LangId == "EN")
                        {
                            _programid = Convert.ToInt32(eventCheck.Custom9);

                        }

                        var program = dbContext.Articles.FirstOrDefault(f => f.ClassificationId == etkinlikProgramClassification && f.Status == 1 && f.Id == _programid);
                        if (program == null && eventCheck.LangId == "EN")
                        {
                            return jss.Serialize(new { status = false, message = "TR program id'si girilmemiş veya yanlış girilmiş", data = "" });
                        }

                        var reservationQuotaCheck = dbContext.Reservations.Where(w => w.EventId == program.Id).ToList();

                        int count = 0;
                        if (reservationQuotaCheck != null)
                        {
                            count = reservationQuotaCheck.Count();
                        }

                        var quota = Convert.ToInt32(program.Custom9);

                        if (count >= quota)
                        {
                            return jss.Serialize(new { status = false, message = "Kota dolu.", data = count });
                        }

                        return jss.Serialize(new { status = true, message = "İşlem başarılı.", data = count });
                    }
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Sub Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }

            return "";
        }

        public string reservation(HttpContext context)
        {
            string name = "", surname = "", email = "", phone = "", eventname = "";
            int eventid = 0, programid = 0;

            bool kvkk = false, isAccept = false;
            try
            {
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

                if (!string.IsNullOrEmpty(context.Request.Form["eventname"]))
                {
                    eventname = context.Request.Form["eventname"].Trim();
                }
                else
                {
                    context.Response.Write(jss.Serialize("eventname boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["eventid"]))
                {
                    eventid = Convert.ToInt32(context.Request.Form["eventid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("eventid boş gönderilemez"));
                    context.Response.End();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["programid"]))
                {
                    programid = Convert.ToInt32(context.Request.Form["programid"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("programid boş gönderilemez"));
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

                if (!string.IsNullOrEmpty(context.Request.Form["isAccept"]))
                {
                    isAccept = Convert.ToBoolean(context.Request.Form["isAccept"].Trim());
                }
                else
                {
                    context.Response.Write(jss.Serialize("isAccept boş gönderilemez"));
                    context.Response.End();
                }

                using (IksvDbContext dbContext = new IksvDbContext())
                {
                    /*var emailCheck = dbContext.Reservations.FirstOrDefault(i => i.Email == email && i.EventId == programid);

                    if (emailCheck != null)
                    {
                        return jss.Serialize(new { status = false, message = "Bu kullanıcı ile daha önce başvuru yapılmıştı!", data = "" });
                    }
                    else
                    {
                        // to do
                    }*/
                    //var eventCheckList = dbContext.Articles.Where(f => f.ClassificationId == etkinlikProgramClassification && f.Status == 1).ToList();
                    var eventCheck = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ClassificationID == etkinlikClassification && f.ArticleID == eventid && f.Status == 1);
                    if (eventCheck != null)
                    {
                        var eventProgramCheck = dbContext.vArticlesZonesFulls.FirstOrDefault(f => f.ClassificationID == etkinlikProgramClassification && f.ArticleID == programid && f.Status == 1);

                        if (eventProgramCheck != null)
                        {

                            var reservationQuotaCheck = dbContext.Reservations.Where(w => w.EventId == eventProgramCheck.ArticleID).ToList();

                            int eventProgramQuota = string.IsNullOrEmpty(eventProgramCheck.Custom9) ? 0 : Convert.ToInt32(eventProgramCheck.Custom9);
                            if (eventProgramQuota >= reservationQuotaCheck.Count)
                            {
                                var reservation = new Reservation();

                                reservation.Name = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(name));
                                reservation.Surname = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(surname));
                                reservation.Email = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(email));
                                reservation.Phone = phone;
                                reservation.Event = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(eventname)) + " " + eventProgramCheck.Date1.ToString();
                                reservation.EventId = eventProgramCheck.ArticleID;
                                reservation.Kvkk = kvkk;
                                reservation.IsAccept = isAccept;
                                reservation.CreateDt = DateTime.Now;
                                reservation.Status = 1;

                                dbContext.Reservations.Add(reservation);
                                if (dbContext.SaveChanges() > 0)
                                {
                                    return jss.Serialize(new { status = true, message = "İşlem başarılı.", data = "" });
                                }
                            }
                            else
                            {
                                return jss.Serialize(new { status = false, message = (eventProgramCheck.LanguageID == "EN" ? "Etkinlik kota dolu." : "Etkinlik kota dolu."), data = "" });
                            }
                        }
                        else
                        {
                            return jss.Serialize(new { status = false, message = "Program bulunamadı veya pasif.", data = "" });
                        }
                    }
                    else
                    {
                        return jss.Serialize(new { status = false, message = "Ekinlik bulunamadı veya pasif.", data = "" });
                    }

                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Plugin hatası : Sub Events", false);
                return jss.Serialize(new { status = false, message = "İşlem sırasında hata oluştu. Hata : " + ex.Message, data = "" });
            }

            return "";
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
    }
}