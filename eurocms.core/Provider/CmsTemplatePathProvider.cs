using EuroCMS.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace EuroCMS.Provider
{
    public class CmsTemplatePathProvider : VirtualPathProvider
    {
        private bool IsTemplatePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.EndsWith(".aspx",
                   StringComparison.InvariantCultureIgnoreCase);
        }
        public override bool FileExists(string virtualPath)
        {
            return (IsTemplatePath(virtualPath) ||
                    base.FileExists(virtualPath));
        }
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsTemplatePath(virtualPath))
                return new CmsTemplateFile(virtualPath);
            else
                return base.GetFile(virtualPath);
        }
    }
}
