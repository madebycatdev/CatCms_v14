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
using System.Web.Mvc.Html;

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,PowerUser,Editor")]
    public class FileTypeController : BaseController
    {
        FileTypeDbContext context = new FileTypeDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "FileType")]
        public ActionResult Index()
        {
            
            var result = from sg in context.SelectFileTypes()
                         group sg by sg.group_name into g
                         select new Group<EuroCMS.Admin.entity.cms_asp_select_file_types_Result, string> 
                         { Key =  g.Key, Values = g };
            
            return View(result.ToList());
        }

        [CmsAuthorize(Permission = "Create", ContentType = "FileType")]
        public ActionResult Create()
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.FileType, null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "FileType")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.FILE_TYPE_CREATE, this));

            try
            {
                var fileType = GetValidFileType(collection, null);
                UpdateFileType(fileType);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "FileType")]
        public ActionResult Edit(int id)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.FileType, null);
            var result = context.SelectFileType(id).FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "FileType")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.FILE_TYPE_EDIT, this));

            try
            {
                var fileType = GetValidFileType(collection, id);
                UpdateFileType(fileType);
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
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.FILE_TYPE_DELETE, this));

            try
            {
                var result = context.DeleteFileType(id).FirstOrDefault();

                switch (result)
                {
                    case "0":
                    case "DELETED":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "1":
                        throw new ApplicationException("This file type used by article files. You need to delete this files first. File Type NOT deleted.");
                    default:
                        throw new ApplicationException("unexpected error! please contact with system administrator");
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
 
        public void UpdateFileType(cms_file_types fileType)
        {
            var result = context.SaveFileType(fileType).FirstOrDefault();

            switch (result.sStat)
            {
                case "D":
                    throw new ApplicationException("This file type name or type alias is already used. Please choose another one.");
                case "U":
                    TempData["Message"] = "Your File Type has been successfully updated.";
                    break;
                default:
                    TempData["Message"] = "Your File Type has been successfully created.";
                    break;
            }
        }

        public cms_file_types GetValidFileType(FormCollection collection, int? id)
        {
            if (string.IsNullOrEmpty(collection["type_name"]))
                throw new Exception("File Type Name required!");

            if (string.IsNullOrEmpty(collection["type_alias"]))
                throw new Exception("File Type Name required!");

            string type_alias = collection["type_alias"] ?? "";
            type_alias = type_alias.GetValidAlias();

            string file1_name = collection["filename_1"] ?? "null";
            string file1_extension = collection["fileextension_1"] ?? "";
            int file1_size = collection["filesize_1"] != null ? Convert.ToInt32(collection["filesize_1"]) : 0;
            string file1_wh = collection["filewh_1"] ?? "";

            string file2_name = collection["filename_2"] ?? "null";
            string file2_extension = collection["fileextension_2"] ?? "";
            int file2_size = collection["filesize_2"] != null ? Convert.ToInt32(collection["filesize_2"]) : 0;
            string file2_wh = collection["filewh_2"] ?? "";

            string file3_name = collection["filename_3"] ?? "null";
            string file3_extension = collection["fileextension_3"] ?? "";
            int file3_size = collection["filesize_3"] != null ? Convert.ToInt32(collection["filesize_3"]) : 0;
            string file3_wh = collection["filewh_3"] ?? "";

            string file4_name = collection["filename_4"] ?? "null";
            string file4_extension = collection["fileextension_4"] ?? "";
            int file4_size = collection["filesize_4"] != null ? Convert.ToInt32(collection["filesize_4"]) : 0;
            string file4_wh = collection["filewh_4"] ?? "";

            string file5_name = collection["filename_5"] ?? "null";
            string file5_extension = collection["fileextension_5"] ?? "";
            int file5_size = collection["filesize_5"] != null ? Convert.ToInt32(collection["filesize_5"]) : 0;
            string file5_wh = collection["filewh_5"] ?? "";

            string file6_name = collection["filename_6"] ?? "null";
            string file6_extension = collection["fileextension_6"] ?? "";
            int file6_size = collection["filesize_6"] != null ? Convert.ToInt32(collection["filesize_6"]) : 0;
            string file6_wh = collection["filewh_6"] ?? "";

            string file7_name = collection["filename_7"] ?? "null";
            string file7_extension = collection["fileextension_7"] ?? "";
            int file7_size = collection["filesize_7"] != null ? Convert.ToInt32(collection["filesize_7"]) : 0;
            string file7_wh = collection["filewh_7"] ?? "";

            string file8_name = collection["filename_8"] ?? "null";
            string file8_extension = collection["fileextension_8"] ?? "";
            int file8_size = collection["filesize_8"] != null ? Convert.ToInt32(collection["filesize_8"]) : 0;
            string file8_wh = collection["filewh_8"] ?? "";

            string file9_name = collection["filename_9"] ?? "null";
            string file9_extension = collection["fileextension_9"] ?? "";
            int file9_size = collection["filesize_9"] != null ? Convert.ToInt32(collection["filesize_9"]) : 0;
            string file9_wh = collection["filewh_9"] ?? "";

            string file10_name = collection["filename_10"] ?? "null";
            string file10_extension = collection["fileextension_10"] ?? "null";
            int file10_size = collection["filesize_10"] != null ? Convert.ToInt32(collection["filesize_10"]) : 0;
            string file10_wh = collection["filewh_10"] ?? "";

            #region Validate FileName
            if (!file1_name.Equals("null") && string.IsNullOrEmpty(file1_name))
                throw new Exception("File Name 1 required!");

            if (!file2_name.Equals("null") && string.IsNullOrEmpty(file2_name))
                throw new Exception("File Name 2 required!");

            if (!file3_name.Equals("null") && string.IsNullOrEmpty(file3_name))
                throw new Exception("File Name 3 required!");

            if (!file4_name.Equals("null") && string.IsNullOrEmpty(file4_name))
                throw new Exception("File Name 4 required!");

            if (!file5_name.Equals("null") && string.IsNullOrEmpty(file5_name))
                throw new Exception("File Name 5 required!");

            if (!file6_name.Equals("null") && string.IsNullOrEmpty(file6_name))
                throw new Exception("File Name 6 required!");

            if (!file7_name.Equals("null") && string.IsNullOrEmpty(file7_name))
                throw new Exception("File Name 7 required!");

            if (!file8_name.Equals("null") && string.IsNullOrEmpty(file8_name))
                throw new Exception("File Name 8 required!");

            if (!file9_name.Equals("null") && string.IsNullOrEmpty(file9_name))
                throw new Exception("File Name 9 required!");

            if (!file10_name.Equals("null") && string.IsNullOrEmpty(file10_name))
                throw new Exception("File Name 10 required!");
            #endregion
            #region Validate Extension
            if (!file1_name.Equals("null") && string.IsNullOrEmpty(file1_extension))
                throw new Exception("You need to specify file extensions to File1");

            if (!file2_name.Equals("null") && string.IsNullOrEmpty(file2_extension))
                throw new Exception("You need to specify file extensions to File2");

            if (!file3_name.Equals("null") && string.IsNullOrEmpty(file3_extension))
                throw new Exception("You need to specify file extensions to File3");

            if (!file4_name.Equals("null") && string.IsNullOrEmpty(file4_extension))
                throw new Exception("You need to specify file extensions to File4");

            if (!file5_name.Equals("null") && string.IsNullOrEmpty(file5_extension))
                throw new Exception("You need to specify file extensions to File5");

            if (!file6_name.Equals("null") && string.IsNullOrEmpty(file6_extension))
                throw new Exception("You need to specify file extensions to File6");

            if (!file7_name.Equals("null") && string.IsNullOrEmpty(file7_extension))
                throw new Exception("You need to specify file extensions to File7");

            if (!file8_name.Equals("null") && string.IsNullOrEmpty(file8_extension))
                throw new Exception("You need to specify file extensions to File8");

            if (!file9_name.Equals("null") && string.IsNullOrEmpty(file9_extension))
                throw new Exception("You need to specify file extensions to File9");

            if (!file10_name.Equals("null") && string.IsNullOrEmpty(file10_extension))
                throw new Exception("You need to specify file extensions to File10");
            #endregion
     
            if (string.IsNullOrEmpty(file1_name))
            {
                file1_extension = string.Empty;
                file1_size = 0;
                file1_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file2_name))
            {
                file2_extension = string.Empty;
                file2_size = 0;
                file2_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file3_name))
            {
                file3_extension = string.Empty;
                file3_size = 0;
                file3_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file4_name))
            {
                file4_extension = string.Empty;
                file4_size = 0;
                file4_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file5_name))
            {
                file5_extension = string.Empty;
                file5_size = 0;
                file5_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file6_name))
            {
                file6_extension = string.Empty;
                file6_size = 0;
                file6_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file7_name))
            {
                file7_extension = string.Empty;
                file7_size = 0;
                file7_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file8_name))
            {
                file8_extension = string.Empty;
                file8_size = 0;
                file8_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file9_name))
            {
                file9_extension = string.Empty;
                file9_size = 0;
                file9_wh = string.Empty;
            }

            if (string.IsNullOrEmpty(file10_name))
            {
                file10_extension = string.Empty;
                file10_size = 0;
                file10_wh = string.Empty;
            }

            cms_file_types fileType = new cms_file_types();
            fileType.type_id = id ?? -1;
            fileType.type_name = collection["type_name"] ?? "";
            fileType.type_alias = type_alias;
            fileType.group_id = !string.IsNullOrEmpty(collection["group_id"]) ? Convert.ToInt32(collection["group_id"]) : 0;
            fileType.structure_description = collection["structure_description"] ?? "";

            fileType.file1_name = file1_name.Equals("null") ? string.Empty : file1_name;
            fileType.file1_extension = file1_extension;
            fileType.file1_size = file1_size;
            fileType.file1_wh = file1_wh;

            fileType.file2_name = file2_name.Equals("null") ? string.Empty : file2_name;
            fileType.file2_extension = file2_extension;
            fileType.file2_size = file2_size;
            fileType.file2_wh = file2_wh;

            fileType.file3_name = file3_name.Equals("null") ? string.Empty : file3_name;
            fileType.file3_extension = file3_extension;
            fileType.file3_size = file3_size;
            fileType.file3_wh = file3_wh;

            fileType.file4_name = file4_name.Equals("null") ? string.Empty : file4_name;
            fileType.file4_extension = file4_extension;
            fileType.file4_size = file4_size;
            fileType.file4_wh = file4_wh;

            fileType.file5_name = file5_name.Equals("null") ? string.Empty : file5_name;
            fileType.file5_extension = file5_extension;
            fileType.file5_size = file5_size;
            fileType.file5_wh = file5_wh;

            fileType.file6_name = file6_name.Equals("null") ? string.Empty : file6_name;
            fileType.file6_extension = file6_extension;
            fileType.file6_size = file6_size;
            fileType.file6_wh = file6_wh;

            fileType.file7_name = file7_name.Equals("null") ? string.Empty : file7_name;
            fileType.file7_extension = file7_extension;
            fileType.file7_size = file7_size;
            fileType.file7_wh = file7_wh;

            fileType.file8_name = file8_name.Equals("null") ? string.Empty : file8_name;
            fileType.file8_extension = file8_extension;
            fileType.file8_size = file8_size;
            fileType.file8_wh = file8_wh;

            fileType.file9_name = file9_name.Equals("null") ? string.Empty : file9_name;
            fileType.file9_extension = file9_extension;
            fileType.file9_size = file9_size;
            fileType.file9_wh = file9_wh;

            fileType.file10_name = file10_name.Equals("null") ? string.Empty : file10_name;
            fileType.file10_extension = file10_extension;
            fileType.file10_size = file10_size;
            fileType.file10_wh = file10_wh;

            return fileType;
        }
    }
}
