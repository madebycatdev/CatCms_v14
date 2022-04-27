using EuroCMS.Admin.entity;
using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,PowerUser,Editor")]
    public class ClassificationController : BaseController
    {
        ClassificationDbContext context = new ClassificationDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "Classification")]
        public ActionResult Index(int? GroupId)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Classification, null);

            var result = from sg in context.SelectClasifications(GroupId)
                         group sg by sg.group_name into g
                         select new Group<EuroCMS.Admin.entity.cms_asp_select_classifications_Result, string> { Key = g.Key, Values = g };

            return View(result.ToList());
        }
 
        [CmsAuthorize(Permission = "Create", ContentType = "Classification")]
        public ActionResult Create()
        {
            return RedirectToAction("Edit", new { id = -1 });
        }
 
        [CmsAuthorize(Permission = "Edit", ContentType = "Classification")]
        public ActionResult Edit(int id)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Classification, null);

            if (id != -1)
            {
                cms_asp_select_classification_details_Result clsfm = context.SelectClassificationDetails(id).FirstOrDefault();

                for (int i = 1; i <= 20; i++)
                {
                    ViewData["combo_values_" + i] = context.SelectClassificationComboValues(id, i).InComboValue();
                }
                return View(clsfm);
            }
            else 
            {
                cms_asp_select_classification_details_Result clsfm = new cms_asp_select_classification_details_Result();
                return View(clsfm);
            }

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "Classification")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            if(id==-1)
                WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CLASSIFICATION_CREATE, this));
            else
                WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CLASSIFICATION_EDIT, this));

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Classification, null);

            cms_asp_select_classification_details_Result clsfm = new cms_asp_select_classification_details_Result();

            try
            {
                UpdateClassification(id, collection);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                ModelState.AddModelError("HATA", ex.Message);
            }

            return View(clsfm);
        }

        public bool substrCount(string stringa, string termine)
        {
            bool intReturn = false;
            stringa = stringa.Replace(" ", "");
            termine = termine.Replace(" ", "");

            if (stringa.Length > 0 && termine.Length > 0)
            {
                Regex objRegEx = new Regex(termine);
                intReturn = objRegEx.IsMatch(stringa);
            }
            else
            {
                intReturn = false;
            }
            return intReturn;

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CLASSIFICATION_DELETE, this));

            try
            {
                var result = context.DeleteClassification(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));

                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        context.DeleteClassificationComboValues(id, 0);
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "1":
                        throw new ApplicationException("This classification was not found OR already deleted before.");
                    case "2":
                        throw new ApplicationException("This classification used on some articles. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
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

        public cms_classifications GetValidClassification(int id, FormCollection collection)
        {
            cms_classifications classification = new cms_classifications();
            classification.classification_id = id;
            classification.classification_name = collection["classification_name"];
            classification.summary_cb = collection["summary_cb"] != null && collection["summary_cb"] == "1" ? true : false;
            classification.enddate_cb = collection["enddate_cb"] != null && collection["enddate_cb"] == "1" ? true : false;
            classification.keywords_cb = collection["keywords_cb"] != null && collection["keywords_cb"] == "1" ? true : false;
            classification.custom1_cb = collection["custom1_cb"] != null && collection["custom1_cb"] == "1" ? true : false;
            classification.custom2_cb = collection["custom2_cb"] != null && collection["custom2_cb"] == "1" ? true : false;
            classification.custom3_cb = collection["custom3_cb"] != null && collection["custom3_cb"] == "1" ? true : false;
            classification.custom4_cb = collection["custom4_cb"] != null && collection["custom4_cb"] == "1" ? true : false;
            classification.custom5_cb = collection["custom5_cb"] != null && collection["custom5_cb"] == "1" ? true : false;
            classification.custom6_cb = collection["custom6_cb"] != null && collection["custom6_cb"] == "1" ? true : false;
            classification.custom7_cb = collection["custom7_cb"] != null && collection["custom7_cb"] == "1" ? true : false;
            classification.custom8_cb = collection["custom8_cb"] != null && collection["custom8_cb"] == "1" ? true : false;
            classification.custom9_cb = collection["custom9_cb"] != null && collection["custom9_cb"] == "1" ? true : false;
            classification.custom10_cb = collection["custom10_cb"] != null && collection["custom10_cb"] == "1" ? true : false;
            classification.custom11_cb = collection["custom11_cb"] != null && collection["custom11_cb"] == "1" ? true : false;
            classification.custom12_cb = collection["custom12_cb"] != null && collection["custom12_cb"] == "1" ? true : false;
            classification.custom13_cb = collection["custom13_cb"] != null && collection["custom13_cb"] == "1" ? true : false;
            classification.custom14_cb = collection["custom14_cb"] != null && collection["custom14_cb"] == "1" ? true : false;
            classification.custom15_cb = collection["custom15_cb"] != null && collection["custom15_cb"] == "1" ? true : false;
            classification.custom16_cb = collection["custom16_cb"] != null && collection["custom16_cb"] == "1" ? true : false;
            classification.custom17_cb = collection["custom17_cb"] != null && collection["custom17_cb"] == "1" ? true : false;
            classification.custom18_cb = collection["custom18_cb"] != null && collection["custom18_cb"] == "1" ? true : false;
            classification.custom19_cb = collection["custom19_cb"] != null && collection["custom19_cb"] == "1" ? true : false;
            classification.custom20_cb = collection["custom20_cb"] != null && collection["custom20_cb"] == "1" ? true : false;
            classification.date1_cb = collection["date1_cb"] != null && collection["date1_cb"] == "1" ? true : false;
            classification.date2_cb = collection["date2_cb"] != null && collection["date2_cb"] == "1" ? true : false;
            classification.date3_cb = collection["date3_cb"] != null && collection["date3_cb"] == "1" ? true : false;
            classification.date4_cb = collection["date4_cb"] != null && collection["date4_cb"] == "1" ? true : false;
            classification.date5_cb = collection["date5_cb"] != null && collection["date5_cb"] == "1" ? true : false;
            classification.custom1_text = collection["custom1_text"] ?? string.Empty;
            classification.custom2_text = collection["custom2_text"] ?? string.Empty;
            classification.custom3_text = collection["custom3_text"] ?? string.Empty;
            classification.custom4_text = collection["custom4_text"] ?? string.Empty;
            classification.custom5_text = collection["custom5_text"] ?? string.Empty;
            classification.custom6_text = collection["custom6_text"] ?? string.Empty;
            classification.custom7_text = collection["custom7_text"] ?? string.Empty;
            classification.custom8_text = collection["custom8_text"] ?? string.Empty;
            classification.custom9_text = collection["custom9_text"] ?? string.Empty;
            classification.custom10_text = collection["custom10_text"] ?? string.Empty;
            classification.custom11_text = collection["custom11_text"] ?? string.Empty;
            classification.custom12_text = collection["custom12_text"] ?? string.Empty;
            classification.custom13_text = collection["custom13_text"] ?? string.Empty;
            classification.custom14_text = collection["custom14_text"] ?? string.Empty;
            classification.custom15_text = collection["custom15_text"] ?? string.Empty;
            classification.custom16_text = collection["custom16_text"] ?? string.Empty;
            classification.custom17_text = collection["custom17_text"] ?? string.Empty;
            classification.custom18_text = collection["custom18_text"] ?? string.Empty;
            classification.custom19_text = collection["custom19_text"] ?? string.Empty;
            classification.custom20_text = collection["custom20_text"] ?? string.Empty;
            classification.custom1_type = collection["custom1_type"] ?? "t";
            classification.custom2_type = collection["custom2_type"] ?? "t";
            classification.custom3_type = collection["custom3_type"] ?? "t";
            classification.custom4_type = collection["custom4_type"] ?? "t";
            classification.custom5_type = collection["custom5_type"] ?? "t";
            classification.custom6_type = collection["custom6_type"] ?? "t";
            classification.custom7_type = collection["custom7_type"] ?? "t";
            classification.custom8_type = collection["custom8_type"] ?? "t";
            classification.custom9_type = collection["custom9_type"] ?? "t";
            classification.custom10_type = collection["custom10_type"] ?? "t";
            classification.flag1_text = collection["flag1_text"] ?? string.Empty;
            classification.flag2_text = collection["flag2_text"] ?? string.Empty;
            classification.flag3_text = collection["flag3_text"] ?? string.Empty;
            classification.flag4_text = collection["flag4_text"] ?? string.Empty;
            classification.flag5_text = collection["flag5_text"] ?? string.Empty;
            classification.date1_text = collection["date1_text"] ?? string.Empty;
            classification.date2_text = collection["date2_text"] ?? string.Empty;
            classification.date3_text = collection["date3_text"] ?? string.Empty;
            classification.date4_text = collection["date4_text"] ?? string.Empty;
            classification.date5_text = collection["date5_text"] ?? string.Empty;
            classification.summary_text = collection["summary_text"] ?? string.Empty;
            classification.enddate_text = collection["enddate_text"] ?? string.Empty;
            classification.keywords_text = collection["keywords_text"] ?? string.Empty;
            classification.article1_text = collection["article1_text"] ?? string.Empty;
            classification.article2_text = collection["article2_text"] ?? string.Empty;
            classification.article3_text = collection["article3_text"] ?? string.Empty;
            classification.article4_text = collection["article4_text"] ?? string.Empty;
            classification.article5_text = collection["article5_text"] ?? string.Empty;
            classification.article1_cb = collection["article1_cb"] != null && collection["article1_cb"] == "1" ? true : false;
            classification.article2_cb = collection["article2_cb"] != null && collection["article2_cb"] == "1" ? true : false;
            classification.article3_cb = collection["article3_cb"] != null && collection["article3_cb"] == "1" ? true : false;
            classification.article4_cb = collection["article4_cb"] != null && collection["article4_cb"] == "1" ? true : false;
            classification.article5_cb = collection["article5_cb"] != null && collection["article5_cb"] == "1" ? true : false;
            classification.custom1_subcolumn = collection["custom1_subcolumn"] != null ? Convert.ToByte(collection["custom1_subcolumn"]) : (byte)0;
            classification.custom2_subcolumn = collection["custom2_subcolumn"] != null ? Convert.ToByte(collection["custom2_subcolumn"]) : (byte)0;
            classification.custom3_subcolumn = collection["custom3_subcolumn"] != null ? Convert.ToByte(collection["custom3_subcolumn"]) : (byte)0;
            classification.custom4_subcolumn = collection["custom4_subcolumn"] != null ? Convert.ToByte(collection["custom4_subcolumn"]) : (byte)0;
            classification.custom5_subcolumn = collection["custom5_subcolumn"] != null ? Convert.ToByte(collection["custom5_subcolumn"]) : (byte)0;
            classification.custom6_subcolumn = collection["custom6_subcolumn"] != null ? Convert.ToByte(collection["custom6_subcolumn"]) : (byte)0;
            classification.custom7_subcolumn = collection["custom7_subcolumn"] != null ? Convert.ToByte(collection["custom7_subcolumn"]) : (byte)0;
            classification.custom8_subcolumn = collection["custom8_subcolumn"] != null ? Convert.ToByte(collection["custom8_subcolumn"]) : (byte)0;
            classification.custom9_subcolumn = collection["custom9_subcolumn"] != null ? Convert.ToByte(collection["custom9_subcolumn"]) : (byte)0;
            classification.custom10_subcolumn = collection["custom10_subcolumn"] != null ? Convert.ToByte(collection["custom10_subcolumn"]) : (byte)0;
            classification.file_required_cb = collection["file_required_cb"] != null && collection["file_required_cb"] == "1" ? true : false;
            classification.file_title_required_cb = collection["file_title_required_cb"] != null && collection["file_title_required_cb"] == "1" ? true : false;
            classification.file_description_required_cb = collection["file_description_required_cb"] != null && collection["file_description_required_cb"] == "1" ? true : false;
            classification.required_file_types = collection["required_file_types"] ?? string.Empty;
            classification.created_by = Membership.GetUser().ProviderUserKey;
            classification.group_id = collection["group_id"] != null ? Convert.ToInt32(collection["group_id"]) : 0;
            classification.structure_description = collection["structure_description"] ?? string.Empty;

            if (string.IsNullOrEmpty(classification.classification_name))
                throw new Exception("Classification Name required!");

            return classification;
        }

        private void UpdateClassification(int  id, FormCollection collection)
        {
            cms_classifications classification = new cms_classifications();

            classification = GetValidClassification(id, collection);

            string group_id = collection["GroupID"].ToString();
            string classification_id = id.ToString();
            string classification_name = collection["classification_name"].ToString().Trim();

            if (string.IsNullOrEmpty(classification_name))
                throw new Exception("Classification Name is required!");

            if (!CmsHelper.IsNumeric(group_id)) { group_id = "0"; }
            if (!CmsHelper.IsNumeric(classification_id)) { classification_id = "0"; }

            var result = context.UpdateClassification(classification).FirstOrDefault();

            string strCv = string.Empty;
            string[] arrCv;
            string[] arrCv2;
            string[] sepCv = new string[1] { "~%~" };
            string[] sepCv2 = new string[1] { "|~|" };

            if (result != null)
            {
                string cStat = result.cStat;
                
                string created = result.created.ToString();
                string Result = string.Empty;
                int iOrderCV = 0;
                string cvID = string.Empty;
                string cvLabel = string.Empty;
                string cvSupID = string.Empty;

                if (cStat == "D")
                {
                    throw new ApplicationException("This classification name is already used. Please choose another one.");
                }
                else if (cStat == "U")
                {
                    TempData["Message"] = "Your classification has been succesflly updated";

                    for (int i = 1; i <= 20; i++)
                    {
                        if (collection["custom" + i + "_type"] == "c")
                        {
                            context.DeleteClassificationComboValues(Convert.ToInt32(classification_id), i);
                            strCv = collection["custom" + i + "_hidden"];
                            //strCv = strCv.Replace(((char)13 + (char)10).ToString(), "~%~");
                            //strCv = strCv.Replace(((char)10).ToString(), "~%~");
                            strCv = strCv.Replace(Environment.NewLine, "~%~");
                            //strCv = strCv.Replace("\r\n", "~%~");
                           // strCv = strCv new Regex("\\n").Replace(strCv, "~%~");
                            arrCv = strCv.Split(sepCv, StringSplitOptions.RemoveEmptyEntries);
                            iOrderCV = 0;
                            foreach (string xCV in arrCv)
                            {
                                iOrderCV++;
                                if (xCV.Contains("|~|"))
                                {
                                    arrCv2 = xCV.Split(sepCv2, 0);
                                    cvID = arrCv2[arrCv2.Length - 2].ToString();
                                    //cvID = arrCv2[arrCv2.Length - 2].ToString().Replace("", "");
                                    cvLabel = arrCv2[arrCv2.Length -1].ToString();
                                    //cvLabel = arrCv2[arrCv2.Length - 1].ToString().Replace(" ", "");

                                    cvSupID = "0";

                                    //if (substrCount(xCV, @"\|~\|"))
                                    //{
                                    //    for (int iCV2 = 0; iCV2 < arrCv2.Length; iCV2++)
                                    //    {
                                    //        cvSupID = cvSupID + "|~|" + arrCv2[iCV2];
                                    //    }
                                    //    cvSupID = cvSupID.Substring(5, cvSupID.Length - 5);
                                    //}
                                    //else
                                    //{
                                    //    cvSupID = "0";
                                    //}
                                }
                                else
                                {
                                    cvSupID = "0";
                                    cvID = xCV.Trim();
                                    cvLabel = xCV.Trim();
                                }

                                if (!string.IsNullOrEmpty(cvID))
                                {
                                    cms_asp_admin_update_classification_combo_values_Result ResultUp = context.UpdateClassificationComboValues(Convert.ToInt32(classification_id), Convert.ToInt32(i), cvSupID.ToString(), cvLabel.ToString(), cvID, Convert.ToInt32(iOrderCV), Membership.GetUser().ProviderUserKey).FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            context.DeleteClassificationComboValues(Convert.ToInt32(classification_id), i);
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "Your classification has been succesflly created";
                   
                    for (int i = 1; i <= 20; i++)
                    {
                        if (collection["custom" + i + "_type"] == "c")
                        {
                            context.DeleteClassificationComboValues(Convert.ToInt32(classification_id), i);
                            strCv = collection["custom" + i + "_hidden"];
                            //strCv = strCv.Replace(((char)13 + (char)10).ToString(), "~%~");
                            //strCv = strCv.Replace(((char)10).ToString(), "~%~");
                            strCv = strCv.Replace(Environment.NewLine, "~%~");
                            //strCv = strCv.Replace("\r\n", "~%~");
                            // strCv = strCv new Regex("\\n").Replace(strCv, "~%~");
                            arrCv = strCv.Split(sepCv, 0, StringSplitOptions.RemoveEmptyEntries);
                            iOrderCV = 0;
                            foreach (string xCV in arrCv)
                            {
                                iOrderCV++;
                                if (xCV.Contains("|~|"))
                                {
                                    arrCv2 = xCV.Split(sepCv2, 0);
                                    cvID = arrCv2[arrCv2.Length - 2].ToString();
                                    //cvID = arrCv2[arrCv2.Length - 2].ToString().Replace(" ", "");
                                    cvLabel = arrCv2[arrCv2.Length - 1].ToString();
                                    //cvLabel = arrCv2[arrCv2.Length - 1].ToString().Replace(" ", "");
                                    cvSupID = "0";

                                    //if (substrCount(xCV, @"\|~\|"))
                                    //{
                                    //    for (int iCV2 = 0; iCV2 < arrCv2.Length; iCV2++)
                                    //    {
                                    //        cvSupID = cvSupID + "|~|" + arrCv2[iCV2];
                                    //    }
                                    //    cvSupID = cvSupID.Substring(5, cvSupID.Length - 5);
                                    //}
                                    //else
                                    //{
                                    //    cvSupID = "0";
                                    //}
                                }
                                else
                                {
                                    cvSupID = "0";
                                    cvID = xCV.Trim();
                                    cvLabel = xCV.Trim();
                                }
 
                                if (!string.IsNullOrEmpty(cvID))
                                {
                                    cms_asp_admin_update_classification_combo_values_Result ResultUp = context.UpdateClassificationComboValues(Convert.ToInt32(classification_id), Convert.ToInt32(i), cvSupID.ToString(), cvLabel.ToString(), cvID, Convert.ToInt32(iOrderCV), Membership.GetUser().ProviderUserKey).FirstOrDefault();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ApplicationException("Saving classification d");
            }

            //switch (result.cStat)
            //{
            //    case "D":
            //        throw new ApplicationException("This classification name is already used. Please choose another one.");
            //    case "U":
            //        TempData["Message"] = "Your Classification has been successfully updated.";
            //        UpdateComboValues();
            //        break;
            //    default:
            //        TempData["Message"] = "Your Classification has been successfully created.";
            //        UpdateComboValues();
            //        break;
            //}
        }
    }
}
