using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EuroCMS.Configuration
{
    public class AccessRuleElement : ConfigurationElement
    {
        public AccessRuleElement(string name)
        {
            Name = name;
            
        }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*{}/;'\"|\\")]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value.ToString(); }
        }

        [ConfigurationProperty("action", IsRequired = true)]
        public SecurityAction Action
        {
            get { return (SecurityAction)this["action"]; }
            set { this["action"] = Enum.Parse(typeof(SecurityAction), value.ToString()); }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public CmsContentType Type
        {
            get { return (CmsContentType)this["type"]; }
            set { this["type"] = Enum.Parse(typeof(CmsContentType), value.ToString()); }
        }

        [ConfigurationProperty("roles", IsRequired = false)]
        public string Roles
        {
            get { return this["roles"].ToString(); }
            set { this["roles"] = value.ToString(); }
        }

        [ConfigurationProperty("users", IsRequired = false)]
        public string Users
        {
            get { return this["users"].ToString(); }
            set { this["users"] = value.ToString(); }
        }
    }
}
