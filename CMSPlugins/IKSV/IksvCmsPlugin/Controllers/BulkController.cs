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
using NPOI.HSSF.UserModel;

namespace EuroCMS.CMSPlugin.IKSV.Controllers
{
    public class BulkController : BaseController
    {
        int etkinlikClassification = 4;
        int etkinlikProgramClassification = 5;
        int etkinlikKategoriClassification = 6;
        // GET: Bulk
        public ActionResult Index()
        {
            return View("~/Views/CMSPlugins/IKSV/Bulk/Index.cshtml");
        }


        //public ActionResult dataimport()
        //{
        //    return View("~/Views/CMSPlugins/IKSV/Bulk/dataimport.cshtml");
        //}

        //[HttpPost]
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
        //                    //ParentId = (ws.GetRow(row).GetCell(1) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(1).NumericCellValue)),
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
        //                    ZoneId = (ws.GetRow(row).GetCell(23) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(23).NumericCellValue)),
        //                    Order = (ws.GetRow(row).GetCell(24) == null ? 0 : Convert.ToInt32(ws.GetRow(row).GetCell(24).NumericCellValue))
        //                };


        //                var df = new DataFormatter();
        //                var parentId = ws.GetRow(row).GetCell(1);
        //                var parentIdString = df.FormatCellValue(ws.GetRow(row).GetCell(1));


        //                if (ws.GetRow(row).GetCell(22) != null)
        //                {
        //                    importData.Date_1 = ws.GetRow(row).GetCell(22).StringCellValue;
        //                }

        //                dataList.Add(importData);
        //            }


        //            List<DataImportModel> subCategoryEvents = new List<DataImportModel>();

        //            #region kategori bağlı etkinlikler

        //            var eventCategories = dataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "TR" && w.ParentId == 0).ToList();

        //            //kategoriler liste
        //            foreach (var category in eventCategories)
        //            {
        //                var articleCategory = SaveArticleData(category);
        //                if (articleCategory != null)
        //                {
        //                    var categoryTrId = articleCategory.Id.ToString();
        //                    var categoryEnId = "";

        //                    //ilişkili en kategoriçekildi
        //                    var categoryEn = dataList.FirstOrDefault(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "EN" && w.Id == category.Id);
        //                    if (categoryEn != null)
        //                    {
        //                        var articleCategoryEn = SaveArticleData(categoryEn);
        //                        if (articleCategoryEn != null)
        //                        {
        //                            categoryEnId = articleCategoryEn.Id.ToString();
        //                        }
        //                    }

        //                    //altkategori kontrol;alt kategori var ise kategori kaydı sonrası etkinlik eklenecek.yoksa direk etkinlik kaydı
        //                    var eventSubCategories = dataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == category.Lang && w.ParentId == category.Id).ToList();
        //                    if (eventSubCategories.Count > 0)
        //                    {
        //                        //kategori kayıt
        //                        foreach (var subCategory in eventSubCategories)
        //                        {
        //                            subCategory.Custom_1 = categoryTrId;
        //                            //tr subcategory kayıt
        //                            var articleSubCategory = SaveArticleData(subCategory);
        //                            if (articleSubCategory != null)
        //                            {
        //                                //alt kategori etkinlik listesi çekilip kategori idsi yazıldı
        //                                var eventsDataSubCategory = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == subCategory.Lang && w.ParentId == subCategory.Id).ToList();
        //                                foreach (var eventDataSubCategory in eventsDataSubCategory)
        //                                {
        //                                    eventDataSubCategory.Custom_10 = articleSubCategory.Id.ToString();
        //                                    subCategoryEvents.Add(eventDataSubCategory);
        //                                }

        //                                var subCategoryEn = dataList.FirstOrDefault(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "EN" && w.Id == subCategory.Id);
        //                                if (subCategoryEn != null)
        //                                {
        //                                    subCategoryEn.Custom_1 = categoryEnId;
        //                                    //en subcategory kayıt
        //                                    var articleCategoryEn = SaveArticleData(subCategoryEn);
        //                                    if (articleCategoryEn != null)
        //                                    {
        //                                        eventsDataSubCategory = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == subCategoryEn.Lang && w.ParentId == subCategoryEn.Id).ToList();
        //                                        foreach (var eventDataSubCategory in eventsDataSubCategory)
        //                                        {
        //                                            eventDataSubCategory.Custom_10 = articleSubCategory.Id.ToString();
        //                                            subCategoryEvents.Add(eventDataSubCategory);
        //                                        }
        //                                    }
        //                                }


        //                                var eventsList = subCategoryEvents.Where(w => w.Lang == "TR").ToList();

        //                                foreach (var item in eventsList)
        //                                {
        //                                    var zone = dbContext.Zones.FirstOrDefault(f => f.Id == item.ZoneId);
        //                                    if (zone != null)
        //                                    {
        //                                        var article = SaveArticleData(item);
        //                                        if (article != null)
        //                                        {
        //                                            var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentId == item.Id && w.Lang == item.Lang).ToList();
        //                                            foreach (var itemProgram in eventsProgram)
        //                                            {
        //                                                itemProgram.Custom_10 = article.Id.ToString();
        //                                                var programArticle = SaveArticleData(itemProgram);
        //                                            }

