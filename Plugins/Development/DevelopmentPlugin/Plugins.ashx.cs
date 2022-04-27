using EuroCMS.Model;
using GraphQL.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace EuroCMS.Plugin.Development
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler
    {
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
                case "events":
                    result = events(context);
                    break;
            }

            context.Response.Write(result);
        }

        public string events(HttpContext context)
        {
            string query = "";
            if (!string.IsNullOrEmpty(context.Request.Form["query"]))
            {
                query = context.Request.Form["query"].Trim();
            }
            else
            {
                context.Response.Write(jss.Serialize("query boş gönderilemez"));
                context.Response.End();
            }

            var schema = GraphQL<CmsDbContext>.CreateDefaultSchema(() => new CmsDbContext());

            var article = schema.AddType<Article>();

            article.AddField(a => a.Headline);
            article.AddField(a => a.Summary);
            article.AddField(a => a.Custom1);
            article.AddField(a => a.NavigationZoneId);
            article.AddField("navs", (db, a) =>(a.NavigationZoneId == 0 ? null : db.Articles.Where(w => w.NavigationZoneId == a.NavigationZoneId).Select(s => s.Headline).AsQueryable()));


            schema.AddListField("articles", db => db.Articles);
            schema.AddField("article", new { id = 0 }, (db, args) => db.Articles.Where(u => u.Id == args.id).FirstOrDefault());
            schema.Complete();

            var gql = new GraphQL<CmsDbContext>(schema);
            var dict = gql.ExecuteQuery(query);


            return jss.Serialize(dict);

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