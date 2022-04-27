using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class ContractsResult
    {
        public List<Contract> contractList { get; set; }
        public string errorMessage { get; set; }
    }
    public class Contract
    {
        public int id { get; set; }
        public string title { get; set; }
        public int code { get; set; }
        public string content { get; set; }
        public bool isRequired { get; set; }
    }
}
