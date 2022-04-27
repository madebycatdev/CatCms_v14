using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace EuroCMS.Provider
{
    public class CmsSqlPersonalizationProvider : SqlPersonalizationProvider
    { 
        private string _ZoneId = "0";
        public string ZoneId
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    if (System.Web.HttpContext.Current.Items["ZoneId"] != null)
                    {
                        return (string)System.Web.HttpContext.Current.Items["ZoneId"];
                    }
                }
                return _ZoneId;
            }
            set
            {
                _ZoneId = value;
            }
        }

        private string _ArticleId = "0";
        public string ArticleId
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    if (System.Web.HttpContext.Current.Items["ArticleId"] != null)
                    {
                        return (string)System.Web.HttpContext.Current.Items["ArticleId"];
                    }
                }
                return _ArticleId;
            }
            set
            {
                _ArticleId = value;
            }
        }

        public string FullPersonalizationPath
        {
            get 
            {
                return string.Format("{0}?ZoneId={1}&ArticleId={2}", System.Web.HttpContext.Current.Request.RawUrl, this.ZoneId, this.ArticleId);
            }
        }
         
        public override PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords)
        {
            if (query.PathToMatch != String.Empty)
            {
                query.PathToMatch = this.FullPersonalizationPath;
            }

            return base.FindState(scope, query, pageIndex, pageSize, out totalRecords);
        }

        public override int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query)
        {
            if (query.PathToMatch != String.Empty)
            {
                query.PathToMatch = this.FullPersonalizationPath;
            }

            return base.GetCountOfState(scope, query);
        }
 
        protected override void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob)
        {
            base.LoadPersonalizationBlobs(webPartManager, this.FullPersonalizationPath, userName, ref sharedDataBlob, ref userDataBlob);
        }
 
        protected override void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName)
        {
            base.ResetPersonalizationBlob(webPartManager, this.FullPersonalizationPath, userName);
        }

        public override int ResetState(PersonalizationScope scope, string[] paths, string[] usernames)
        {
            return base.ResetState(scope, new string[] { this.FullPersonalizationPath }, usernames);
        }

        public override int ResetUserState(string path, DateTime userInactiveSinceDate)
        {
            return base.ResetUserState(this.FullPersonalizationPath, userInactiveSinceDate);
        }

        protected override void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob)
        {
            base.SavePersonalizationBlob(webPartManager, this.FullPersonalizationPath, userName, dataBlob);
        }

        public override void Initialize(string name, NameValueCollection configSettings)
        {
            //GroupName = configSettings["groupName"];
            //if (string.IsNullOrEmpty(GroupName)) GroupName = "PageGroup";
            //configSettings.Remove("groupName");
            base.Initialize(name, configSettings);
        }
    }
}
