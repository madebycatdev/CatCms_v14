using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace EuroCMS.Core
{
    class AssemblyResourceVirtualFile : VirtualFile
    {
        string path;
        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }
        public override System.IO.Stream Open()
        {
            string[] parts = path.Split('/');
            string assemblyName = parts[2];
            string resourceName = parts[3];
            //assemblyName = Path.Combine(HttpRuntime.BinDirectory,
                                    //    assemblyName);
             
            System.Reflection.Assembly assembly =
               System.Reflection.Assembly.LoadFile( HttpContext.Current.Server.MapPath("~/App_Resource/Test/bin/WebApplication1.dll"));

            System.Reflection.Assembly.Load(HttpContext.Current.Server.MapPath("~/App_Resource/Test/bin/WebApplication1.dll"));

            if (assembly != null)
            {
                return assembly.GetManifestResourceStream("WebApplication1.Test.ascx");
            }
            return null;
        }
    }
}
