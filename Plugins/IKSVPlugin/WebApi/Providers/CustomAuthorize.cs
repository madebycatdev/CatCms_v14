using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EuroCMS.Plugin.IKSV.Providers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            const string value = "8bc61a7f-97bd-4479-90d3-70f052d3a8f9";
            AuthenticationHeaderValue authenticationHeaderValue = actionContext.Request.Headers.Authorization;
            bool isAuthorized = false;
            if (authenticationHeaderValue == null)
            {
                return isAuthorized;
            }

            if (authenticationHeaderValue.Scheme != "Bearer")
            {
                return isAuthorized;
            }

            if (authenticationHeaderValue.Parameter == value)
            {
                isAuthorized = true;
            }

            return isAuthorized;
        }
    }
}