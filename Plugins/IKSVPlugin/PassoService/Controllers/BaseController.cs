using PassoService.Helpers;
using PassoService.Models;
using PassoService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PassoService.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            ErrorsLog.WriteLog("ERROR : " + filterContext.HttpContext.Request.Url.ToString(), filterContext.Exception.StackTrace);
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Error" }
                });

            filterContext.ExceptionHandled = true;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentSession.AccessToken == null)
                CurrentSession.AccessToken = PassoApiService.GetToken();


            if (CurrentSession.Products == null)
            {
                var products = PassoApiService.GetProductList();

                if (products != null && products.result)
                    products.productList.Reverse();

                CurrentSession.Products = products != null && products.result ? products.productList : null;
            }


        }
    }
}