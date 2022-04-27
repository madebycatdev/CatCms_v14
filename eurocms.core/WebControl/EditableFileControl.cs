using EuroCMS.Core;
using EuroCMS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [DefaultProperty("DataValueField")]
    [ToolboxData("<{0}:EditableFile runat=server></{0}:EditableFile>")]
    public class EditableFile : System.Web.UI.WebControls.WebControl
    {

        private string _FileID = string.Empty;
        [Bindable(true)]
        [Localizable(true)]
        public string FileID
        {
            get
            {
                return _FileID;
            }

            set
            {

                _FileID = value;
            }
        }


        private string _DataValueField = string.Empty;
        [Bindable(true)]
        [Localizable(true)]
        public string DataValueField
        {
            get
            {
                return _DataValueField;
            }

            set
            {

                _DataValueField = value;
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {

            //  writer.RenderBeginTag("h2");
            //base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //  writer.RenderEndTag();
            //   base.RenderEndTag(writer);
        }



        protected override void RenderContents(HtmlTextWriter output)
        {
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];

            string sub_template = string.Empty;

            string EditableType = "article_file";
            string EditableID = "article_id";

            string EditableName = DataValueField;

            string[] fArri = DataValueField.Replace("##", "").Split('_');
            int articleID = Convert.ToInt32(ArticleDetails["article_id"]);
            string[] fReturn = new string[15];
            DataTable dt;
            if (string.IsNullOrEmpty(FileID))
            {
                dt = Dal.Instance.SelectGetAFilesData(fArri[1].ToString(), articleID);
            }
            else
            {
                dt = Dal.Instance.SelectGetAFilesDataByFileID(fArri[1].ToString(), articleID, Convert.ToInt32(FileID));
            }


            string EditableAlias = fArri[1].ToString();
            if (dt.Rows.Count > 0)
            {
                string file_title = dt.Rows[0][0].ToString();
                string file_comment = dt.Rows[0][1].ToString();
                string file_name_1 = dt.Rows[0][2].ToString();
                string file_name_2 = dt.Rows[0][3].ToString();
                string file_name_3 = dt.Rows[0][4].ToString();
                string file_name_4 = dt.Rows[0][5].ToString();
                string file_name_5 = dt.Rows[0][6].ToString();
                string file_name_6 = dt.Rows[0][7].ToString();
                string file_name_7 = dt.Rows[0][8].ToString();
                string file_name_8 = dt.Rows[0][9].ToString();
                string file_name_9 = dt.Rows[0][10].ToString();
                string file_name_10 = dt.Rows[0][11].ToString();

                if (!string.IsNullOrEmpty(file_name_1))
                    fReturn[1] = "/i/content/" + articleID + "_" + file_name_1;

                if (!string.IsNullOrEmpty(file_name_2))
                    fReturn[2] = "/i/content/" + articleID + "_" + file_name_2;

                if (!string.IsNullOrEmpty(file_name_3))
                    fReturn[3] = "/i/content/" + articleID + "_" + file_name_3;

                if (!string.IsNullOrEmpty(file_name_4))
                    fReturn[4] = "/i/content/" + articleID + "_" + file_name_4;

                if (!string.IsNullOrEmpty(file_name_5))
                    fReturn[5] = "/i/content/" + articleID + "_" + file_name_5;

                if (!string.IsNullOrEmpty(file_name_6))
                    fReturn[6] = "/i/content/" + articleID + "_" + file_name_6;

                if (!string.IsNullOrEmpty(file_name_7))
                    fReturn[7] = "/i/content/" + articleID + "_" + file_name_7;

                if (!string.IsNullOrEmpty(file_name_8))
                    fReturn[8] = "/i/content/" + articleID + "_" + file_name_8;

                if (!string.IsNullOrEmpty(file_name_9))
                    fReturn[9] = "/i/content/" + articleID + "_" + file_name_9;

                if (!string.IsNullOrEmpty(file_name_10))
                    fReturn[10] = "/i/content/" + articleID + "_" + file_name_10;

                fReturn[0] = file_title;
                fReturn[11] = file_comment;
            }

            string outPutHtml = string.Empty;
            if (CmsHelper.IsNumeric(fArri[2]))
            {
                if (fArri.Length > 3)
                {
                    outPutHtml = !string.IsNullOrEmpty(fReturn[Convert.ToInt32(fArri[2])]) ? "exist" : "hidden";
                }
                else
                {
                    outPutHtml = fReturn[Convert.ToInt32(fArri[2])];
                }

            }
            else
            {
                if (fArri[2] == "title")
                {
                    outPutHtml = fReturn[0];
                }
                else if (fArri[2] == "comment")
                {
                    outPutHtml = fReturn[11];
                }

            }

            string fileId = string.IsNullOrEmpty(FileID) ? "Boş" : FileID;

            if (Page.User.Identity.IsAuthenticated && (Page.User.IsInRole("Administrator") || Page.User.IsInRole("Editor") || Page.User.IsInRole("Author") || Page.User.IsInRole("ContentManager") || Page.User.IsInRole("ContentEntry") || Page.User.IsInRole("UserCreator") ))
            {
                // output.Write("<div data-type=\"" + EditableType + "\" data-alias=\""+EditableAlias+"\" data-id=\"" + EditableID + "\" data-name=\"" + EditableName + "\" contenteditable=\"true\">" + outPutHtml + "</div>");
                output.Write(outPutHtml);
            }
            else
            {
                output.Write(outPutHtml);
            }


        }

    }
}
