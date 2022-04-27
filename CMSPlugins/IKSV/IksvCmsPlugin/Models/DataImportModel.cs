using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.CMSPlugin.IKSV.Models
{
    public class DataImportModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string[] ParentIdString { get; set; }
        public int ClassificationId { get; set; }
        public string Lang { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string Content_1 { get; set; }
        public string Content_2 { get; set; }
        public string Content_3 { get; set; }
        public string Content_4 { get; set; }
        public string Content_5 { get; set; }
        public string Custom_1 { get; set; }
        public string Custom_2 { get; set; }
        public string Custom_3 { get; set; }
        public string Custom_4 { get; set; }
        public string Custom_5 { get; set; }
        public string Custom_6 { get; set; }
        public string Custom_7 { get; set; }
        public string Custom_8 { get; set; }
        public string Custom_9 { get; set; }
        public string Custom_10 { get; set; }
        public string Tags { get; set; }
        public string Date_1 { get; set; }
        public int ZoneId { get; set; }
        public int NavigationZoneId { get; set; }
        public int Order { get; set; }
        public int ArticleId { get; set; }

    }
}