<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="false" Inherits="CKFinder.Settings.ConfigFile" %>
<%@ Import Namespace="CKFinder.Settings" %>
<script runat="server">

     

	/**
	 * This function must check the user session to be sure that he/she is
	 * authorized to upload and access files using CKFinder.
	 */
	public override bool CheckAuthentication()
	{
		// WARNING : DO NOT simply return "true". By doing so, you are allowing
		// "anyone" to upload and list the files in your server. You must implement
		// some kind of session validation here. Even something very simple as...
		//
        return HttpContext.Current.User.Identity.IsAuthenticated
            && (HttpContext.Current.User.IsInRole("Administrator")
            || HttpContext.Current.User.IsInRole("PowerUser")
            || HttpContext.Current.User.IsInRole("Editor")
            || HttpContext.Current.User.IsInRole("Author")
            || HttpContext.Current.User.IsInRole("ContentManager")
            || HttpContext.Current.User.IsInRole("ContentEntry"))
            || HttpContext.Current.User.IsInRole("UserCreator");
		//
		// ... where Session[ "IsAuthorized" ] is set to "true" as soon as the
		// user logs on your system.

        //if (Page.User.Identity.IsAuthenticated) //Eðer kullanýcý giriþi yapýlmýþ ise
        //    return true;
        //else
        //    return false;
 
	}

	/**
	 * All configuration settings must be defined here.
	 */
	public override void SetConfig()
	{
		// Paste your license name and key here. If left blank, CKFinder will
		// be fully functional, in Demo Mode.
		LicenseName = "";
		LicenseKey = "";

		// The base URL used to reach files in CKFinder through the browser.
        BaseUrl = "/i/"; 

       
		// The phisical directory in the server where the file will end up. If
		// blank, CKFinder attempts to resolve BaseUrl.
        BaseDir = HttpContext.Current.Server.MapPath("/i/");  


		// Optional: enable extra plugins (remember to copy .dll files first).
		Plugins = new string[] {
			//"CKFinder.Plugins.ImageResize, CKFinder_ImageResize"
		};
        
		// Settings for extra plugins.
		PluginSettings = new Hashtable();
		PluginSettings.Add("ImageResize_smallThumb", "90x90" );
		PluginSettings.Add("ImageResize_mediumThumb", "120x120" );
		PluginSettings.Add("ImageResize_largeThumb", "180x180" );
		// Name of the watermark image in plugins/watermark folder
        //PluginSettings.Add("Watermark_source", "logo.gif" );
        //PluginSettings.Add("Watermark_marginRight", "5" );
        //PluginSettings.Add("Watermark_marginBottom", "5" );
        //PluginSettings.Add("Watermark_quality", "90" );
        //PluginSettings.Add("Watermark_transparency", "80" );

		// Thumbnail settings.
		// "Url" is used to reach the thumbnails with the browser, while "Dir"
		// points to the physical location of the thumbnail files in the server.
		Thumbnails.Url = BaseUrl;
		if ( BaseDir != "" ) {
			Thumbnails.Dir = BaseDir;
		}
		Thumbnails.Enabled = true;
		Thumbnails.DirectAccess = false;
		Thumbnails.MaxWidth = 100;
		Thumbnails.MaxHeight = 100;
		Thumbnails.Quality = 80;

		// Set the maximum size of uploaded images. If an uploaded image is
		// larger, it gets scaled down proportionally. Set to 0 to disable this
		// feature.
		Images.MaxWidth = 5000;
		Images.MaxHeight = 5000;
		Images.Quality = 80;

		// Indicates that the file size (MaxSize) for images must be checked only
		// after scaling them. Otherwise, it is checked right after uploading.
		CheckSizeAfterScaling = true;

		// Increases the security on an IIS web server.
		// If enabled, CKFinder will disallow creating folders and uploading files whose names contain characters
		// that are not safe under an IIS 6.0 web server.
		DisallowUnsafeCharacters = true;

		// If CheckDoubleExtension is enabled, each part of the file name after a dot is
		// checked, not only the last part. In this way, uploading foo.php.rar would be
		// denied, because "php" is on the denied extensions list.
		// This option is used only if ForceSingleExtension is set to false.
		CheckDoubleExtension = true;

		// Due to security issues with Apache modules, it is recommended to leave the
		// following setting enabled. It can be safely disabled on IIS.
		ForceSingleExtension = true;

		// For security, HTML is allowed in the first Kb of data for files having the
		// following extensions only.
		HtmlExtensions = new string[] { "html", "htm", "xml", "js" };

		// Folders to not display in CKFinder, no matter their location. No
		// paths are accepted, only the folder name.
		// The * and ? wildcards are accepted.
		// By default folders starting with a dot character are disallowed.
		HideFolders = new string[] { ".*", "CVS" };

		// Files to not display in CKFinder, no matter their location. No
		// paths are accepted, only the file name, including extension.
		// The * and ? wildcards are accepted.
		HideFiles = new string[] { ".*" };

		// Perform additional checks for image files.
		SecureImageUploads = true;

		// The session variable name that CKFinder must use to retrieve the
		// "role" of the current user. The "role" is optional and can be used
		// in the "AccessControl" settings (bellow in this file).
		RoleSessionVar = "CKFinder_UserRole";

        
        string currentUserRole = string.Empty;
        
        if (Page.User.IsInRole("Administrator"))
            currentUserRole = "Administrator";
        else if (Page.User.IsInRole("PowerUser"))
            currentUserRole = "PowerUser";
        else if (Page.User.IsInRole("Editor"))
            currentUserRole = "Editor";
        else if (Page.User.IsInRole("Author"))
            currentUserRole = "Author";
        else if (Page.User.IsInRole("ContentManager"))
            currentUserRole = "ContentManager";
        else if (Page.User.IsInRole("ContentEntry"))
            currentUserRole = "ContentEntry";
        else if (Page.User.IsInRole("UserCreator"))
            currentUserRole = "UserCreator";
   
        Session[RoleSessionVar] = currentUserRole;
        
		// ACL (Access Control) settings. Used to restrict access or features
		// to specific folders.
		// Several "AccessControl.Add()" calls can be made, which return a
		// single ACL setting object to be configured. All properties settings
		// are optional in that object.
		// Subfolders inherit their default settings from their parents' definitions.
		//
		//	- The "Role" property accepts the special "*" value, which means
		//	  "everybody".
		//	- The "ResourceType" attribute accepts the special value "*", which
		//	  means "all resource types".

        AccessControl acl = AccessControl.Add();
        acl.Role = "*";
        acl.ResourceType = "*";
        acl.Folder = "/";

        acl.FolderView = true;
        acl.FolderCreate = true;
        acl.FolderRename = true;
        acl.FolderDelete = false;

        acl.FileView = true;
        acl.FileUpload = true;
        acl.FileRename = true;
        acl.FileDelete = false;
        
        AccessControl acl2 = AccessControl.Add();
        acl2.Role = "Administrator";
        acl2.ResourceType = "*";
        acl2.Folder = "/";

        acl2.FolderView = true;
        acl2.FolderCreate = true;
        acl2.FolderRename = true;
        acl2.FolderDelete = true;

        acl2.FileView = true;
        acl2.FileUpload = true;
        acl2.FileRename = true;
        acl2.FileDelete = true;
 
        AccessControl acl3 = AccessControl.Add();
        acl3.Role = "PowerUser";
        acl3.ResourceType = "*";
        acl3.Folder = "/";

        acl3.FolderView = true;
        acl3.FolderCreate = true;
        acl3.FolderRename = true;
        acl3.FolderDelete = true;

        acl3.FileView = true;
        acl3.FileUpload = true;
        acl3.FileRename = true;
        acl3.FileDelete = true;

        AccessControl acl4 = AccessControl.Add();
        acl4.Role = "Editor";
        acl4.ResourceType = "*";
        acl4.Folder = "/";

        acl4.FolderView = true;
        acl4.FolderCreate = true;
        acl4.FolderRename = true;
        acl4.FolderDelete = true;

        acl4.FileView = true;
        acl4.FileUpload = true;
        acl4.FileRename = true;
        acl4.FileDelete = true;

        //New Role
        AccessControl acl5 = AccessControl.Add();
        acl5.Role = "ContentEntry";
        acl5.ResourceType = "*";
        acl5.Folder = "/";

        acl5.FolderView = true;
        acl5.FolderCreate = true;
        acl5.FolderRename = true;
        acl5.FolderDelete = true;

        acl5.FileView = true;
        acl5.FileUpload = true;
        acl5.FileRename = true;
        acl5.FileDelete = true;

        AccessControl acl6 = AccessControl.Add();
        acl6.Role = "ContentManager";
        acl6.ResourceType = "*";
        acl6.Folder = "/";
           
        acl6.FolderView = true;
        acl6.FolderCreate = true;
        acl6.FolderRename = true;
        acl6.FolderDelete = true;
           
        acl6.FileView = true;
        acl6.FileUpload = true;
        acl6.FileRename = true;
        acl6.FileDelete = true;

        AccessControl ac17 = AccessControl.Add();
        ac17.Role = "UserCreator";
        ac17.ResourceType = "*";
        ac17.Folder = "/";

        ac17.FolderView = true;
        ac17.FolderCreate = true;
        ac17.FolderRename = true;
        ac17.FolderDelete = true;
           
        ac17.FileView = true;
        ac17.FileUpload = true;
        ac17.FileRename = true;
        ac17.FileDelete = true;

   
		// Resource Type settings.
		// A resource type is nothing more than a way to group files under
		// different paths, each one having different configuration settings.
		// Each resource type name must be unique.
		// When loading CKFinder, the "type" querystring parameter can be used
		// to display a specific type only. If "type" is omitted in the URL,
		// the "DefaultResourceTypes" settings is used (may contain the
		// resource type names separated by a comma). If left empty, all types
		// are loaded.

		// ==============================================================================
		// ATTENTION: Flash files with `swf' extension, just like HTML files, can be used
		// to execute JavaScript code and to e.g. perform an XSS attack. Grant permission
		// to upload `.swf` files only if you understand and can accept this risk.
		// ==============================================================================

		DefaultResourceTypes = "";

		ResourceType type;

        type = ResourceType.Add("assets");
        type.Url = BaseUrl + "assets/"+Session["Site"].ToString() +"/";
        type.Dir = BaseDir == "" ? "" : BaseDir + "assets/"+Session["Site"].ToString()+"/";
        type.MaxSize = 100000000;
        type.AllowedExtensions = new string[] { "ogv", "webm", "ogg", "m4v", "oga", "spx", "js", "css", "swf", "flv", "bmp", "gif", "jpeg", "jpg", "png", "woff", "ttf", "eot", "svg", "7z", "aiff", "asf", "avi", "bmp", "csv", "doc", "docx", "fla", "flv", "gif", "gz", "gzip", "jpeg", "jpg", "mid", "mov", "mp3", "mp4", "mpc", "mpeg", "mpg", "ods", "odt", "pdf", "png", "ppt", "pptx", "pxd", "qt", "ram", "rar", "rm", "rmi", "rmvb", "rtf", "sdc", "sitd", "swf", "sxc", "sxw", "tar", "tgz", "tif", "tiff", "txt", "vsd", "wav", "wma", "wmv", "xls", "xlsx", "zip" };
        type.DeniedExtensions = new string[] { };
         
        
        //type = ResourceType.Add( "files" );
        //type.Url = BaseUrl + "files/";
        //type.Dir = BaseDir == "" ? "" : BaseDir + "files/";
        //type.MaxSize = 0;
        //type.AllowedExtensions = new string[] { "woff", "ttf", "eot", "svg", "7z", "aiff", "asf", "avi", "bmp", "csv", "doc", "docx", "fla", "flv", "gif", "gz", "gzip", "jpeg", "jpg", "mid", "mov", "mp3", "mp4", "mpc", "mpeg", "mpg", "ods", "odt", "pdf", "png", "ppt", "pptx", "pxd", "qt", "ram", "rar", "rm", "rmi", "rmvb", "rtf", "sdc", "sitd", "swf", "sxc", "sxw", "tar", "tgz", "tif", "tiff", "txt", "vsd", "wav", "wma", "wmv", "xls", "xlsx", "zip" };
        //type.DeniedExtensions = new string[] { };

        //type = ResourceType.Add( "images" );
        //type.Url = BaseUrl + "images/";
        //type.Dir = BaseDir == "" ? "" : BaseDir + "images/";
        //type.MaxSize = 0;
        //type.AllowedExtensions = new string[] { "bmp", "gif", "jpeg", "jpg", "png" };
        //type.DeniedExtensions = new string[] { };

        //type = ResourceType.Add( "flash" );
        //type.Url = BaseUrl + "flash/";
        //type.Dir = BaseDir == "" ? "" : BaseDir + "flash/";
        //type.MaxSize = 0;
        //type.AllowedExtensions = new string[] { "swf", "flv" };
        //type.DeniedExtensions = new string[] { };

        //type = ResourceType.Add("styles");
        //type.Url = BaseUrl + "styles/";
        //type.Dir = BaseDir == "" ? "" : BaseDir + "styles/";
        //type.MaxSize = 0;
        //type.AllowedExtensions = new string[] { "css", "flv" };
        //type.DeniedExtensions = new string[] { };
   
	}

</script>
