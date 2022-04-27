using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.IKSV.ViewModels
{
    public class AnnouncementDataModel
    {
        public int articleId { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public List<AnnouncementFile> files { get; set; }
        public string alias { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime updateDate { get; set; }
        public int order { get; set; }
    }
    public class AnnouncementFile
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
}