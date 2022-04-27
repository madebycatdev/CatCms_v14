using System;
using System.ComponentModel;

namespace EuroCMS.Plugin.DExpert.Models
{
    public class AppointmentModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DefaultValue(false)]
        public bool KvkkSubmit { get; set; }
        [DefaultValue(false)]
        public bool EtradeSubmit { get; set; }
        public string ExpertiseAimName { get; set; }
        public string ExpertisePackageName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string CityName { get; set; }
        public string BranchCode { get; set; }
        public string SmsCode { get; set; }
        public bool SmsAllowed { get; set; }
        [DefaultValue(false)]
        public bool EmailSmsSubmit { get; set; }
    }
}