using System;
using System.Collections.Generic;

namespace EuroCMS.Plugin.IKSV.Models
{

    public class EventDataModel
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string zone { get; set; }
        public int zoneId { get; set; }
        public string date { get; set; }
        public string endDate { get; set; }
        public string time { get; set; }
        public DateTime dateFormatted { get; set; }
        public DateTime? endDateFormatted { get; set; }
        public string place { get; set; }
        public List<ArticleFileItem> files { get; set; }
        public List<Program> programs { get; set; }
        public int order { get; set; }
        public string alias { get; set; }
        public string category { get; set; }
        public string thumb { get; set; }
        public string tags { get; set; }
        public string tagIds { get; set; }
        public bool activity { get; set; }
        public string section { get; set; }
        public string director { get; set; }
        public string day { get; set; }
        public string dayString { get; set; }
        public string monthString { get; set; }
        public string month { get; set; }
        public int year { get; set; }
        public bool flag1 { get; set; }
        public bool flag2 { get; set; }
        public bool flag3 { get; set; }
        public bool flag4 { get; set; }
        public bool flag5 { get; set; }
    }

    public class Program
    {

        public string description { get; set; }
        public DateTime date { get; set; }
        public DateTime? endDate { get; set; }
        public string dateString { get; set; }
        public string dateFormettedString { get; set; }
        public string place { get; set; }
        public string ticketUrl { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string time { get; set; }
        public bool flag1 { get; set; }
        public bool flag2 { get; set; }
        public bool flag3 { get; set; }
        public bool flag4 { get; set; }
        public bool flag5 { get; set; }
        public string day { get; set; }
        public string dayString { get; set; }
        public string monthString { get; set; }
        public string month { get; set; }
        public int year { get; set; }

    }

    public class chart
    {
        public string day { get; set; }
        public string month { get; set; }
        public string monthString { get; set; }
        public string year { get; set; }
        public string date { get; set; }
        public List<chartItem> events { get; set; }
        public bool status { get; set; }
        public DateTime fullDate { get; set; }
    }

    public class chartItem
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string date { get; set; }
        public DateTime date1 { get; set; }
        public string enddate { get; set; }
        public string place { get; set; }
        public string alias { get; set; }
        public string thumb { get; set; }
        public string ticket { get; set; }
        public bool flag1 { get; set; }
        public bool activity { get; set; }
    }


    public class programchart
    {
        public string day { get; set; }
        public string month { get; set; }
        public string monthString { get; set; }
        public string year { get; set; }
        public string date { get; set; }
        //public List<string> sessions { get; set; }
        public List<programchartItem> events { get; set; }
        public bool status { get; set; }
        public bool selected { get; set; }
    }


    public class programchartItem
    {
        public string place { get; set; }
        public List<programchartdetail> details { get; set; }
    }

    public class programchartdetail
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string date { get; set; }
        public string alias { get; set; }
        public string ticket { get; set; }
        public bool status { get; set; }
    }



    public class salonevent
    {
        public int articleid { get; set; }
        public string headline { get; set; }
        public string alias { get; set; }
        public DateTime date_1 { get; set; }
        public string custom_1 { get; set; }
        public string custom_2 { get; set; }
        public string custom_3 { get; set; }
        public string custom_4 { get; set; }
        public int dayNo { get; set; }
        public string day { get; set; }
        public int monthNo { get; set; }
        public string month { get; set; }
        public int year { get; set; }
        public string thumbnail { get; set; }
        public string time { get; set; }
    }

}