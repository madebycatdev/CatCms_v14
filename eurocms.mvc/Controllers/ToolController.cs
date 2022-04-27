using EuroCMS.Admin.Common;
using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.IO;
using EuroCMS.Admin.Models;
using EuroCMS.Admin.ViewModels;
using System.Data.Entity;

namespace EuroCMS.Admin.Controllers
{
    public class ToolController : BaseController
    {
        ArticleOrderDbContext context = new ArticleOrderDbContext();
        CmsDbContext dbContext = new CmsDbContext();
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult ckfinder()
        {
            return View();
        }

        [CmsAuthorize(Roles = "Editor,ContentManager,ContentEntry,UserCreator", ContentType = "FileManager")]
        public ActionResult FileManager(int? id)
        {
            if (id.HasValue)
            {
                CmsDbContext dbContext = new CmsDbContext();
                var site = dbContext.Sites.FirstOrDefault(f => f.Id == id.Value);
                //ViewBag.Site = site.FilePath;
                Session["Site"] = site.FilePath;
            }
            else
            {
                //ViewBag.Site = "/";
                Session["Site"] = "";
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator")]
        public ActionResult ArticleFileRevision()
        {
            return View();
        }

        public int deletedArticleFileCount = 0, deletedArticleFileDirCount = 0;

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator")]
        public ActionResult ArticleFileRevisionDelete(FormCollection collection)
        {
            try
            {
                TempData.Clear();
                int revisionCount = !string.IsNullOrEmpty(collection["RevisionCount"]) ? Convert.ToInt32(collection["RevisionCount"].Trim()) : 3;
                revisionCount = revisionCount < 3 ? 3 : revisionCount;
                CmsDbContext dbContext = new CmsDbContext();
                dbContext.Configuration.AutoDetectChangesEnabled = false;
                dbContext.Configuration.ValidateOnSaveEnabled = false;
                List<int> listArticleId = new List<int>();
                listArticleId = dbContext.FileRevisions.Select(s => s.ArticleId).Distinct().ToList();
                foreach (int articleID in listArticleId)
                {
                    List<ArticleFileRevision> listFileRevision = new List<Model.ArticleFileRevision>();
                    listFileRevision = dbContext.FileRevisions.Where(s => s.ArticleId == articleID && s.RevisionStatus != "L").OrderByDescending(od => od.RevisionDate).ToList();
                    if (listFileRevision.Count > (revisionCount - 1))
                    {
                        listFileRevision = listFileRevision.Skip(revisionCount).ToList();
                        foreach (ArticleFileRevision articleFileRev in listFileRevision)
                        {
                            List<ArticleFileRevisionFile> listFileRevisionFile = new List<ArticleFileRevisionFile>();
                            listFileRevisionFile = dbContext.FileRevisionFiles.Where(s => s.ArticleId == articleID && s.RevisionId == articleFileRev.RevisionId).ToList();
                            if (listFileRevisionFile != null)
                            {
                                int i = 0;
                                foreach (ArticleFileRevisionFile revisionFile in listFileRevisionFile)
                                {
                                    DeleteArticleFilesByArticleFileRev(revisionFile);
                                    dbContext.FileRevisionFiles.Attach(revisionFile);
                                    dbContext.FileRevisionFiles.Remove(revisionFile);
                                    dbContext.SaveChanges();
                                }
                            }
                            dbContext.FileRevisions.Attach(articleFileRev);
                            dbContext.FileRevisions.Remove(articleFileRev);
                            dbContext.SaveChanges();
                        }
                    }
                }

                TempData["HasError"] = false;
                TempData["Message"] = "Article Files Revisions has been successfully deleted." + Environment.NewLine + deletedArticleFileDirCount.ToString() + " directories and " + deletedArticleFileCount.ToString() + " files deleted";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("ArticleFileRevision");
        }

        public void DeleteArticleFilesByArticleFileRev(ArticleFileRevisionFile revisionFile)
        {
            string tmpPath = "/i/tmp/", contentPath = "/i/content/";
            try
            {
                if (revisionFile == null)
                {
                    return;
                }

                contentPath = contentPath + revisionFile.ArticleId.ToString() + "_";
                tmpPath = tmpPath + revisionFile.RevisionId.ToString();
                string fileTmpPath = tmpPath + "/" + revisionFile.ArticleId.ToString() + "_";

                string file1Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File1);
                string file2Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File2);
                string file3Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File3);
                string file4Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File4);
                string file5Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File5);
                string file6Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File6);
                string file7Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File7);
                string file8Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File8);
                string file9Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File9);
                string file10Tmp = HttpContext.Server.MapPath(fileTmpPath + revisionFile.File10);

                //string file1Content = HttpContext.Server.MapPath(contentPath + revisionFile.File1);
                //string file2Content = HttpContext.Server.MapPath(contentPath + revisionFile.File2);
                //string file3Content = HttpContext.Server.MapPath(contentPath + revisionFile.File3);
                //string file4Content = HttpContext.Server.MapPath(contentPath + revisionFile.File4);
                //string file5Content = HttpContext.Server.MapPath(contentPath + revisionFile.File5);
                //string file6Content = HttpContext.Server.MapPath(contentPath + revisionFile.File6);
                //string file7Content = HttpContext.Server.MapPath(contentPath + revisionFile.File7);
                //string file8Content = HttpContext.Server.MapPath(contentPath + revisionFile.File8);
                //string file9Content = HttpContext.Server.MapPath(contentPath + revisionFile.File9);
                //string file10Content = HttpContext.Server.MapPath(contentPath + revisionFile.File10);


                DeleteFile(file1Tmp);
                DeleteFile(file2Tmp);
                DeleteFile(file3Tmp);
                DeleteFile(file4Tmp);
                DeleteFile(file5Tmp);
                DeleteFile(file6Tmp);
                DeleteFile(file7Tmp);
                DeleteFile(file8Tmp);
                DeleteFile(file9Tmp);
                DeleteFile(file10Tmp);
                //DeleteFile(file1Content);
                //DeleteFile(file2Content);
                //DeleteFile(file3Content);
                //DeleteFile(file4Content);
                //DeleteFile(file5Content);
                //DeleteFile(file6Content);
                //DeleteFile(file7Content);
                //DeleteFile(file8Content);
                //DeleteFile(file9Content);
                //DeleteFile(file10Content);

                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(HttpContext.Server.MapPath(tmpPath));
                    IEnumerable<FileInfo> listFiles;
                    listFiles = directoryInfo.EnumerateFiles("*");
                    if (listFiles == null)
                    {
                        System.IO.Directory.Delete(HttpContext.Server.MapPath(tmpPath));
                        deletedArticleFileDirCount++;
                    }
                    else if (listFiles.Count() <= 0)
                    {
                        System.IO.Directory.Delete(HttpContext.Server.MapPath(tmpPath));
                        deletedArticleFileDirCount++;
                    }
                }
                catch (Exception exDirDelete)
                {
                    CmsHelper.SaveErrorLog(exDirDelete, string.Empty, true);
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
                return;
            }
        }

        public bool DeleteFile(string path)
        {
            bool isDeleted = false;
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    deletedArticleFileCount++;
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }
            return isDeleted;
        }

        [CmsAuthorize(Roles = "PowerUser")]
        public ActionResult BulkImport()
        {
            return View();
        }

        [CmsAuthorize(Roles = "PowerUser")]
        public ActionResult BulkArticleCreate(FormCollection collection)
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
                    string connectionString = string.Empty;
                    if (file.FileName.Contains(".xml"))
                    {
                        GetBulkDataFromXml(path);
                    }
                    else
                    {
                        if (file.FileName.Contains(".xlsx"))
                        {
                            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
                        }
                        else
                        {
                            //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=YES\";";
                            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "; Extended Properties=\"Excel 8.0;HDR=Yes;\";";
                        }

                        OleDbConnection conn = new OleDbConnection(connectionString);
                        conn.Open();
                        DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();

                        string strSQL = "SELECT * FROM [" + firstSheetName + "]";
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                        DataSet ds = new DataSet();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(ds);

                        Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                        CmsDbContext dbContext = new CmsDbContext();

                        bool result = true;

                        #region Get Article From Excel Data
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Article a = new Article();
                            a.Headline = ds.Tables[0].Rows[i]["Headline"].ToString();
                            a.Summary = ds.Tables[0].Rows[i]["Summary"].ToString();
                            a.ClassificationId = Convert.ToInt32(ds.Tables[0].Rows[i]["ClassificationId"].ToString());
                            a.LangId = ds.Tables[0].Rows[i]["LangId"].ToString();
                            a.Article1 = ds.Tables[0].Rows[i]["Content_1"].ToString();
                            a.Article2 = ds.Tables[0].Rows[i]["Content_2"].ToString();
                            a.Custom1 = ds.Tables[0].Rows[i]["Custom_1"].ToString();
                            a.Custom2 = ds.Tables[0].Rows[i]["Custom_2"].ToString();
                            a.Custom3 = ds.Tables[0].Rows[i]["Custom_3"].ToString();
                            a.Custom4 = ds.Tables[0].Rows[i]["Custom_4"].ToString();
                            a.Custom5 = ds.Tables[0].Rows[i]["Custom_5"].ToString();
                            a.Custom6 = ds.Tables[0].Rows[i]["Custom_6"].ToString();
                            a.Custom7 = ds.Tables[0].Rows[i]["Custom_7"].ToString();
                            a.Custom8 = ds.Tables[0].Rows[i]["Custom_8"].ToString();
                            a.Custom9 = ds.Tables[0].Rows[i]["Custom_9"].ToString();
                            a.Custom10 = ds.Tables[0].Rows[i]["Custom_10"].ToString();


                            //date
                            if (ds.Tables[0].Rows[i].Table.Columns.Contains("Date_1"))
                            {
                                a.date_1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date_1"].ToString());
                            }
                            //date

                            a.Created = DateTime.Now;
                            a.CreatedBy = currentUserId;
                            a.Status = (byte)1;
                            a.Updated = DateTime.Now;
                            a.Startdate = DateTime.Now;

                            #region Tags
                            string tags = ds.Tables[0].Rows[i]["Tags"].ToString();
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

                            result = BulkSaveArticle(a, ds, i);
                        }
                        #endregion

                        if (result)
                        {
                            TempData["Message"] = "Bulk Import Completed";
                        }
                        else
                        {
                            TempData["HasError"] = true;
                            TempData["Message"] = "Bulk Import Failed";
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;
                }
            }

            return RedirectToAction("BulkImport");
        }

        private void GetBulkDataFromXml(string path)
        {
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            XmlDocument doc = new XmlDocument();
            CmsDbContext dbContext = new CmsDbContext();
            bool result = true;
            doc.Load(path);

            //doc.GetElementsByTagName("article")[0]["Headline"].InnerText
            for (int i = 0; i < doc.GetElementsByTagName("article").Count; i++)
            {
                Article a = new Article();
                a.Headline = doc.GetElementsByTagName("article")[i]["Headline"].InnerText;
                a.Summary = doc.GetElementsByTagName("article")[i]["Summary"].InnerText;
                a.ClassificationId = Convert.ToInt32(doc.GetElementsByTagName("article")[i]["ClassificationId"].InnerText);
                a.LangId = doc.GetElementsByTagName("article")[i]["LangId"].InnerText;
                a.Article1 = doc.GetElementsByTagName("article")[i]["Content_1"].InnerText;
                a.Article2 = doc.GetElementsByTagName("article")[i]["Content_2"].InnerText;
                a.Custom1 = doc.GetElementsByTagName("article")[i]["Custom_1"].InnerText;
                a.Custom2 = doc.GetElementsByTagName("article")[i]["Custom_2"].InnerText;
                a.Custom3 = doc.GetElementsByTagName("article")[i]["Custom_3"].InnerText;
                a.Custom4 = doc.GetElementsByTagName("article")[i]["Custom_4"].InnerText;
                a.Custom5 = doc.GetElementsByTagName("article")[i]["Custom_5"].InnerText;
                a.Custom6 = doc.GetElementsByTagName("article")[i]["Custom_6"].InnerText;
                a.Custom7 = doc.GetElementsByTagName("article")[i]["Custom_7"].InnerText;
                a.Custom8 = doc.GetElementsByTagName("article")[i]["Custom_8"].InnerText;
                a.Custom9 = doc.GetElementsByTagName("article")[i]["Custom_9"].InnerText;
                a.Custom10 = doc.GetElementsByTagName("article")[i]["Custom_10"].InnerText;

                a.Created = DateTime.Now;
                a.CreatedBy = currentUserId;
                a.Status = (byte)1;
                a.Updated = DateTime.Now;
                a.Startdate = DateTime.Now;

                #region Tags
                string tags = doc.GetElementsByTagName("article")[i]["Tags"].InnerText;
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

                XmlNodeList columns = doc.SelectNodes("articles/article")[0].ChildNodes;

                dbContext.Articles.Add(a);
                if (dbContext.SaveChanges() > 0)
                {
                    ArticleRevision ar = new ArticleRevision();
                    ar.Headline = a.Headline;
                    ar.Summary = a.Summary;
                    ar.ClassificationId = a.ClassificationId;
                    ar.LangId = a.LangId;
                    ar.Article1 = a.Article1;
                    ar.Article2 = a.Article2;
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
                        foreach (XmlNode dc in columns)
                        {
                            if (dc.Name.Contains("ZoneId"))
                            {
                                int zoneId = Convert.ToInt32(dc.Name.Substring(dc.Name.IndexOf("ZoneId_") + "ZoneId_".Length));
                                string alias = doc.GetElementsByTagName("article")[i]["Alias_" + zoneId].InnerText;

                                ArticleZone az = new ArticleZone();
                                az.Article = a;
                                az.ArticleID = a.Id;
                                az.AzAlias = alias;
                                az.AzOrder = 0;
                                az.IsAliasProtected = true;
                                az.ZoneID = zoneId;

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
                                    dbContext.ArticleZoneRevisions.Add(azr);

                                    if (dbContext.SaveChanges() > 0)
                                    {
                                        // hepsi tamam
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }

                if (result)
                {
                    TempData["Message"] = "Bulk Import Completed";
                }
                else
                {
                    TempData["HasError"] = true;
                    TempData["Message"] = "Bulk Import Failed";
                }
            }
        }

        private bool BulkSaveArticle(Article a, DataSet ds, int i)
        {
            CmsDbContext dbContext = new CmsDbContext();
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            DataColumnCollection columns = ds.Tables[0].Columns;
            bool result = true;

            dbContext.Articles.Add(a);
            if (dbContext.SaveChanges() > 0)
            {
                ArticleRevision ar = new ArticleRevision();
                ar.Headline = a.Headline;
                ar.Summary = a.Summary;
                ar.ClassificationId = a.ClassificationId;
                ar.LangId = a.LangId;
                ar.Article1 = a.Article1;
                ar.Article2 = a.Article2;
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


                //date
                ar.date_1 = a.date_1;
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
                    foreach (DataColumn dc in columns)
                    {
                        if (dc.ColumnName.Contains("ZoneId"))
                        {
                            //int zoneId = Convert.ToInt32(dc.ColumnName.Substring(dc.ColumnName.IndexOf("ZoneId_") + "ZoneId_".Length));
                            string zoneId = dc.ColumnName.Substring(dc.ColumnName.IndexOf("ZoneId_") + "ZoneId_".Length);

                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ZoneId_" + zoneId].ToString()))
                            {
                                int tempZoneId = Convert.ToInt32(ds.Tables[0].Rows[i]["ZoneId_" + zoneId].ToString());
                                string alias = ds.Tables[0].Rows[i]["Alias_" + zoneId].ToString();


                                ArticleZone az = new ArticleZone();
                                az.Article = a;
                                az.ArticleID = a.Id;
                                az.AzAlias = alias;
                                az.AzOrder = 0;
                                az.IsAliasProtected = true;
                                az.ZoneID = tempZoneId;

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
                                    dbContext.ArticleZoneRevisions.Add(azr);

                                    if (dbContext.SaveChanges() > 0)
                                    {
                                        if (string.IsNullOrEmpty(az.AzAlias))
                                        {
                                            alias = CmsHelper.CreateAliasWithUrlStructure(a.Id, Convert.ToInt32(tempZoneId));
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
                                        result = false;
                                    }
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                            //else
                            //{
                            //    result = false;
                            //}

                        }
                    }
                    #endregion
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

    }
}
