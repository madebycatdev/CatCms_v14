using EuroCMS.Admin.Common;
using EuroCMS.CMSPlugin.StandardProfil.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.CMSPlugin.StandardProfil.Controllers
{
    public class SpLogController : BaseController
    {
        // GET: SpLog
        public ActionResult Index()
        {
            return View("~/Views/CMSPlugins/StandardProfil/SpLog/Index.cshtml");
        }

        public ActionResult tracelog()
        {
            StandardProfilDbContext context = new StandardProfilDbContext();

            var data = context.TraceLogs.ToList();
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("IpAddress");
            sheetRow.CreateCell(2).SetCellValue("CountryCode");
            sheetRow.CreateCell(3).SetCellValue("CountryName");
            sheetRow.CreateCell(4).SetCellValue("SelectedCountryCode");
            sheetRow.CreateCell(5).SetCellValue("SelectedCountryName");
            sheetRow.CreateCell(6).SetCellValue("Name");
            sheetRow.CreateCell(7).SetCellValue("NameOfQib");
            sheetRow.CreateCell(8).SetCellValue("Email");
            sheetRow.CreateCell(9).SetCellValue("Permission");
            sheetRow.CreateCell(10).SetCellValue("Type");
            sheetRow.CreateCell(11).SetCellValue("CreateDate");

            sheetRowIndex++;
            foreach (var item in data)
            {
                sheetRow = sheet.CreateRow(sheetRowIndex);
                sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.IpAddress)));
                sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CountryCode)));
                sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CountryName)));
                sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.SelectedCountryCode)));
                sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.SelectedCountryName)));
                sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Name)));
                sheetRow.CreateCell(7).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.NameOfQib)));
                sheetRow.CreateCell(8).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                sheetRow.CreateCell(9).SetCellValue(item.Permission);
                sheetRow.CreateCell(10).SetCellValue((item.Type == 1 ? "restricted" : (item.Type == 2 ? "authorized" : (item.Type == 3 ? "usa" : "-"))));
                sheetRow.CreateCell(11).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CreateDate.ToString())));
                sheetRowIndex++;
            }
            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {

                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
            return View("~/Views/CMSPlugins/StandardProfil/SpLog/tracelog.cshtml");
        }
    }
}