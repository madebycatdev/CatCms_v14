using EuroCMS.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.Admin.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ForbiddenAccess()
        {
            //HttpContext.Response.StatusCode = 403;
            return View();
        } 
    }
}
