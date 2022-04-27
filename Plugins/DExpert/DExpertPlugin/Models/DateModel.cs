using System.Collections.Generic;

namespace EuroCMS.Plugin.DExpert.Models
{
    public class DateModel
    {
        public DateModel()
        {
            this.DisabledDayList = new List<string>();
        }

        public List<string> DisabledDayList { get; set; }

        public string LastEnabledDay { get; set; }
        public string FirstEnabledDay { get; set; }
    }
}