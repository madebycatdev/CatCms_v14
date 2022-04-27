using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.Admin.Controllers
{
    public class AddObjectsController : BaseController
    {
        //
        // GET: /AddObjects/
        PortletDbContext context = new PortletDbContext();
        BreadCrumbDbContext contextB = new BreadCrumbDbContext();
        [CmsAuthorize(Permission = "View")]
        public ActionResult Index()
        {
            CmsDbContext dbContext = new CmsDbContext();
            var result = context.SelectAll(-1);
            ViewData["BreadCrumb"] = contextB.SelectBreadCrumbs();
            ViewData["Splash"] = dbContext.Splashes.Where(s => s.Status == 1).ToList();
            return View(result);
        }
    }
}
