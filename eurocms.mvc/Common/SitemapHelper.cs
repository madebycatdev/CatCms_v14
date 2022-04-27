using EuroCMS.Admin.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EuroCMS.Admin.Common
{
    
    public class DefaultVisibilityProvider : SiteMapNodeVisibilityProviderBase
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            string s = sourceMetadata.ContainsKey("HtmlHelper") ? sourceMetadata["HtmlHelper"].ToString(): string.Empty;

            if (
                    (s.Equals("MvcSiteMapProvider.Web.Html.MenuHelper")
                    && ((node.Action != "Edit" && node.Action != "Revisions" && node.Action != "Details") && s.Equals("MvcSiteMapProvider.Web.Html.MenuHelper")))
                    || ((node.Action != "Edit" && node.Action != "Revisions" && node.Action != "Details") && s.Equals("MvcSiteMapProvider.Web.Html.SiteMapPathHelper"))
                )
            {
                return true;
            }
            return false;
        }
    }

    public class AdministratorVisibilityProvider : DefaultVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return Roles.IsUserInRole("Administrator");       
        }
    }

    public class PowerUserVisibilityProvider : DefaultVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                 .Where(t => t.Contains("Administrator")
                     || t.Contains("PowerUser")
                     ).Count() > 0;
        }
    }

    public class EditorVisibilityProvider : DefaultVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                 .Where(t => t.Contains("Administrator")
                     || t.Contains("PowerUser")
                     || t.Contains("Editor")
                     ).Count() > 0;
        }
    }

    public class AuthorVisibilityProvider : DefaultVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                 .Where(t => t.Contains("Administrator") 
                     || t.Contains("PowerUser")
                     || t.Contains("Editor")
                     || t.Contains("Author")
                     || t.Contains("ContentManager")
                     || t.Contains("UserCreator")
                     ).Count() > 0;
        }
    }    

    public class AdminUserCreatorVisibilityProvider : AdministratorVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {           
            return (base.IsVisible(node, sourceMetadata) 
                || (Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                .Where(t => t.Contains("UserCreator")
                    ).Count() > 0));
        }
    }

    public class PowerUserContentManagerVisibilityProvider : PowerUserVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return (base.IsVisible(node, sourceMetadata)
                || (Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                .Where(t => t.Contains("ContentManager")
                    || t.Contains("UserCreator")
                    ).Count() > 0));
        }
    }

    public class AuthorContentEntryVisibilityProvider : AuthorVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return (base.IsVisible(node, sourceMetadata)
                || (Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                .Where(t => t.Contains("ContentEntry")
                    ).Count() > 0));
        }
    }

    public class EditorContentEntryVisibilityProvider : EditorVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return (base.IsVisible(node, sourceMetadata)
                || (Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                .Where(t => t.Contains("ContentEntry")
                    || t.Contains("UserCreator")
                    ).Count() > 0));
        }
    }

    public class PowerUserContentEntryVisibilityProvider : PowerUserContentManagerVisibilityProvider
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return (base.IsVisible(node, sourceMetadata)
                || (Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)
                .Where(t => t.Contains("ContentEntry")
                    ).Count() > 0));
        }
    }

    public class ArticleFileVisibilityProvider : SiteMapNodeVisibilityProviderBase
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            string s = sourceMetadata.ContainsKey("HtmlHelper") ? sourceMetadata["HtmlHelper"].ToString() : string.Empty;

            if (
                    s.Equals("MvcSiteMapProvider.Web.Html.SiteMapPathHelper")
                )
            {
                return true;
            }
            return false;
        }
    }
    
    public class ClsfDynamicNodeProvider : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            using (ClassificationDbContext context = new ClassificationDbContext())
            {
                var result = context.SelectClasifications(0);
                
                List<DynamicNode> dnodes = new List<DynamicNode>();

                foreach (var clsf in result)
                {
                    

                    DynamicNode dynamicNode = new DynamicNode { Title = clsf.classification_name };
                    dynamicNode.ParentKey = "content";
                    dynamicNode.Key = "clsf_" + clsf.classification_id;
                    dynamicNode.Attributes.Add("ClsfId", clsf.classification_id.ToString());
                    dynamicNode.RouteValues.Add("ClsfId", clsf.classification_id.ToString());

                    //DynamicNode dynamicNode2 = new DynamicNode { Title = "Create" };
                    //dynamicNode2.ParentKey = dynamicNode.Key;
                    //dynamicNode2.Action = "Create";
                    //dynamicNode2.Controller = "Article";
                    //dynamicNode2.Key = "clsf_create_" + clsf.classification_id;
                    //dynamicNode2.PreservedRouteParameters.Add("id");
                    ////dynamicNode2.PreservedRouteParameters.Add("ClsfId");
                    //dynamicNode2.RouteValues.Add("ClsfId", clsf.classification_id.ToString());

                    //DynamicNode dynamicNode3 = new DynamicNode { Title = "Search" };
                    //dynamicNode3.ParentKey = dynamicNode.Key;
                    //dynamicNode3.Key = "clsf_search_" + clsf.classification_id;
                    //dynamicNode3.Action = "Index";
                    //dynamicNode3.Controller = "Article";
                    //dynamicNode3.PreservedRouteParameters.Add("id");
                    //// dynamicNode3.PreservedRouteParameters.Add("ClsfId");
                    //dynamicNode3.RouteValues.Add("ClsfId", clsf.classification_id.ToString());

                    dnodes.Add(dynamicNode);
                    //dnodes.Add(dynamicNode2);
                    //dnodes.Add(dynamicNode3);
                    //yield return dynamicNode;
                }

                return dnodes;
                 
                //node.RouteValues.Add("id", "12" );

                //return result.Select(t => new DynamicNode()
                //{
                //    Title = t.classification_name,
                //    RouteValues.Add("id", ),
                //    ParentKey = "content"
                //});
            }
        }

        private static bool CompareMustMatchRouteValues(IDictionary<string, object> mvcNodeRouteValues, IDictionary<string, object> routeValues)
        {
            var routeKeys = mvcNodeRouteValues.Keys;

            foreach (var pair in routeValues)
            {
                if (routeKeys.Contains(pair.Key) &&
                    mvcNodeRouteValues[pair.Key].ToString().ToLowerInvariant() != pair.Value.ToString().ToLowerInvariant())
                    return false;
            }
            return true;
        }
    }

}