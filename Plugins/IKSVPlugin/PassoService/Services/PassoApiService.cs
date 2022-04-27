using Newtonsoft.Json;
using PassoService.Helpers;
using PassoService.Models;
using PassoService.Models.Request;
using PassoService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Services
{
    public class PassoApiService
    {
        public static Token GetToken()
        {
            var credential = new
            {
                grant_type = "client_credentials",
                client_id = ClientHelper.ClientId,
                client_secret = ClientHelper.ClientSecret
            };
            var credentialString = JsonConvert.SerializeObject(credential, Formatting.None);

            var response = ClientHelper.RequestAsync(string.Empty, "identity/authenticate", credentialString);

            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<Token>(response.Result);

            }

            return null;
        }

        public static LoginResult Login(string cardNo, string pin)
        {
            var credential = new
            {
                grant_type = "password",
                client_id = ClientHelper.ClientId,
                client_secret = ClientHelper.ClientSecret,
                username = cardNo,
                password = pin
            };
            var credentialString = JsonConvert.SerializeObject(credential, Formatting.None);
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "user/login", credentialString);

            if (!string.IsNullOrEmpty(response.Result))
            {
                
                return JsonConvert.DeserializeObject<LoginResult>(response.Result);

            }

            return null;
        }

        public static ForgotPinResult ForgotPin(ForgotPinRequest forgotPinRequest)
        {
            String credentialString = JsonConvert.SerializeObject(forgotPinRequest, Formatting.None);
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "user/forgotpin", credentialString);
            string result = response.Result;
            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<ForgotPinResult>(response.Result);

            }
            return null;
        }

        public static ProductResult GetProductList()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "product/getproductlist", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<ProductResult>(response.Result);
            }

            return null;
        }

        public static MemberInfoResult GetMemberInfo()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.LoginToken.access_token, "user/getmemberinfo", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                var responseContent = JsonConvert.DeserializeObject<MemberInfoResult>(response.Result);
                return responseContent;
            }
            return null;
        }

        public static AddressResult GetUserAddresses()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.LoginToken.access_token, "user/getaddress", string.Empty);

            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<AddressResult>(response.Result);

            }

            return null;
        }

        public static CountryResult GetCountries()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "address/getcountries", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<CountryResult>(response.Result);
            }

            return null;
        }

        public static CityResult GetCities()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "address/getcities", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<CityResult>(response.Result);
            }

            return null;
        }

        public static TownResult GetTowns(string cityCode)
        {
            var requestParams = new
            {
                cityCode = cityCode
            };

            var credentialString = JsonConvert.SerializeObject(requestParams, Formatting.None);
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "address/gettowns", credentialString);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<TownResult>(response.Result);
            }

            return null;
        }

        public static ContractsResult GetContracts()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "application/getcontractlist", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<ContractsResult>(response.Result);
            }

            return null;
        }

        public static DeliveryTypeResult GetDeliveryTypes()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "application/getdeliverytypelist", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<DeliveryTypeResult>(response.Result);
            }

            return null;
        }

        public static UserProductResult GetUserProductList()
        {
            var response = ClientHelper.RequestAsync(CurrentSession.LoginToken.access_token, "user/getproduct", string.Empty);
            if (!string.IsNullOrEmpty(response.Result))
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return JsonConvert.DeserializeObject<UserProductResult>(response.Result, settings);
            }

            return null;
        }

        public static StartResult Start(StartRequest requestParams)
        {
            var credentialString = JsonConvert.SerializeObject(requestParams, Formatting.None);

            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "application/start", credentialString);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<StartResult>(response.Result);

            }
            return null;
        }

        public static UpdateMemberInfoResult UpdateMemberInfo(UpdateMemberInfoRequest updateMemberInfoRequest)
        {
            var credentialString = JsonConvert.SerializeObject(updateMemberInfoRequest, Formatting.None);

            var response = ClientHelper.RequestAsync(CurrentSession.LoginToken.access_token, "user/updatememberinfo", credentialString);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<UpdateMemberInfoResult>(response.Result);

            }
            return null;
        }

        public static SavePaymentResult SavePayment(SavePaymentRequest requestParams)
        {
            var credentialString = Newtonsoft.Json.JsonConvert.SerializeObject(requestParams, Formatting.None);
            ErrorsLog.WriteLog("INFO : " + "SavePayment", credentialString);
            var response = ClientHelper.RequestAsync(CurrentSession.AccessToken.access_token, "product/savepayment", credentialString);
            if (!string.IsNullOrEmpty(response.Result))
            {
                return JsonConvert.DeserializeObject<SavePaymentResult>(response.Result);
            }
            return null;
        }

        public static RenewProductResult ReNewProduct(RenewProductRequest requestParams)
        {
            var credentialString = JsonConvert.SerializeObject(requestParams, Formatting.None);
            ErrorsLog.WriteLog("INFO : " + "ReNewProduct", credentialString);
            var response = ClientHelper.RequestAsync(CurrentSession.LoginToken.access_token, "product/renewproduct", credentialString);
            string result = response.Result;
            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<RenewProductResult>(response.Result);
            }
            return null;
        }
    }
}