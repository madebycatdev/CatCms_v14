using EuroCMS.Core;
using EuroCMS.Model;
using Newtonsoft.Json;
using OravPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EuroCMS.Plugin.Orav
{
    public class Plugins : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string result = "", action = "";

            try
            {
                if (!string.IsNullOrEmpty(context.Request["plugin"]))
                {
                    action = context.Request["plugin"];
                    var zoneId = Convert.ToInt32(context.Request["zoneid"]);

                    switch (action.ToLower().Trim())
                    {
                        case "getarticles":
                            var year = Convert.ToInt32(context.Request["year"]);
                            var page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
                            var pageSize = context.Request["pageSize"] == null ? 10 : Convert.ToInt32(context.Request["pageSize"]);
                            var articles = GetArticles(zoneId, year, page, pageSize);
                            result = JsonConvert.SerializeObject(Response<Paging<ArticleInfo>>.CreateSuccessResponse(articles));
                            break;
                        case "getarchives":
                            var archives = GetArchives(zoneId);
                            result = JsonConvert.SerializeObject(Response<List<Archive>>.CreateSuccessResponse(archives));
                            break;
                    }
                }
                else
                {
                    result = JsonConvert.SerializeObject(Response<string>.CreateFailResponse(300, "'plugin' required"));
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "", false);
                result = JsonConvert.SerializeObject(Response<string>.CreateFailResponse(500, ex.ToString()));
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(result);
        }

        public Paging<ArticleInfo> GetArticles(int zoneId, int year, int page = 1, int pageSize = 10)
        {
            try
            {
                CmsDbContext dbContext = new CmsDbContext();
                var files = new List<ArticleFile>();
                var dbArticles = new List<vArticlesZonesFull>();

                var articleQuery = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zoneId && w.Status == 1 && w.Date1.Value.Year == year)
                    .OrderBy(m => m.Date1.Value);

                dbArticles = articleQuery.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                var Ids = dbArticles.Select(m => m.ArticleID);

                files = dbContext.Files.Where(m => Ids.Contains(m.ArticleId)).ToList();

                Paging<ArticleInfo> articles = new Paging<ArticleInfo>
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalCount = articleQuery.Count()
                };

                foreach (var item in dbArticles)
                {
                    ArticleInfo article = new ArticleInfo()
                    {
                        Title = item.Headline,
                        Summary = item.Summary,
                        Id = item.ArticleID,
                        Url = item.ArticleZoneAlias,
                        Image = files.Where(m => m.ArticleId == item.ArticleID).Select(m => m.File1).FirstOrDefault(),
                        CreationDate = item.Date1.Value
                    };
                    articles.Data.Add(article);
                }

                return articles;
            }
            catch (System.Exception)
            {
                throw;
            }            
        }

        public List<Archive> GetArchives(int zoneId)
        {
            var archives = new List<Archive>();

            using (CmsDbContext dbContext = new CmsDbContext())
            {
                var archiveGroup = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == zoneId && w.Status == 1 && w.Date1 != null).GroupBy(m => m.Date1.Value.Year).ToList();
                archives = archiveGroup.Select(m => new Archive() { Year = m.Key, Count = m.Count() }).ToList();
            }

            return archives;
        }
    }
}