using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.IKSV.Models
{
    public class SectionDataModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<SectionDataModel> subSection { get; set; }
        public string alias { get; set; }
        public bool isMain { get; set; }
    }
}