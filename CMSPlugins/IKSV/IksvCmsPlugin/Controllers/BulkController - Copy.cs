using EuroCMS.Admin.Common;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using EuroCMS.Core;
using EuroCMS.CMSPlugin.IKSV.Models;
using NPOI.SS.UserModel;
using System.IO;
using EuroCMS.Admin.Models;

namespace EuroCMS.CMSPlugin.IKSV.Controllers
{
    public class BulkController : BaseController
    {
        int etkinlikClassification = 4;
        int etkinlikProgramClassification = 5;
        // GET: Bulk
        public ActionResult Index()
        {
            return View("~/Views/CMSPlugins/IKSV/Bulk/Index.cshtml");
        }

        public ActionResult dataimport()
        {
            return View("~/Views/CMSPlugins/IKSV/Bulk/dataimport.cshtml");
        }

        [HttpPost]
        public ActionResult dataimport(FormCollection collection)
        {

            HttpPostedFileBase file = Request.Files["upload"];
            if (file != null && file.ContentLength > 0)
            {
                string fileName = file.FileName.Split('.')[0];
                string extension = file.FileName.Split('.')[1];

                try
                {
                    #region Save File
                    string path = Server.MapPath("/i/content") + "//" + fileName + " - " + DateTime.Now.ToLongDateString() + "." + extension;
                    Request.Files["upload"].SaveAs(path);
                    #endregion

                    #region Get Datatable From Excel

                    Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                    CmsDbContext dbContext = new CmsDbContext();

                    List<DataImportModel> dataList = new List<DataImportModel>();

                    IWorkbook wb = WorkbookFactory.Create(new FileStream(
                               Path.GetFullPath(path),
                               FileMode.Open, FileAccess.Read,
                               FileShare.ReadWrite));

                    ISheet ws = wb.GetSheetAt(0);

                    for (int row = 1; row <= ws.LastRowNum; row++)
                    {
                        var importData = new DataImportModel
                        {
                            Id = (ws.GetRow(row).GetCell(0) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(0).NumericCellValue)),
                            ParentId = (ws.GetRow(row).GetCell(1) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(1).NumericCellValue)),
                            ClassificationId = (ws.GetRow(row).GetCell(2) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(2).NumericCellValue)),
                            Lang = (ws.GetRow(row).GetCell(3) == null ? "" : ws.GetRow(row).GetCell(3).StringCellValue),
                            Headline = (ws.GetRow(row).GetCell(4) == null ? "" : ws.GetRow(row).GetCell(4).StringCellValue),
                            Summary = (ws.GetRow(row).GetCell(5) == null ? "" : ws.GetRow(row).GetCell(5).StringCellValue),
                            Content_1 = (ws.GetRow(row).GetCell(6) == null ? "" : ws.GetRow(row).GetCell(6).StringCellValue),
                            Content_2 = (ws.GetRow(row).GetCell(7) == null ? "" : ws.GetRow(row).GetCell(7).StringCellValue),
                            Content_3 = (ws.GetRow(row).GetCell(8) == null ? "" : ws.GetRow(row).GetCell(8).StringCellValue),
                            Content_4 = (ws.GetRow(row).GetCell(9) == null ? "" : ws.GetRow(row).GetCell(9).StringCellValue),
                            Content_5 = (ws.GetRow(row).GetCell(10) == null ? "" : ws.GetRow(row).GetCell(10).StringCellValue),
                            Custom_1 = (ws.GetRow(row).GetCell(11) == null ? "" : ws.GetRow(row).GetCell(11).StringCellValue),
                            Custom_2 = (ws.GetRow(row).GetCell(12) == null ? "" : ws.GetRow(row).GetCell(12).StringCellValue),
                            Custom_3 = (ws.GetRow(row).GetCell(13) == null ? "" : ws.GetRow(row).GetCell(13).StringCellValue),
                            Custom_4 = (ws.GetRow(row).GetCell(14) == null ? "" : ws.GetRow(row).GetCell(14).StringCellValue),
                            Custom_5 = (ws.GetRow(row).GetCell(15) == null ? "" : ws.GetRow(row).GetCell(15).StringCellValue),
                            Custom_6 = (ws.GetRow(row).GetCell(16) == null ? "" : ws.GetRow(row).GetCell(16).StringCellValue),
                            Custom_7 = (ws.GetRow(row).GetCell(17) == null ? "" : ws.GetRow(row).GetCell(17).StringCellValue),
                            Custom_8 = (ws.GetRow(row).GetCell(18) == null ? "" : ws.GetRow(row).GetCell(18).StringCellValue),
                            Custom_9 = (ws.GetRow(row).GetCell(19) == null ? "" : ws.GetRow(row).GetCell(19).StringCellValue),
                            Custom_10 = (ws.GetRow(row).GetCell(20) == null ? "" : ws.GetRow(row).GetCell(20).StringCellValue),
                            Tags = (ws.GetRow(row).GetCell(21) == null ? "" : ws.GetRow(row).GetCell(21).StringCellValue),
                            ZoneId = (ws.GetRow(row).GetCell(23) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(23).NumericCellValue)),
                            Order = (ws.GetRow(row).GetCell(24) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(24).NumericCellValue))
                        };

                        if (ws.GetRow(row).GetCell(22) != null)
                        {
                            importData.Date_1 = ws.GetRow(row).GetCell(22).StringCellValue;
                        }