        //                                            //ingilizce data çekildi
        //                                            var getEnDataFromExcel = subCategoryEvents.FirstOrDefault(f => f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
        //                                            if (getEnDataFromExcel != null)
        //                                            {
        //                                                var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
        //                                                if (zoneEn != null)
        //                                                {
        //                                                    //ingilizce data kayıt edildi.
        //                                                    var enData = SaveArticleData(getEnDataFromExcel);
        //                                                    if (enData != null)
        //                                                    {
        //                                                        //ingilizce data program 
        //                                                        var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentId == getEnDataFromExcel.Id && w.Lang == getEnDataFromExcel.Lang).ToList();
        //                                                        foreach (var itemProgram in eventsProgramEn)
        //                                                        {
        //                                                            itemProgram.Custom_10 = enData.Id.ToString();
        //                                                            var programArticleEn = SaveArticleData(itemProgram);
        //                                                        }

        //                                                        //trdata revizyon cekiliyor
        //                                                        var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
        //                                                        if (getTrRev != null)
        //                                                        {
        //                                                            int langRelId = 1;

        //                                                            var lastLang = dbContext.LanguageRelations.OrderBy(o => o.LanguageRelationID).ToList().LastOrDefault();
        //                                                            if (lastLang != null)
        //                                                            {
        //                                                                langRelId = Convert.ToInt32(lastLang.LanguageRelationID) + 1;
        //                                                            }

        //                                                            //turkce ingilizceye eşitleniyor
        //                                                            var langRelFL = new LanguageRelation();
        //                                                            langRelFL.LanguageRelationID = langRelId;
        //                                                            langRelFL.ArticleID = article.Id;
        //                                                            langRelFL.ZoneID = item.ZoneId;
        //                                                            dbContext.LanguageRelations.Add(langRelFL);
        //                                                            if (dbContext.SaveChanges() > 0)
        //                                                            {
        //                                                                //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
        //                                                                var langRelFLRev = new LanguageRelationRevision();
        //                                                                langRelFLRev.LanguageRelationID = langRelId;
        //                                                                langRelFLRev.ArticleID = article.Id;
        //                                                                langRelFLRev.ZoneID = item.ZoneId;
        //                                                                langRelFLRev.RevisionID = getTrRev.RevisionId;
        //                                                                dbContext.LanguageRelationRevisions.Add(langRelFLRev);

        //                                                                if (dbContext.SaveChanges() > 0)
        //                                                                {
        //                                                                    //endata revizyon cekiliyor
        //                                                                    var getEnRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == enData.Id && f.RevisionStatus == "L");
        //                                                                    if (getEnRev != null)
        //                                                                    {
        //                                                                        //ingilzce turkceye eşitleniyor
        //                                                                        var langRelLF = new LanguageRelation();
        //                                                                        langRelLF.LanguageRelationID = langRelId;
        //                                                                        langRelLF.ArticleID = enData.Id;
        //                                                                        langRelLF.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                                        dbContext.LanguageRelations.Add(langRelLF);
        //                                                                        if (dbContext.SaveChanges() > 0)
        //                                                                        {
        //                                                                            //ingilzce turkceye eşitleniyor revision kaydı yapılıyor.
        //                                                                            var langRelLFRev = new LanguageRelationRevision();
        //                                                                            langRelLFRev.LanguageRelationID = langRelId;
        //                                                                            langRelLFRev.ArticleID = enData.Id;
        //                                                                            langRelLFRev.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                                            langRelLFRev.RevisionID = getEnRev.RevisionId;
        //                                                                            dbContext.LanguageRelationRevisions.Add(langRelLFRev);
        //                                                                            dbContext.SaveChanges();
        //                                                                        }
        //                                                                    }
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                                else
        //                                                {
        //                                                    TempData["HasError"] = true;
        //                                                    TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + getEnDataFromExcel.ZoneId;
        //                                                }
        //                                            }

        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        TempData["HasError"] = true;
        //                                        TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + item.ZoneId;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var eventList = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == category.Lang && w.ParentId == category.Id).ToList();
        //                        //alt kategorilere bağlı olmayan etkinlikler kayıt
        //                        foreach (var item in eventList)
        //                        {
        //                            var zone = dbContext.Zones.FirstOrDefault(f => f.Id == item.ZoneId);
        //                            if (zone != null)
        //                            {
        //                                item.Custom_10 = categoryTrId;
        //                                var article = SaveArticleData(item);

        //                                if (article != null)
        //                                {
        //                                    //tr program
        //                                    var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentId == item.Id && w.Lang == item.Lang).ToList();
        //                                    foreach (var itemProgram in eventsProgram)
        //                                    {
        //                                        itemProgram.Custom_10 = article.Id.ToString();
        //                                        var programArticle = SaveArticleData(itemProgram);
        //                                    }

