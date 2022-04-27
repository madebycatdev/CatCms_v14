using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroCMS.WebControl
{
    public interface ICmsControl
    {
        string DisplayName { get; }

        string VersionName { get; }

        int VersionLevel { get; }

        string Author { get; }
    }
}
