using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.IKSV.ViewModels
{
    public class EventDataModel
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string zone { get; set; }
        public string detail { get; set; }
        public string detailForWeb { get; set; }
        public int zoneId { get; set; }
        public List<EventFile> files { get; set; }
        public List<Program> programs { get; set; }
        public List<ArticleList> articleList { get; set; }
        public List<BienalEvent> bieanalEvents { get; set; }
        public List<AltkatModel> altkatEvents { get; set; }
        public List<KidBook> kidBookList { get; set; }
        public string alias { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public bool activity { get; set; }
        public List<section> sections { get; set; }
        public string director { get; set; }
        public string spotify { get; set; }
        public string appleMusic { get; set; }
        public DateTime recordDate {get;set;}
        public DateTime updateDate { get; set; }
        public string tagIds { get; set; }
        public int order { get; set; }
        public string section { get; set; }
        public string thumb { get; set; }
        public string place { get; set; }
        public string link { get; set; }
    }

    public class Program
    {
        public string description { get; set; }
        public DateTime date { get; set; }
        public string dateString { get; set; }
        public string dateFormettedString { get; set; }
        public string place { get; set; }
        public string ticketUrl { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string coordinateLink { get; set; }
        public string time { get; set; }
        public bool flag1 { get; set; }
        public int day { get; set; }
        public string dayString { get; set; }
        public string monthString { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public DateTime updateDate { get; set; }

    }

    public class EventFile
    {
        public long id { get; set; }
        public int articleid { get; set; }
        public int type { get; set; }
        public string title { get; set; }
        public string commnent { get; set; }
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string file4 { get; set; }
        public string file5 { get; set; }
        public string file6 { get; set; }
        public string file7 { get; set; }
        public string file8 { get; set; }
        public string file9 { get; set; }
        public string file10 { get; set; }
        public int order { get; set; }
    }

    public class section
    {
        public int id { get; set; }

        public string title { get; set; }
    }

    public class BienalEvent
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string zone { get; set; }
        public string detail { get; set; }
        public string detailForWeb { get; set; }
        public int zoneId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public DateTime dateFormatted { get; set; }
        public List<Program> bienalPrograms { get; set; }
        public List<EventFile> eventsFiles { get; set; }
        public string alias { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public bool activity { get; set; }
        public List<section> sections { get; set; }
        public string director { get; set; }
        public string spotify { get; set; }
        public string appleMusic { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime updateDate { get; set; }
        public string tagIds { get; set; }
        public int order { get; set; }
        public string section { get; set; }
        public string thumb { get; set; }
        public string place { get; set; }

    }

    public class AltkatModel
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string zone { get; set; }
        public string detail { get; set; }
        public string detailForWeb { get; set; }
        public int zoneId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public DateTime dateFormatted { get; set; }
        public List<Program> altkatPrograms { get; set; }
        public List<EventFile> eventsFiles { get; set; }
        public string alias { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public bool activity { get; set; }
        public List<section> sections { get; set; }
        public string director { get; set; }
        public string spotify { get; set; }
        public string appleMusic { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime updateDate { get; set; }
        public string tagIds { get; set; }
        public int order { get; set; }
        public string section { get; set; }
        public string thumb { get; set; }
        public string place { get; set; }


    }

    public class ArticleList
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string zone { get; set; }
        public string detail { get; set; }
        public string detailForWeb { get; set; }
        public int zoneId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public DateTime dateFormatted { get; set; }
        public List<Program> programs { get; set; }
        public List<EventFile> files { get; set; }
        public string relatedArticles { get; set; }
        public string alias { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public bool activity { get; set; }
        public List<section> sections { get; set; }
        public string director { get; set; }
        public string spotify { get; set; }
        public string appleMusic { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime updateDate { get; set; }
        public string tagIds { get; set; }
        public int order { get; set; }
        public string section { get; set; }
        public string thumb { get; set; }
        public string place { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
    }

    public class KidBook
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string zone { get; set; }
        public string detail { get; set; }
        public string detailForWeb { get; set; }
        public int zoneId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public DateTime dateFormatted { get; set; }
        public List<EventFile> files { get; set; }
        public string relatedArticles { get; set; }
        public string alias { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public bool activity { get; set; }
        public List<section> sections { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime updateDate { get; set; }
        public string tagIds { get; set; }
        public int order { get; set; }
        public string section { get; set; }
        public string thumb { get; set; }
        public string writer { get; set; }
        public string illustrator { get; set; }
        public string videoLink { get; set; }
        public string listenLink { get; set; }

    }
}