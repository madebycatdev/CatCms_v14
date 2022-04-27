using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class AddressResult
    {
        public List<Address> userAddress { get; set; }
        public string result { get; set; }
        public string errorMessage { get; set; }
    }
    public class Address
    {
        public string countryCode { get; set; }
        public string cityCode { get; set; }
        public string townCode { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public bool isDeliveryAddress { get; set; }
        public bool isBillingAddress { get; set; }
    }
}
