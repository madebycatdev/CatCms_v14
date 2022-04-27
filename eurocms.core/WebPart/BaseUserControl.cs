using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eurocms.controls.WebPart
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        private string _contentHtml;
        public BaseUserControl()
        {

        }

        public string ContentHtml
        {
            get { return _contentHtml; }
            set { _contentHtml = value; }
        }
    }
}
