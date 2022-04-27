using EuroCMS.Plugin.StandardProfil.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;

namespace EuroCMS.Plugin.StandardProfil
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string result = "", plugin = "";

            if (!string.IsNullOrEmpty(context.Request.Form["plugin"]))
            {
                plugin = context.Request.Form["plugin"].ToLower().Trim();
            }
            else
            {
                result = JsonConvert.SerializeObject(new { status = false, message = "plugin alanı boş gönderilemez", data = "" });
            }

            switch (plugin)
            {
                case "process":
                    result = process(context);
                    break;
            }

            context.Response.Write(result);
        }

        public string process(HttpContext context)
        {
            string phase = "", result = "", selectedCountryCode = "", selectedCountryName = ""; 

            try
            {
                using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                {
                    var currentIp = Core.CmsHelper.GetCurrentIP();
                    var request = (HttpWebRequest)WebRequest.Create("https://api.ipstack.com/" + currentIp + "?access_key=2496046eb0860665f10a26e38c0aea0e");
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    IpStackModel ipStack = JsonConvert.DeserializeObject<IpStackModel>(responseString);

                    if (!string.IsNullOrEmpty(context.Request.Form["phase"]))
                    {
                        phase = context.Request.Form["phase"].Trim();
                    }
                    else
                    {
                        context.Response.Write(JsonConvert.SerializeObject(new { status = false, message = "phase alanı boş gönderilemez", data = "" }));
                        context.Response.End();
                    }

                    switch (phase)
                    {
                        case "login":
                            if (!string.IsNullOrEmpty(context.Request.Form["selectedCountryCode"]))
                            {
                                selectedCountryCode = context.Request.Form["selectedCountryCode"].Trim();
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { status = false, message = "selectedCountryCode alanı boş gönderilemez", data = "" });
                            }

                            if (!string.IsNullOrEmpty(context.Request.Form["selectedCountryName"]))
                            {
                                selectedCountryName = context.Request.Form["selectedCountryName"].Trim();
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { status = false, message = "selectedCountryName alanı boş gönderilemez", data = "" });
                            }

                            //using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                            //{
                            var checkCountry = dbContext.CountryLists.FirstOrDefault(f => f.CountryCode == selectedCountryCode);
                            if (checkCountry != null)
                            {
                                var traceLog = new TraceLog();
                                traceLog.IpAddress = (string.IsNullOrEmpty(ipStack.ip) ? "" : ipStack.ip);
                                traceLog.CountryCode = (string.IsNullOrEmpty(ipStack.country_code) ? "" : ipStack.country_code);
                                traceLog.CountryName = (string.IsNullOrEmpty(ipStack.country_name) ? "" : ipStack.country_name);
                                traceLog.SelectedCountryCode = selectedCountryCode;
                                traceLog.SelectedCountryName = selectedCountryName;
                                traceLog.Type = (int)CaseType.restricted;
                                traceLog.CreateDate = DateTime.Now;

                                dbContext.TraceLogs.Add(traceLog);
                                if (dbContext.SaveChanges() > 0)
                                {
                                    return JsonConvert.SerializeObject(new { status = true, message = "", data = Enum.GetName(typeof(CaseType), CaseType.restricted) });
                                }
                            }
                            else if (selectedCountryCode == "US")
                            {
                                return JsonConvert.SerializeObject(new { status = true, message = "", data = Enum.GetName(typeof(CaseType), CaseType.usa) });
                            }
                            else if (checkCountry == null)
                            {
                                return JsonConvert.SerializeObject(new { status = true, message = "", data = Enum.GetName(typeof(CaseType), CaseType.authorized) });
                            }
                            //}
                            break;

                        case "security":
                            string casetype = "";

                            if (!string.IsNullOrEmpty(context.Request.Form["casetype"]))
                            {
                                casetype = context.Request.Form["casetype"].Trim();
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { status = false, message = "casetype alanı boş gönderilemez", data = "" });
                            }

                            if (!string.IsNullOrEmpty(context.Request.Form["selectedCountryCode"]))
                            {
                                selectedCountryCode = context.Request.Form["selectedCountryCode"].Trim();
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { status = false, message = "selectedCountryCode alanı boş gönderilemez", data = "" });
                            }

                            if (!string.IsNullOrEmpty(context.Request.Form["selectedCountryName"]))
                            {
                                selectedCountryName = context.Request.Form["selectedCountryName"].Trim();
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { status = false, message = "selectedCountryName alanı boş gönderilemez", data = "" });
                            }

                            string redirectUrl = "/";
                            //using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                            //{
                            var checkRedirect = context.Session["returnUrl"];
                            if (checkRedirect != null)
                            {
                                redirectUrl = checkRedirect.ToString();
                            }
                            else
                            {
                                var pageRedirect = dbContext.CmsConfigs.FirstOrDefault(f => f.Name == "PageRedirect");
                                if (pageRedirect != null)
                                {
                                    if (!string.IsNullOrEmpty(pageRedirect.RemoteValue))
                                    {
                                        redirectUrl = pageRedirect.RemoteValue;
                                    }
                                }
                            }
                            //}

                            switch (casetype)
                            {
                                case "usa":
                                    string name = "", nameOfQib = "", email = "";
                                    bool permission = false;

                                    if (!string.IsNullOrEmpty(context.Request.Form["name"]))
                                    {
                                        name = context.Request.Form["name"].Trim();
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new { status = false, message = "name alanı boş gönderilemez", data = "" });
                                    }

                                    if (!string.IsNullOrEmpty(context.Request.Form["nameOfQib"]))
                                    {
                                        nameOfQib = context.Request.Form["nameOfQib"].Trim();
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new { status = false, message = "nameOfQib alanı boş gönderilemez", data = "" });
                                    }

                                    if (!string.IsNullOrEmpty(context.Request.Form["email"]))
                                    {
                                        email = context.Request.Form["email"].Trim();
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new { status = false, message = "email alanı boş gönderilemez", data = "" });
                                    }

                                    if (!string.IsNullOrEmpty(context.Request.Form["permission"]))
                                    {
                                        permission = Convert.ToBoolean(context.Request.Form["permission"].Trim());
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new { status = false, message = "permission alanı boş gönderilemez", data = "" });
                                    }

                                    //using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                                    //{
                                    var traceLog = new TraceLog();
                                    traceLog.IpAddress = (string.IsNullOrEmpty(ipStack.ip) ? "" : ipStack.ip);
                                    traceLog.CountryCode = (string.IsNullOrEmpty(ipStack.country_code) ? "" : ipStack.country_code);
                                    traceLog.CountryName = (string.IsNullOrEmpty(ipStack.country_name) ? "" : ipStack.country_name);
                                    traceLog.SelectedCountryCode = selectedCountryCode;
                                    traceLog.SelectedCountryName = selectedCountryName;
                                    traceLog.Type = (int)CaseType.usa;
                                    traceLog.Permission = permission;
                                    traceLog.CreateDate = DateTime.Now;

                                    dbContext.TraceLogs.Add(traceLog);
                                    if (dbContext.SaveChanges() > 0)
                                    {
                                        if (permission)
                                        {
                                            HttpContext.Current.Session["SpSecurityCheck"] = true;
                                            return JsonConvert.SerializeObject(new { status = true, message = "", data = redirectUrl });
                                        }
                                        else
                                        {
                                            return JsonConvert.SerializeObject(new { status = true, message = "", data = Enum.GetName(typeof(CaseType), CaseType.restricted) });
                                        }
                                    }
                                    //}
                                    break;

                                case "authorized":

                                    if (!string.IsNullOrEmpty(context.Request.Form["permission"]))
                                    {
                                        permission = Convert.ToBoolean(context.Request.Form["permission"].Trim());
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new { status = false, message = "permission alanı boş gönderilemez", data = "" });
                                    }

                                    //using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
                                    //{
                                    traceLog = new TraceLog();
                                    traceLog.IpAddress = (string.IsNullOrEmpty(ipStack.ip) ? "" : ipStack.ip);
                                    traceLog.CountryCode = (string.IsNullOrEmpty(ipStack.country_code) ? "" : ipStack.country_code);
                                    traceLog.CountryName = (string.IsNullOrEmpty(ipStack.country_name) ? "" : ipStack.country_name);
                                    traceLog.SelectedCountryCode = selectedCountryCode;
                                    traceLog.SelectedCountryName = selectedCountryName;
                                    traceLog.Type = (int)CaseType.authorized;
                                    traceLog.Permission = permission;
                                    traceLog.CreateDate = DateTime.Now;

                                    dbContext.TraceLogs.Add(traceLog);
                                    if (dbContext.SaveChanges() > 0)
                                    {
                                        if (permission)
                                        {
                                            HttpContext.Current.Session["SpSecurityCheck"] = true;
                                            return JsonConvert.SerializeObject(new { status = true, message = "", data = redirectUrl });
                                        }
                                        else
                                        {
                                            return JsonConvert.SerializeObject(new { status = true, message = "", data = Enum.GetName(typeof(CaseType), CaseType.restricted) });
                                        }
                                    }
                                    //}
                                    break;
                            }
                            break;

                        default:
                            return JsonConvert.SerializeObject(new { status = false, message = "geçersiz phase", data = "" });
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return JsonConvert.SerializeObject(new { status = false, message = ex.Message , data = "" });
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}