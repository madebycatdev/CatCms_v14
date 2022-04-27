using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.IKSV.Models
{
    public class GetArticleModel
    {
        public int articleid { get; set; }
        public int classificationid { get; set; }
        public int zoneid { get; set; }
        public string zone { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }      
        public string custom1 { get; set; }
        public string custom2 { get; set; }
        public string custom3 { get; set; }
        public string custom4 { get; set; }
        public string custom5 { get; set; }
        public bool activity { get; set; }
        public string date1 { get; set; }
        public string alias { get; set; }
        public string year { get; set; }
        public int order { get; set; }
        public List<fileReturn> files { get; set; }
    }

    public class fileReturn
    {
        public int filetypeid { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int order { get; set; }
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string file4 { get; set; }
    }
}