        //                                    //ingilizce data çekildi
        //                                    var getEnDataFromExcel = dataList.FirstOrDefault(f => f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
        //                                    if (getEnDataFromExcel != null)
        //                                    {
        //                                        var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
        //                                        if (zoneEn != null)
        //                                        {
        //                                            //ingilizce data kayıt edildi.
        //                                            getEnDataFromExcel.Custom_10 = categoryEnId;
        //                                            var enData = SaveArticleData(getEnDataFromExcel);
        //                                            if (enData != null)
        //                                            {
        //                                                //ingilizce data program 
        //                                                var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentId == getEnDataFromExcel.Id && w.Lang == getEnDataFromExcel.Lang).ToList();
        //                                                foreach (var itemProgram in eventsProgramEn)
        //                                                {
        //                                                    itemProgram.Custom_10 = enData.Id.ToString();
        //                                                    var programArticleEn = SaveArticleData(itemProgram);
        //                                                }

        //                                                //trdata revizyon cekiliyor
        //                                                var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
        //                                                if (getTrRev != null)
        //                                                {
        //                                                    int langRelId = 1;

        //                                                    var lastLang = dbContext.LanguageRelations.OrderBy(o => o.LanguageRelationID).ToList().LastOrDefault();
        //                                                    if (lastLang != null)
        //                                                    {
        //                                                        langRelId = Convert.ToInt32(lastLang.LanguageRelationID) + 1;
        //                                                    }

        //                                                    //turkce ingilizceye eşitleniyor
        //                                                    var langRelFL = new LanguageRelation();
        //                                                    langRelFL.LanguageRelationID = langRelId;
        //                                                    langRelFL.ArticleID = article.Id;
        //                                                    langRelFL.ZoneID = item.ZoneId;
        //                                                    dbContext.LanguageRelations.Add(langRelFL);
        //                                                    if (dbContext.SaveChanges() > 0)
        //                                                    {
        //                                                        //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
        //                                                        var langRelFLRev = new LanguageRelationRevision();
        //                                                        langRelFLRev.LanguageRelationID = langRelId;
        //                                                        langRelFLRev.ArticleID = article.Id;
        //                                                        langRelFLRev.ZoneID = item.ZoneId;
        //                                                        langRelFLRev.RevisionID = getTrRev.RevisionId;
        //                                                        dbContext.LanguageRelationRevisions.Add(langRelFLRev);

        //                                                        if (dbContext.SaveChanges() > 0)
        //                                                        {
        //                                                            //endata revizyon cekiliyor
        //                                                            var getEnRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == enData.Id && f.RevisionStatus == "L");
        //                                                            if (getEnRev != null)
        //                                                            {
        //                                                                //ingilzce turkceye eşitleniyor
        //                                                                var langRelLF = new LanguageRelation();
        //                                                                langRelLF.LanguageRelationID = langRelId;
        //                                                                langRelLF.ArticleID = enData.Id;
        //                                                                langRelLF.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                                dbContext.LanguageRelations.Add(langRelLF);
        //                                                                if (dbContext.SaveChanges() > 0)
        //                                                                {
        //                                                                    //ingilzce turkceye eşitleniyor revision kaydı yapılıyor.
        //                                                                    var langRelLFRev = new LanguageRelationRevision();
        //                                                                    langRelLFRev.LanguageRelationID = langRelId;
        //                                                                    langRelLFRev.ArticleID = enData.Id;
        //                                                                    langRelLFRev.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
        //                                                                    langRelLFRev.RevisionID = getEnRev.RevisionId;
        //                                                                    dbContext.LanguageRelationRevisions.Add(langRelLFRev);
        //                                                                    dbContext.SaveChanges();
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            TempData["HasError"] = true;
        //                                            TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + getEnDataFromExcel.ZoneId;
        //                                        }
        //                                    }

        //                                }
        //                            }
        //                            else
        //                            {
        //                                TempData["HasError"] = true;
        //                                TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + item.ZoneId;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["HasError"] = true;
        //                    TempData["Message"] = category.Headline + " kayıt edilemedi.";
        //                }
        //            }
        //            #endregion

        //            #region kategoriye bağlı olmayan etkinlikler


        //            var events = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == "TR" && w.ParentId == 0).ToList();
        //            foreach (var item in events)
        //            {
        //                var zone = dbContext.Zones.FirstOrDefault(f => f.Id == item.ZoneId);
        //                if (zone != null)
        //                {
        //                    var article = SaveArticleData(item);

        //                    if (article != null)
        //                    {
        //                        var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentId == item.Id && w.Lang == item.Lang).ToList();
        //                        foreach (var itemProgram in eventsProgram)
        //                        {
        //                            itemProgram.Custom_10 = article.Id.ToString();
        //                            var programArticle = SaveArticleData(itemProgram);
        //                        }

        //                        //ingilizce data çekildi
        //                        var getEnDataFromExcel = dataList.FirstOrDefault(f => f.ClassificationId == etkinlikClassification && f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
        //                        if (getEnDataFromExcel != null)
        //                        {
        //                            var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
        //                            if (zoneEn != null)
        //                            {
        //                                //ingilizce data kayıt edildi.
        //                                var enData = SaveArticleData(getEnDataFromExcel);
        //                                if (enData != null)
        //                                {
        //                                    //ingilizce data program 
        //                                    var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentId == getEnDataFromExcel.Id && w.Lang == getEnDataFromExcel.Lang).ToList();
        //                                    foreach (var itemProgram in eventsProgramEn)
        //                                    {
        //                                        itemProgram.Custom_10 = enData.Id.ToString();
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
        //            #endregion

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

