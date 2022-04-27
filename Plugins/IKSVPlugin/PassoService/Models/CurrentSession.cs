using PassoService.Helpers;
using PassoService.Models.Response;
using PassoService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Models
{
    public class CurrentSession
    {
        public static Token AccessToken
        {
            get
            {
                if (HttpContext.Current.Session["PassoApiAccessToken"] == null)
                    return null;
                return (Token)HttpContext.Current.Session["PassoApiAccessToken"];
            }
            set { HttpContext.Current.Session["PassoApiAccessToken"] = value; }
        }

        public static LoginResult LoginToken
        {
            get
            {
                if (HttpContext.Current.Session["PassoLoginAccessToken"] == null)
                    return null;
                return (LoginResult)HttpContext.Current.Session["PassoLoginAccessToken"];
            }
            set { HttpContext.Current.Session["PassoLoginAccessToken"] = value; }
        }

        public static List<Product> Products
        {
            get
            {
                if (HttpContext.Current.Session["PassoProducts"] == null)
                    return null;
                return (List<Product>)HttpContext.Current.Session["PassoProducts"];
            }
            set { HttpContext.Current.Session["PassoProducts"] = value; }
        }

        public static OrderDetail OrderDetail
        {
            get
            {
                if (HttpContext.Current.Session["PassoOrderDetail"] == null)
                    return null;
                return (OrderDetail)HttpContext.Current.Session["PassoOrderDetail"];
            }
            set { HttpContext.Current.Session["PassoOrderDetail"] = value; }
        }
        public static MemberInfoResult MemberInfo
        {
            get
            {
                if (HttpContext.Current.Session["PassoMemberInfo"] == null)
                    return null;
                return (MemberInfoResult)HttpContext.Current.Session["PassoMemberInfo"];
            }
            set { HttpContext.Current.Session["PassoMemberInfo"] = value; }
        }

        public static UserProductDetail CurrentUserProduct
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUserProduct"] == null)
                    return null;
                return (UserProductDetail)HttpContext.Current.Session["CurrentUserProduct"];
            }
            set { HttpContext.Current.Session["CurrentUserProduct"] = value; }
        }

        public static List<Country> Countries
        {
            get
            {
                if (!CacheHelper.IsIncache("PassoCountries"))
                {
                    if (!CacheHelper.IsIncache("PassoCountries"))
                    {
                        var countries = PassoApiService.GetCountries().countryList;
                        CacheHelper.SaveTocache("PassoCountries", countries, DateTime.Now.AddDays(7));
                        return countries;
                    }
                }
                return CacheHelper.GetFromCache<List<Country>>("PassoCountries");
            }
        }

        public static List<City> Cities
        {
            get
            {
                if (!CacheHelper.IsIncache("PassoCities"))
                {
                    if (!CacheHelper.IsIncache("PassoCities"))
                    {
                        var cities = PassoApiService.GetCities().cityList;
                        CacheHelper.SaveTocache("PassoCities", cities, DateTime.Now.AddDays(7));
                        return cities;
                    }
                }
                return CacheHelper.GetFromCache<List<City>>("PassoCities");
            }
        }

        public static List<Contract> Contracts
        {
            get
            {
                if (!CacheHelper.IsIncache("PassoContract"))
                {
                    if (!CacheHelper.IsIncache("PassoContract"))
                    {
                        var cities = PassoApiService.GetContracts().contractList;
                        CacheHelper.SaveTocache("PassoContract", cities, DateTime.Now.AddDays(7));
                        return cities;
                    }
                }
                return CacheHelper.GetFromCache<List<Contract>>("PassoContract");
            }
        }

        public static List<DeliveryType> DeliveryTypes
        {
            get
            {
                if (!CacheHelper.IsIncache("PassoDeliveryTypes"))
                {
                    if (!CacheHelper.IsIncache("PassoDeliveryTypes"))
                    {
                        var cities = PassoApiService.GetDeliveryTypes().deliveryTypeList;
                        CacheHelper.SaveTocache("PassoDeliveryTypes", cities, DateTime.Now.AddDays(7));
                        return cities;
                    }
                }
                return CacheHelper.GetFromCache<List<DeliveryType>>("PassoDeliveryTypes");
            }
        }
    }
}