using EuroCMS.Admin.entity;
using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Configuration;
using EuroCMS.Model;
using static EuroCMS.Admin.Controllers.ArticleController;

namespace EuroCMS.Admin.Controllers
{
    public class ArticleFileController : BaseController
    {
        ArticleFileDbContext context = new ArticleFileDbContext();

        [CmsAuthorize(Roles = "ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Index(int ArticleId, int RevisionId, long? FileRevisionId)
        {
            List<cms_article_files_revision_files> articleFiles = new List<cms_article_files_revision_files>();
            List<cms_asp_admin_select_article_files_revision_list_Result> revisions = new List<cms_asp_admin_select_article_files_revision_list_Result>();
            try
            {
                revisions = context.SelectArticleFilesRevisions(ArticleId);
                ViewData["article_file_revisions"] = revisions;

                if (FileRevisionId == null)
                {
                    var lastFileRevision =
                            context.SelectArticleFileLastRevision(ArticleId)
                            .FirstOrDefault();

                    if (lastFileRevision == null)
                        FileRevisionId = -1;
                    else
                        FileRevisionId = lastFileRevision.rev_id;
                }

                articleFiles = context.SelectArticleFiles(FileRevisionId ?? 0);

                ViewBag.ArticleId = ArticleId;
                ViewBag.FileRevisionId = FileRevisionId;
                ViewBag.RevisionId = RevisionId;

                bool Livem = false;
                List<entity.cms_asp_admin_select_article_files_revision_list_Result> listm = revisions;

                foreach (entity.cms_asp_admin_select_article_files_revision_list_Result item in listm)
                {
                    if (item.revision_status == "L" && FileRevisionId == item.rev_id)
                    {
                        Livem = true;
                        ViewBag.RevisionStatus = "L";
                    }
                }

                if (!Livem)
                {
                    ViewBag.RevisionStatus = "N";
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                string errorDetail = ex.Message + " - " + ex.InnerException + " - Line: " + line.ToString() + " - FullStackTrace: " + ex.StackTrace;
                ModelState.AddModelError("HATA", errorDetail);
            }
            return View(articleFiles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser,Editor,ContentManager,ContentEntry,UserCreator", Permission = "Discard", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Discard(int ArticleId, int RevisionId, int FileRevisionId)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_FILE_DISCARD, this));

            string rCode = context.DiscardArticleFiles(FileRevisionId, ArticleId).FirstOrDefault().rCode.ToString();

            try
            {
                if (rCode == "OK")
                {
                    TempData["Message"] = "Your article files revision is discarded.";
                }
                else
                    throw new ApplicationException("Your article file resivison is not aviable for discard operation");
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index", new { ArticleId, RevisionId, FileRevisionId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser,Editor,ContentManager,ContentEntry,UserCreator", Permission = "Approve", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Approve(int ArticleId, int RevisionId, int FileRevisionId)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_FILE_APPROVE, this));

            string filename = string.Empty;
            int pref = 0;
            int preflen = 0;

            try
            {

                string aStat = context.ApproveArticleFiles(FileRevisionId, Membership.GetUser().ProviderUserKey).FirstOrDefault().aStat.ToString();

                if (aStat == "OK")
                {
                    string tmpsfolder = Server.MapPath("/i/tmp/" + FileRevisionId);
                    string contentfolder = Server.MapPath("/i/content");

                    bool isExists = System.IO.Directory.Exists(tmpsfolder);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(tmpsfolder);

                    DirectoryInfo directory = new DirectoryInfo(tmpsfolder);
                    var Files = directory.GetFiles().ToList();
                    foreach (var f in Files)
                    {
                        filename = f.Name;
                        pref = ArticleId;
                        preflen = pref.ToString().Length;
                        if (filename.Substring(0, preflen) == pref.ToString())
                        {
                            if (System.IO.Directory.Exists(contentfolder + "\\" + filename))
                            {
                                DirectoryInfo directory2 = new DirectoryInfo(contentfolder + "\\" + filename);
                                directory2.Delete();
                            }
                        }
                    }

                    var FileNames = "";
                    foreach (var f in Files)
                    {
                        filename = f.Name;
                        pref = ArticleId;
                        preflen = pref.ToString().Length;
                        if (filename.Substring(0, preflen) == pref.ToString())
                        {
                            if (System.IO.File.Exists(Path.Combine(tmpsfolder, filename)))
                            {

                                FileNames += f.Name + "\n";
                                System.IO.File.Copy(Path.Combine(tmpsfolder, filename), Path.Combine(contentfolder, filename), true);
                            }
                        }
                    }

                    TempData["Message"] = "Your article files revision is approved and published.\nPublished files:\n--------------------------------\n" + FileNames;
                }
                else if (aStat == "OKA")
                {
                    throw new ApplicationException("Your article files revision is approved and ready for administrator approval.");
                }
                else if (aStat == "NOT_AVAILABLE")
                {
                    throw new ApplicationException("Your article files are NOT available for approval. article files not found or already approved before");
                }
                else
                {
                    throw new ApplicationException("Error occured on article file approval. Error Code: " + aStat);
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index", new { ArticleId, RevisionId, FileRevisionId });
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles= "ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Create(FormCollection collection, int ArticleId, long RevisionId, int? TypeId, int? FileId, long? FileRevisionId)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_FILE_SAVE, this));

            ViewBag.FileTypes = Bags.GetFileTypes();
            ViewBag.FtpFiles = Bags.GetFtpFolderFiles();

            ViewBag.ArticleId = ArticleId;
            ViewBag.RevisionId = RevisionId;
            ViewBag.TypeId = TypeId ?? -1;
            ViewBag.FileId = FileId ?? -1;
            ViewBag.FileRevisionId = FileRevisionId ?? -1;

            #region GetValues
            //string TypeId = collection["selFileType"];
            //string ArticleId = collection["article_id"];
            //string RevisionId = collection["arid"];
            //string FileRevisionId = collection["rev_id"];
            string ArticleFileRevisionId = collection["af_rf_id"];
            string RevisionStatus = string.IsNullOrEmpty(collection["rev_status"]) ? "N" : collection["rev_status"];

            string FileComment = collection["filecomment"].Replace("\\", "_||_");
            string FileTitle = collection["filetitle"].Replace("\\", "_||_");

            string FileOrder = collection["fileorder"] ?? "0";
            string FileType = collection["selFileType"];

            ViewData["file_title"] = FileTitle;
            ViewData["file_comment"] = FileComment;
            ViewData["file_order"] = FileOrder;
            #endregion

            //long NewFileRevisionId = !string.IsNullOrEmpty(FileRevisionId) ? Convert.ToInt64(FileRevisionId) : -1;
            long NewFileRevisionId = FileRevisionId ?? -1;
            long NewArticleFileRevisionId = -1;

            string old_ArticleFileRevisionId = ArticleFileRevisionId;
            string old_FileRevisionId = FileRevisionId.ToString();

            var FileTypeDetails = context.SelectFileTypesDetails(Convert.ToInt32(TypeId)).FirstOrDefault();

            try
            {

                #region ctrlFtp
                string FtpChk1 = collection["FtpChk1"] ?? "false";
                string FtpChk2 = collection["FtpChk2"] ?? "false";
                string FtpChk3 = collection["FtpChk3"] ?? "false";
                string FtpChk4 = collection["FtpChk4"] ?? "false";
                string FtpChk5 = collection["FtpChk5"] ?? "false";
                string FtpChk6 = collection["FtpChk6"] ?? "false";
                string FtpChk7 = collection["FtpChk7"] ?? "false";
                string FtpChk8 = collection["FtpChk8"] ?? "false";
                string FtpChk9 = collection["FtpChk9"] ?? "false";
                string FtpChk10 = collection["FtpChk10"] ?? "false";

                string Ftp1 = collection["Ftp1"] ?? string.Empty;
                string Ftp2 = collection["Ftp2"] ?? string.Empty;
                string Ftp3 = collection["Ftp3"] ?? string.Empty;
                string Ftp4 = collection["Ftp4"] ?? string.Empty;
                string Ftp5 = collection["Ftp5"] ?? string.Empty;
                string Ftp6 = collection["Ftp6"] ?? string.Empty;
                string Ftp7 = collection["Ftp7"] ?? string.Empty;
                string Ftp8 = collection["Ftp8"] ?? string.Empty;
                string Ftp9 = collection["Ftp9"] ?? string.Empty;
                string Ftp10 = collection["Ftp10"] ?? string.Empty;
                #endregion

                string ftpsfolder = Server.MapPath("/i/ftp");
                string tmpsfolder = Server.MapPath("/i/tmp");

                #region Get FileType Details
                var ftDetail = context.SelectFileTypesDetails(Convert.ToInt32(TypeId)).FirstOrDefault();

                string file1 = ftDetail.file1_name;
                string file1ext = ftDetail.file1_extension;
                int file1size = ftDetail.file1_size;
                string file1_wh = ftDetail.file1_wh;

                string file2 = ftDetail.file2_name;
                string file2ext = ftDetail.file2_extension;
                int file2size = ftDetail.file2_size;
                string file2_wh = ftDetail.file2_wh;

                string file3 = ftDetail.file3_name;
                string file3ext = ftDetail.file3_extension;
                int file3size = ftDetail.file3_size;
                string file3_wh = ftDetail.file3_wh;

                string file4 = ftDetail.file4_name;
                string file4ext = ftDetail.file4_extension;
                int file4size = ftDetail.file4_size;
                string file4_wh = ftDetail.file4_wh;

                string file5 = ftDetail.file5_name;
                string file5ext = ftDetail.file5_extension;
                int file5size = ftDetail.file5_size;
                string file5_wh = ftDetail.file5_wh;

                string file6 = ftDetail.file6_name;
                string file6ext = ftDetail.file6_extension;
                int file6size = ftDetail.file6_size;
                string file6_wh = ftDetail.file6_wh;

                string file7 = ftDetail.file7_name;
                string file7ext = ftDetail.file7_extension;
                int file7size = ftDetail.file7_size;
                string file7_wh = ftDetail.file7_wh;

                string file8 = ftDetail.file8_name;
                string file8ext = ftDetail.file8_extension;
                int file8size = ftDetail.file8_size;
                string file8_wh = ftDetail.file8_wh;

                string file9 = ftDetail.file9_name;
                string file9ext = ftDetail.file9_extension;
                int file9size = ftDetail.file9_size;
                string file9_wh = ftDetail.file9_wh;

                string file10 = ftDetail.file10_name;
                string file10ext = ftDetail.file10_extension;
                int file10size = ftDetail.file10_size;
                string file10_wh = ftDetail.file10_wh;
                #endregion
                #region SetVariables
                string file_1 = string.Empty;
                string file_2 = string.Empty;
                string file_3 = string.Empty;
                string file_4 = string.Empty;
                string file_5 = string.Empty;
                string file_6 = string.Empty;
                string file_7 = string.Empty;
                string file_8 = string.Empty;
                string file_9 = string.Empty;
                string file_10 = string.Empty;

                string ufile_1 = string.Empty;
                string ufile_2 = string.Empty;
                string ufile_3 = string.Empty;
                string ufile_4 = string.Empty;
                string ufile_5 = string.Empty;
                string ufile_6 = string.Empty;
                string ufile_7 = string.Empty;
                string ufile_8 = string.Empty;
                string ufile_9 = string.Empty;
                string ufile_10 = string.Empty;

                int file_1_size = 0;
                int file_2_size = 0;
                int file_3_size = 0;
                int file_4_size = 0;
                int file_5_size = 0;
                int file_6_size = 0;
                int file_7_size = 0;
                int file_8_size = 0;
                int file_9_size = 0;
                int file_10_size = 0;

                int flg1 = 0;
                int flg2 = 0;
                int flg3 = 0;
                int flg4 = 0;
                int flg5 = 0;
                int flg6 = 0;
                int flg7 = 0;
                int flg8 = 0;
                int flg9 = 0;
                int flg10 = 0;

                string fstat = string.Empty;
                string rStat = string.Empty;
                #endregion
                #region Upload From PC & Validation
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                    if (hpf.ContentLength == 0)
                        continue;

                    switch (file)
                    {
                        case "File1":
                            file_1 = hpf.FileName;
                            file_1_size = hpf.ContentLength;
                            flg1 = ValidateFile(file_1, file_1_size, file1ext, file1size);
                            break;
                        case "File2":
                            file_2 = hpf.FileName;
                            file_2_size = hpf.ContentLength;
                            flg2 = ValidateFile(file_2, file_2_size, file2ext, file2size);
                            break;
                        case "File3":
                            file_3 = hpf.FileName;
                            file_3_size = hpf.ContentLength;
                            flg3 = ValidateFile(file_3, file_3_size, file3ext, file3size);
                            break;
                        case "File4":
                            file_4 = hpf.FileName;
                            file_4_size = hpf.ContentLength;
                            flg4 = ValidateFile(file_4, file_4_size, file4ext, file4size);
                            break;
                        case "File5":
                            file_5 = hpf.FileName;
                            file_5_size = hpf.ContentLength;
                            flg5 = ValidateFile(file_5, file_5_size, file5ext, file5size);
                            break;
                        case "File6":
                            file_6 = hpf.FileName;
                            file_6_size = hpf.ContentLength;
                            flg6 = ValidateFile(file_6, file_6_size, file6ext, file6size);
                            break;
                        case "File7":
                            file_7 = hpf.FileName;
                            file_7_size = hpf.ContentLength;
                            flg7 = ValidateFile(file_7, file_7_size, file7ext, file7size);
                            break;
                        case "File8":
                            file_8 = hpf.FileName;
                            file_8_size = hpf.ContentLength;
                            flg8 = ValidateFile(file_8, file_8_size, file8ext, file8size);
                            break;
                        case "File9":
                            file_9 = hpf.FileName;
                            file_9_size = hpf.ContentLength;
                            flg9 = ValidateFile(file_9, file_9_size, file9ext, file9size);
                            break;
                        case "File10":
                            file_10 = hpf.FileName;
                            file_10_size = hpf.ContentLength;
                            flg10 = ValidateFile(file_10, file_10_size, file10ext, file10size);
                            break;
                        default:
                            break;
                    }
                }

                #endregion
                #region Upload From FTP & Validation

                if (FtpChk1.Equals("true"))
                {
                    file_1 = Ftp1;
                    string fullPath = Path.Combine(ftpsfolder, file_1);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_1_size = Convert.ToInt32(fi.Length);
                        flg1 = ValidateFile(file_1, file_1_size, file1ext, file1size);
                    }
                }
                if (FtpChk2.Equals("true"))
                {
                    file_2 = Ftp2;
                    string fullPath = Path.Combine(ftpsfolder, file_2);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_2_size = Convert.ToInt32(fi.Length);
                        flg2 = ValidateFile(file_2, file_2_size, file2ext, file2size);
                    }
                }
                if (FtpChk3.Equals("true"))
                {
                    file_3 = Ftp3;
                    string fullPath = Path.Combine(ftpsfolder, file_3);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_3_size = Convert.ToInt32(fi.Length);
                        flg3 = ValidateFile(file_3, file_3_size, file3ext, file3size);
                    }
                }
                if (FtpChk4.Equals("true"))
                {
                    file_4 = Ftp4;
                    string fullPath = Path.Combine(ftpsfolder, file_4);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_4_size = Convert.ToInt32(fi.Length);
                        flg4 = ValidateFile(file_4, file_4_size, file4ext, file4size);
                    }
                }
                if (FtpChk5.Equals("true"))
                {
                    file_5 = Ftp5;
                    string fullPath = Path.Combine(ftpsfolder, file_5);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_5_size = Convert.ToInt32(fi.Length);
                        flg5 = ValidateFile(file_5, file_5_size, file5ext, file5size);
                    }
                }
                if (FtpChk6.Equals("true"))
                {
                    file_6 = Ftp6;
                    string fullPath = Path.Combine(ftpsfolder, file_6);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_6_size = Convert.ToInt32(fi.Length);
                        flg1 = ValidateFile(file_6, file_6_size, file6ext, file6size);
                    }
                }
                if (FtpChk7.Equals("true"))
                {
                    file_7 = Ftp7;
                    string fullPath = Path.Combine(ftpsfolder, file_7);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_7_size = Convert.ToInt32(fi.Length);
                        flg7 = ValidateFile(file_7, file_7_size, file7ext, file7size);
                    }
                }
                if (FtpChk8.Equals("true"))
                {
                    file_8 = Ftp8;
                    string fullPath = Path.Combine(ftpsfolder, file_8);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_8_size = Convert.ToInt32(fi.Length);
                        flg8 = ValidateFile(file_8, file_8_size, file8ext, file8size);
                    }
                }
                if (FtpChk9.Equals("true"))
                {
                    file_9 = Ftp9;
                    string fullPath = Path.Combine(ftpsfolder, file_9);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_9_size = Convert.ToInt32(fi.Length);
                        flg9 = ValidateFile(file_9, file_9_size, file9ext, file9size);
                    }
                }
                if (FtpChk10.Equals("true"))
                {
                    file_10 = Ftp1;
                    string fullPath = Path.Combine(ftpsfolder, file_10);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_10_size = Convert.ToInt32(fi.Length);
                        flg10 = ValidateFile(file_10, file_10_size, file10ext, file10size);
                    }
                }

                #endregion

                if (RevisionStatus != "N")
                {
                    ArticleFileRevisionId = "-1";
                    FileRevisionId = -1;
                }

                ufile_1 = !string.IsNullOrEmpty(file_1) && flg1 == 1 ? "1_" + file_1 : ufile_1 = string.Empty;
                ufile_2 = !string.IsNullOrEmpty(file_2) && flg2 == 1 ? "2_" + file_2 : ufile_2 = string.Empty;
                ufile_3 = !string.IsNullOrEmpty(file_3) && flg3 == 1 ? "3_" + file_3 : ufile_3 = string.Empty;
                ufile_4 = !string.IsNullOrEmpty(file_4) && flg4 == 1 ? "4_" + file_4 : ufile_4 = string.Empty;
                ufile_5 = !string.IsNullOrEmpty(file_5) && flg5 == 1 ? "5_" + file_5 : ufile_5 = string.Empty;
                ufile_6 = !string.IsNullOrEmpty(file_6) && flg6 == 1 ? "6_" + file_6 : ufile_6 = string.Empty;
                ufile_7 = !string.IsNullOrEmpty(file_7) && flg7 == 1 ? "7_" + file_7 : ufile_7 = string.Empty;
                ufile_8 = !string.IsNullOrEmpty(file_8) && flg8 == 1 ? "8_" + file_8 : ufile_8 = string.Empty;
                ufile_9 = !string.IsNullOrEmpty(file_9) && flg9 == 1 ? "9_" + file_9 : ufile_9 = string.Empty;
                ufile_10 = !string.IsNullOrEmpty(file_10) && flg10 == 1 ? "10_" + file_10 : ufile_10 = string.Empty;

                #region ErrorMessage
                if (string.IsNullOrEmpty(ufile_1) && !string.IsNullOrEmpty(file_1))
                    throw new ApplicationException("File 1 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_2) && !string.IsNullOrEmpty(file_2))
                    throw new ApplicationException("File 2 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_3) && !string.IsNullOrEmpty(file_3))
                    throw new ApplicationException("File 3 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_4) && !string.IsNullOrEmpty(file_4))
                    throw new ApplicationException("File 4 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_5) && !string.IsNullOrEmpty(file_5))
                    throw new ApplicationException("File 5 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_6) && !string.IsNullOrEmpty(file_6))
                    throw new ApplicationException("File 6 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_7) && !string.IsNullOrEmpty(file_7))
                    throw new ApplicationException("File 7 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_8) && !string.IsNullOrEmpty(file_8))
                    throw new ApplicationException("File 8 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_9) && !string.IsNullOrEmpty(file_9))
                    throw new ApplicationException("File 9 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_10) && !string.IsNullOrEmpty(file_10))
                    throw new ApplicationException("File 10 is not allowed format or file size too large!");

                #endregion

                if ((
                    !string.IsNullOrEmpty(ufile_1) ||
                    !string.IsNullOrEmpty(ufile_2) ||
                    !string.IsNullOrEmpty(ufile_3) ||
                    !string.IsNullOrEmpty(ufile_4) ||
                    !string.IsNullOrEmpty(ufile_5) ||
                    !string.IsNullOrEmpty(ufile_6) ||
                    !string.IsNullOrEmpty(ufile_7) ||
                    !string.IsNullOrEmpty(ufile_8) ||
                    !string.IsNullOrEmpty(ufile_9) ||
                    !string.IsNullOrEmpty(ufile_10)) && ArticleFileRevisionId == "-1")
                {
                    var result = context.UpdateArticleFileRevision(ArticleFileRevisionId, FileRevisionId.ToString(), ArticleId.ToString(), FileTitle, FileOrder, ufile_1, ufile_2, ufile_3, ufile_4, ufile_5, ufile_6, ufile_7, ufile_8, ufile_9, ufile_10, FileType, FileComment, Membership.GetUser().ProviderUserKey);

                    if (result.Count != 0)
                    {
                        NewArticleFileRevisionId = result[0].af_rf_id ?? -1;
                        NewFileRevisionId = result[0].rev_id ?? -1;
                        fstat = result[0].fstat;
                        rStat = result[0].rstat;
                    }
                }
                else
                    throw new Exception("At least one file must be selected");

                string img_prefix = ArticleId + "_";
                string tmpFolder = tmpsfolder + "\\" + NewFileRevisionId.ToString();
                //string tmpOldFolder = tmpsfolder + "\\" + FileRevisionId.ToString();
                string tmpOldFolder = tmpsfolder + "\\" + old_FileRevisionId.ToString();

                if (!Directory.Exists(tmpFolder))
                {
                    Directory.CreateDirectory(tmpFolder);
                }

                string insertMsg = string.Empty;

                if (RevisionStatus != "N")
                {

                    var result2 = context.CopyArticleFiles(old_FileRevisionId.ToString(), NewFileRevisionId.ToString(), old_ArticleFileRevisionId.ToString(), ArticleId.ToString());

                    //tmpOldFolder = tmpsfolder + "\\" + old_FileRevisionId.ToString();

                    #region Move to NewRevision Folder
                    if (Directory.Exists(tmpOldFolder) && !tmpOldFolder.Contains("-1"))
                    {
                        DirectoryInfo directory = new DirectoryInfo(tmpOldFolder);
                        var files = directory.GetFiles();

                        foreach (FileInfo item in files)
                        {
                            if (System.IO.File.Exists(Path.Combine(tmpOldFolder, item.FullName)))
                            {
                                System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name), true);
                            }
                            else
                            {
                                System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name), true);
                            }
                        }
                    }
                    #endregion
                }

                #region SaveAs
                if (flg1 == 1 && !string.IsNullOrEmpty(file_1))
                {
                    string db_file_1 = file_1;
                    file_1 = img_prefix + "1_" + file_1;
                    if (!FtpChk1.Equals("true"))
                        Request.Files["File1"].SaveAs(tmpFolder + "\\" + file_1);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp1, tmpFolder + "\\" + file_1, true);
                    insertMsg = insertMsg + ftDetail.file1_name + ": 1_" + db_file_1 + " uploaded. \n";
                }

                if (flg2 == 1 && !string.IsNullOrEmpty(file_2))
                {
                    string db_file_2 = file_2;
                    file_2 = img_prefix + "2_" + file_2;
                    if (!FtpChk2.Equals("true"))
                        Request.Files["File2"].SaveAs(tmpFolder + "\\" + file_2);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp2, tmpFolder + "\\" + file_2, true);

                    insertMsg = insertMsg + ftDetail.file2_name + ": 2_" + db_file_2 + " uploaded. \n";
                }

                if (flg3 == 1 && !string.IsNullOrEmpty(file_3))
                {
                    string db_file_3 = file_3;
                    file_3 = img_prefix + "3_" + file_3;
                    if (!FtpChk3.Equals("true"))
                        Request.Files["File3"].SaveAs(tmpFolder + "\\" + file_3);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp3, tmpFolder + "\\" + file_3, true);

                    insertMsg = insertMsg + ftDetail.file3_name + ": 3_" + db_file_3 + " uploaded. \n";
                }

                if (flg4 == 1 && !string.IsNullOrEmpty(file_4))
                {
                    string db_file_4 = file_4;
                    file_4 = img_prefix + "4_" + file_4;
                    if (!FtpChk4.Equals("true"))
                        Request.Files["File4"].SaveAs(tmpFolder + "\\" + file_4);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp4, tmpFolder + "\\" + file_4, true);

                    insertMsg = insertMsg + ftDetail.file4_name + ": 4_" + db_file_4 + " uploaded. \n";
                }

                if (flg5 == 1 && !string.IsNullOrEmpty(file_5))
                {
                    string db_file_5 = file_5;
                    file_5 = img_prefix + "5_" + file_5;
                    if (!FtpChk5.Equals("true"))
                        Request.Files["File5"].SaveAs(tmpFolder + "\\" + file_5);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp5, tmpFolder + "\\" + file_5, true);

                    insertMsg = insertMsg + ftDetail.file5_name + ": 5_" + db_file_5 + " uploaded. \n";
                }

                if (flg6 == 1 && !string.IsNullOrEmpty(file_6))
                {
                    string db_file_6 = file_6;
                    file_6 = img_prefix + "6_" + file_6;
                    if (!FtpChk6.Equals("true"))
                        Request.Files["File6"].SaveAs(tmpFolder + "\\" + file_6);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp6, tmpFolder + "\\" + file_6, true);

                    insertMsg = insertMsg + ftDetail.file6_name + ": 6_" + db_file_6 + " uploaded. \n";
                }

                if (flg7 == 1 && !string.IsNullOrEmpty(file_7))
                {
                    string db_file_7 = file_7;
                    file_7 = img_prefix + "7_" + file_7;
                    if (!FtpChk7.Equals("true"))
                        Request.Files["File7"].SaveAs(tmpFolder + "\\" + file_7);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp7, tmpFolder + "\\" + file_7, true);

                    insertMsg = insertMsg + ftDetail.file7_name + ": 1_" + db_file_7 + " uploaded. \n";
                }

                if (flg8 == 1 && !string.IsNullOrEmpty(file_8))
                {
                    string db_file_8 = file_8;
                    file_8 = img_prefix + "8_" + file_8;
                    if (!FtpChk8.Equals("true"))
                        Request.Files["File8"].SaveAs(tmpFolder + "\\" + file_8);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp8, tmpFolder + "\\" + file_8, true);

                    insertMsg = insertMsg + ftDetail.file8_name + ": 1_" + db_file_8 + " uploaded. \n";
                }

                if (flg9 == 1 && !string.IsNullOrEmpty(file_9))
                {
                    string db_file_9 = file_9;
                    file_9 = img_prefix + "9_" + file_9;
                    if (!FtpChk9.Equals("true"))
                        Request.Files["File9"].SaveAs(tmpFolder + "\\" + file_9);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp9, tmpFolder + "\\" + file_9, true);

                    insertMsg = insertMsg + ftDetail.file9_name + ": 9_" + db_file_9 + " uploaded. \n";
                }

                if (flg10 == 1 && !string.IsNullOrEmpty(file_10))
                {
                    string db_file_10 = file_10;
                    file_10 = img_prefix + "10_" + file_10;
                    if (!FtpChk10.Equals("true"))
                        Request.Files["File10"].SaveAs(tmpFolder + "\\" + file_10);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp10, tmpFolder + "\\" + file_10, true);

                    insertMsg = insertMsg + ftDetail.file10_name + ": 10_" + db_file_10 + " uploaded. \n";
                }
                #endregion

                TempData["Message"] = insertMsg;

                return RedirectToAction("Index", new
                {
                    ArticleId = ArticleId,
                    RevisionId = RevisionId,
                    FileRevisionId = NewFileRevisionId
                });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;

                return View(FileTypeDetails);
            }
        }

        [CmsAuthorize(Roles = "ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Create(int ArticleId, int RevisionId, int? FileRevisionId, int? FileId, int? TypeId, string RevisionStatus)
        {

            ViewBag.FileTypes = Bags.GetFileTypes();
            ViewBag.FtpFiles = Bags.GetFtpFolderFiles();

            long fr = FileRevisionId ?? 0;

            cms_asp_admin_select_article_files_revision_list_Result Files = context.SelectArticleFilesRevisions(ArticleId).Where(m => m.rev_id == fr).FirstOrDefault();

            ViewBag.ArticleId = ArticleId;
            ViewBag.RevisionId = RevisionId;
            ViewBag.TypeId = TypeId ?? -1;
            ViewBag.FileId = FileId ?? -1;
            ViewBag.FileRevisionId = FileRevisionId ?? -1;

            if (Files != null)
            {
                ViewBag.RevisionStatus = Files.revision_status;
            }

            var FileTypeDetails = context.SelectFileTypesDetails(TypeId ?? -1).FirstOrDefault();

            return View(FileTypeDetails);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Edit(FormCollection collection, int id, int? TypeId, long FileRevisionId, int ArticleId, long RevisionId)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_FILE_EDIT, this));

            ViewBag.FileTypes = Bags.GetFileTypes();
            ViewBag.FtpFiles = Bags.GetFtpFolderFiles();

            ViewBag.ArticleId = ArticleId;
            ViewBag.TypeId = TypeId ?? -1;
            ViewBag.FileRevisionId = FileRevisionId;

            #region GetValues
            //string TypeId = collection["TypeID"];
            //string ArticleId = collection["ArticleId"];
            //string RevisionId = collection["RevisionId"];
            //string FileRevisionId = collection["af_rf_id"];
            string ArticleFileRevisionId = collection["FileID"];
            string RevisionStatus = string.IsNullOrEmpty(collection["rev_status"]) ? "N" : collection["rev_status"];
            string oldAFR = FileRevisionId.ToString();


            string FileComment = collection["filecomment"].Replace("\\", "_||_");
            string FileTitle = collection["filetitle"].Replace("\\", "_||_");

            string FileOrder = collection["fileorder"] ?? "0";
            string FileType = collection["TypeID"];

            ViewData["file_title"] = FileTitle;
            ViewData["file_comment"] = FileComment;
            ViewData["file_order"] = FileOrder;

            #endregion

            long NewFileRevisionId = -1;
            //long NewArticleFileRevisionId = !string.IsNullOrEmpty(FileRevisionId) ? -1 : Convert.ToInt64(FileRevisionId);
            long NewArticleFileRevisionId = FileRevisionId;
            string old_ArticleFileRevisionId = ArticleFileRevisionId;
            string old_FileRevisionId = FileRevisionId.ToString();

            try
            {
                #region ctrlFtp
                string FtpChk1 = collection["FtpChk1"] ?? "false";
                string FtpChk2 = collection["FtpChk2"] ?? "false";
                string FtpChk3 = collection["FtpChk3"] ?? "false";
                string FtpChk4 = collection["FtpChk4"] ?? "false";
                string FtpChk5 = collection["FtpChk5"] ?? "false";
                string FtpChk6 = collection["FtpChk6"] ?? "false";
                string FtpChk7 = collection["FtpChk7"] ?? "false";
                string FtpChk8 = collection["FtpChk8"] ?? "false";
                string FtpChk9 = collection["FtpChk9"] ?? "false";
                string FtpChk10 = collection["FtpChk10"] ?? "false";

                string Ftp1 = collection["Ftp1"] ?? string.Empty;
                string Ftp2 = collection["Ftp2"] ?? string.Empty;
                string Ftp3 = collection["Ftp3"] ?? string.Empty;
                string Ftp4 = collection["Ftp4"] ?? string.Empty;
                string Ftp5 = collection["Ftp5"] ?? string.Empty;
                string Ftp6 = collection["Ftp6"] ?? string.Empty;
                string Ftp7 = collection["Ftp7"] ?? string.Empty;
                string Ftp8 = collection["Ftp8"] ?? string.Empty;
                string Ftp9 = collection["Ftp9"] ?? string.Empty;
                string Ftp10 = collection["Ftp10"] ?? string.Empty;
                #endregion

                string ftpsfolder = Server.MapPath("/i/ftp");
                string tmpsfolder = Server.MapPath("/i/tmp");

                #region Get FileType Details
                var ftDetail = context.SelectFileTypesDetails(Convert.ToInt32(TypeId)).FirstOrDefault();

                string file1 = ftDetail.file1_name;
                string file1ext = ftDetail.file1_extension;
                int file1size = ftDetail.file1_size;
                string file1_wh = ftDetail.file1_wh;

                string file2 = ftDetail.file2_name;
                string file2ext = ftDetail.file2_extension;
                int file2size = ftDetail.file2_size;
                string file2_wh = ftDetail.file2_wh;

                string file3 = ftDetail.file3_name;
                string file3ext = ftDetail.file3_extension;
                int file3size = ftDetail.file3_size;
                string file3_wh = ftDetail.file3_wh;

                string file4 = ftDetail.file4_name;
                string file4ext = ftDetail.file4_extension;
                int file4size = ftDetail.file4_size;
                string file4_wh = ftDetail.file4_wh;

                string file5 = ftDetail.file5_name;
                string file5ext = ftDetail.file5_extension;
                int file5size = ftDetail.file5_size;
                string file5_wh = ftDetail.file5_wh;

                string file6 = ftDetail.file6_name;
                string file6ext = ftDetail.file6_extension;
                int file6size = ftDetail.file6_size;
                string file6_wh = ftDetail.file6_wh;

                string file7 = ftDetail.file7_name;
                string file7ext = ftDetail.file7_extension;
                int file7size = ftDetail.file7_size;
                string file7_wh = ftDetail.file7_wh;

                string file8 = ftDetail.file8_name;
                string file8ext = ftDetail.file8_extension;
                int file8size = ftDetail.file8_size;
                string file8_wh = ftDetail.file8_wh;

                string file9 = ftDetail.file9_name;
                string file9ext = ftDetail.file9_extension;
                int file9size = ftDetail.file9_size;
                string file9_wh = ftDetail.file9_wh;

                string file10 = ftDetail.file10_name;
                string file10ext = ftDetail.file10_extension;
                int file10size = ftDetail.file10_size;
                string file10_wh = ftDetail.file10_wh;
                #endregion
                #region SetValues
                string file_1 = string.Empty;
                string file_2 = string.Empty;
                string file_3 = string.Empty;
                string file_4 = string.Empty;
                string file_5 = string.Empty;
                string file_6 = string.Empty;
                string file_7 = string.Empty;
                string file_8 = string.Empty;
                string file_9 = string.Empty;
                string file_10 = string.Empty;

                string ufile_1 = string.Empty;
                string ufile_2 = string.Empty;
                string ufile_3 = string.Empty;
                string ufile_4 = string.Empty;
                string ufile_5 = string.Empty;
                string ufile_6 = string.Empty;
                string ufile_7 = string.Empty;
                string ufile_8 = string.Empty;
                string ufile_9 = string.Empty;
                string ufile_10 = string.Empty;

                string hdnFile_1 = string.Empty;
                string hdnFile_2 = string.Empty;
                string hdnFile_3 = string.Empty;
                string hdnFile_4 = string.Empty;
                string hdnFile_5 = string.Empty;
                string hdnFile_6 = string.Empty;
                string hdnFile_7 = string.Empty;
                string hdnFile_8 = string.Empty;
                string hdnFile_9 = string.Empty;
                string hdnFile_10 = string.Empty;

                int file_1_size = 0;
                int file_2_size = 0;
                int file_3_size = 0;
                int file_4_size = 0;
                int file_5_size = 0;
                int file_6_size = 0;
                int file_7_size = 0;
                int file_8_size = 0;
                int file_9_size = 0;
                int file_10_size = 0;

                int flg1 = 0;
                int flg2 = 0;
                int flg3 = 0;
                int flg4 = 0;
                int flg5 = 0;
                int flg6 = 0;
                int flg7 = 0;
                int flg8 = 0;
                int flg9 = 0;
                int flg10 = 0;

                string fstat = string.Empty;
                string rStat = string.Empty;
                #endregion
                #region Upload From PC & Validation
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                    if (hpf.ContentLength == 0)
                        continue;

                    switch (file)
                    {
                        case "File1":
                            file_1 = hpf.FileName;
                            file_1_size = hpf.ContentLength;
                            flg1 = ValidateFile(file_1, file_1_size, file1ext, file1size);
                            break;
                        case "File2":
                            file_2 = hpf.FileName;
                            file_2_size = hpf.ContentLength;
                            flg2 = ValidateFile(file_2, file_2_size, file2ext, file2size);
                            break;
                        case "File3":
                            file_3 = hpf.FileName;
                            file_3_size = hpf.ContentLength;
                            flg3 = ValidateFile(file_3, file_3_size, file3ext, file3size);
                            break;
                        case "File4":
                            file_4 = hpf.FileName;
                            file_4_size = hpf.ContentLength;
                            flg4 = ValidateFile(file_4, file_4_size, file4ext, file4size);
                            break;
                        case "File5":
                            file_5 = hpf.FileName;
                            file_5_size = hpf.ContentLength;
                            flg5 = ValidateFile(file_5, file_5_size, file5ext, file5size);
                            break;
                        case "File6":
                            file_6 = hpf.FileName;
                            file_6_size = hpf.ContentLength;
                            flg6 = ValidateFile(file_6, file_6_size, file6ext, file6size);
                            break;
                        case "File7":
                            file_7 = hpf.FileName;
                            file_7_size = hpf.ContentLength;
                            flg7 = ValidateFile(file_7, file_7_size, file7ext, file7size);
                            break;
                        case "File8":
                            file_8 = hpf.FileName;
                            file_8_size = hpf.ContentLength;
                            flg8 = ValidateFile(file_8, file_8_size, file8ext, file8size);
                            break;
                        case "File9":
                            file_9 = hpf.FileName;
                            file_9_size = hpf.ContentLength;
                            flg9 = ValidateFile(file_9, file_9_size, file9ext, file9size);
                            break;
                        case "File10":
                            file_10 = hpf.FileName;
                            file_10_size = hpf.ContentLength;
                            flg10 = ValidateFile(file_10, file_10_size, file10ext, file10size);
                            break;
                        default:
                            break;
                    }
                }

                #endregion
                #region Upload From FTP & Validation

                if (FtpChk1.Equals("true"))
                {
                    file_1 = Ftp1;
                    string fullPath = Path.Combine(ftpsfolder, file_1);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_1_size = Convert.ToInt32(fi.Length);
                        flg1 = ValidateFile(file_1, file_1_size, file1ext, file1size);
                    }
                }
                if (FtpChk2.Equals("true"))
                {
                    file_2 = Ftp2;
                    string fullPath = Path.Combine(ftpsfolder, file_2);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_2_size = Convert.ToInt32(fi.Length);
                        flg2 = ValidateFile(file_2, file_2_size, file2ext, file2size);
                    }
                }
                if (FtpChk3.Equals("true"))
                {
                    file_3 = Ftp3;
                    string fullPath = Path.Combine(ftpsfolder, file_3);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_3_size = Convert.ToInt32(fi.Length);
                        flg3 = ValidateFile(file_3, file_3_size, file3ext, file3size);
                    }
                }
                if (FtpChk4.Equals("true"))
                {
                    file_4 = Ftp4;
                    string fullPath = Path.Combine(ftpsfolder, file_4);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_4_size = Convert.ToInt32(fi.Length);
                        flg4 = ValidateFile(file_4, file_4_size, file4ext, file4size);
                    }
                }
                if (FtpChk5.Equals("true"))
                {
                    file_5 = Ftp5;
                    string fullPath = Path.Combine(ftpsfolder, file_5);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_5_size = Convert.ToInt32(fi.Length);
                        flg5 = ValidateFile(file_5, file_5_size, file5ext, file5size);
                    }
                }
                if (FtpChk6.Equals("true"))
                {
                    file_6 = Ftp6;
                    string fullPath = Path.Combine(ftpsfolder, file_6);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_6_size = Convert.ToInt32(fi.Length);
                        flg1 = ValidateFile(file_6, file_6_size, file6ext, file6size);
                    }
                }
                if (FtpChk7.Equals("true"))
                {
                    file_7 = Ftp7;
                    string fullPath = Path.Combine(ftpsfolder, file_7);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_7_size = Convert.ToInt32(fi.Length);
                        flg7 = ValidateFile(file_7, file_7_size, file7ext, file7size);
                    }
                }
                if (FtpChk8.Equals("true"))
                {
                    file_8 = Ftp8;
                    string fullPath = Path.Combine(ftpsfolder, file_8);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_8_size = Convert.ToInt32(fi.Length);
                        flg8 = ValidateFile(file_8, file_8_size, file8ext, file8size);
                    }
                }
                if (FtpChk9.Equals("true"))
                {
                    file_9 = Ftp9;
                    string fullPath = Path.Combine(ftpsfolder, file_9);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_9_size = Convert.ToInt32(fi.Length);
                        flg9 = ValidateFile(file_9, file_9_size, file9ext, file9size);
                    }
                }
                if (FtpChk10.Equals("true"))
                {
                    file_10 = Ftp1;
                    string fullPath = Path.Combine(ftpsfolder, file_10);
                    if (System.IO.File.Exists(fullPath))
                    {
                        FileInfo fi = new FileInfo(fullPath);
                        file_10_size = Convert.ToInt32(fi.Length);
                        flg10 = ValidateFile(file_10, file_10_size, file10ext, file10size);
                    }
                }

                #endregion

                if (RevisionStatus != "N")
                {
                    ArticleFileRevisionId = "-1";
                    FileRevisionId = -1;
                }

                ufile_1 = !string.IsNullOrEmpty(file_1) && flg1 == 1 ? "1_" + file_1 : ufile_1 = collection["Hdn_File1"];
                ufile_2 = !string.IsNullOrEmpty(file_2) && flg2 == 1 ? "2_" + file_2 : ufile_2 = collection["Hdn_File2"];
                ufile_3 = !string.IsNullOrEmpty(file_3) && flg3 == 1 ? "3_" + file_3 : ufile_3 = collection["Hdn_File3"];
                ufile_4 = !string.IsNullOrEmpty(file_4) && flg4 == 1 ? "4_" + file_4 : ufile_4 = collection["Hdn_File4"];
                ufile_5 = !string.IsNullOrEmpty(file_5) && flg5 == 1 ? "5_" + file_5 : ufile_5 = collection["Hdn_File5"];
                ufile_6 = !string.IsNullOrEmpty(file_6) && flg6 == 1 ? "6_" + file_6 : ufile_6 = collection["Hdn_File6"];
                ufile_7 = !string.IsNullOrEmpty(file_7) && flg7 == 1 ? "7_" + file_7 : ufile_7 = collection["Hdn_File7"];
                ufile_8 = !string.IsNullOrEmpty(file_8) && flg8 == 1 ? "8_" + file_8 : ufile_8 = collection["Hdn_File8"];
                ufile_9 = !string.IsNullOrEmpty(file_9) && flg9 == 1 ? "9_" + file_9 : ufile_9 = collection["Hdn_File9"];
                ufile_10 = !string.IsNullOrEmpty(file_10) && flg10 == 1 ? "10_" + file_10 : ufile_10 = collection["Hdn_File10"];

                ufile_1 = !string.IsNullOrEmpty(file_1) && flg1 == 1 ? "1_" + file_1 : ufile_1;
                ufile_2 = !string.IsNullOrEmpty(file_2) && flg2 == 1 ? "2_" + file_2 : ufile_2;
                ufile_3 = !string.IsNullOrEmpty(file_3) && flg3 == 1 ? "3_" + file_3 : ufile_3;
                ufile_4 = !string.IsNullOrEmpty(file_4) && flg4 == 1 ? "4_" + file_4 : ufile_4;
                ufile_5 = !string.IsNullOrEmpty(file_5) && flg5 == 1 ? "5_" + file_5 : ufile_5;
                ufile_6 = !string.IsNullOrEmpty(file_6) && flg6 == 1 ? "6_" + file_6 : ufile_6;
                ufile_7 = !string.IsNullOrEmpty(file_7) && flg7 == 1 ? "7_" + file_7 : ufile_7;
                ufile_8 = !string.IsNullOrEmpty(file_8) && flg8 == 1 ? "8_" + file_8 : ufile_8;
                ufile_9 = !string.IsNullOrEmpty(file_9) && flg9 == 1 ? "9_" + file_9 : ufile_9;
                ufile_10 = !string.IsNullOrEmpty(file_10) && flg10 == 1 ? "10_" + file_10 : ufile_10;

                ufile_1 = string.IsNullOrEmpty(ufile_1) ? string.Empty : ufile_1;
                ufile_2 = string.IsNullOrEmpty(ufile_2) ? string.Empty : ufile_2;
                ufile_3 = string.IsNullOrEmpty(ufile_3) ? string.Empty : ufile_3;
                ufile_4 = string.IsNullOrEmpty(ufile_4) ? string.Empty : ufile_4;
                ufile_5 = string.IsNullOrEmpty(ufile_5) ? string.Empty : ufile_5;
                ufile_6 = string.IsNullOrEmpty(ufile_6) ? string.Empty : ufile_6;
                ufile_7 = string.IsNullOrEmpty(ufile_7) ? string.Empty : ufile_7;
                ufile_8 = string.IsNullOrEmpty(ufile_8) ? string.Empty : ufile_8;
                ufile_9 = string.IsNullOrEmpty(ufile_9) ? string.Empty : ufile_9;
                ufile_10 = string.IsNullOrEmpty(ufile_10) ? string.Empty : ufile_10;

                #region ErrorMessage
                if (string.IsNullOrEmpty(ufile_1) && !string.IsNullOrEmpty(file_1))
                    throw new ApplicationException("File 1 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_2) && !string.IsNullOrEmpty(file_2))
                    throw new ApplicationException("File 2 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_3) && !string.IsNullOrEmpty(file_3))
                    throw new ApplicationException("File 3 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_4) && !string.IsNullOrEmpty(file_4))
                    throw new ApplicationException("File 4 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_5) && !string.IsNullOrEmpty(file_5))
                    throw new ApplicationException("File 5 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_6) && !string.IsNullOrEmpty(file_6))
                    throw new ApplicationException("File 6 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_7) && !string.IsNullOrEmpty(file_7))
                    throw new ApplicationException("File 7 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_8) && !string.IsNullOrEmpty(file_8))
                    throw new ApplicationException("File 8 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_9) && !string.IsNullOrEmpty(file_9))
                    throw new ApplicationException("File 9 is not allowed format or file size too large!");
                if (string.IsNullOrEmpty(ufile_10) && !string.IsNullOrEmpty(file_10))
                    throw new ApplicationException("File 10 is not allowed format or file size too large!");
                #endregion

                if (
                    (!string.IsNullOrEmpty(ufile_1) ||
                    !string.IsNullOrEmpty(ufile_2) ||
                    !string.IsNullOrEmpty(ufile_3) ||
                    !string.IsNullOrEmpty(ufile_4) ||
                    !string.IsNullOrEmpty(ufile_5) ||
                    !string.IsNullOrEmpty(ufile_6) ||
                    !string.IsNullOrEmpty(ufile_7) ||
                    !string.IsNullOrEmpty(ufile_8) ||
                    !string.IsNullOrEmpty(ufile_9) ||
                    !string.IsNullOrEmpty(ufile_10)))
                {
                    var result = context.UpdateArticleFileRevision(ArticleFileRevisionId, FileRevisionId.ToString(), ArticleId.ToString(), FileTitle, FileOrder, ufile_1, ufile_2, ufile_3, ufile_4, ufile_5, ufile_6, ufile_7, ufile_8, ufile_9, ufile_10, FileType, FileComment, Membership.GetUser().ProviderUserKey);

                    if (result.Count != 0)
                    {
                        NewArticleFileRevisionId = result[0].af_rf_id ?? -1;
                        NewFileRevisionId = result[0].rev_id ?? -1;
                        fstat = result[0].fstat;
                        rStat = result[0].rstat;
                    }

                }
                else
                    throw new Exception("At least one file must be selected");

                string img_prefix = ArticleId + "_";
                string tmpFolder = tmpsfolder + "\\" + NewFileRevisionId.ToString();
                string tmpOldFolder = tmpsfolder + "\\" + old_FileRevisionId.ToString();

                if (!Directory.Exists(tmpFolder))
                {
                    Directory.CreateDirectory(tmpFolder);
                }

                string insertMsg = string.Empty;

                if (RevisionStatus != "N")
                {
                    // NewFileRevisionId = NewArticleFileRevisionId;
                    var result2 = context.CopyArticleFiles(old_FileRevisionId, NewFileRevisionId.ToString(), old_ArticleFileRevisionId.ToString(), ArticleId.ToString());

                    #region Move to NewRevision Folder
                    if (Directory.Exists(tmpOldFolder) && !tmpOldFolder.Contains("-1"))
                    {
                        DirectoryInfo directory = new DirectoryInfo(tmpOldFolder);
                        var files = directory.GetFiles();

                        foreach (FileInfo item in files)
                        {
                            string filename = item.FullName;

                            if (
                                  System.IO.File.Exists(Path.Combine(tmpOldFolder, filename))
                                  && (filename != ArticleId.ToString() + "_" + ufile_1)
                                && (filename != ArticleId.ToString() + "_" + ufile_2)
                                && (filename != ArticleId.ToString() + "_" + ufile_3)
                                && (filename != ArticleId.ToString() + "_" + ufile_4)
                                && (filename != ArticleId.ToString() + "_" + ufile_5)
                                && (filename != ArticleId.ToString() + "_" + ufile_6)
                                && (filename != ArticleId.ToString() + "_" + ufile_7)
                                && (filename != ArticleId.ToString() + "_" + ufile_8)
                                && (filename != ArticleId.ToString() + "_" + ufile_9)
                                && (filename != ArticleId.ToString() + "_" + ufile_10)
                                  )
                            {
                                System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name), true);
                            }
                            else
                            {
                                System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name), true);
                            }
                        }
                    }
                    #endregion
                }

                #region SaveAs
                if (flg1 == 1 && !string.IsNullOrEmpty(file_1))
                {
                    string db_file_1 = file_1;
                    file_1 = img_prefix + "1_" + file_1;
                    if (!FtpChk1.Equals("true"))
                        Request.Files["File1"].SaveAs(tmpFolder + "\\" + file_1);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp1, tmpFolder + "\\" + file_1, true);
                    insertMsg = insertMsg + ftDetail.file1_name + ": 1_" + db_file_1 + " uploaded. \n";
                }

                if (flg2 == 1 && !string.IsNullOrEmpty(file_2))
                {
                    string db_file_2 = file_2;
                    file_2 = img_prefix + "2_" + file_2;
                    if (!FtpChk2.Equals("true"))
                        Request.Files["File2"].SaveAs(tmpFolder + "\\" + file_2);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp2, tmpFolder + "\\" + file_2, true);

                    insertMsg = insertMsg + ftDetail.file2_name + ": 2_" + db_file_2 + " uploaded. \n";
                }

                if (flg3 == 1 && !string.IsNullOrEmpty(file_3))
                {
                    string db_file_3 = file_3;
                    file_3 = img_prefix + "3_" + file_3;
                    if (!FtpChk3.Equals("true"))
                        Request.Files["File3"].SaveAs(tmpFolder + "\\" + file_3);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp3, tmpFolder + "\\" + file_3, true);

                    insertMsg = insertMsg + ftDetail.file3_name + ": 3_" + db_file_3 + " uploaded. \n";
                }

                if (flg4 == 1 && !string.IsNullOrEmpty(file_4))
                {
                    string db_file_4 = file_4;
                    file_4 = img_prefix + "4_" + file_4;
                    if (!FtpChk4.Equals("true"))
                        Request.Files["File4"].SaveAs(tmpFolder + "\\" + file_4);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp4, tmpFolder + "\\" + file_4, true);

                    insertMsg = insertMsg + ftDetail.file4_name + ": 4_" + db_file_4 + " uploaded. \n";
                }

                if (flg5 == 1 && !string.IsNullOrEmpty(file_5))
                {
                    string db_file_5 = file_5;
                    file_5 = img_prefix + "5_" + file_5;
                    if (!FtpChk5.Equals("true"))
                        Request.Files["File5"].SaveAs(tmpFolder + "\\" + file_5);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp5, tmpFolder + "\\" + file_5, true);

                    insertMsg = insertMsg + ftDetail.file5_name + ": 5_" + db_file_5 + " uploaded. \n";
                }

                if (flg6 == 1 && !string.IsNullOrEmpty(file_6))
                {
                    string db_file_6 = file_6;
                    file_6 = img_prefix + "6_" + file_6;
                    if (!FtpChk6.Equals("true"))
                        Request.Files["File6"].SaveAs(tmpFolder + "\\" + file_6);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp6, tmpFolder + "\\" + file_6, true);

                    insertMsg = insertMsg + ftDetail.file6_name + ": 6_" + db_file_6 + " uploaded. \n";
                }

                if (flg7 == 1 && !string.IsNullOrEmpty(file_7))
                {
                    string db_file_7 = file_7;
                    file_7 = img_prefix + "7_" + file_7;
                    if (!FtpChk7.Equals("true"))
                        Request.Files["File7"].SaveAs(tmpFolder + "\\" + file_7);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp7, tmpFolder + "\\" + file_7, true);

                    insertMsg = insertMsg + ftDetail.file7_name + ": 1_" + db_file_7 + " uploaded. \n";
                }

                if (flg8 == 1 && !string.IsNullOrEmpty(file_8))
                {
                    string db_file_8 = file_8;
                    file_8 = img_prefix + "8_" + file_8;
                    if (!FtpChk8.Equals("true"))
                        Request.Files["File8"].SaveAs(tmpFolder + "\\" + file_8);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp8, tmpFolder + "\\" + file_8, true);

                    insertMsg = insertMsg + ftDetail.file8_name + ": 1_" + db_file_8 + " uploaded. \n";
                }

                if (flg9 == 1 && !string.IsNullOrEmpty(file_9))
                {
                    string db_file_9 = file_9;
                    file_9 = img_prefix + "9_" + file_9;
                    if (!FtpChk9.Equals("true"))
                        Request.Files["File9"].SaveAs(tmpFolder + "\\" + file_9);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp9, tmpFolder + "\\" + file_9, true);

                    insertMsg = insertMsg + ftDetail.file9_name + ": 9_" + db_file_9 + " uploaded. \n";
                }

                if (flg10 == 1 && !string.IsNullOrEmpty(file_10))
                {
                    string db_file_10 = file_10;
                    file_10 = img_prefix + "10_" + file_10;
                    if (!FtpChk10.Equals("true"))
                        Request.Files["File10"].SaveAs(tmpFolder + "\\" + file_10);
                    else
                        System.IO.File.Copy(ftpsfolder + "\\" + Ftp10, tmpFolder + "\\" + file_10, true);

                    insertMsg = insertMsg + ftDetail.file10_name + ": 10_" + db_file_10 + " uploaded. \n";
                }
                #endregion

                TempData["Message"] = "Article file successfully updated. <br />" + insertMsg;

                return RedirectToAction("Index", new
                {
                    ArticleId = ArticleId,
                    RevisionId = RevisionId,
                    FileRevisionId = NewFileRevisionId,
                    TypeId = TypeId
                });
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;

                return RedirectToAction("Edit", new
                {
                    ArticleId = collection["ArticleId"],
                    RevisionId = collection["RevisionId"],
                    FileRevisionId = FileRevisionId,
                    TypeId = TypeId
                });
            }
        }


        [CmsAuthorize(Roles = "ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Edit(int id, int? TypeId, int FileRevisionId, int ArticleId, int RevisionId)
        {

            ViewBag.FileTypes = Bags.GetFileTypes();

            cms_asp_admin_select_article_files_revision_file_details_Result Files = context.SelectArticleFileRevisionFileDetails(id, FileRevisionId, ArticleId).FirstOrDefault();

            ViewData["type_details"] = context.SelectFileTypesDetails(TypeId ?? -1).FirstOrDefault();
            ViewData["file_name_1"] = (Files != null) ? Files.file_name_1 : string.Empty;
            ViewData["file_name_2"] = (Files != null) ? Files.file_name_2 : string.Empty;
            ViewData["file_name_3"] = (Files != null) ? Files.file_name_3 : string.Empty;
            ViewData["file_name_4"] = (Files != null) ? Files.file_name_4 : string.Empty;
            ViewData["file_name_5"] = (Files != null) ? Files.file_name_5 : string.Empty;
            ViewData["file_name_6"] = (Files != null) ? Files.file_name_6 : string.Empty;
            ViewData["file_name_7"] = (Files != null) ? Files.file_name_7 : string.Empty;
            ViewData["file_name_8"] = (Files != null) ? Files.file_name_8 : string.Empty;
            ViewData["file_name_9"] = (Files != null) ? Files.file_name_9 : string.Empty;
            ViewData["file_name_10"] = (Files != null) ? Files.file_name_10 : string.Empty;
            ViewData["file_comment"] = (Files != null) ? Files.file_comment : string.Empty;
            ViewData["file_order"] = (Files != null) ? Files.file_order : 0;
            ViewData["file_title"] = (Files != null) ? Files.file_title : string.Empty;
            ViewData["revision_status"] = (Files != null) ? Files.revision_status : string.Empty;
            ViewData["FileID"] = FileRevisionId;
            var FileTypes = context.SelectFileTypesDetails(TypeId ?? -1).FirstOrDefault();

            return View(FileTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Delete", ContentType = "Article", ContentIdParam = "ArticleId")]
        public ActionResult Delete(int RevisionId, long FileRevisionId, int ArticleId, int FileId, string RevisionStatus)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_FILE_DELETE, this));

            cms_asp_admin_select_article_files_revision_file_details_Result aFiles = context.SelectArticleFileRevisionFileDetails(FileId, FileRevisionId, ArticleId)[0];

            if (RevisionStatus == "N")
            {
                string fSonuc = context.DeleteArticleFile(FileRevisionId, ArticleId, FileId).FirstOrDefault().rCode;

                if (fSonuc == "DF")
                {

                    TempData["Message"] = "File(s) deleted succesfully";
                }
                else
                {
                    TempData["Message"] = "The file(s) not found. File not deleted.";
                }

                string tmpsfolder = Server.MapPath("/i/tmp/" + FileRevisionId + "/" + ArticleId);

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_1)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_1));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_2)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_2));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_3)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_3));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_4)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_4));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_5)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_5));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_6)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_6));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_7)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_7));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_8)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_8));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_9)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_9));
                }

                if (System.IO.File.Exists(Path.Combine(tmpsfolder, aFiles.file_name_10)))
                {
                    System.IO.File.Delete(Path.Combine(tmpsfolder, aFiles.file_name_10));
                }

                return RedirectToAction("Index", new
                {
                    ArticleId = ArticleId,
                    RevisionId = RevisionId,
                    FileRevisionId = FileRevisionId
                });
            }
            else
            {
                int nFileId = -2;
                int nRevisionId = -1;

                cms_asp_admin_update_article_files_revision_Result fSOnuc = context.UpdateArticleFileRevision(nFileId.ToString(), nRevisionId.ToString(), ArticleId.ToString(), "", "0", "", "", "", "", "", "", "", "", "", "", "0", "", Membership.GetUser().ProviderUserKey).FirstOrDefault();


                long oldFileRevId = FileRevisionId;

                FileRevisionId = fSOnuc.af_rf_id ?? -1;

                if (fSOnuc.rev_id > 0)
                {
                    List<cms_article_files_revision_files> listOldFileRevisionFile = new List<cms_article_files_revision_files>();
                    listOldFileRevisionFile = context.SelectArticleFiles(oldFileRevId);

                    foreach (cms_article_files_revision_files oldFileRevisionFile in listOldFileRevisionFile)
                    {
                        if (aFiles.file_name_1 != oldFileRevisionFile.file_name_1)
                        {
                            cms_asp_admin_update_article_files_revision_Result insertNewFileRevisionFiles = context.UpdateArticleFileRevision(
                            "-1", fSOnuc.rev_id.ToString(),
                            ArticleId.ToString(),
                            oldFileRevisionFile.file_title,
                            oldFileRevisionFile.file_order.ToString(),
                            oldFileRevisionFile.file_name_1,
                            oldFileRevisionFile.file_name_2,
                            oldFileRevisionFile.file_name_3,
                            oldFileRevisionFile.file_name_4,
                            oldFileRevisionFile.file_name_5,
                            oldFileRevisionFile.file_name_6,
                            oldFileRevisionFile.file_name_7,
                            oldFileRevisionFile.file_name_8,
                            oldFileRevisionFile.file_name_9,
                            oldFileRevisionFile.file_name_10,
                            oldFileRevisionFile.file_type_id.ToString(),
                            oldFileRevisionFile.file_comment,
                            Membership.GetUser().ProviderUserKey).FirstOrDefault();
                        }
                    }


                    //cms_asp_copy_article_files_revision_files_Result cSonuc = context.CopyArticleFiles(oldFileRevId.ToString(), fSOnuc.rev_id.ToString(), FileId.ToString(), ArticleId.ToString()).FirstOrDefault();
                    string contentfolder = Server.MapPath("/i/content");
                    string tmpsfolder = Server.MapPath("/i/tmp");
                    string tmpFolder = tmpsfolder + "\\" + fSOnuc.rev_id.ToString();
                    string tmpOldFolder = tmpsfolder + "\\" + oldFileRevId;


                    if (!Directory.Exists(tmpFolder))
                    {
                        Directory.CreateDirectory(tmpFolder);
                    }

                    if (!Directory.Exists(tmpOldFolder))
                    {
                        Directory.CreateDirectory(tmpOldFolder);
                    }

                    #region Copy to NewRevision Folder
                    if (Directory.Exists(tmpOldFolder) && !tmpOldFolder.Contains("-1"))
                    {
                        DirectoryInfo directory = new DirectoryInfo(tmpOldFolder);
                        var files = directory.GetFiles();

                        foreach (FileInfo item in files)
                        {
                            if (!string.IsNullOrEmpty(aFiles.file_name_1) && !item.Name.Contains(aFiles.file_name_1))
                            {
                                if (System.IO.File.Exists(Path.Combine(tmpOldFolder, item.FullName)))
                                {
                                    System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name), true);
                                }
                                else
                                {
                                    System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name), true);
                                }
                            }
                        }
                    }
                    #endregion

                   
                }

                TempData["Message"] = "File(s) deleted succesfully\n--------------\nNew revision created";

                return RedirectToAction("Index", new
                {
                    ArticleId = ArticleId,
                    RevisionId = RevisionId,
                    FileRevisionId = fSOnuc.rev_id
                });
            }
        }



        //SendToApprove

        public string GetSendToApproveName(string type)
        {
            string returnVal = "";
            switch (type)
            {
                case "AA":
                    returnVal = "Article Approve";
                    break;
                case "ZA":
                    returnVal = "Zone Approve";
                    break;
                case "FA":
                    returnVal = "Article File Approve";
                    break;
            }
            return returnVal;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "View", ContentType = "Article")]
        public ActionResult SendToApprove(int ArticleId, long? RevisionId, long? FileRevisionId, int? ClsfId, Guid UserId, string SendToApproveMessage, string ReturnUrl)
        {
            TempData.Clear();
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_FILE_SENDTOAPPROVE, this));
            try
            {
                if (string.IsNullOrEmpty(UserId.ToString()) || UserId.ToString() == "-1")
                {
                    throw new ApplicationException("Please select user");
                }

                CmsDbContext dbContext = new CmsDbContext();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                vAspNetMembershipUser getCurrentUser = new vAspNetMembershipUser();
                vAspNetMembershipUser getSelectedUser = new vAspNetMembershipUser();
                vArticlesZonesFull getArticleZoneFull = new vArticlesZonesFull();
                InstantMessaging getInstantMessaging = new InstantMessaging();

                getArticleZoneFull = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ArticleID == ArticleId).FirstOrDefault();
                getSelectedUser = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == UserId && v.IsApproved && !v.IsLockedOut).FirstOrDefault();
                getCurrentUser = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == currentUserId && v.IsApproved && !v.IsLockedOut).FirstOrDefault();

                if (getArticleZoneFull == null)
                {
                    throw new ApplicationException("Article is not found");
                }
                if (getSelectedUser == null || getCurrentUser == null)
                {
                    throw new ApplicationException("User is not found");
                }

                getInstantMessaging = dbContext.InstantMessagings.Where(im => im.From == currentUserId && im.Type == SendToApproveType.ArticleFileApprove.ToString() && im.RelatedId == ArticleId && im.ReadDate == null).FirstOrDefault();

                if (getInstantMessaging != null)
                {
                    throw new ApplicationException("You can not send to approve request for this article because you already sent");
                }

                var selectedUserProfile = System.Web.Profile.ProfileBase.Create(getSelectedUser.UserName, false);
                var currentUserProfile = System.Web.Profile.ProfileBase.Create(getCurrentUser.UserName, false);

                string selectedUserFullName = selectedUserProfile.GetPropertyValue("System.FullName").ToString().Trim();
                string currentUserFullName = currentUserProfile.GetPropertyValue("System.FullName").ToString().Trim();

                string articleRevisionPreviewUrl = "http://" + HttpContext.Request.Url.Host.ToString() + "/web/-1,-1," + (RevisionId != null ? RevisionId.ToString() : ArticleId.ToString());
                string articleUrl = "";
                string editArticleUrl = "";
                string mailBody = "";

                articleUrl = getArticleZoneFull.ArticleZoneAlias;
                if (string.IsNullOrEmpty(articleUrl))
                {
                    articleUrl = CmsHelper.getContentLinkAlias(getArticleZoneFull.ZoneID.ToString(), getArticleZoneFull.ArticleID.ToString(), getArticleZoneFull.SiteName, getArticleZoneFull.ZoneGroupName, getArticleZoneFull.ZoneName, getArticleZoneFull.Headline, "");
                }
                articleUrl = "http://" + HttpContext.Request.Url.Host + (articleUrl.StartsWith("/") ? articleUrl : "/" + articleUrl);

                editArticleUrl = "http://" + HttpContext.Request.Url.Host + "/cms/ArticleFile?ArticleId=" + ArticleId.ToString() + "&RevisionId=" + RevisionId.ToString() + "&FileRevisionId=" + FileRevisionId.ToString();

                // Mail Template Render Start
                var view = ViewEngines.Engines.FindView(ControllerContext, "SendToApproveMailTemplate", null);
                var writer = new StringWriter();
                var viewContext = new ViewContext(ControllerContext, view.View, ViewData, TempData, writer);
                view.View.Render(viewContext, writer);
                writer.Flush();
                mailBody = writer.ToString();
                int startIndex = mailBody.IndexOf("<!-- Start Template -->");
                int endIndex = mailBody.IndexOf("<!-- End Template -->");
                mailBody = mailBody.Substring(startIndex, endIndex - startIndex);
                mailBody = mailBody.Replace("<!-- Start Template -->", "");
                mailBody = mailBody.Replace("##ApproveType##", "ARTICLEFILE");
                mailBody = mailBody.Replace("##senderNameSurname##", currentUserFullName);
                mailBody = mailBody.Replace("##message##", SendToApproveMessage);
                mailBody = mailBody.Replace("##contentName##", getArticleZoneFull.Headline.Trim());
                mailBody = mailBody.Replace("##contentUrl##", articleUrl);
                mailBody = mailBody.Replace("##contentCMSUrl##", editArticleUrl);
                mailBody = mailBody.Replace("##date##", DateTime.Now.ToString("dd MMMM yyyy hh:mm"));
                mailBody = "<html><head></head>" + mailBody + "</html>";
                // Mail Template Render End


                // DB Insert Start
                InstantMessaging insertInstantMessaging = new InstantMessaging();
                insertInstantMessaging.CreateDate = DateTime.Now;
                insertInstantMessaging.From = currentUserId;
                insertInstantMessaging.Message = SendToApproveMessage.Trim(); //currentUserFullName + " kullanıcısı " + getArticleZoneFull.Headline.Trim() + " isimli article üzerinde bir değişiklik yaptı ve bunu onaylamanızı istiyor.";
                insertInstantMessaging.Subject = "ArticleFile Approve Request";
                insertInstantMessaging.RelatedId = getArticleZoneFull.ArticleID;
                insertInstantMessaging.RelatedName = RevisionId.ToString();
                insertInstantMessaging.To = UserId;
                insertInstantMessaging.Type = SendToApproveType.ArticleFileApprove;
                dbContext.InstantMessagings.Add(insertInstantMessaging);
                dbContext.SaveChanges();
                // DB Insert End


                // MAIL SEND START
                var mailResult = MailSender.SendMail(getSelectedUser.Email.Trim(), null, null, "ArticleFile Approve Request", mailBody, null);
                if (mailResult.status)
                {
                    TempData["Message"] = "Success";
                }
                else
                {
                    TempData["Message"] = mailResult.message;
                }
                // MAIL SEND END

            }
            catch (Exception ex)
            {
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", new { ArticleId = ArticleId, RevisionId = RevisionId, FileRevisionId = FileRevisionId });
            }
        }

        //SendToApprove


        private int ValidateFile(string filePath, int fileSize, string validExtensions, int validFileSize)
        {
            int retVal = 0;
            string ext = Path.GetExtension(filePath).Trim('.');
            List<string> exts = validExtensions.Replace(" ", "").Split(',').ToList<string>();
            if (exts.Contains(ext))
                retVal = 1;

            if (retVal > 0 && validFileSize > 0)
            {
                double fs = Math.Round((double)fileSize / 1024);
                if (fs > (double)validFileSize)
                    retVal = -1;
            }

            return retVal;
        }
    }
}