        //    


        #region filmdataimport

        public ActionResult import()
        {
            return View("~/Views/CMSPlugins/IKSV/Bulk/import.cshtml");
        }

        [HttpPost]
        public ActionResult import(FormCollection collection)
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

                    Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                    CmsDbContext dbContext = new CmsDbContext();

                    List<DataImportModel> dataList = new List<DataImportModel>();
                    List<DataImportModel> addedDataList = new List<DataImportModel>();
                    List<DataImportModel> errorDataList = new List<DataImportModel>();

                    IWorkbook wb = WorkbookFactory.Create(new FileStream(
                               Path.GetFullPath(path),
                               FileMode.Open, FileAccess.Read,
                               FileShare.ReadWrite));

                    ISheet ws = wb.GetSheetAt(0);

                    for (int row = 1; row <= ws.LastRowNum; row++)
                    {
                        var df = new DataFormatter();
                        var importData = new DataImportModel
                        {
                            Id = (ws.GetRow(row).GetCell(0) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(0)))),
                            ParentIdString = (ws.GetRow(row).GetCell(1) == null ? null : df.FormatCellValue(ws.GetRow(row).GetCell(1)).Split(',').ToArray()),
                            ClassificationId = (ws.GetRow(row).GetCell(2) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(2)))),
                            Lang = (ws.GetRow(row).GetCell(3) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(3))),
                            Headline = (ws.GetRow(row).GetCell(4) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(4))),
                            Summary = (ws.GetRow(row).GetCell(5) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(5))),
                            Content_1 = (ws.GetRow(row).GetCell(6) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(6))),
                            Content_2 = (ws.GetRow(row).GetCell(7) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(7))),
                            Content_3 = (ws.GetRow(row).GetCell(8) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(8))),
                            Content_4 = (ws.GetRow(row).GetCell(9) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(9))),
                            Content_5 = (ws.GetRow(row).GetCell(10) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(10))),
                            Custom_1 = (ws.GetRow(row).GetCell(11) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(11))),
                            Custom_2 = (ws.GetRow(row).GetCell(12) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(12))),
                            Custom_3 = (ws.GetRow(row).GetCell(13) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(13))),
                            Custom_4 = (ws.GetRow(row).GetCell(14) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(14))),
                            Custom_5 = (ws.GetRow(row).GetCell(15) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(15))),
                            Custom_6 = (ws.GetRow(row).GetCell(16) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(16))),
                            Custom_7 = (ws.GetRow(row).GetCell(17) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(17))),
                            Custom_8 = (ws.GetRow(row).GetCell(18) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(18))),
                            Custom_9 = (ws.GetRow(row).GetCell(19) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(19))),
                            Custom_10 = (ws.GetRow(row).GetCell(20) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(20))),
                            Tags = (ws.GetRow(row).GetCell(21) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(21))),
                            ZoneId = (ws.GetRow(row).GetCell(23) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(23)))),
                            Order = (ws.GetRow(row).GetCell(24) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(24))))
                        };

                        if (ws.GetRow(row).GetCell(22) != null)
                        {
                            importData.Date_1 = df.FormatCellValue(ws.GetRow(row).GetCell(22));
                        }

                        dataList.Add(importData);
                    }

                    int count = 0;

                    var eventCategories = dataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "TR" && w.ParentIdString.Contains("0")).ToList();
                    foreach (var eventCategory in eventCategories)
                    {
                        var articleCategory = SaveArticleData(eventCategory);
                        if (articleCategory != null)
                        {
                            count++;
                            eventCategory.ArticleId = articleCategory.Id;
                            addedDataList.Add(eventCategory);

                            //tr alt kategori ekleme
                            var eventSubCategories = dataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "TR" && w.ParentIdString.Contains(eventCategory.Id.ToString())).ToList();
                            foreach (var eventSubCategory in eventSubCategories)
                            {
                                eventSubCategory.Custom_1 = articleCategory.Id.ToString();
                                var articleSubCategory = SaveArticleData(eventSubCategory);
                                if (articleSubCategory != null)
                                {
                                    count++;
                                    eventSubCategory.ArticleId = articleSubCategory.Id;
                                    addedDataList.Add(eventSubCategory);
                                }
                                else
                                {
                                    errorDataList.Add(eventSubCategory);
                                }
                            }


                            //ilişkili en kategori çekildi
                            var categoryEn = dataList.FirstOrDefault(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "EN" && w.Id == eventCategory.Id);
                            if (categoryEn != null)
                            {
                                var articleCategoryEn = SaveArticleData(categoryEn);
                                if (articleCategoryEn != null)
                                {
                                    count++;
                                    categoryEn.ArticleId = articleCategoryEn.Id;
                                    addedDataList.Add(categoryEn);

                                    eventSubCategories = dataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == categoryEn.Lang && w.ParentIdString.Contains(categoryEn.Id.ToString())).ToList();
                                    foreach (var eventSubCategory in eventSubCategories)
                                    {
                                        eventSubCategory.Custom_1 = articleCategoryEn.Id.ToString();
                                        var articleSubCategory = SaveArticleData(eventSubCategory);
                                        if (articleSubCategory != null)
                                        {
                                            count++;
                                            eventSubCategory.ArticleId = articleSubCategory.Id;
                                            addedDataList.Add(eventSubCategory);
                                        }
                                        else
                                        {
                                            errorDataList.Add(eventSubCategory);
                                        }
                                    }
                                }
                                else
                                {
                                    errorDataList.Add(categoryEn);
                                }
                            }

                        }
                        else
                        {
                            errorDataList.Add(eventCategory);
                        }
                    }

                    #region kapalı

                    //eventCategories = addedDataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == "TR" && w.ParentIdString.Contains("0")).ToList();
                    //foreach (var eventCategory in eventCategories)
                    //{
                    //    var eventSubCategories = addedDataList.Where(w => w.ClassificationId == etkinlikKategoriClassification && w.Lang == eventCategory.Lang && w.ParentIdString.Contains(eventCategory.Id.ToString())).ToList();
                    //    foreach (var eventSubCategory in eventSubCategories)
                    //    {
                    //        var eventsList = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == eventSubCategory.Lang && w.ParentIdString.Contains(eventSubCategory.Id.ToString())).ToList();
                    //        foreach (var item in eventsList)
                    //        {
                    //            //event kategori ekleme
                    //            item.Custom_10 = eventSubCategory.ArticleId.ToString();

                    //            var article = SaveArticleData(item);
                    //            if (article != null)
                    //            {
                    //                var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(item.Id.ToString()) && w.Lang == item.Lang).ToList();
                    //                foreach (var itemProgram in eventsProgram)
                    //                {
                    //                    itemProgram.Custom_10 = article.Id.ToString();
                    //                    var programArticle = SaveArticleData(itemProgram);
                    //                }

                    //                //ingilizce data çekildi
                    //                var getEnDataFromExcel = dataList.FirstOrDefault(f => f.ClassificationId == etkinlikClassification && f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
                    //                if (getEnDataFromExcel != null)
                    //                {
                    //                    var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
                    //                    if (zoneEn != null)
                    //                    {
                    //                        //en kategori get
                    //                        var enCategory = addedDataList.FirstOrDefault(f => f.ClassificationId == etkinlikKategoriClassification && f.Id == eventSubCategory.Id && f.Lang == "EN");
                    //                        if (enCategory != null)
                    //                        {
                    //                            //event kategori ekleme
                    //                            getEnDataFromExcel.Custom_10 = enCategory.ArticleId.ToString();
                    //                        }

                    //                        //ingilizce data kayıt edildi.
                    //                        var enData = SaveArticleData(getEnDataFromExcel);
                    //                        if (enData != null)
                    //                        {
                    //                            //ingilizce data program 
                    //                            var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(getEnDataFromExcel.Id.ToString())  && w.Lang == getEnDataFromExcel.Lang).ToList();
                    //                            foreach (var itemProgram in eventsProgramEn)
                    //                            {
                    //                                itemProgram.Custom_10 = enData.Id.ToString();
                    //                                var programArticleEn = SaveArticleData(itemProgram);
                    //                            }

                    //                            //trdata revizyon cekiliyor
                    //                            var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
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
                    //                                langRelFL.ArticleID = article.Id;
                    //                                langRelFL.ZoneID = item.ZoneId;
                    //                                dbContext.LanguageRelations.Add(langRelFL);
                    //                                if (dbContext.SaveChanges() > 0)
                    //                                {
                    //                                    //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
                    //                                    var langRelFLRev = new LanguageRelationRevision();
                    //                                    langRelFLRev.LanguageRelationID = langRelId;
                    //                                    langRelFLRev.ArticleID = article.Id;
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
                    //                    else
                    //                    {
                    //                        TempData["HasError"] = true;
                    //                        TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + getEnDataFromExcel.ZoneId;
                    //                    }
                    //                }

                    //            }
                    //            else
                    //            {
                    //                errorDataList.Add(item);
                    //            }
                    //        }
                    //    }

                    //    alt kategorisiz olan etkinlikler

                    //    var eventList = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == eventCategory.Lang && w.ParentIdString.Contains(eventCategory.Id.ToString())).ToList();
                    //    foreach (var item in eventList)
                    //    {
                    //        //event kategori ekleme
                    //        item.Custom_10 = eventCategory.ArticleId.ToString();

                    //        var article = SaveArticleData(item);
                    //        if (article != null)
                    //        {
                    //            var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(item.Id.ToString()) && w.Lang == item.Lang).ToList();
                    //            foreach (var itemProgram in eventsProgram)
                    //            {
                    //                itemProgram.Custom_10 = article.Id.ToString();
                    //                var programArticle = SaveArticleData(itemProgram);
                    //            }

                    //            //ingilizce data çekildi
                    //            var getEnDataFromExcel = dataList.FirstOrDefault(f => f.ClassificationId == etkinlikClassification && f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
                    //            if (getEnDataFromExcel != null)
                    //            {
                    //                var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
                    //                if (zoneEn != null)
                    //                {
                    //                    //en kategori get
                    //                    var enCategory = addedDataList.FirstOrDefault(f => f.ClassificationId == etkinlikKategoriClassification && f.Id == eventCategory.Id && f.Lang == "EN");
                    //                    if (enCategory != null)
                    //                    {
                    //                        //event kategori ekleme
                    //                        getEnDataFromExcel.Custom_10 = enCategory.ArticleId.ToString();
                    //                    }

                    //                    //ingilizce data kayıt edildi.
                    //                    var enData = SaveArticleData(getEnDataFromExcel);
                    //                    if (enData != null)
                    //                    {
                    //                        //ingilizce data program 
                    //                        var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(getEnDataFromExcel.Id.ToString()) && w.Lang == getEnDataFromExcel.Lang).ToList();
                    //                        foreach (var itemProgram in eventsProgramEn)
                    //                        {
                    //                            itemProgram.Custom_10 = enData.Id.ToString();
                    //                            var programArticleEn = SaveArticleData(itemProgram);
                    //                        }

                    //                        //trdata revizyon cekiliyor
                    //                        var getTrRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
                    //                        if (getTrRev != null)
                    //                        {
                    //                            int langRelId = 1;

                    //                            var lastLang = dbContext.LanguageRelations.OrderBy(o => o.LanguageRelationID).ToList().LastOrDefault();
                    //                            if (lastLang != null)
                    //                            {
                    //                                langRelId = Convert.ToInt32(lastLang.LanguageRelationID) + 1;
                    //                            }

                    //                            //turkce ingilizceye eşitleniyor
                    //                            var langRelFL = new LanguageRelation();
                    //                            langRelFL.LanguageRelationID = langRelId;
                    //                            langRelFL.ArticleID = article.Id;
                    //                            langRelFL.ZoneID = item.ZoneId;
                    //                            dbContext.LanguageRelations.Add(langRelFL);
                    //                            if (dbContext.SaveChanges() > 0)
                    //                            {
                    //                                //turkce ingilizceye eşitleniyor revision kaydı yapılıyor.
                    //                                var langRelFLRev = new LanguageRelationRevision();
                    //                                langRelFLRev.LanguageRelationID = langRelId;
                    //                                langRelFLRev.ArticleID = article.Id;
                    //                                langRelFLRev.ZoneID = item.ZoneId;
                    //                                langRelFLRev.RevisionID = getTrRev.RevisionId;
                    //                                dbContext.LanguageRelationRevisions.Add(langRelFLRev);

                    //                                if (dbContext.SaveChanges() > 0)
                    //                                {
                    //                                    //endata revizyon cekiliyor
                    //                                    var getEnRev = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == enData.Id && f.RevisionStatus == "L");
                    //                                    if (getEnRev != null)
                    //                                    {
                    //                                        //ingilzce turkceye eşitleniyor
                    //                                        var langRelLF = new LanguageRelation();
                    //                                        langRelLF.LanguageRelationID = langRelId;
                    //                                        langRelLF.ArticleID = enData.Id;
                    //                                        langRelLF.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
                    //                                        dbContext.LanguageRelations.Add(langRelLF);
                    //                                        if (dbContext.SaveChanges() > 0)
                    //                                        {
                    //                                            //ingilzce turkceye eşitleniyor revision kaydı yapılıyor.
                    //                                            var langRelLFRev = new LanguageRelationRevision();
                    //                                            langRelLFRev.LanguageRelationID = langRelId;
                    //                                            langRelLFRev.ArticleID = enData.Id;
                    //                                            langRelLFRev.ZoneID = Convert.ToInt32(getEnDataFromExcel.ZoneId);
                    //                                            langRelLFRev.RevisionID = getEnRev.RevisionId;
                    //                                            dbContext.LanguageRelationRevisions.Add(langRelLFRev);
                    //                                            dbContext.SaveChanges();
                    //                                        }
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    TempData["HasError"] = true;
                    //                    TempData["Message"] = "Excel içerisindeki Zone Bulunamadı. ZoneId: " + getEnDataFromExcel.ZoneId;
                    //                }
                    //            }

                    //        }
                    //        else
                    //        {
                    //            errorDataList.Add(item);
                    //        }
                    //    }
                    //}
                    #endregion

                    //kategorisiz 
                    //var events = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == "TR" && w.ParentIdString.Contains("0")).ToList();
                    var events = dataList.Where(w => w.ClassificationId == etkinlikClassification && w.Lang == "TR").ToList();
                    foreach (var item in events)
                    {
                        var categories = addedDataList.Where(w => w.Lang == item.Lang && item.ParentIdString.Contains(w.Id.ToString())).ToList();
                        if (categories != null)
                        {
                            item.Custom_10 = string.Join(",", categories.Select(s => s.ArticleId).ToArray());
                        }
                        var article = SaveArticleData(item);
                        if (article != null)
                        {
                            count++;
                            var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(item.Id.ToString()) && w.Lang == item.Lang).ToList();
                            foreach (var itemProgram in eventsProgram)
                            {
                                itemProgram.Custom_10 = article.Id.ToString();
                                var programArticle = SaveArticleData(itemProgram);
                                count++;
                            }

                            //ingilizce data çekildi
                            var getEnDataFromExcel = dataList.FirstOrDefault(f => f.ClassificationId == etkinlikClassification && f.Lang == "EN" && !string.IsNullOrEmpty(f.Headline) && f.Id == item.Id);
                            if (getEnDataFromExcel != null)
                            {
                                var zoneEn = dbContext.Zones.FirstOrDefault(f => f.Id == getEnDataFromExcel.ZoneId);
                                if (zoneEn != null)
                                {
                                    categories = addedDataList.Where(w => w.Lang == getEnDataFromExcel.Lang && getEnDataFromExcel.ParentIdString.Contains(w.Id.ToString())).ToList();
                                    if (categories != null)
                                    {
                                        getEnDataFromExcel.Custom_10 = string.Join(",", categories.Select(s => s.ArticleId).ToArray());
                                    }
                                    //ingilizce data kayıt edildi.
                                    var enData = SaveArticleData(getEnDataFromExcel);
                                    if (enData != null)
                                    {
                                        count++;
                                        //ingilizce data program 
                                        var eventsProgramEn = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(getEnDataFromExcel.Id.ToString()) && w.Lang == getEnDataFromExcel.Lang).ToList();
                                        foreach (var itemProgram in eventsProgramEn)
                                        {
                                            itemProgram.Custom_10 = enData.Id.ToString();
                                            var programArticleEn = SaveArticleData(itemProgram);
                                            count++;
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
                        else
                        {
                            errorDataList.Add(item);
                        }
                    }
                    TempData["HasError"] = false;
                    TempData["Message"] = "Bulk import complete " + count + " articles created.";

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return View("~/Views/CMSPlugins/IKSV/Bulk/import.cshtml");
        }

        #endregion


        public ActionResult ProgramImport()
        {
            int[] zoneGroups = { 83, 84 };
            using (CmsDbContext dbContext = new CmsDbContext())
            {
                ViewBag.ZoneList = dbContext.Zones.Where(w => zoneGroups.Contains(w.ZoneGroupId)).ToList();
            }
            return View("~/Views/CMSPlugins/IKSV/Bulk/ProgramImport.cshtml");
        }

        [HttpPost]
        public ActionResult ProgramImport(FormCollection collection)
        {
            var zoneId = 0;
            if (string.IsNullOrEmpty(collection["zoneid"].ToString()))
            {
                TempData["HasError"] = true;
                TempData["Message"] = "ZoneId hatalı.";
            }
            else
            {
                CmsDbContext dbContext = new CmsDbContext();
                zoneId = Convert.ToInt32(collection["zoneid"].ToString());

                var datas = dbContext.vArticlesZonesFulls.Where(w => w.ClassificationID == etkinlikClassification && w.ZoneID == zoneId && w.Status == 1 && !w.Flag1).ToList();

                var workbook = new HSSFWorkbook();
                var sheet = workbook.CreateSheet("etkinlikProgram");
                var sheetRowIndex = 0;
                var sheetRow = sheet.CreateRow(sheetRowIndex);
                sheetRow.CreateCell(0).SetCellValue("Id");
                sheetRow.CreateCell(1).SetCellValue("ParentId");
                sheetRow.CreateCell(2).SetCellValue("ClassificationId");
                sheetRow.CreateCell(3).SetCellValue("LangId");
                sheetRow.CreateCell(4).SetCellValue("Headline");
                sheetRow.CreateCell(5).SetCellValue("Summary");
                sheetRow.CreateCell(6).SetCellValue("Content_1");
                sheetRow.CreateCell(7).SetCellValue("Content_2");
                sheetRow.CreateCell(8).SetCellValue("Content_3");
                sheetRow.CreateCell(9).SetCellValue("Content_4");
                sheetRow.CreateCell(10).SetCellValue("Content_5");
                sheetRow.CreateCell(11).SetCellValue("Custom_1");
                sheetRow.CreateCell(12).SetCellValue("Custom_2");
                sheetRow.CreateCell(13).SetCellValue("Custom_3");
                sheetRow.CreateCell(14).SetCellValue("Custom_4");
                sheetRow.CreateCell(15).SetCellValue("Custom_5");
                sheetRow.CreateCell(16).SetCellValue("Custom_6");
                sheetRow.CreateCell(17).SetCellValue("Custom_7");
                sheetRow.CreateCell(18).SetCellValue("Custom_8");
                sheetRow.CreateCell(19).SetCellValue("Custom_9");
                sheetRow.CreateCell(20).SetCellValue("Custom_10");
                sheetRow.CreateCell(21).SetCellValue("Tags");
                sheetRow.CreateCell(22).SetCellValue("Date_1");
                sheetRow.CreateCell(23).SetCellValue("ZoneId");
                sheetRow.CreateCell(24).SetCellValue("Order");

                sheetRowIndex++;
                foreach (var item in datas)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(item.ArticleID);
                    sheetRow.CreateCell(1).SetCellValue("");
                    sheetRow.CreateCell(2).SetCellValue(item.ClassificationID);
                    sheetRow.CreateCell(3).SetCellValue(item.LanguageID);
                    sheetRow.CreateCell(4).SetCellValue(item.Headline);
                    sheetRow.CreateCell(23).SetCellValue(item.ZoneID);
                    sheetRow.CreateCell(24).SetCellValue(item.AzOrder);

                    sheetRowIndex++;
                }
                // Save the Excel spreadsheet to a MemoryStream and return it to the client
                using (var exportData = new MemoryStream())
                {

                    workbook.Write(exportData);
                    string saveAsFileName = string.Format("etkinlikProgram-{0:d}.xls", DateTime.Now.ToString("MM_dd_yyyy")).Replace("/", "-");
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                    Response.Clear();
                    Response.BinaryWrite(exportData.GetBuffer());
                    Response.End();
                }
            }
            return View("~/Views/CMSPlugins/IKSV/Bulk/ProgramImport.cshtml");
        }

        public ActionResult SaveProgramImport()
        {
            return View("~/Views/CMSPlugins/IKSV/Bulk/SaveProgramImport.cshtml");
        }

        [HttpPost]
        public ActionResult SaveProgramImport(FormCollection collection)
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

                    Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                    CmsDbContext dbContext = new CmsDbContext();

                    List<DataImportModel> dataList = new List<DataImportModel>();
                    int count = 0;

                    IWorkbook wb = WorkbookFactory.Create(new FileStream(
                               Path.GetFullPath(path),
                               FileMode.Open, FileAccess.Read,
                               FileShare.ReadWrite));

                    ISheet ws = wb.GetSheetAt(0);

                    for (int row = 1; row <= ws.LastRowNum; row++)
                    {
                        var df = new DataFormatter();
                        var importData = new DataImportModel
                        {
                            Id = (ws.GetRow(row).GetCell(0) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(0)))),
                            ParentIdString = (ws.GetRow(row).GetCell(1) == null ? null : df.FormatCellValue(ws.GetRow(row).GetCell(1)).Split(',').ToArray()),
                            ClassificationId = (ws.GetRow(row).GetCell(2) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(2)))),
                            Lang = (ws.GetRow(row).GetCell(3) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(3))),
                            Headline = (ws.GetRow(row).GetCell(4) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(4))),
                            Summary = (ws.GetRow(row).GetCell(5) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(5))),
                            Content_1 = (ws.GetRow(row).GetCell(6) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(6))),
                            Content_2 = (ws.GetRow(row).GetCell(7) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(7))),
                            Content_3 = (ws.GetRow(row).GetCell(8) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(8))),
                            Content_4 = (ws.GetRow(row).GetCell(9) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(9))),
                            Content_5 = (ws.GetRow(row).GetCell(10) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(10))),
                            Custom_1 = (ws.GetRow(row).GetCell(11) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(11))),
                            Custom_2 = (ws.GetRow(row).GetCell(12) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(12))),
                            Custom_3 = (ws.GetRow(row).GetCell(13) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(13))),
                            Custom_4 = (ws.GetRow(row).GetCell(14) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(14))),
                            Custom_5 = (ws.GetRow(row).GetCell(15) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(15))),
                            Custom_6 = (ws.GetRow(row).GetCell(16) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(16))),
                            Custom_7 = (ws.GetRow(row).GetCell(17) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(17))),
                            Custom_8 = (ws.GetRow(row).GetCell(18) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(18))),
                            Custom_9 = (ws.GetRow(row).GetCell(19) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(19))),
                            Custom_10 = (ws.GetRow(row).GetCell(20) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(20))),
                            Tags = (ws.GetRow(row).GetCell(21) == null ? "" : df.FormatCellValue(ws.GetRow(row).GetCell(21))),
                            ZoneId = (ws.GetRow(row).GetCell(23) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(23)))),
                            Order = (ws.GetRow(row).GetCell(24) == null ? 0 : Convert.ToInt32(df.FormatCellValue(ws.GetRow(row).GetCell(24))))
                        };

                        if (ws.GetRow(row).GetCell(22) != null)
                        {
                            importData.Date_1 = df.FormatCellValue(ws.GetRow(row).GetCell(22));
                        }

                        dataList.Add(importData);
                    }

                    var events = dataList.Where(w => w.ClassificationId == etkinlikClassification).ToList();
                    foreach (var item in events)
                    {
                        if (item.Id > 0)
                        {
                            var eventsProgram = dataList.Where(w => w.ClassificationId == etkinlikProgramClassification && w.ParentIdString.Contains(item.Id.ToString()) && w.Lang == item.Lang).ToList();
                            foreach (var itemProgram in eventsProgram)
                            {
                                itemProgram.Custom_10 = item.Id.ToString();
                                var programArticle = SaveArticleData(itemProgram);
                                count++;
                            }
                        }
                    }
                    TempData["HasError"] = false;
                    TempData["Message"] = "Bulk import complete " + count + " articles created.";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View("~/Views/CMSPlugins/IKSV/Bulk/SaveProgramImport.cshtml");
        }

        private Article SaveArticleData(DataImportModel item)
        {
            try
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
                                    az.AzAlias = (a.ClassificationId == etkinlikProgramClassification ? alias + (item.Lang == "TR" ? "-program" : "-programme") : alias);
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
            catch (Exception)
            {
                return null;
            }

        }

    }
}