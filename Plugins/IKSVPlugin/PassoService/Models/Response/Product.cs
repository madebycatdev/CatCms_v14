using System;
using System.Collections.Generic;

namespace PassoService.Models.Response
{
    public class ProductResult
    {
        public List<Product> productList { get; set; }
        public bool result { get; set; }
        public string errorMessage { get; set; }
    }
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string code { get; set; }
        public string classCode
        {
            get
            {
                switch (this.code)
                {
                    case "0110":
                        return "black";
                    case "0210":
                    default:
                        return "white";
                    case "0310":
                        return "red";
                    case "0410":
                        return "yellow";
                    case "0510":
                        return  "blue";
                }
            }
        }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public List<ProductPrice> productPriceList { get; set; }


    }
    public class PaymentHistory
    {
        public string cardNo { get; set; }
        public PaymentModel paymentModel { get; set; }
        public string deliveryType { get; set; }
        public string title { get; set; }


    }
    public class ProductPrice
    {
        public string productPriceId { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public int productType { get; set; }
    }
    public class UserProductResult
    {
        public List<UserProductDetail> userProduct { get; set; }
        public bool result { get; set; }
        public string errorMessage { get; set; }
    }
    public class UserProductDetail
    {
        public int productId { get; set; }
        public int productPriceId { get; set; }
        public string orderId { get; set; }
        public string cardNo { get; set; }
        public string imageUrl { get; set; }
        public int status { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
     
        public bool isRenewable { get; set; }
        public List<UserProductPrice> productPriceList { get; set; }
        public List<PaymentHistory> paymentHistoryList { get; set; }

    }
    public class UserProductPrice
    {
        public string cardNo { get; set; }
        public string paymentModel { get; set; }
        public string deliveryType { get; set; }
        public string title { get; set; }
    }

    public class PaymentModel
    {
        public int paymentType { get; set; }
        public string description { get; set; }
        public string cardNumber { get; set; }
        public int installmentCount { get; set; }
        public string provisionCode { get; set; }
        public string paymentDate { get; set; }

      
    }
}
