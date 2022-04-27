using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.IKSV.Models
{
    public class VenueDetail
    {
        public int articleId { get; set; }
        public int zoneId { get; set; }
        public string zone { get; set; }
        public string alias { get; set; }
        public string headline { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public string dateString { get; set; }
        public string place { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string district { get; set; }
        public string discount { get; set; }
        public string category { get; set; }
        public int order { get; set; }
        public List<ArticleFileItem> files { get; set; }
    }

   
}