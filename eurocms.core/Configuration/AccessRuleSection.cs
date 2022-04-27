using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EuroCMS.Configuration
{

    public class AccessRuleSection : ConfigurationSection
    {
        [ConfigurationProperty("accessRules", IsDefaultCollection = true)]
        public AccessRuleSectionCollection Rules 
        {
            get 
            {
                return (AccessRuleSectionCollection)base["accessRules"];
            }
        }
    }
}
