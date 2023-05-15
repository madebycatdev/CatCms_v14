using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.IKSV.Models
{
    public class GetSequentialResult
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Previous { get; set; }
        public string PreviousImage { get; set; }
        public string Next { get; set; }
        public string NextImage { get; set; }
        public string NextHeadline { get; set; }
        public string PrevHeadline { get; set; }
        public string NextFile1 { get; set; }
        public string PrevFile1 { get; set; }
        public int Current { get; set; }
        public int Total { get; set; }
    }
}