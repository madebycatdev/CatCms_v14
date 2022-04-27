using System;

namespace OravPlugin.Models
{
    public class ArticleInfo
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
