using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using EuroCMS.Admin.Common;

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,UserCreator")]
    public class ErrorLogController : BaseController
    {
        //
        // GET: /ErrorLog/

        public ActionResult Index(string Keyword, string UserId, string StartDate, string EndDate, bool? Status0, int? page)
        {
            //List<ErrorLog> errorList = new List<ErrorLog>();
            //List<vAspNetMembershipUser> userList = new List<vAspNetMembershipUser>();
            //CmsDbContext dbContext = new CmsDbContext();
            //List<ErrorLog> resultList = new List<ErrorLog>();

            //errorList = dbContext.ErrorLogs.ToList();
            //userList = dbContext.vAspNetMembershipUsers.ToList();
            //ViewBag.Users = userList;

            //resultList.AddRange(errorList);
            //if (!string.IsNullOrEmpty(UserId))
            //{
            //    resultList = resultList.Where(x => x.UserId.ToString() == UserId).ToList();
            //}
            //if (!string.IsNullOrEmpty(Keyword))
            //{
            //    resultList = resultList.Where(x => x.GetType().GetProperties().Any(y => (y.GetValue(x, null) == null ? string.Empty : y.GetValue(x, null)).ToString().ToLower().Contains(Keyword.ToLower()))).ToList();
            //    //resultList = resultList.Where(x => x.GetType().GetProperties().Any(y => y.GetValue(x, null).ToString().ToLower().Contains(Keyword.ToLower()))).ToList();

            //    //errorList.FirstOrDefault().GetType().GetProperties().Any(x => x.GetValue(errorList.FirstOrDefault()).ToString())
            //}
            //if (!string.IsNullOrEmpty(StartDate))
            //{
            //    DateTime dt = Convert.ToDateTime(StartDate);
            //    resultList = resultList.Where(x => x.LogDate >= dt).ToList();
            //}
            //if (!string.IsNullOrEmpty(EndDate))
            //{
            //    DateTime dt = Convert.ToDateTime(EndDate);
            //    resultList = resultList.Where(x => x.LogDate <= dt).ToList();
            //}
            //if (Status0 == true)
            //{
            //    resultList = resultList.Where(x => x.IsInCms).ToList();
            //}

            //ViewBag.Keyword = Keyword;
            //ViewBag.UserId = UserId;
            //ViewBag.StartDate = StartDate;
            //ViewBag.EndDate = EndDate;
            //ViewBag.Status0 = Status0;
            //ViewBag.ErrorCount = resultList.Count;

            //int pageNum = (page == null) ? 1 : Convert.ToInt32(page);



            IEnumerable<ErrorLog> errorList;
            List<vAspNetMembershipUser> userList = new List<vAspNetMembershipUser>();
            CmsDbContext dbContext = new CmsDbContext();
            IEnumerable<ErrorLog> resultList;

            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now;

            //if (!string.IsNullOrEmpty(UserId))
            //{
            //    resultList = resultList.Where(x => x.UserId.ToString() == UserId);
            //}
            //if (!string.IsNullOrEmpty(Keyword))
            //{
            //    resultList = resultList.Where(x => x.GetType().GetProperties().Any(y => (y.GetValue(x, null) == null ? string.Empty : y.GetValue(x, null)).ToString().ToLower().Contains(Keyword.ToLower())));
            //    //resultList = resultList.Where(x => x.GetType().GetProperties().Any(y => y.GetValue(x, null).ToString().ToLower().Contains(Keyword.ToLower()))).ToList();

            //    //errorList.FirstOrDefault().GetType().GetProperties().Any(x => x.GetValue(errorList.FirstOrDefault()).ToString())
            //}
            if (!string.IsNullOrEmpty(StartDate))
            {
                startDate = Convert.ToDateTime(StartDate);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                endDate = Convert.ToDateTime(EndDate);
            }
           

            errorList = dbContext.ErrorLogs.Where(x=> x.LogDate >= startDate && x.LogDate <= endDate).OrderByDescending(x => x.LogDate).Take(100000);


            userList = dbContext.vAspNetMembershipUsers.ToList();
            ViewBag.Users = userList;

            resultList = errorList;
            if (!string.IsNullOrEmpty(UserId))
            {
                resultList = resultList.Where(x => x.UserId.ToString() == UserId);
            }
            if (!string.IsNullOrEmpty(Keyword))
            {
                resultList = resultList.Where(x => x.GetType().GetProperties().Any(y => (y.GetValue(x, null) == null ? string.Empty : y.GetValue(x, null)).ToString().ToLower().Contains(Keyword.ToLower())));
                //resultList = resultList.Where(x => x.GetType().GetProperties().Any(y => y.GetValue(x, null).ToString().ToLower().Contains(Keyword.ToLower()))).ToList();

                //errorList.FirstOrDefault().GetType().GetProperties().Any(x => x.GetValue(errorList.FirstOrDefault()).ToString())
            }

            if (Status0 == true)
            {
                resultList = resultList.Where(x => x.IsInCms);
            }


            ViewBag.Keyword = Keyword;
            ViewBag.UserId = UserId;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Status0 = Status0;
            ViewBag.ErrorCount = resultList.Count();

            int pageNum = (page == null) ? 1 : Convert.ToInt32(page);


            return View(resultList.ToPagedList(pageNum, 25));

        }
    }
}
