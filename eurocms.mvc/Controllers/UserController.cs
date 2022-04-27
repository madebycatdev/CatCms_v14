using EuroCMS.Admin.Common;
using EuroCMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eurocms.mvc.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        [CmsAuthorize(Permission = "View", ContentType = "User")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5

        [CmsAuthorize(Permission = "View", ContentType = "User")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create

        [CmsAuthorize(Permission = "Create", ContentType = "User")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [CmsAuthorize(Permission = "Create", ContentType = "User")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                return View();
            }
        }

        //
        // GET: /User/Edit/5

        [CmsAuthorize(Permission = "Edit", ContentType = "User")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [CmsAuthorize(Permission = "Edit", ContentType = "User")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                return View();
            }
        }
    }
}
