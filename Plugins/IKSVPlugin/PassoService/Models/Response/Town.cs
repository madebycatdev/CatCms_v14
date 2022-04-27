using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class TownResult
    {
        public List<Town> townList { get; set; }
        public bool result { get; set; }
        public string errorMessage { get; set; }
    }
    public class Town
    {
        public string townCode { get; set; }
        public string townName { get; set; }
    }

}
