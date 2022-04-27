using EuroCMS.Admin.entity;
using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;
using EuroCMS.Model;
using Newtonsoft.Json;

namespace EuroCMS.Admin.Controllers
{
    public class LanguageController : BaseController
    {
        LanguageDbContext context = new LanguageDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "Language")]
        public ActionResult Index()
        {
            var domains = context.SelectLanguages();
            return View(domains);
        }

        [CmsAuthorize(Permission = "List", ContentType = "Language")]
        public ActionResult List()
        {
            TempData.Clear();
            var domains = context.SelectLanguages();
            return Json(domains, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "View", ContentType = "Language")]
        public ActionResult Details(string id)
        {
            TempData.Clear();
            var result = context.SelectLanguage(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "Create", ContentType = "Language")]
        public ActionResult Create()
        {
            TempData.Clear();
            return View();
        }
		
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "Language")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LANGUAGE_CREATE, this));

            try
            {
                cms_languages language = new cms_languages();

                language.lang_id = HttpUtility.HtmlEncode(collection["lang_id"]);
                language.lang_name = HttpUtility.HtmlEncode(collection["language"]);
                language.lang_xml = HttpUtility.HtmlEncode(collection["lang_xml"]);
                language.lang_order = Convert.ToInt32(collection["lang_order"]);
                // language.publisher_id = Convert.ToInt32(Session["publisher_id"]);
                language.publisher_id = Membership.GetUser().ProviderUserKey;
                #region Alias
                string alias = collection["language_alias"];
                if (string.IsNullOrEmpty(alias))
                {
                    alias = CheckAlias(collection["language_name"], "-1");
                }

                language.lang_alias = alias;
                #endregion

                var result = context.CreateOrUpdateLanguage(language);
                switch (result[0].lStat)
                {
                    case "U":
                        TempData["Message"] = "Your language has been successfully updated.";
                        break;
                    default:
                        TempData["Message"] = "Your language has been successfully created.";
                        break;
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "Language")]
        public ActionResult Edit(string id)
        {
            TempData.Clear();
            var result = context.SelectLanguage(id);

            if (string.IsNullOrEmpty(result[0].lang_alias))
            {
                result[0].lang_alias = CheckAlias(result[0].lang_name, id.ToString());
                result[0].lang_alias = JsonConvert.DeserializeObject(result[0].lang_alias).ToString();
            }

            return View(result[0]);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "Language")]
        public ActionResult Edit(string id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LANGUAGE_EDIT, this));

            try
            {
                cms_languages language = new cms_languages();
                language.lang_id = HttpUtility.HtmlEncode(collection["lang_id"]);
                language.lang_name = collection["language"];
                language.lang_xml = HttpUtility.HtmlEncode(collection["lang_xml"]);
                language.lang_order = Convert.ToInt32(collection["lang_order"]);
                language.publisher_id = Membership.GetUser().ProviderUserKey;
                #region Alias
                string alias = collection["language_alias"];
                if (string.IsNullOrEmpty(alias))
                {
                    alias = CheckAlias(collection["language_name"], id);
                }

                language.lang_alias = alias;
                #endregion

                var result = context.CreateOrUpdateLanguage(language);
                switch (result[0].lStat)
                {
                    case "U":
                        TempData["Message"] = "Your language has been successfully updated.";
                        break;
                    default:
                        TempData["Message"] = "Your language has been successfully created.";
                        break;
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(string id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LANGUAGE_DELETE, this));

            try
            {
                var result = context.DeleteLanguage(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
                switch (result[0].rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "1":
                        throw new ApplicationException("This language was not found OR already deleted.");
                    case "2":
                        throw new ApplicationException("This language is used on article relations and can not be deleted" + result.Select(m => m.found_name).InHtmlNewLine());
                    case "3":
                        throw new ApplicationException("There are some articles that using this language. Language can not deleted." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "4":
                        throw new ApplicationException("There is no any other language on CMS sytem. You can not delete this language.");
                    case "5":
                        throw new ApplicationException("You do not have access to delete this language. Also you are logged!");
                    default:
                        throw new ApplicationException("unexpected error!");
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [CmsAuthorize()]
        public string CheckAlias(string text, string id)
        {
            string result = string.Empty;
            text = text.Trim();

            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    string cleanText = CmsHelper.StringToAlphaNumeric(text, false);
                    CmsDbContext dbContext = new CmsDbContext();
                    List<Language> langs = dbContext.Languages.Where(x => x.Alias == cleanText).ToList();

                    langs.Remove(langs.Where(x => x.ID == id).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                    if (langs == null || langs.Count == 0)
                    {
                        //ok 
                        result = cleanText;
                    }
                    else
                    {
                        //çakışma var
                        int counter = 2;
                        while (dbContext.Languages.Where(x => x.Alias == cleanText + "-" + counter).ToList().Count > 0)
                        {
                            counter++;
                        }
                        //son - cleanText + "-" + counter
                        result = cleanText + "-" + counter;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "_#NOK#_";
                CmsHelper.SaveErrorLog(ex, "Can't create language alias", true);
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
