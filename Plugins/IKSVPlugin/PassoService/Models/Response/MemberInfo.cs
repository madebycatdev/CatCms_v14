using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class MemberInfoResult
    {
        public string errorMessage { get; set; }
        public string name { get; set; }
        public string membershipDate { get; set; }
        public int gender { get; set; }
        public string surname { get; set; }
        public bool result { get; set; }
        public string email { get; set; }
        public string mobilePhone { get; set; }
        public string birthDate { get; set; }
        public string workPhone { get; set; }
        public string LoggedToken { get; set; }
    }

    public class UpdateMemberInfoResult
    {
        public string errorMessage { get; set; }
        public bool result { get; set; }
    }
 
}
