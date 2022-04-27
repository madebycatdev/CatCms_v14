
using Newtonsoft.Json;
using PassoService.Helpers;
using PassoService.Models;
using PassoService.Models.Response;
using PassoService.Models.Request;
using System.Linq;
using System.Web.Mvc;
using PassoService.Services;
using System.Collections.Generic;
using System.Web;
using System;

namespace PassoService.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Signup(int? id)
        {
            if (id == null || id == 0)
                return Redirect("~/uye-girisi");

            var productDetail = CurrentSession.Products.FirstOrDefault(x => x.productId == id);
            if (productDetail == null)
                return Redirect("~/uye-girisi");

            var orderDetail = new OrderDetail();
            orderDetail.product = productDetail;
            CurrentSession.OrderDetail = orderDetail;

            ViewBag.Countries = CurrentSession.Countries;
            ViewBag.Cities = CurrentSession.Cities;
            ViewBag.ProductDetail = productDetail;
            return View(new StartRequest());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Signup(StartRequest model, int id, string captchaResponse,
            string billingOtherTown, string billingOtherCity,
            string deliveryOtherTown, string deliveryOtherCity)
        {
            var isCaptchaValid = RecaptchaV2Helper.Validate(captchaResponse);

            if (!isCaptchaValid)
            {
                ViewBag.ResultMessage = "Güvenlik kodunu doğrulayın.";
                return View(model);
            }
            var product = CurrentSession.Products.FirstOrDefault(x => x.productId == id);
            string productprice = product.productPriceList.FirstOrDefault(x => x.productType == 0).productPriceId;
            int productpriceId = Convert.ToInt32(productprice);

            model.productPriceId = model.productPriceId != productpriceId ? model.productPriceId : productpriceId;
            model.name = model.name.ClearString();
            model.surname = model.surname.ClearString();
            model.mobilePhone = model.mobilePhone.ClearString().ClearSpaces();
            model.workPhone = model.workPhone.ClearString().ClearSpaces();
            model.billingAddress.address = model.billingAddress.address.ClearString();
            model.deliveryAddress.address = model.deliveryAddress.address.ClearString();

            model.billingAddress.cityCode = model.billingAddress.countryCode != "TR" ? billingOtherCity.ClearString() : model.billingAddress.cityCode;
            model.billingAddress.townCode = model.billingAddress.countryCode != "TR" ? billingOtherTown.ClearString() : model.billingAddress.townCode;

            model.deliveryAddress.cityCode = model.deliveryAddress.countryCode != "TR" ? deliveryOtherCity.ClearString() : model.deliveryAddress.cityCode;
            model.deliveryAddress.townCode = model.deliveryAddress.countryCode != "TR" ? deliveryOtherTown.ClearString() : model.deliveryAddress.townCode;


            var result = PassoApiService.Start(model);

            if (result.result && !string.IsNullOrEmpty(result.orderId))
            {

                var orderDetail = CurrentSession.OrderDetail;
                orderDetail.orderId = result.orderId;
                orderDetail.userId = result.userId;
                orderDetail.userInfo = model;
                orderDetail.isNewUser = true;
                CurrentSession.OrderDetail = orderDetail;

                return Redirect("~/odeme");
            }

            var productDetail = CurrentSession.Products.FirstOrDefault(x => x.productId == id);
            ViewBag.Countries = CurrentSession.Countries;
            ViewBag.Cities = CurrentSession.Cities;

            ViewBag.BillingAddressTowns = model.billingAddress.cityCode != string.Empty ? PassoApiService.GetTowns(model.billingAddress.cityCode).townList : null;
            ViewBag.DeliveryAddressTowns = model.deliveryAddress.cityCode != string.Empty ? PassoApiService.GetTowns(model.deliveryAddress.cityCode).townList : null;

            ViewBag.ProductDetail = productDetail;
            ViewBag.ResultMessage = result.errorMessage;
            ViewBag.StartModel = model;
            return View(model);
        }

        public ActionResult Payment(string result, string message)
        {
            if (CurrentSession.OrderDetail == null || string.IsNullOrEmpty(CurrentSession.OrderDetail.orderId))
                return Redirect("~/uye-girisi");

            if (result == "success")
            {
                return Redirect("~/hosgeldiniz");
            }
            else
            {
                ViewBag.ResultMessage = message;
            }
            ViewBag.Contracts = CurrentSession.Contracts;
            ViewBag.DeliveryTypes = CurrentSession.DeliveryTypes;
            ViewBag.Countries = CurrentSession.Countries;
            ViewBag.Cities = CurrentSession.Cities;

            var cityCode = CurrentSession.OrderDetail.userInfo.deliveryAddress.cityCode;
            ViewBag.Towns = PassoApiService.GetTowns(cityCode).townList;

            return View(CurrentSession.OrderDetail);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Payment(SavePaymentRequest model, string paymentCaptchaResponse, string otherTown, string otherCity)
        {
            var isCaptchaValid = RecaptchaV2Helper.Validate(paymentCaptchaResponse);

            if (!isCaptchaValid)
            {
                ViewBag.ResultMessage = "Güvenlik kodunu doğrulayın.";
                return View(CurrentSession.OrderDetail);
            }

            var contracts = Request.Form.AllKeys.Where(x=> x.Contains("contract-")).ToList();
            List<string> contractIds = new List<string>();
            foreach (var item in contracts)
            {
                contractIds.Add(Request.Form[item]);
            }

            if (contractIds.Count>0)
            {
                model.contractIdList = Array.ConvertAll(contractIds.ToArray(), s => int.Parse(s));
            }

            model.deliveryAddress.address = model.deliveryAddress.address.ClearString();
            model.deliveryAddress.cityCode = model.deliveryAddress.countryCode != "TR" ? otherCity.ClearString() : model.deliveryAddress.cityCode;
            model.deliveryAddress.townCode = model.deliveryAddress.countryCode != "TR" ? otherTown.ClearString() : model.deliveryAddress.townCode;


            var result = PassoApiService.SavePayment(model);
            if (result.result && !string.IsNullOrEmpty(result.paymentUrl))
            {

                // ViewBag.RedirectUrl = result.paymentUrl;
                return Redirect(result.paymentUrl);
            }

            ViewBag.Contracts = CurrentSession.Contracts;
            ViewBag.DeliveryTypes = CurrentSession.DeliveryTypes;
            ViewBag.Countries = CurrentSession.Countries;
            ViewBag.Cities = CurrentSession.Cities;

            var cityCode = CurrentSession.OrderDetail.userInfo.deliveryAddress.cityCode;
            ViewBag.Towns = PassoApiService.GetTowns(cityCode).townList;

            return View(CurrentSession.OrderDetail);
        }

        public ActionResult Welcome()
        {
            if (CurrentSession.OrderDetail == null || CurrentSession.OrderDetail.userInfo == null)
                return Redirect("~/uye-girisi");
            var model = CurrentSession.OrderDetail;

            if (CurrentSession.CurrentUserProduct != null && !model.isNewUser)
            {
                var productDetail = CurrentSession.Products.FirstOrDefault(x => x.productId == CurrentSession.CurrentUserProduct.productId);
                model.product = productDetail;
            }

            CurrentSession.OrderDetail = null;

            return View(model);
        }

        public ActionResult Signin()
        {
            if (CurrentSession.LoginToken != null)
                return Redirect("~/uye-bilgileri");
            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Signin(string cardNo, string pin, string captchaResponse)
        {
            var isCaptchaValid = RecaptchaV2Helper.Validate(captchaResponse);
            if (!isCaptchaValid)
            {
                ViewBag.ResultMessage = "Güvenlik kodunu doğrulayın.";
                return View();
            }

            cardNo = cardNo.ClearSpaces();
            var loginToken = PassoApiService.Login(cardNo, pin);

            if (loginToken != null && !string.IsNullOrEmpty(loginToken.access_token))
            {
                CurrentSession.LoginToken = loginToken;

                return Redirect("~/uye-bilgileri");
            }

            ViewBag.ResultMessage = loginToken.message;
            return View();
        }

        public ActionResult UserInfo()
        {
            if (CurrentSession.LoginToken == null)
                return Redirect("~/uye-girisi");

            var memberInfo = PassoApiService.GetMemberInfo();
            CurrentSession.MemberInfo = memberInfo;

            //add cookie user info 
            string token = Guid.NewGuid().ToString();
            HttpCookie cookie = new HttpCookie("PassoLoggedUserToken", token);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            Response.SetCookie(cookie);


            CurrentSession.MemberInfo.LoggedToken = token;

            if (memberInfo != null && memberInfo.result)
            {
                var products = PassoApiService.GetUserProductList();
                var addresses = PassoApiService.GetUserAddresses();

                var defaultProduct = products != null ? products.userProduct.FirstOrDefault() : null;
                if (defaultProduct == null)
                {
                    TempData["ResultMessage"] = "Lale Kartınız bulunmamaktadır.";
                    return Redirect("~/uye-girisi");
                }

                CurrentSession.CurrentUserProduct = defaultProduct;
                ViewBag.DefaultProduct = defaultProduct;
                ViewBag.Products = products != null ? products.userProduct : null;
                ViewBag.Countries = CurrentSession.Countries;
                ViewBag.Cities = CurrentSession.Cities;

                ViewBag.Contracts = CurrentSession.Contracts;
                ViewBag.DeliveryTypes = CurrentSession.DeliveryTypes;

                if (addresses != null)
                {
                    var billingAddress = addresses.userAddress.FirstOrDefault(x => x.isBillingAddress);
                    var deliveryAddress = addresses.userAddress.FirstOrDefault(x => x.isDeliveryAddress);

                    string billingAddressCity = billingAddress != null ? billingAddress.cityCode : string.Empty;
                    string deliveryAddressCity = deliveryAddress != null ? deliveryAddress.cityCode : string.Empty;


                    ViewBag.BillingAddressTowns = billingAddressCity != string.Empty ? PassoApiService.GetTowns(billingAddressCity).townList : null;
                    ViewBag.DeliveryAddressTowns = deliveryAddressCity != string.Empty ? PassoApiService.GetTowns(deliveryAddressCity).townList : null;

                    ViewBag.BillingAddress = billingAddress;
                    ViewBag.DeliveryAddress = deliveryAddress;
                }

                return View(memberInfo);
            }

            return Redirect("~/uye-girisi");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UserInfo(UpdateMemberInfoRequest model,
            string captchaResponse,
            string billingOtherTown, string billingOtherCity,
            string deliveryOtherTown, string deliveryOtherCity)
        {
            var isCaptchaValid = RecaptchaV2Helper.Validate(captchaResponse);

            if (!isCaptchaValid)
            {
                TempData["ResultMessage"] = "Güvenlik kodunu doğrulayın.";
                return Redirect("~/uye-bilgileri");
            }

            model.name = model.name.ClearString();
            model.surname = model.surname.ClearString();
            model.mobilePhone = model.mobilePhone.ClearString().ClearSpaces();
            model.workPhone = model.workPhone.ClearString().ClearSpaces();
            model.billingAddress.address = model.billingAddress.address.ClearString();
            model.deliveryAddress.address = model.deliveryAddress.address.ClearString();

            model.billingAddress.cityCode = model.billingAddress.countryCode != "TR" ? billingOtherCity : model.billingAddress.cityCode;
            model.billingAddress.townCode = model.billingAddress.countryCode != "TR" ? billingOtherTown : model.billingAddress.townCode;

            model.deliveryAddress.cityCode = model.deliveryAddress.countryCode != "TR" ? deliveryOtherCity : model.deliveryAddress.cityCode;
            model.deliveryAddress.townCode = model.deliveryAddress.countryCode != "TR" ? deliveryOtherTown : model.deliveryAddress.townCode;

            var result = PassoApiService.UpdateMemberInfo(model);
            TempData["ResultMessage"] = result.result ? "Üyelik bigileriniz güncellendi." : result.errorMessage;
            return Redirect("~/uye-bilgileri");
        }


        [HttpPost]
        public JsonResult ForgotPin(ForgotPinRequest forgotPinRequest, string forgetFormCaptcha)
        {
            var isCaptchaValid = RecaptchaV2Helper.Validate(forgetFormCaptcha);

            if (!isCaptchaValid)
            {
                return new JsonResult() { Data = new { result = false, message = "Güvenlik kodunu doğrulayın.", statusCode = 500 } };
            }

            forgotPinRequest.cardNumber = forgotPinRequest.cardNumber.ClearSpaces();
            forgotPinRequest.mobilePhone = forgotPinRequest.mobilePhone.ClearSpaces().ClearString();

            var response = PassoApiService.ForgotPin(forgotPinRequest);
            string message = !string.IsNullOrEmpty(response.message) ? response.message : response.errorMessage;

            return new JsonResult() { Data = new { result = response.result, message = message, statusCode = response.result ? 200 : 500 } };
        }

        [HttpPost]

        public JsonResult GetTowns(string cityId)
        {
            var towns = PassoApiService.GetTowns(cityId).townList;
            return new JsonResult() { Data = towns };
        }

        [HttpPost]
        public ActionResult RenewProduct(RenewProductRequest model, string renewCaptchaResponse, string otherTown, string otherCity)
        {
            var isCaptchaValid = RecaptchaV2Helper.Validate(renewCaptchaResponse);

            if (!isCaptchaValid)
            {
                TempData["ResultMessage"] = "Güvenlik kodunu doğrulayın.";
                return Redirect("~/uye-bilgileri");
            }

            model.deliveryAddress.address = model.deliveryAddress.address.ClearString();
            model.deliveryAddress.cityCode = model.deliveryAddress.countryCode != "TR" ? otherCity : model.deliveryAddress.cityCode;
            model.deliveryAddress.townCode = model.deliveryAddress.countryCode != "TR" ? otherTown : model.deliveryAddress.townCode;


            var contracts = Request.Form.AllKeys.Where(x => x.Contains("contract-")).ToList();
            List<string> contractIds = new List<string>();
            foreach (var item in contracts)
            {
                contractIds.Add(Request.Form[item]);
            }

            if (contractIds.Count > 0)
            {
                model.contractIdList = Array.ConvertAll(contractIds.ToArray(), s => int.Parse(s));
            }

            var result = PassoApiService.ReNewProduct(model);

            if (result.result && !string.IsNullOrEmpty(result.paymentUrl))
            {
                var orderDetail = CurrentSession.OrderDetail ?? new OrderDetail();

                orderDetail.orderId = result.orderId;
                orderDetail.isNewUser = false;
                orderDetail.userInfo = new StartRequest() { name = CurrentSession.MemberInfo.name, surname = CurrentSession.MemberInfo.surname, mobilePhone = result.mobilePhone };
                CurrentSession.OrderDetail = orderDetail;

                return Redirect(result.paymentUrl);
            }

            ViewBag.ResultMessage = !result.result ? result.ErrorMessage : "";
            TempData["ResultMessage"] = !result.result ? result.ErrorMessage : "";
            return RedirectToAction("UserInfo");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            if (Request.Cookies["PassoLoggedUserToken"] != null)
            {
                Response.Cookies["PassoLoggedUserToken"].Expires = DateTime.Now.AddDays(-1);
            }

            return Redirect("~/uye-girisi");
        }


        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLoggedUserInfo(string token)
        {
            if (CurrentSession.MemberInfo != null && CurrentSession.MemberInfo.LoggedToken == token)
            {
                return Json(new
                {
                    status = 200,
                    message = String.Concat(CurrentSession.MemberInfo.name, " ", CurrentSession.MemberInfo.surname),
                    link = "/uye-islemleri/uye-bilgileri",
                    logoutLink = "/uye-islemleri/cikis-yap"
                });
            }

            return Json(new { status = 400, message = "User is not logged in!" });
        }



    }


}