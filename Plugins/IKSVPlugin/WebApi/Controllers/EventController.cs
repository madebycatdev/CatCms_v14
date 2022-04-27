using EuroCMS.Model;
using EuroCMS.Plugin.IKSV.Providers;
using EuroCMS.Plugin.IKSV.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace EuroCMS.Plugin.IKSV.Controllers
{
    public class EventController : ApiController
    {
        int etkinlikClassification = 4;
        int etkinlikProgramClassification = 5;
        int etkinlikKategoriClassification = 6;
        int MenuClassification = 16;


        [HttpPost]
        [CustomAuthorize]
        public IHttpActionResult GetEvents()
        {
            List<int> zone_ids = new List<int>();
            List<int> altkatZone_ids = new List<int>();
            string musicZone = "", filmZone = "", cazzZone = "", tiyatroZone = "", salonZone = "", bienalZone = "", lang = "";
            int programMusic = 0, programFilm = 0, programCazz = 0, programTiyatro = 0, programSalon = 0, programBienal = 0;
            bool activity = false, paralel = false;
            try
            {
                System.Web.HttpContextWrapper context = this.Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                context.Response.ContentType = "text/json";

                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    var zoneid = context.Request.Form["zone_id"].Trim();
                    zone_ids = zoneid.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["altkatZone_id"]))
                {
                    var zoneid = context.Request.Form["altkatZone_id"].Trim();
                    altkatZone_ids = zoneid.Split(',').Select(m => Convert.ToInt32(m)).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.Form["lang"]))
                {
                    lang = context.Request.Form["lang"].Trim();
                }
                else
                {
                    return Ok(new { results = "lang boş gönderilemez" });
                }

                if (!string.IsNullOrEmpty(context.Request.Form["activity"]))
                {
                    activity = Convert.ToBoolean(context.Request.Form["activity"].Trim());

                }

                if (!string.IsNullOrEmpty(context.Request.Form["paralel"]))
                {
                    paralel = Convert.ToBoolean(context.Request.Form["paralel"].Trim());

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

                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<EventDataModel> list = new List<EventDataModel>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
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

                    //tiyatro
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

                    List<vArticlesZonesFull> articles = null;
                    #region iksv gala - etkinlik

                    if (zone_ids.Count > 0)
                    {

                       var eventDatas = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID) && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();

                        articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity && (w.Date1.Value < DateTime.Now && w.Date2.Value > DateTime.Now)).ToList();
                        //articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity && string.IsNullOrEmpty(w.Custom9)).ToList();

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
                                programListItem.longitude = program.Custom7.ToString();
                                programListItem.latitude = program.Custom6.ToString();
                                programListItem.coordinateLink = program.Custom3.ToString();
                                programListItem.updateDate = program.Updated;
                                programList.Add(programListItem);
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;

                            listItem.programs = programList;
                            listItem.alias = article.ArticleZoneAlias;
                            listItem.category = article.ZoneName;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;

                            var sectionList = new List<section>();
                            var categories = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID) && w.Status == 1 && w.ClassificationID == etkinlikKategoriClassification).ToList();
                            if (article.Custom10.Contains(","))
                            {
                                var customSplit = article.Custom10.Split(',').Select(s => Convert.ToInt32(s)).ToList();
                                categories = categories.Where(w => customSplit.Contains(w.ArticleID)).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }
                            else if (!string.IsNullOrEmpty(article.Custom10))
                            {
                                var categoryId = Convert.ToInt32(article.Custom10);
                                categories = categories.Where(w => w.ArticleID == categoryId).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }

                            listItem.sections = sectionList;
                            listItem.director = article.Custom2;

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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
                                programListItem.longitude = program.Custom7.ToString();
                                programListItem.latitude = program.Custom6.ToString();
                                programListItem.coordinateLink = program.Custom3.ToString();
                                programListItem.updateDate = program.Updated;
                                programList.Add(programListItem);
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.spotify = article.Custom3;
                            listItem.appleMusic = article.Custom8;

                            listItem.programs = programList;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Müzik" : "Music"));
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;

                            var sectionList = new List<section>();
                            var categories = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == musicZoneId && w.Status == 1 && w.ClassificationID == etkinlikKategoriClassification).ToList();
                            if (article.Custom10.Contains(","))
                            {
                                var customSplit = article.Custom10.Split(',').Select(s => Convert.ToInt32(s)).ToList();
                                categories = categories.Where(w => customSplit.Contains(w.ArticleID)).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }
                            else if (!string.IsNullOrEmpty(article.Custom10))
                            {
                                var categoryId = Convert.ToInt32(article.Custom10);
                                categories = categories.Where(w => w.ArticleID == categoryId).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }

                            listItem.sections = sectionList;
                            listItem.director = article.Custom2;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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

                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            List<Program> programList = new List<Program>();

                            var programs = eventDatas.Where(w => w.ZoneID == article.ZoneID && w.Status == 1 && w.ClassificationID == etkinlikProgramClassification).OrderBy(o => o.Date1).ToList();
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
                                programListItem.longitude = program.Custom7.ToString();
                                programListItem.latitude = program.Custom6.ToString();
                                programListItem.coordinateLink = program.Custom3.ToString();
                                programListItem.updateDate = program.Updated;
                                programList.Add(programListItem);
                            }

                            var listItem = new EventDataModel();
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;

                            listItem.programs = programList;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Etkinlik" : "Event") : (lang == "tr" ? "Film" : "Film"));
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;

                            var sectionList = new List<section>();
                            var categories = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == filmZoneId && w.Status == 1 && w.ClassificationID == etkinlikKategoriClassification).ToList();
                            if (article.Custom10.Contains(","))
                            {
                                var customSplit = article.Custom10.Split(',').Select(s => Convert.ToInt32(s)).ToList();
                                categories = categories.Where(w => customSplit.Contains(w.ArticleID)).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }
                            else if (!string.IsNullOrEmpty(article.Custom10))
                            {
                                var categoryId = Convert.ToInt32(article.Custom10);
                                categories = categories.Where(w => w.ArticleID == categoryId).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }

                            listItem.sections = sectionList;
                            listItem.director = article.Custom2;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
                                eventFile.order = file.FileOrder;
                                fileList.Add(eventFile);
                            }
                            listItem.files = fileList;
                            listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

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
                                programListItem.longitude = program.Custom7.ToString();
                                programListItem.latitude = program.Custom6.ToString();
                                programListItem.coordinateLink = program.Custom3.ToString();
                                programListItem.updateDate = program.Updated;
                                programList.Add(programListItem);
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.spotify = article.Custom3;
                            listItem.appleMusic = article.Custom8;

                            listItem.programs = programList;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Yan Etkinlik" : "Side Event") : (lang == "tr" ? "Caz" : "Jazz"));
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;

                            var sectionList = new List<section>();
                            var categories = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == cazzZoneId && w.Status == 1 && w.ClassificationID == etkinlikKategoriClassification).ToList();
                            if (article.Custom10.Contains(","))
                            {
                                var customSplit = article.Custom10.Split(',').Select(s => Convert.ToInt32(s)).ToList();
                                categories = categories.Where(w => customSplit.Contains(w.ArticleID)).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }
                            else if (!string.IsNullOrEmpty(article.Custom10))
                            {
                                var categoryId = Convert.ToInt32(article.Custom10);
                                categories = categories.Where(w => w.ArticleID == categoryId).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }

                            listItem.sections = sectionList;
                            listItem.director = article.Custom2;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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
                        //muzik festival data
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
                                programListItem.longitude = program.Custom7.ToString();
                                programListItem.latitude = program.Custom6.ToString();
                                programListItem.coordinateLink = program.Custom3.ToString();
                                programListItem.flag1 = program.Flag1;
                                programListItem.updateDate = program.Updated;
                                programList.Add(programListItem);
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.spotify = article.Custom3;
                            listItem.appleMusic = article.Custom8;

                            listItem.programs = programList;
                            listItem.category = (article.Flag1 ? (lang == "tr" ? "Tiyatro Yan Etkinlik" : "Theater Side Event") : (article.Flag3 ? (lang == "tr" ? "Paralel Etkinlik" : "Parallel Event") : (lang == "tr" ? "Tiyatro" : "Theater")));
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;

                            var sectionList = new List<section>();
                            var categories = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == tiyatroZoneId && w.Status == 1 && w.ClassificationID == etkinlikKategoriClassification).ToList();
                            if (article.Custom10.Contains(","))
                            {
                                var customSplit = article.Custom10.Split(',').Select(s => Convert.ToInt32(s)).ToList();
                                categories = categories.Where(w => customSplit.Contains(w.ArticleID)).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }
                            else if (!string.IsNullOrEmpty(article.Custom10))
                            {
                                var categoryId = Convert.ToInt32(article.Custom10);
                                categories = categories.Where(w => w.ArticleID == categoryId).ToList();
                                sectionList = categories.Select(s => new section { id = s.ArticleID, title = s.Headline }).ToList();
                            }

                            listItem.sections = sectionList;
                            listItem.director = article.Custom2;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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
                        //salon data
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
                                programListItem.longitude = program.Custom7.ToString();
                                programListItem.latitude = program.Custom6.ToString();
                                programListItem.coordinateLink = program.Custom3.ToString();
                                programListItem.flag1 = program.Flag1;
                                programListItem.updateDate = program.Updated;
                                programList.Add(programListItem);
                            }

                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.spotify = article.Custom3;
                            listItem.appleMusic = article.Custom8;

                            listItem.programs = programList;
                            listItem.category = article.Custom10;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;

                            listItem.director = article.Custom2;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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

                    if (zone_ids.Count > 0)
                    {
                        byte nd = 0;
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => zone_ids.Contains(w.ZoneID) && w.Status == 1 && w.ClassificationID == MenuClassification).ToList(); 
                        articles = eventDatas.Where(w => w.Status == 1 && w.Flag1 == activity && w.NavigationDisplay != nd).ToList();

                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();
                        List<BienalEvent> bienalEvents = new List<BienalEvent>();
                        List<ArticleList> articleList = new List<ArticleList>();
                        foreach (var article in articles)
                        {
                            var listItem = new EventDataModel();
                            bienalEvents = getBienalEvents(article.NavigationZoneID, activity, lang);
                            if (bienalEvents.Count() > 0)
                            {
                                listItem.bieanalEvents = bienalEvents;
                            }
                            else
                            {
                                articleList = getArticleList(article.NavigationZoneID, activity, lang);
                                listItem.articleList = articleList;
                            }
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;
                            listItem.order = article.AzOrder;
                            listItem.link = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.spotify = article.Custom3;     
                            listItem.appleMusic = article.Custom8;
                            listItem.category = article.Custom10;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;
                            

                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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

                    if (altkatZone_ids.Count > 0)
                    {
                        byte nd = 0;
                        var eventDatas = dbContext.vArticlesZonesFulls.Where(w => altkatZone_ids.Contains(w.ZoneID) && w.Status == 1 && w.ClassificationID == MenuClassification).ToList(); 
                        articles = eventDatas.Where(w => w.Status == 1 && w.Flag1 == activity && w.NavigationDisplay != nd).ToList();

                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();
                        List<AltkatModel> altkatEvents = new List<AltkatModel>();
                        List<KidBook> kidBookList = new List<KidBook>();
                        
                        foreach (var article in articles)
                        {
                            var listItem = new EventDataModel();
                            var altkatEventZone = article.Article2.Trim();
                            if (!string.IsNullOrEmpty(altkatEventZone))
                            {
                                var altkatEventZoneId = Convert.ToInt32(altkatEventZone);
                                altkatEvents = getAltkatEvents(altkatEventZoneId, activity, lang);
                                listItem.altkatEvents = altkatEvents;
                            }
                            
                            kidBookList = getKidBookList(article.NavigationZoneID, activity, lang);
                            listItem.kidBookList = kidBookList;
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.zone = article.ZoneName;
                            listItem.zoneId = article.ZoneID;
                            listItem.order = article.AzOrder;
                            listItem.link = article.Custom1;
                            listItem.activity = article.Flag1;
                            listItem.tags = article.TagContents;
                            listItem.tagIds = article.TagIds;
                            listItem.section = article.Custom10;
                            listItem.director = article.Custom2;
                            listItem.spotify = article.Custom3;
                            listItem.appleMusic = article.Custom8;
                            listItem.category = article.Custom10;
                            listItem.detail = article.Article5;
                            listItem.detailForWeb = article.Article1;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;


                            List<EventFile> fileList = new List<EventFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var eventFile = new EventFile();
                                eventFile.id = file.FileId;
                                eventFile.articleid = file.ArticleId;
                                eventFile.type = file.FileTypeId;
                                eventFile.title = file.Title;
                                eventFile.commnent = file.Comment;
                                eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
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

                return Ok(new { results = list.OrderBy(l => l.order) });
            }
            catch (Exception ex)
            {
                return Ok(new { results = ex.Message + " - " + ex.InnerException + " - " + ex.StackTrace });
            }
        }

        public List<BienalEvent> getBienalEvents(int bienalZone, Boolean activity, string lang)
        {

            if (bienalZone == 0)
            {
                return new List<BienalEvent>();
            }
            else
            {
                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<BienalEvent> list = new List<BienalEvent>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    List<vArticlesZonesFull> articles = null;
                    var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == bienalZone && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                    articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();
                    

                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                    foreach (var article in articles)
                    {
                        List<Program> programList = new List<Program>();
                        var listItem = new BienalEvent();
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

                        listItem.articleId = article.ArticleID;
                        listItem.headline = article.Headline;
                        listItem.zone = article.ZoneName;
                        listItem.zoneId = article.ZoneID;
                        listItem.bienalPrograms = programList;
                        listItem.order = article.AzOrder;
                        listItem.thumb = article.Custom1;
                        listItem.section = article.Custom10;
                        listItem.spotify = article.Custom3;
                        listItem.appleMusic = article.Custom8;
                        listItem.category = article.Custom10;
                        listItem.activity = article.Flag1;
                        listItem.tags = article.TagContents;
                        listItem.tagIds = article.TagIds;
                        listItem.director = article.Custom2;
                        listItem.detail = article.Article5;
                        listItem.detailForWeb = article.Article1;
                        listItem.recordDate = article.Created;
                        listItem.updateDate = article.Updated;

                        List<EventFile> fileList = new List<EventFile>();

                        var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                        foreach (var file in articleFiles)
                        {
                            var eventFile = new EventFile();
                            eventFile.id = file.FileId;
                            eventFile.articleid = file.ArticleId;
                            eventFile.type = file.FileTypeId;
                            eventFile.title = file.Title;
                            eventFile.commnent = file.Comment;
                            eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                            eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                            eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                            eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                            eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                            eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                            eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                            eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                            eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                            eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
                            eventFile.order = file.FileOrder;
                            fileList.Add(eventFile);

                        }

                        listItem.eventsFiles = fileList;
                        listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                        list.Add(listItem);
                    }
                    return list;
                }
            }
        }

        public List<AltkatModel> getAltkatEvents(int altkatZone, Boolean activity, string lang)
        {

            if (altkatZone == 0)
            {
                return new List<AltkatModel>();
            }
            else
            {
                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<AltkatModel> list = new List<AltkatModel>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    List<vArticlesZonesFull> articles = null;
                    var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == altkatZone && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                    articles = eventDatas.Where(w => w.ClassificationID == etkinlikClassification && w.Flag1 == activity).ToList();


                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                    foreach (var article in articles)
                    {
                        List<Program> programList = new List<Program>();
                        var listItem = new AltkatModel();
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

                        listItem.articleId = article.ArticleID;
                        listItem.headline = article.Headline;
                        listItem.zone = article.ZoneName;
                        listItem.zoneId = article.ZoneID;
                        listItem.altkatPrograms = programList;
                        listItem.order = article.AzOrder;
                        listItem.thumb = article.Custom1;
                        listItem.section = article.Custom10;
                        listItem.spotify = article.Custom3;
                        listItem.appleMusic = article.Custom8;
                        listItem.category = article.Custom10;
                        listItem.activity = article.Flag1;
                        listItem.tags = article.TagContents;
                        listItem.tagIds = article.TagIds;
                        listItem.director = article.Custom2;
                        listItem.detail = article.Article5;
                        listItem.detailForWeb = article.Article1;
                        listItem.recordDate = article.Created;
                        listItem.updateDate = article.Updated;

                        List<EventFile> fileList = new List<EventFile>();

                        var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                        foreach (var file in articleFiles)
                        {
                            var eventFile = new EventFile();
                            eventFile.id = file.FileId;
                            eventFile.articleid = file.ArticleId;
                            eventFile.type = file.FileTypeId;
                            eventFile.title = file.Title;
                            eventFile.commnent = file.Comment;
                            eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                            eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                            eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                            eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                            eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                            eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                            eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                            eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                            eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                            eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
                            eventFile.order = file.FileOrder;
                            fileList.Add(eventFile);

                        }

                        listItem.eventsFiles = fileList;
                        listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                        list.Add(listItem);
                    }
                    return list;
                }
            }
        }

        public List<ArticleList> getArticleList(int zoneId, Boolean activity, string lang)
        {

            if (zoneId == 0)
            {
                return new List<ArticleList>();
            }
            else
            {
                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<ArticleList> list = new List<ArticleList>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    List<vArticlesZonesFull> articles = null;
                    var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                    articles = eventDatas.Where(w => w.Status == 1 && w.Flag1 == activity).ToList();

                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();
                    foreach (var article in articles)
                    {
                        
                        var listItem = new ArticleList();
                        
                        listItem.articleId = article.ArticleID;
                        listItem.headline = article.Headline;
                        listItem.zone = article.ZoneName;
                        listItem.zoneId = article.ZoneID;
                        listItem.order = article.AzOrder;
                        listItem.activity = article.Flag1;
                        listItem.tags = article.TagContents;
                        listItem.tagIds = article.TagIds;
                        listItem.section = article.Custom10;
                        listItem.appleMusic = article.Custom8;
                        listItem.category = article.Custom10;
                        listItem.detail = article.Article5;
                        listItem.detailForWeb = article.Article1;
                        listItem.recordDate = article.Created;
                        listItem.updateDate = article.Updated;
                        listItem.relatedArticles = article.Custom5;
                        listItem.latitude = article.Custom3;
                        listItem.longitude = article.Custom4;
                        listItem.address = article.Custom2;

                        List<EventFile> fileList = new List<EventFile>();

                        var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                        foreach (var file in articleFiles)
                        {
                            var eventFile = new EventFile();
                            eventFile.id = file.FileId;
                            eventFile.articleid = file.ArticleId;
                            eventFile.type = file.FileTypeId;
                            eventFile.title = file.Title;
                            eventFile.commnent = file.Comment;
                            eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                            eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                            eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                            eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                            eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                            eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                            eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                            eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                            eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                            eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
                            eventFile.order = file.FileOrder;
                            fileList.Add(eventFile);

                        }

                        listItem.files = fileList;
                        listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                        list.Add(listItem);
                        list.OrderBy(l => l.order);
                    }
                }
                return list;
                }
            }

        public List<KidBook> getKidBookList(int zoneId, Boolean activity, string lang)
        {

            if (zoneId == 0)
            {
                return new List<KidBook>();
            }
            else
            {
                DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
                List<KidBook> list = new List<KidBook>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    List<vArticlesZonesFull> articles = null;
                    var eventDatas = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zoneId && w.Status == 1 && w.ClassificationID != etkinlikKategoriClassification).ToList();
                    articles = eventDatas.Where(w => w.Status == 1 && w.Flag1 == activity).ToList();

                    var articleIds = articles.Select(s => s.ArticleID).ToList();
                    var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();
                    foreach (var article in articles)
                    {

                        var listItem = new KidBook();

                        listItem.articleId = article.ArticleID;
                        listItem.headline = article.Headline;
                        listItem.zone = article.ZoneName;
                        listItem.zoneId = article.ZoneID;
                        listItem.order = article.AzOrder;
                        listItem.activity = article.Flag1;
                        listItem.tags = article.TagContents;
                        listItem.tagIds = article.TagIds;
                        listItem.detail = article.Article5;
                        listItem.detailForWeb = article.Article1;
                        listItem.recordDate = article.Created;
                        listItem.updateDate = article.Updated;
                        listItem.writer = article.Custom1;
                        listItem.illustrator = article.Custom2;
                        listItem.listenLink = "i/assets/iksv/altkat/sounds/" + article.Custom3;
                        listItem.videoLink = article.Custom4;



                        List<EventFile> fileList = new List<EventFile>();

                        var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                        foreach (var file in articleFiles)
                        {
                            var eventFile = new EventFile();
                            eventFile.id = file.FileId;
                            eventFile.articleid = file.ArticleId;
                            eventFile.type = file.FileTypeId;
                            eventFile.title = file.Title;
                            eventFile.commnent = file.Comment;
                            eventFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                            eventFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                            eventFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                            eventFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                            eventFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                            eventFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                            eventFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                            eventFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                            eventFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                            eventFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
                            eventFile.order = file.FileOrder;
                            fileList.Add(eventFile);

                        }

                        listItem.files = fileList;
                        listItem.alias = article.DomainName.Trim() + "/" + article.ArticleZoneAlias;

                        list.Add(listItem);
                        list.OrderBy(l => l.order);
                    }
                }
                return list;
            }
        }

    }
}
