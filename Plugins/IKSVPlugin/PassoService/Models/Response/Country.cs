using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class CountryResult
    {
        public List<Country> countryList {get; set;}
        public bool result { get; set; }
        public string errorMessage { get; set; }
    }
    public class Country
    {
        public string countryCode { get; set; }
        public string countryName { get; set; }
    }
}
