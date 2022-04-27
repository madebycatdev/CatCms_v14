using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroCMS.Web
{
    public interface ICmsHttpModule
    {
        string DisplayName { get; }

        string VersionName { get; }

        int VersionLevel { get; }

        string Author { get; }
    }
}
