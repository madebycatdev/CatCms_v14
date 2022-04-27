
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class WebEventLogModel
    {
        public string EventId { get; set; }

        public DateTime EventTimeUtc { get; set; }

        public DateTime EventTime { get; set; }

        public string EventType { get; set; }

        public decimal EventSequence { get; set; }

        public decimal EventOccurrence { get; set; }

        public int EventCode { get; set; }

        public int EventDetailCode { get; set; }

        public string Message { get; set; }

        public string ApplicationPath { get; set; }

        public string ApplicationVirtualPath { get; set; }

        public string MachineName { get; set; }

        public string RequestUrl { get; set; }

        public string ExceptionType { get; set; }

        public string Details { get; set; }
    }

    public class WebEventLogDbContext : BaseDbContext
    {
        public WebEventLogDbContext()
            : base()
        { }

        public List<WebEventLogModel> GetWebEventLogs(string WebEventType, DateTime BeginDate, DateTime EndDate, int PageIndex, int PageSize, out int Total)
        {

            var WebEventTypeParam = new SqlParameter
            {
                ParameterName = "WebEventType",
                Value = WebEventType ?? ""
            };

            var BeginDateParam = new SqlParameter
            {
                ParameterName = "BeginDate",
                Value = BeginDate
            };

            var EndDateParam = new SqlParameter
            {
                ParameterName = "EndDate",
                Value = EndDate
            };

            var PageIndexParam = new SqlParameter
            {
                ParameterName = "PageIndex",
                Value = PageIndex
            };

            var PageSizeParam = new SqlParameter
            {
                ParameterName = "PageSize",
                Value = PageSize
            };

            var TotalRecords = new SqlParameter {
                   ParameterName = "TotalRecords",
                   Value=0,
                   Direction = ParameterDirection.Output };

            
            IEnumerable<WebEventLogModel> results = this.Database.SqlQuery<WebEventLogModel>
                ("cms_WebEvent_GetLogEvent @WebEventType, @BeginDate, @EndDate, @PageIndex, @PageSize, @TotalRecords out", WebEventTypeParam, BeginDateParam, EndDateParam, PageIndexParam, PageSizeParam, TotalRecords);

            var logs = results.ToList();
            Total = (int)TotalRecords.Value;

            return logs;
        }

        public void ClearWebEventLogs()
        {
            this.Database.ExecuteSqlCommand
                ("dbo.cms_WebEvent_ClearAllEvent");
        }
    }

    
}