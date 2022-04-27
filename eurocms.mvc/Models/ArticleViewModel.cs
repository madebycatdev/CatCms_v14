using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class ArticleViewModel
    {
        public ArticleModel Article { get; set; }

        public ArticleModel Revision { get; set; }
    }
    public class ArticleModel
    {
        public string Status { get; set; }
        public int Classification { get; set; }
        public string Lang { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string Container1 { get; set; }
        public string Container2 { get; set; }
        public string Container3 { get; set; }
        public string Container4 { get; set; }
        public string Container5 { get; set; }
    }
}