                        dataList.Add(importData);
                    }

                    //classification id etkinlik ve parent id sıfır olanlar eklenecek (güncellemesi yapılmadı)
                    var events = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == "TR").ToList();

                    foreach (var item in events)
                    {
                        var zone = dbContext.Zones.FirstOrDefault(f => f.Id == item.ZoneId);
                        if (zone != null)
                        {
                            var article = SaveArticleData(item);

                            if (article != null)
                            {
                                var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.Id == item.Id && w.Lang == item.Lang).ToList();
                                foreach (var itemProgram in eventsProgram)
                                {
                                    itemProgram.Custom_10 = article.Id.ToString();
                                    var programArticle = SaveArticleData(itemProgram);
                                }

                                //ingilizce data çekildi
                                var getEnDataFromExcel = dataList.FirstOrDefault(f => f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
                                if (getEnDataFromExcel != null)
                                {
                                    var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
                                    if (zoneEn != null)
                                    {
                                        //ingilizce data kayıt edildi.
                                        var enData = SaveArticleData(getEnDataFromExcel);
                                        if (enData != null)
                                        {
                                            //ingilizce data program 
                                            var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.Id == getEnDataFromExcel.Id && w.Lang == getEnDataFromExcel.Lang).ToList();
                                            foreach (var itemProgram in eventsProgramEn)
                                            {
                                                itemProgram.Custom_10 = enData.Id.ToString();
                                                var programArticleEn = SaveArticleData(itemProgram);
                                            }

                                            //trdata revizyon cekiliyor
                                            var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
                                            if (getTrRev != null)
                                            {
                                                int langRelId = 1;

                                                var lastLang = dbContext.LanguageRelations.OrderBy(o => o.LanguageRelationID).ToList().LastOrDefault();
                                                if (lastLang != null)
                                                {
                                                    langRelId = Convert.ToInt32(lastLang.LanguageRelationID) + 1;
                                                }

                                                //turkce ingilizceye eşitleniyor
                                                var langRelFL = new LanguageRelation();
                                                langRelFL.LanguageRelationID = langRelId;
                                                langRelFL.ArticleID = article.Id;
                                                langRelFL.ZoneID = item.ZoneId;
                                                dbContext.LanguageRelations.Add(langRelFL);
                                                if (dbContext.SaveChanges() > 0)
                                                {
                                                    //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
                                                    var langRelFLRev = new LanguageRelationRevision();
                                                    langRelFLRev.LanguageRelationID = langRelId;
                                                    langRelFLRev.ArticleID = article.Id;
                                                    langRelFLRev.ZoneID = item.ZoneId;
                                                    langRelFLRev.RevisionID = getTrRev.RevisionId;
                                                    dbContext.LanguageRelationRevisions.Add(langRelFLRev);

                                                    if (dbContext.SaveChanges() > 0)
                                                    {
                                                        //endata revizyon cekiliyor
                                                        var getEnRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == enData.Id && f.RevisionStatus == "L");
                                                        if (getEnRev != null)
                                                        {
                                                            //ingilzce turkceye eşitleniyor
                                                            var langRelLF = new LanguageRelation();
                                                            langRelLF.LanguageRelationID = langRelId;
                                                            langRelLF.ArticleID = enData.Id;
                                                            langRelLF.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
                                                            dbContext.LanguageRelations.Add(langRelLF);
                                                            if (dbContext.SaveChanges() > 0)
                                                            {
                                                                //ingilzce turkceye eşitleniyor revision kaydı yapılıyor.
                                                                var langRelLFRev = new LanguageRelationRevision();
                                                                langRelLFRev.LanguageRelationID = langRelId;
                                                                langRelLFRev.ArticleID = enData.Id;
                                                                langRelLFRev.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
                                                                langRelLFRev.RevisionID = getEnRev.RevisionId;
                                                                dbContext.LanguageRelationRevisions.Add(langRelLFRev);
                                                                dbContext.SaveChanges();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TempData["HasError"] = true;
                                        TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + getEnDataFromExcel.ZoneId;
                                    }
                                }

                            }
                        }
                        else
                        {
                            TempData["HasError"] = true;
                            TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + item.ZoneId;
                        }
                    }

                    TempData["Message"] = "Bulk Import Completed";
                    #endregion
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;
                }
            }

            return View("~/Views/CMSPlugins/IKSV/Bulk/dataimport.cshtml");
        }












        //public ActionResult dataimport(FormCollection collection)
        //{

        //    HttpPostedFileBase file = Request.Files["upload"];
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        string fileName = file.FileName.Split('.')[0];
        //        string extension = file.FileName.Split('.')[1];

        //        try
        //        {
        //            #region Save File
        //            string path = Server.MapPath("/i/content") + "//" + fileName + " - " + DateTime.Now.ToLongDateString() + "." + extension;
        //            Request.Files["upload"].SaveAs(path);
        //            #endregion

        //            #region Get Datatable From Excel

        //            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
        //            CmsDbContext dbContext = new CmsDbContext();

        //            List<DataImportModel> dataList = new List<DataImportModel>();

        //            IWorkbook wb = WorkbookFactory.Create(new FileStream(
        //                       Path.GetFullPath(path),
        //                       FileMode.Open, FileAccess.Read,
        //                       FileShare.ReadWrite));

        //            ISheet ws = wb.GetSheetAt(0);

        //            for (int row = 1; row <= ws.LastRowNum; row++)
        //            {
        //                var importData = new DataImportModel
        //                {
        //                    Id = (ws.GetRow(row).GetCell(0) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(0).NumericCellValue)),
        //                    ClassificationId = (ws.GetRow(row).GetCell(1) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(1).NumericCellValue)),
        //                    Lang = (ws.GetRow(row).GetCell(2) == null ? "" : ws.GetRow(row).GetCell(2).StringCellValue),
        //                    Headline = (ws.GetRow(row).GetCell(3) == null ? "" : ws.GetRow(row).GetCell(3).StringCellValue),
        //                    Summary = (ws.GetRow(row).GetCell(4) == null ? "" : ws.GetRow(row).GetCell(4).StringCellValue),
        //                    Content_1 = (ws.GetRow(row).GetCell(5) == null ? "" : ws.GetRow(row).GetCell(5).StringCellValue),
        //                    Content_2 = (ws.GetRow(row).GetCell(6) == null ? "" : ws.GetRow(row).GetCell(6).StringCellValue),
        //                    Content_3 = (ws.GetRow(row).GetCell(7) == null ? "" : ws.GetRow(row).GetCell(7).StringCellValue),
        //                    Content_4 = (ws.GetRow(row).GetCell(8) == null ? "" : ws.GetRow(row).GetCell(8).StringCellValue),
        //                    Content_5 = (ws.GetRow(row).GetCell(9) == null ? "" : ws.GetRow(row).GetCell(9).StringCellValue),
        //                    Custom_1 = (ws.GetRow(row).GetCell(10) == null ? "" : ws.GetRow(row).GetCell(10).StringCellValue),
        //                    Custom_2 = (ws.GetRow(row).GetCell(11) == null ? "" : ws.GetRow(row).GetCell(11).StringCellValue),
        //                    Custom_3 = (ws.GetRow(row).GetCell(12) == null ? "" : ws.GetRow(row).GetCell(12).StringCellValue),
        //                    Custom_4 = (ws.GetRow(row).GetCell(13) == null ? "" : ws.GetRow(row).GetCell(13).StringCellValue),
        //                    Custom_5 = (ws.GetRow(row).GetCell(14) == null ? "" : ws.GetRow(row).GetCell(14).StringCellValue),
        //                    Custom_6 = (ws.GetRow(row).GetCell(15) == null ? "" : ws.GetRow(row).GetCell(15).StringCellValue),
        //                    Custom_7 = (ws.GetRow(row).GetCell(16) == null ? "" : ws.GetRow(row).GetCell(16).StringCellValue),
        //                    Custom_8 = (ws.GetRow(row).GetCell(17) == null ? "" : ws.GetRow(row).GetCell(17).StringCellValue),
        //                    Custom_9 = (ws.GetRow(row).GetCell(18) == null ? "" : ws.GetRow(row).GetCell(18).StringCellValue),
        //                    Custom_10 = (ws.GetRow(row).GetCell(19) == null ? "" : ws.GetRow(row).GetCell(19).StringCellValue),
        //                    Tags = (ws.GetRow(row).GetCell(20) == null ? "" : ws.GetRow(row).GetCell(20).StringCellValue),
        //                    ZoneId = (ws.GetRow(row).GetCell(22) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(22).NumericCellValue))
        //                };

        //                if (ws.GetRow(row).GetCell(21) != null)
        //                {
        //                    importData.Date_1 = ws.GetRow(row).GetCell(21).StringCellValue;
        //                }

        //                dataList.Add(importData);
        //            }

        //            var events = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == "TR").ToList();

        //            foreach (var item in events)
        //            {
        //                var zone = dbContext.Zones.FirstOrDefault(f => f.Id == item.ZoneId);
        //                if (zone != null)
        //                {
        //                    #region subZoneKayıt

        //                    var articleSubZone = new Zone();
        //                    string zoneName = "Etkinlik Program: " + item.Headline;
        //                    articleSubZone = new Zone();
        //                    articleSubZone.ZoneGroupId = zone.ZoneGroupId;
        //                    articleSubZone.ZoneTypeId = 0;
        //                    articleSubZone.Status = "A";
        //                    articleSubZone.Name = (zoneName.Length > 100 ? zoneName.Substring(0, 98) : zoneName);
        //                    articleSubZone.CssMerge = 0;
        //                    articleSubZone.CssId = 0;
        //                    articleSubZone.MobileCssId = 0;
        //                    articleSubZone.PrintCssId = 0;
        //                    articleSubZone.TemplateId = 1;
        //                    articleSubZone.MobileTemplateId = 0;
        //                    articleSubZone.CreatedBy = currentUserId;
        //                    articleSubZone.Created = DateTime.Now;
        //                    articleSubZone.Updated = DateTime.Now;
        //                    articleSubZone.Append1 = 0;
        //                    articleSubZone.Append2 = 0;
        //                    articleSubZone.Append3 = 0;
        //                    articleSubZone.Append4 = 0;
        //                    articleSubZone.Append5 = 0;
        //                    articleSubZone.LangId = zone.LangId;
        //                    articleSubZone.Alias = CmsHelper.StringToAlphaNumeric(articleSubZone.Name, false);

        //                    dbContext.Zones.Add(articleSubZone);
        //                    if (dbContext.SaveChanges() > 0)
        //                    {
        //                        //zone revision kayıt
        //                        ZoneRevision articleSubZoneRevision = new ZoneRevision();
        //                        articleSubZoneRevision.CreatedBy = currentUserId;
        //                        articleSubZoneRevision.RevisionDate = DateTime.Now;
        //                        articleSubZoneRevision.RevisionStatus = "L";
        //                        articleSubZoneRevision.RevisedBy = currentUserId;
        //                        articleSubZoneRevision.Approved = DateTime.Now;
        //                        articleSubZoneRevision.ApprovedBy = currentUserId;
        //                        articleSubZoneRevision.ZoneId = articleSubZone.Id;
        //                        articleSubZoneRevision.ZoneGroupId = articleSubZone.ZoneGroupId;
        //                        articleSubZoneRevision.ZoneTypeId = 0;
        //                        articleSubZoneRevision.ZoneStatus = "A";
        //                        articleSubZoneRevision.Name = articleSubZone.Name;
        //                        articleSubZoneRevision.CssMerge = 0;
        //                        articleSubZoneRevision.CssId = 0;
        //                        articleSubZoneRevision.MobileCssId = 0;
        //                        articleSubZoneRevision.PrintCssId = 0;
        //                        articleSubZoneRevision.TemplateId = 1;
        //                        articleSubZoneRevision.MobileTemplateId = 0;
        //                        articleSubZoneRevision.Append1 = 0;
        //                        articleSubZoneRevision.Append2 = 0;
        //                        articleSubZoneRevision.Append3 = 0;
        //                        articleSubZoneRevision.Append4 = 0;
        //                        articleSubZoneRevision.Append5 = 0;
        //                        articleSubZoneRevision.ContentEditorType1 = "H";
        //                        articleSubZoneRevision.ContentEditorType2 = "H";
        //                        articleSubZoneRevision.ContentEditorType3 = "H";
        //                        articleSubZoneRevision.ContentEditorType4 = "H";
        //                        articleSubZoneRevision.ContentEditorType5 = "H";
        //                        articleSubZoneRevision.LangId = articleSubZone.LangId;
        //                        articleSubZoneRevision.Alias = articleSubZone.Alias;

        //                        dbContext.ZoneRevisions.Add(articleSubZoneRevision);
        //                        if (dbContext.SaveChanges() > 0)
        //                        {
        //                            item.NavigationZoneId = articleSubZone.Id;
        //                        }

        //                    }
        //                    #endregion

        //                    var article = SaveArticleData(item);

        //                    if (article != null)
        //                    {
        //                        var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.Id == item.Id && w.Lang == item.Lang).ToList();
        //                        foreach (var itemProgram in eventsProgram)
        //                        {
        //                            itemProgram.ZoneId = item.NavigationZoneId;
        //                            var programArticle = SaveArticleData(itemProgram);
        //                        }


        //                        //ingilizce data çekildi
        //                        var getEnDataFromExcel = dataList.FirstOrDefault(f => f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
        //                        if (getEnDataFromExcel != null)
        //                        {

        //                            var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
        //                            if (zoneEn != null)
        //                            {
        //                                #region subZoneKayıt

        //                                var articleSubZoneEn = new Zone();
        //                                string zoneNameEn = "Etkinlik Program: " + getEnDataFromExcel.Headline;
        //                                articleSubZone = new Zone();
        //                                articleSubZone.ZoneGroupId = zoneEn.ZoneGroupId;
        //                                articleSubZone.ZoneTypeId = 0;
        //                                articleSubZone.Status = "A";
        //                                articleSubZone.Name = (zoneName.Length > 100 ? zoneName.Substring(0, 98) : zoneName);
        //                                articleSubZone.CssMerge = 0;
        //                                articleSubZone.CssId = 0;
        //                                articleSubZone.MobileCssId = 0;
        //                                articleSubZone.PrintCssId = 0;
        //                                articleSubZone.TemplateId = 1;
        //                                articleSubZone.MobileTemplateId = 0;
        //                                articleSubZone.CreatedBy = currentUserId;
        //                                articleSubZone.Created = DateTime.Now;
        //                                articleSubZone.Updated = DateTime.Now;
        //                                articleSubZone.Append1 = 0;
        //                                articleSubZone.Append2 = 0;
        //                                articleSubZone.Append3 = 0;
        //                                articleSubZone.Append4 = 0;
        //                                articleSubZone.Append5 = 0;
        //                                articleSubZone.LangId = zone.LangId;
        //                                articleSubZone.Alias = CmsHelper.StringToAlphaNumeric(articleSubZone.Name, false);

        //                                dbContext.Zones.Add(articleSubZone);
        //                                if (dbContext.SaveChanges() > 0)
        //                                {
        //                                    //zone revision kayıt
        //                                    ZoneRevision articleSubZoneRevision = new ZoneRevision();
        //                                    articleSubZoneRevision.CreatedBy = currentUserId;
        //                                    articleSubZoneRevision.RevisionDate = DateTime.Now;
        //                                    articleSubZoneRevision.RevisionStatus = "L";
        //                                    articleSubZoneRevision.RevisedBy = currentUserId;
        //                                    articleSubZoneRevision.Approved = DateTime.Now;
        //                                    articleSubZoneRevision.ApprovedBy = currentUserId;
        //                                    articleSubZoneRevision.ZoneId = articleSubZone.Id;
        //                                    articleSubZoneRevision.ZoneGroupId = articleSubZone.ZoneGroupId;
        //                                    articleSubZoneRevision.ZoneTypeId = 0;
        //                                    articleSubZoneRevision.ZoneStatus = "A";
        //                                    articleSubZoneRevision.Name = articleSubZone.Name;
        //                                    articleSubZoneRevision.CssMerge = 0;
        //                                    articleSubZoneRevision.CssId = 0;
        //                                    articleSubZoneRevision.MobileCssId = 0;
        //                                    articleSubZoneRevision.PrintCssId = 0;
        //                                    articleSubZoneRevision.TemplateId = 1;
        //                                    articleSubZoneRevision.MobileTemplateId = 0;
        //                                    articleSubZoneRevision.Append1 = 0;
        //                                    articleSubZoneRevision.Append2 = 0;
        //                                    articleSubZoneRevision.Append3 = 0;
        //                                    articleSubZoneRevision.Append4 = 0;
        //                                    articleSubZoneRevision.Append5 = 0;
        //                                    articleSubZoneRevision.ContentEditorType1 = "H";
        //                                    articleSubZoneRevision.ContentEditorType2 = "H";
        //                                    articleSubZoneRevision.ContentEditorType3 = "H";
        //                                    articleSubZoneRevision.ContentEditorType4 = "H";
        //                                    articleSubZoneRevision.ContentEditorType5 = "H";
        //                                    articleSubZoneRevision.LangId = articleSubZone.LangId;
        //                                    articleSubZoneRevision.Alias = articleSubZone.Alias;

        //                                    dbContext.ZoneRevisions.Add(articleSubZoneRevision);
        //                                    if (dbContext.SaveChanges() > 0)
        //                                    {
        //                                        getEnDataFromExcel.NavigationZoneId = articleSubZone.Id;
        //                                    }

        //                                }
        //                                #endregion


        //                                //ingilizce data kayıt edildi.
        //                                var enData = SaveArticleData(getEnDataFromExcel);
        //                                if (enData != null)
        //                                {
        //                                    //ingilizce data program 
        //                                    var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.Id == getEnDataFromExcel.Id && w.Lang == getEnDataFromExcel.Lang).ToList();
        //                                    foreach (var itemProgram in eventsProgramEn)
        //                                    {
        //                                        itemProgram.ZoneId = getEnDataFromExcel.NavigationZoneId;
        //                                        var programArticleEn = SaveArticleData(itemProgram);
        //                                    }

        //                                    //trdata revizyon cekiliyor
        //                                    var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
        //                                    if (getTrRev != null)
        //                                    {
        //                                        int langRelId = 1;

        //                                        var lastLang = dbContext.LanguageRelations.OrderBy(o => o.LanguageRelationID).ToList().LastOrDefault();
        //                                        if (lastLang != null)
        //                                        {
        //                                            langRelId = Convert.ToInt32(lastLang.LanguageRelationID) + 1;
        //                                        }

        //                                        //turkce ingilizceye eşitleniyor
        //                                        var langRelFL = new LanguageRelation();
        //                                        langRelFL.LanguageRelationID = langRelId;
        //                                        langRelFL.ArticleID = article.Id;
        //                                        langRelFL.ZoneID = item.ZoneId;
        //                                        dbContext.LanguageRelations.Add(langRelFL);
        //                                        if (dbContext.SaveChanges() > 0)
        //                                        {
        //                                            //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
        //                                            var langRelFLRev = new LanguageRelationRevision();
        //                                            langRelFLRev.LanguageRelationID = langRelId;
        //                                            langRelFLRev.ArticleID = article.Id;
        //                                            langRelFLRev.ZoneID = item.ZoneId;
        //                                            langRelFLRev.RevisionID = getTrRev.RevisionId;
        //                                            dbContext.LanguageRelationRevisions.Add(langRelFLRev);

        //                                            if (dbContext.SaveChanges() > 0)
        //                                            {
        //                                                //endata revizyon cekiliyor
        //                                                var getEnRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == enData.Id && f.RevisionStatus == "L");
        //                                                if (getEnRev != null)
        //                                                {
        //                                                    //ingilzce turkceye eşitleniyor
        //                                                    var langRelLF = new LanguageRelation();
        //                                                    langRelLF.LanguageRelationID = langRelId;
        //                                                    langRelLF.ArticleID = enData.Id;
        //                                                    langRelLF.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                    dbContext.LanguageRelations.Add(langRelLF);
        //                                                    if (dbContext.SaveChanges() > 0)
        //                                                    {
        //                                                        //ingilzce turkceye eşitleniyor revision kaydı yapılıyor.
        //                                                        var langRelLFRev = new LanguageRelationRevision();
        //                                                        langRelLFRev.LanguageRelationID = langRelId;
        //                                                        langRelLFRev.ArticleID = enData.Id;
        //                                                        langRelLFRev.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                        langRelLFRev.RevisionID = getEnRev.RevisionId;
        //                                                        dbContext.LanguageRelationRevisions.Add(langRelLFRev);
        //                                                        dbContext.SaveChanges();
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                TempData["HasError"] = true;
        //                                TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + getEnDataFromExcel.ZoneId;
        //                            }
        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    TempData["HasError"] = true;
        //                    TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + item.ZoneId;
        //                }
        //            }

        //            TempData["Message"] = "Bulk Import Completed";
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            CmsHelper.SaveErrorLog(ex, string.Empty, true);
        //            TempData["HasError"] = true;
        //            TempData["Message"] = ex.Message;
        //        }
        //    }


        //    return View("~/Views/CMSPlugins/IKSV/Bulk/dataimport.cshtml");
        //}



        //private Article BulkSaveArticle(Article a, int zoneid)
        //{
        //    CmsDbContext dbContext = new CmsDbContext();
        //    Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

        //    dbContext.Articles.Add(a);
        //    if (dbContext.SaveChanges() > 0)
        //    {
        //        ArticleRevision ar = new ArticleRevision();
        //        ar.Headline = a.Headline;
        //        ar.Summary = a.Summary;
        //        ar.ClassificationId = a.ClassificationId;
        //        ar.NavigationZoneId = a.NavigationZoneId;
        //        ar.LangId = a.LangId;
        //        ar.Article1 = a.Article1;
        //        ar.Article2 = a.Article2;
        //        ar.Article3 = a.Article3;
        //        ar.Article4 = a.Article4;
        //        ar.Article5 = a.Article5;
        //        ar.Custom1 = a.Custom1;
        //        ar.Custom2 = a.Custom2;
        //        ar.Custom3 = a.Custom3;
        //        ar.Custom4 = a.Custom4;
        //        ar.Custom5 = a.Custom5;
        //        ar.Custom6 = a.Custom6;
        //        ar.Custom7 = a.Custom7;
        //        ar.Custom8 = a.Custom8;
        //        ar.Custom9 = a.Custom9;
        //        ar.Custom10 = a.Custom10;
        //        ar.Cl1 = a.Cl1;
        //        ar.Cl2 = a.Cl2;
        //        ar.Cl3 = a.Cl3;
        //        ar.Cl4 = a.Cl4;
        //        ar.Cl5 = a.Cl5;

        //        //date
        //        ar.date_1 = a.date_1;
        //        //date

        //        ar.Created = a.Created;
        //        ar.CreatedBy = a.CreatedBy;
        //        ar.Status = (byte)1;
        //        ar.RevisionStatus = "L";
        //        ar.TagContents = a.TagContents;
        //        ar.TagIds = a.TagIds;
        //        ar.ArticleId = a.Id;
        //        ar.Approved = DateTime.Now;
        //        ar.ApprovedBy = currentUserId;
        //        ar.RevisedBy = currentUserId;
        //        ar.Startdate = DateTime.Now;

        //        dbContext.ArticleRevisions.Add(ar);
        //        if (dbContext.SaveChanges() > 0)
        //        {
        //            //article ve revizyonu oluşturuldu
        //            #region Zones
        //            string alias = "";

        //            ArticleZone az = new ArticleZone();
        //            az.Article = a;
        //            az.ArticleID = a.Id;
        //            az.AzAlias = alias;
        //            az.AzOrder = 0;
        //            az.IsAliasProtected = true;
        //            az.ZoneID = zoneid;
        //            az.IsPage = (a.ClassificationId == etkinlikProgramClassification ? false : true);

        //            dbContext.ArticleZones.Add(az);
        //            if (dbContext.SaveChanges() > 0)
        //            {
        //                ArticleZoneRevision azr = new ArticleZoneRevision();
        //                azr.ArticleID = a.Id;
        //                azr.AzAlias = az.AzAlias;
        //                azr.AzOrder = az.AzOrder;
        //                azr.IsAliasProtected = az.IsAliasProtected;
        //                azr.ZoneID = az.ZoneID;
        //                azr.RevID = ar.RevisionId;
        //                azr.IsPage = az.IsPage;
        //                dbContext.ArticleZoneRevisions.Add(azr);

        //                if (dbContext.SaveChanges() > 0)
        //                {
        //                    if (string.IsNullOrEmpty(az.AzAlias))
        //                    {
        //                        alias = CmsHelper.CreateAliasWithUrlStructure(a.Id, Convert.ToInt32(zoneid));
        //                        az.AzAlias = alias;
        //                        dbContext.Entry(az).State = EntityState.Modified;
        //                        if (dbContext.SaveChanges() > 0)
        //                        {
        //                            azr.AzAlias = az.AzAlias;
        //                            dbContext.Entry(azr).State = EntityState.Modified;
        //                            if (dbContext.SaveChanges() > 0)
        //                            {
        //                                // hepsi tamam
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    a = null;
        //                }
        //            }
        //            else
        //            {
        //                a = null;
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            a = null;
        //        }
        //    }
        //    else
        //    {
        //        a = null;
        //    }

        //    return a;
        //}

        //public string ZoneCheckAlias(string text, string id, string parentId)
        //{
        //    string result = string.Empty;
        //    text = text.Trim();

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(text))
        //        {
        //            string cleanText = CmsHelper.StringToAlphaNumeric(text, false);
        //            int currentZoneGroupId = Convert.ToInt32(parentId);
        //            CmsDbContext dbContext = new CmsDbContext();
        //            List<Zone> zones = dbContext.Zones.Where(x => x.Alias == cleanText && x.ZoneGroupId == currentZoneGroupId).ToList();

        //            int currentId = Convert.ToInt32(id);

        //            zones.Remove(zones.Where(x => x.Id == currentId).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

        //            if (zones == null || zones.Count == 0)
        //            {
        //                //ok 
        //                result = cleanText;
        //            }
        //            else
        //            {
        //                //çakışma var 
        //                int counter = 2;
        //                while (dbContext.Zones.Where(x => x.Alias == cleanText + "-" + counter && x.ZoneGroupId == currentZoneGroupId).ToList().Count > 0)
        //                {
        //                    counter++;
        //                }
        //                //son - cleanText + "-" + counter
        //                result = cleanText + "-" + counter;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "_#NOK#_";
        //        CmsHelper.SaveErrorLog(ex, "Can't create zone alias", true);
        //    }

        //    return result;
        //}





        //public ActionResult Import()
        //{
        //    return View("~/Views/CMSPlugins/IKSV/Bulk/import.cshtml");
        //}

        //[HttpPost]
        //public ActionResult Import(FormCollection collection)
        //{
        //    HttpPostedFileBase file = Request.Files["upload"];
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        string fileName = file.FileName.Split('.')[0];
        //        string extension = file.FileName.Split('.')[1];
        //        try
        //        {
        //            #region Save File
        //            string path = Server.MapPath("/i/content") + "//" + fileName + " - " + DateTime.Now.ToLongDateString() + "." + extension;
        //            Request.Files["upload"].SaveAs(path);
        //            #endregion


        //            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
        //            CmsDbContext dbContext = new CmsDbContext();

        //            List<DataImportModel> dataList = new List<DataImportModel>();

        //            IWorkbook wb = WorkbookFactory.Create(new FileStream(
        //                       Path.GetFullPath(path),
        //                       FileMode.Open, FileAccess.Read,
        //                       FileShare.ReadWrite));

        //            ISheet ws = wb.GetSheetAt(0);

        //            for (int row = 1; row <= ws.LastRowNum; row++)
        //            {
        //                var importData = new DataImportModel
        //                {
        //                    Id = (ws.GetRow(row).GetCell(0) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(0).NumericCellValue)),
        //                    ParentId = (ws.GetRow(row).GetCell(1) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(1).NumericCellValue)),
        //                    ClassificationId = (ws.GetRow(row).GetCell(2) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(2).NumericCellValue)),
        //                    Lang = (ws.GetRow(row).GetCell(3) == null ? "" : ws.GetRow(row).GetCell(3).StringCellValue),
        //                    Headline = (ws.GetRow(row).GetCell(4) == null ? "" : ws.GetRow(row).GetCell(4).StringCellValue),
        //                    Summary = (ws.GetRow(row).GetCell(5) == null ? "" : ws.GetRow(row).GetCell(5).StringCellValue),
        //                    Content_1 = (ws.GetRow(row).GetCell(6) == null ? "" : ws.GetRow(row).GetCell(6).StringCellValue),
        //                    Content_2 = (ws.GetRow(row).GetCell(7) == null ? "" : ws.GetRow(row).GetCell(7).StringCellValue),
        //                    Content_3 = (ws.GetRow(row).GetCell(8) == null ? "" : ws.GetRow(row).GetCell(8).StringCellValue),
        //                    Content_4 = (ws.GetRow(row).GetCell(9) == null ? "" : ws.GetRow(row).GetCell(9).StringCellValue),
        //                    Content_5 = (ws.GetRow(row).GetCell(10) == null ? "" : ws.GetRow(row).GetCell(10).StringCellValue),
        //                    Custom_1 = (ws.GetRow(row).GetCell(11) == null ? "" : ws.GetRow(row).GetCell(11).StringCellValue),
        //                    Custom_2 = (ws.GetRow(row).GetCell(12) == null ? "" : ws.GetRow(row).GetCell(12).StringCellValue),
        //                    Custom_3 = (ws.GetRow(row).GetCell(13) == null ? "" : ws.GetRow(row).GetCell(13).StringCellValue),
        //                    Custom_4 = (ws.GetRow(row).GetCell(14) == null ? "" : ws.GetRow(row).GetCell(14).StringCellValue),
        //                    Custom_5 = (ws.GetRow(row).GetCell(15) == null ? "" : ws.GetRow(row).GetCell(15).StringCellValue),
        //                    Custom_6 = (ws.GetRow(row).GetCell(16) == null ? "" : ws.GetRow(row).GetCell(16).StringCellValue),
        //                    Custom_7 = (ws.GetRow(row).GetCell(17) == null ? "" : ws.GetRow(row).GetCell(17).StringCellValue),
        //                    Custom_8 = (ws.GetRow(row).GetCell(18) == null ? "" : ws.GetRow(row).GetCell(18).StringCellValue),
        //                    Custom_9 = (ws.GetRow(row).GetCell(19) == null ? "" : ws.GetRow(row).GetCell(19).StringCellValue),
        //                    Custom_10 = (ws.GetRow(row).GetCell(20) == null ? "" : ws.GetRow(row).GetCell(20).StringCellValue),
        //                    Tags = (ws.GetRow(row).GetCell(21) == null ? "" : ws.GetRow(row).GetCell(21).StringCellValue),
        //                    ZoneId = (ws.GetRow(row).GetCell(13) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(23).NumericCellValue))
        //                };


        //                if (ws.GetRow(row).GetCell(22) != null)
        //                {
        //                    if (!string.IsNullOrEmpty(ws.GetRow(row).GetCell(22).StringCellValue))
        //                    {
        //                        importData.Date_1 = ws.GetRow(row).GetCell(22).StringCellValue;
        //                    }
        //                }

        //                dataList.Add(importData);
        //            }

        //            //türkce data filtrelendi.
        //            var datas = dataList.Where(w => w.Lang == "TR" && w.ClassificationId == etkinlikClassification).ToList();
        //            foreach (var item in datas)
        //            {
        //                //turkce data kayıt edildi
        //                var trData = SaveArticleData(item);
        //                if (trData != null)
        //                {
        //                    ////tr etkinlik program
        //                    //var trProgramData = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.Id == item.Id && w.Lang == item.Lang).ToList();
        //                    //if (trProgramData != null)
        //                    //{
        //                    //    foreach (var trProgramDataItem in trProgramData)
        //                    //    {
        //                    //        trProgramDataItem.Custom_10 = trData.Id.ToString();
        //                    //        SaveArticleData(trProgramDataItem);
        //                    //    }
        //                    //}                            

        //                    //ingilizce data çekildi
        //                    var getEnDataFromExcel = dataList.FirstOrDefault(f => f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id && f.ClassificationId == etkinlikClassification);
        //                    if (getEnDataFromExcel != null)
        //                    {
        //                        //ingilizce data kayıt edildi.
        //                        var enData = SaveArticleData(getEnDataFromExcel);
        //                        if (enData != null)
        //                        {
        //                            ////en etkinlik program
        //                            //var enProgramData = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.Id == getEnDataFromExcel.Id && w.Lang == getEnDataFromExcel.Lang).ToList();
        //                            //if (enProgramData != null)
        //                            //{
        //                            //    foreach (var enProgramDataItem in enProgramData)
        //                            //    {
        //                            //        enProgramDataItem.Custom_10 = enData.Id.ToString();
        //                            //        SaveArticleData(enProgramDataItem);
        //                            //    }
        //                            //}

        //                            //trdata revizyon cekiliyor
        //                            var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == trData.Id && f.RevisionStatus == "L");
        //                            if (getTrRev != null)
        //                            {
        //                                int langRelId = 1;

        //                                var lastLang = dbContext.LanguageRelations.OrderBy(o => o.LanguageRelationID).ToList().LastOrDefault();
        //                                if (lastLang != null)
        //                                {
        //                                    langRelId = Convert.ToInt32(lastLang.LanguageRelationID) + 1;
        //                                }

        //                                //turkce ingilizceye eşitleniyor
        //                                var langRelFL = new LanguageRelation();
        //                                langRelFL.LanguageRelationID = langRelId;
        //                                langRelFL.ArticleID = trData.Id;
        //                                langRelFL.ZoneID = item.ZoneId;
        //                                dbContext.LanguageRelations.Add(langRelFL);
        //                                if (dbContext.SaveChanges() > 0)
        //                                {
        //                                    //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
        //                                    var langRelFLRev = new LanguageRelationRevision();
        //                                    langRelFLRev.LanguageRelationID = langRelId;
        //                                    langRelFLRev.ArticleID = trData.Id;
        //                                    langRelFLRev.ZoneID = item.ZoneId;
        //                                    langRelFLRev.RevisionID = getTrRev.RevisionId;
        //                                    dbContext.LanguageRelationRevisions.Add(langRelFLRev);

        //                                    if (dbContext.SaveChanges() > 0)
        //                                    {
        //                                        //endata revizyon cekiliyor
        //                                        var getEnRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == enData.Id && f.RevisionStatus == "L");
        //                                        if (getEnRev != null)
        //                                        {
        //                                            //ingilzce turkceye eşitleniyor
        //                                            var langRelLF = new LanguageRelation();
        //                                            langRelLF.LanguageRelationID = langRelId;
        //                                            langRelLF.ArticleID = enData.Id;
        //                                            langRelLF.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                            dbContext.LanguageRelations.Add(langRelLF);
        //                                            if (dbContext.SaveChanges() > 0)
        //                                            {
        //                                                //ingilzce turkceye eşitleniyor revision kaydı yapılıyor.
        //                                                var langRelLFRev = new LanguageRelationRevision();
        //                                                langRelLFRev.LanguageRelationID = langRelId;
        //                                                langRelLFRev.ArticleID = enData.Id;
        //                                                langRelLFRev.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                langRelLFRev.RevisionID = getEnRev.RevisionId;
        //                                                dbContext.LanguageRelationRevisions.Add(langRelLFRev);
        //                                                dbContext.SaveChanges();
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            TempData["Message"] = "Bulk Import Completed";
        //        }
        //        catch (Exception ex)
        //        {
        //            CmsHelper.SaveErrorLog(ex, string.Empty, true);
        //            TempData["HasError"] = true;
        //            TempData["Message"] = ex.Message;
        //        }
        //    }
        //    return View("~/Views/CMSPlugins/IKSV/Bulk/import.cshtml");
        //}



        private Article SaveArticleData(DataImportModel item)
        {
            CmsDbContext dbContext = new CmsDbContext();
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

            Article a = new Article();
            a.Headline = item.Headline;
            a.Summary = item.Summary;
            a.ClassificationId = item.ClassificationId;
            a.NavigationZoneId = item.NavigationZoneId;
            a.LangId = item.Lang;
            a.Article1 = item.Content_1;
            a.Article2 = item.Content_2;
            a.Article3 = item.Content_3;
            a.Article4 = item.Content_4;
            a.Article5 = item.Content_5;
            a.Custom1 = item.Custom_1;
            a.Custom2 = item.Custom_2;
            a.Custom3 = item.Custom_3;
            a.Custom4 = item.Custom_4;
            a.Custom5 = item.Custom_5;
            a.Custom6 = item.Custom_6;
            a.Custom7 = item.Custom_7;
            a.Custom8 = item.Custom_8;
            a.Custom9 = item.Custom_9;
            a.Custom10 = item.Custom_10;
            a.Cl1 = 0;
            a.Cl2 = 0;
            a.Cl3 = 0;
            a.Cl4 = 0;
            a.Cl5 = 0;

            ////date
            //if (!string.IsNullOrEmpty(item.Date_1))
            //{
            //    a.date_1 = Convert.ToDateTime(item.Date_1);
            //}
            ////date


            if (!string.IsNullOrEmpty(item.Date_1))
            {
                a.date_1 = Convert.ToDateTime(item.Date_1);
                a.date_2 = Convert.ToDateTime(item.Date_1);
            }


            a.Created = DateTime.Now;
            a.CreatedBy = currentUserId;
            a.Status = (byte)1;
            a.Updated = DateTime.Now;
            a.Startdate = DateTime.Now;

            #region Tags
            string tags = item.Tags;
            if (!string.IsNullOrEmpty(tags))
            {
                List<string> tagList = tags.Split(',').ToList();
                List<Tag> AllTags = dbContext.Tags.ToList();
                List<int> tagIds = new List<int>();

                foreach (string s in tagList)
                {
                    if (AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault() != null)
                    {
                        Tag tCurrent = AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault();
                        tagIds.Add(tCurrent.ID);
                    }
                    else
                    {
                        Tag newTag = new Tag { AddedDate = DateTime.Now, IsActive = true, Counter = 0, PublisherID = currentUserId, SiteID = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : 6, Text = s };

                        dbContext.Tags.Add(newTag);
                        if (dbContext.SaveChanges() > 0)
                        {
                            tagIds.Add(newTag.ID);
                        }
                    }
                }
                a.TagIds = string.Join(",", tagIds.ToArray());
                a.TagContents = tags;
            }
            #endregion

            dbContext.Articles.Add(a);
            if (dbContext.SaveChanges() > 0)
            {
                ArticleRevision ar = new ArticleRevision();
                ar.Headline = a.Headline;
                ar.Summary = a.Summary;
                ar.ClassificationId = a.ClassificationId;
                ar.NavigationZoneId = a.NavigationZoneId;
                ar.LangId = a.LangId;
                ar.Article1 = a.Article1;
                ar.Article2 = a.Article2;
                ar.Article3 = a.Article3;
                ar.Article4 = a.Article4;
                ar.Article5 = a.Article5;
                ar.Custom1 = a.Custom1;
                ar.Custom2 = a.Custom2;
                ar.Custom3 = a.Custom3;
                ar.Custom4 = a.Custom4;
                ar.Custom5 = a.Custom5;
                ar.Custom6 = a.Custom6;
                ar.Custom7 = a.Custom7;
                ar.Custom8 = a.Custom8;
                ar.Custom9 = a.Custom9;
                ar.Custom10 = a.Custom10;

                ar.Cl1 = a.Cl1;
                ar.Cl2 = a.Cl2;
                ar.Cl3 = a.Cl3;
                ar.Cl4 = a.Cl4;
                ar.Cl5 = a.Cl5;

                //date
                ar.date_1 = a.date_1;
                ar.date_2 = a.date_2;
                //date

                ar.Created = a.Created;
                ar.CreatedBy = a.CreatedBy;
                ar.Status = (byte)1;
                ar.RevisionStatus = "L";
                ar.TagContents = a.TagContents;
                ar.TagIds = a.TagIds;
                ar.ArticleId = a.Id;
                ar.Approved = DateTime.Now;
                ar.ApprovedBy = currentUserId;
                ar.RevisedBy = currentUserId;
                ar.Startdate = DateTime.Now;

                dbContext.ArticleRevisions.Add(ar);
                if (dbContext.SaveChanges() > 0)
                {
                    //article ve revizyonu oluşturuldu
                    #region Zones
                    string alias = "";

                    ArticleZone az = new ArticleZone();
                    az.Article = a;
                    az.ArticleID = a.Id;
                    az.AzAlias = alias;
                    az.AzOrder = item.Order;
                    az.IsAliasProtected = true;
                    az.ZoneID = item.ZoneId;
                    az.IsPage = (a.ClassificationId == etkinlikProgramClassification ? false : true);

                    dbContext.ArticleZones.Add(az);
                    if (dbContext.SaveChanges() > 0)
                    {
                        ArticleZoneRevision azr = new ArticleZoneRevision();
                        azr.ArticleID = a.Id;
                        azr.AzAlias = az.AzAlias;
                        azr.AzOrder = az.AzOrder;
                        azr.IsAliasProtected = az.IsAliasProtected;
                        azr.ZoneID = az.ZoneID;
                        azr.RevID = ar.RevisionId;
                        azr.IsPage = az.IsPage;
                        dbContext.ArticleZoneRevisions.Add(azr);

                        if (dbContext.SaveChanges() > 0)
                        {
                            if (string.IsNullOrEmpty(az.AzAlias))
                            {
                                alias = CmsHelper.CreateAliasWithUrlStructure(a.Id, Convert.ToInt32(az.ZoneID));
                                az.AzAlias = alias;
                                dbContext.Entry(az).State = EntityState.Modified;
                                if (dbContext.SaveChanges() > 0)
                                {
                                    azr.AzAlias = az.AzAlias;
                                    dbContext.Entry(azr).State = EntityState.Modified;
                                    if (dbContext.SaveChanges() > 0)
                                    {
                                        // hepsi tamam
                                    }
                                }
                            }
                        }
                        else
                        {
                            a = null;
                        }
                    }
                    else
                    {
                        a = null;
                    }
                    #endregion
                }
                else
                {
                    a = null;
                }
            }
            else
            {
                a = null;
            }

            return a;
        }

    }
}