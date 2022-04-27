using EuroCMS.Admin.Common;
using EuroCMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eurocms.mvc.Controllers
{
    public class LogController : BaseController
    {
        //
        // GET: /Log/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Log/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Log/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Log/Create

        [HttpPost]
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
        // GET: /Log/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Log/Edit/5

        [HttpPost]
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
        // GET: /Log/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Log/Delete/5

        [HttpPost]
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
