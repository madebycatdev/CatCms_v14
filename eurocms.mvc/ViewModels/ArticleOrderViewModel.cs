using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.ViewModels
{
    public class ArticleOrderViewModel
    {
        [Required]
        public int ZoneId { get; set; }

        public List<Zone> ZoneList { get; set; }

        public List<int> ZoneGroupIds { get; set; }

        public List<ArticleOrderArticle> result { get; set; }
       
        public string ids { get; set; }
    }

    public class ArticleOrderList
    {
        public int id { get; set; }
    }

    public class ArticleOrderArticle
    {
        public int id { get; set; }

        public string headline { get; set; }

        public int status { get; set; }
    }
}