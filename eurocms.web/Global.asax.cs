using EuroCMS.Hangfire;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
//using EuroCMS.Plugin.Skylife.Scheduling; // ÖENEMLİ: BU SATIR SKYLIFE PROJESİ DIŞINDAKİ DİĞER TÜM PROJELER İÇİN SİLİNMELİDİR!

namespace EuroCMS.FrontEnd
{
    public class Global : EuroCMS.Web.CmsApplication
    {
        //protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Headers.Set("Server", "");
        //    HttpContext.Current.Response.Headers.Add("Set-Cookie", "HttpOnly;Secure;SameSite=Strict");

        //}
        //void Session_Start(object sender, EventArgs e)
        //{
        //    if (Request.Cookies.Count > 0)
        //        foreach (string cookie in Request.Cookies.AllKeys)
        //        {
        //            Response.Cookies[cookie].Secure = true;
        //        }
        //}


        // Skylife otomatik veri çekimi için Application_Start event'inin comment'i kaldırıldı. Normalde Application_Start event'i comment halinde olmalı!
        // Yukarıdaki using EuroCMS.Plugin.Skylife.Scheduling; kodu Skylife dışındaki projeler için silinmeli.
        void Application_Start(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsServiceRefresh"]) && ConfigurationManager.AppSettings["IsServiceRefresh"].Trim().ToLower() == "on")
            //{
            //    //Database.SetInitializer<CmsDbContext>(null);
            //    SchedulerWrapper scheduler = new SchedulerWrapper();
            //    scheduler.RunJob();
            //}

            HangfireAspNet.Use(GetHangfireServers);

            if (ConfigurationManager.AppSettings["IsHangfireActive"] != null && bool.Parse(ConfigurationManager.AppSettings["IsHangfireActive"]))
            {
                var classes = ConfigurationManager.AppSettings["HangfireClasses"].ToString().Trim().Split(',');
                var cronExpressions = ConfigurationManager.AppSettings["HangfireCronExpressions"].ToString().Replace("|", "").Split(',');

                for (int i = 0; i < classes.Length; i++)
                {
                    var className = classes[i];
                    var cronExpression = cronExpressions[i];
                    var instance = (IHangfireJob)GetInstance(className);
                    RecurringJob.AddOrUpdate(() => instance.Run(), cronExpression, TimeZoneInfo.Local);
                }
            }
        }

        private object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }

        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage();

            yield return new BackgroundJobServer();
        }
    }
}
