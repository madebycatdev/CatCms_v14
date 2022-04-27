using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class CityResult
    {
        public List<City> cityList { get; set; }
        public bool result { get; set; }
        public string errorMessage { get; set; }
    }
    public class City
    {
        //public string countryCode { get; set; }
        public string cityCode { get; set; }
        public string cityName { get; set; }
    }
}
