using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EuroCMS.Configuration
{
    [ConfigurationCollection(typeof(AccessRuleElement), AddItemName = "rule")]
    public class AccessRuleSectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AccessRuleElement("default");
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AccessRuleElement)element).Name;
        }
 
        public AccessRuleElement this[string name]
        {
            get
            {
                return BaseGet(name) != null ? (AccessRuleElement)BaseGet(name) : null;
            }
        }

        public void Add(AccessRuleElement rule)
        {
            this.Add(rule);
        }

        public object[] GetAllKeys()
        {
            return base.BaseGetAllKeys();
        }
    }
}
