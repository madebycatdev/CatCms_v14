using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Threading;
using System.Web;

namespace EuroCMS.Admin.Common
{
    public class PermissionProvider
    {
        /////<summary>Gets or sets the name of the application 
        /////to store and retrieve permission information for. 
        /////</summary>
        //public static string ApplicationName
        //{
        //    get
        //    {
        //        if (Provider == null)
        //        {
        //            ProviderException exp = new ProviderException("Provider must not be null");
        //            throw exp;
        //        }
        //        return Provider.ApplicationName;
        //    }

        //    set
        //    {
        //        if (Provider == null)
        //        {
        //            ProviderException exp = new ProviderException("Provider must not be null");
        //            throw exp;
        //        }
        //        Provider.ApplicationName = value;
        //    }
        //}

        /////<summary>Gets or set the permission provider for the application.  </summary>
        //public static PermissionProvider Provider { get; set; }

        //private static void CheckStaticProperties()
        //{
        //    if (Provider == null)
        //    {
        //        ProviderException exp = new ProviderException("Provider must not be null");
        //        throw exp;
        //    }
        //}

        ///// <summary>
        ///// Gets a value indicating whether the currently logged-on user 
        ///// has in the specified permission. 
        ///// </summary>
        ///// <param name="permissionName">The name of the permission to search in.</param>
        ///// <returns>true if the currently logged-on user has in the specified permission; otherwise, false. </returns>
        //public static bool UserHasPermission(string permissionName)
        //{
        //    CheckStaticProperties();
        //    if (Thread.CurrentPrincipal != null)
        //        return UserHasPermission(Thread.CurrentPrincipal.Identity.Name, permissionName);
        //    else
        //        return false;
        //}

        ///// <summary>
        ///// Gets a value indicating whether the specified user has the specified permission. 
        ///// </summary>
        ///// <param name="userName">The name of the user to search for. </param>
        ///// <param name="permissionName">The name of the permission to search in.</param>
        ///// <returns>true if the currently logged-on user has in the specified permission; otherwise, false. </returns>
        //public static bool UserHasPermission(string userName, string permissionName)
        //{
        //    CheckStaticProperties();

        //    if (string.IsNullOrEmpty(userName))
        //    {
        //        ArgumentNullException aneu = new ArgumentNullException("userName");
        //        throw aneu;
        //    }

        //    if (string.IsNullOrEmpty(permissionName))
        //    {
        //        ArgumentNullException aneu = new ArgumentNullException("permissionName");
        //        throw aneu;
        //    }
        //    return Provider.UserHasPermission(userName, permissionName);
        //}
    }
}