using EuroCMS.Model;
using EuroCMS.Plugin.IKSV.Providers;
using EuroCMS.Plugin.IKSV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EuroCMS.Plugin.IKSV.Controllers
{
    public class AnnouncementController : ApiController
    {
        [HttpPost]
        [CustomAuthorize]
        public IHttpActionResult GetAnnouncements()
        {
            int zoneId = 0;
            try
            {
                System.Web.HttpContextWrapper context = this.Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                context.Response.ContentType = "text/json";

                if (!string.IsNullOrEmpty(context.Request.Form["zone_id"]))
                {
                    zoneId = Convert.ToInt32(context.Request.Form["zone_id"].Trim());
                }
                else
                {
                    return Ok(new { results = "zone_id boş gönderilemez" });
                }

                List<AnnouncementDataModel> list = new List<AnnouncementDataModel>();

                using (CmsDbContext dbContext = new CmsDbContext())
                {
                    var articles = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zoneId && w.Status == 1).ToList();
                    if (articles != null)
                    {
                        var articleIds = articles.Select(s => s.ArticleID).ToList();
                        var files = dbContext.Files.Where(f => articleIds.Contains(f.ArticleId)).ToList();

                        foreach (var article in articles)
                        {
                            var listItem = new AnnouncementDataModel();
                            listItem.articleId = article.ArticleID;
                            listItem.headline = article.Headline;
                            listItem.description = article.Article5;
                            listItem.alias = article.ArticleZoneAlias;
                            listItem.recordDate = article.Created;
                            listItem.updateDate = article.Updated;
                            listItem.order = article.AzOrder;

                            List<AnnouncementFile> fileList = new List<AnnouncementFile>();

                            var articleFiles = files.Where(w => w.ArticleId == article.ArticleID).ToList();
                            foreach (var file in articleFiles)
                            {
                                var announcementFile = new AnnouncementFile();
                                announcementFile.id = file.FileId;
                                announcementFile.type = file.FileTypeId;
                                announcementFile.title = file.Title;
                                announcementFile.commnent = file.Comment;
                                announcementFile.file1 = (string.IsNullOrEmpty(file.File1) ? "" : file.ArticleId + "_" + file.File1);
                                announcementFile.file2 = (string.IsNullOrEmpty(file.File2) ? "" : file.ArticleId + "_" + file.File2);
                                announcementFile.file3 = (string.IsNullOrEmpty(file.File3) ? "" : file.ArticleId + "_" + file.File3);
                                announcementFile.file4 = (string.IsNullOrEmpty(file.File4) ? "" : file.ArticleId + "_" + file.File4);
                                announcementFile.file5 = (string.IsNullOrEmpty(file.File5) ? "" : file.ArticleId + "_" + file.File5);
                                announcementFile.file6 = (string.IsNullOrEmpty(file.File6) ? "" : file.ArticleId + "_" + file.File6);
                                announcementFile.file7 = (string.IsNullOrEmpty(file.File7) ? "" : file.ArticleId + "_" + file.File7);
                                announcementFile.file8 = (string.IsNullOrEmpty(file.File8) ? "" : file.ArticleId + "_" + file.File8);
                                announcementFile.file9 = (string.IsNullOrEmpty(file.File9) ? "" : file.ArticleId + "_" + file.File9);
                                announcementFile.file10 = (string.IsNullOrEmpty(file.File10) ? "" : file.ArticleId + "_" + file.File10);
                                announcementFile.articleid = file.ArticleId;
                                announcementFile.order = file.FileOrder;
                                fileList.Add(announcementFile);
                            }

                            listItem.files = fileList;

                            if (article.ArticleType == 1)
                            {
                                listItem.alias = article.ArticleTypeDetail;
                            }
                            else if (article.ArticleType == 2)
                            {
                                var splitArticleTypeDetail = article.ArticleTypeDetail.Split('-');
                                if (splitArticleTypeDetail.Count() > 0)
                                {
                                    var redZoneId = Convert.ToInt32(splitArticleTypeDetail[0]);
                                    var redArticleId = Convert.ToInt32(splitArticleTypeDetail[1]);
                                    var redirectArticle = dbContext.vArticlesZonesFulls.FirstOrDefault(w => w.ZoneID == redZoneId && w.ArticleID == redArticleId && w.Status == 1);
                                    if (redirectArticle != null)
                                    {
                                        listItem.alias = redirectArticle.DomainName.Trim() + "/" + redirectArticle.ArticleZoneAlias;
                                    }
                                }
                            }

                            list.Add(listItem);
                        }

                        list = list.OrderBy(o => o.order).ToList();

                        return Ok(new { results = list });
                    }
                    return Ok(new { results = "Duyuru bulunamadı.zone_id değerini kontrol ediniz." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { results = ex.Message + " - " + ex.InnerException + " - " + ex.StackTrace });
            }
        }
    }
}
