using System.Configuration;
using System.Web.Mvc;

namespace EuroCMS.Admin.Areas.cms
{
    public class cmsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return ConfigurationManager.AppSettings["EuroCMS.AreaName"].ToString();
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "cms_default",
                "cms/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
