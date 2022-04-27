using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,UserCreator")]
    public class WebEventLogController : BaseController
    {
        WebEventLogDbContext context = new WebEventLogDbContext();

        int pageSize = 50;
        int totalResult = 0;
        int currentPage = 1;

        [CmsAuthorize(Permission = "View", ContentType = "WebEventLog")]
        public ActionResult Index(int? page, string WebEventType, string BeginDate, string EndDate)
        {
            currentPage = page ?? 1;

            var values = Enum.GetValues(typeof(EuroCMS.Management.CmsWebEventType));
            List<SelectListItem> enumList = new List<SelectListItem>();
            enumList.Add(new SelectListItem() { Text = "Error & Failure", Value = "" });
            foreach (var c in values)
            {
                enumList.Add(new SelectListItem() { Text = ((EuroCMS.Management.CmsWebEventType)c).ToString(), Value = c.ToString() });
            }

            ViewBag.WebEventType = WebEventType;

            ViewData["CmsWebEventTypes"] = enumList;
             
            DateTime _BeginDate = DateTime.MinValue;
            DateTime _EndDate = DateTime.MaxValue;

            if(!DateTime.TryParse(BeginDate, out _BeginDate))
                _BeginDate = (DateTime)SqlDateTime.MinValue;

            if(!DateTime.TryParse(EndDate, out _EndDate))
                _EndDate = (DateTime)SqlDateTime.MaxValue;


            ViewData["BeginDate"] = BeginDate;
            ViewData["EndDate"] = EndDate;

            var result = context.GetWebEventLogs(WebEventType, _BeginDate, _EndDate, currentPage - 1, pageSize, out totalResult);

            var resultAsPagedList = new StaticPagedList<WebEventLogModel>(result, currentPage, pageSize, totalResult);

            return View(resultAsPagedList);
        }
    }
}
