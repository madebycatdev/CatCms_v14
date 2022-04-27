using System;

namespace EuroCMS.Plugin.DExpert.Models
{
    public class UpdateModel
    {
        public string AppointmentId { get; set; }
        public string ExpertisePackageName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BranchCode { get; set; }

        public string ExpertiseAimName { get; set; }

        public string Phone { get; set; }
        public bool SmsAllowed { get; set; }
        public string AppointmentTime { get; set; }
        public string CityName { get; set; }
        public string SmsCode { get; set; }
    }
}