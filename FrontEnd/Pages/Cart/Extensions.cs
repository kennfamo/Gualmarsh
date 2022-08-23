using BackEnd.Model;
using FrontEnd.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace FrontEnd.Pages.Cart
{
    public class Extensions
    {
        public string PayPalLogin()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                var dict = new Dictionary<string, string>();
                dict.Add("Content-Type", "application/x-www-form-urlencoded");
                dict.Add("grant_type", "client_credentials");
                HttpResponseMessage response = serviceObj.PostAsyncEncoded("v1/oauth2/token/", dict);
                response.EnsureSuccessStatusCode();

                var content = response.Content.ReadAsStringAsync().Result;
                JsonProperties? json = JsonConvert.DeserializeObject<JsonProperties>(content);
                return json.AccessToken;


            }
            catch (Exception)
            {
                throw;
            }

        }
        public string OrderBodyToJson(string domain, IEnumerable<ShoppingCart> shoppingCartList, OrderHeader orderHeader, CurrencyExchange currencyExchange, string discountAmount, double shipping)
        {
            double dollarRate = Double.Parse(currencyExchange.Venta);
            int i = 0;
            double total = 0.00;
            Item[] itemArray = new Item[shoppingCartList.Count()];
            foreach (var item in shoppingCartList)
            {
                Item orderDetails = new()
                {
                    Name = item.Product.Name,
                    Quantity = item.Quantity.ToString(),
                    UnitAmount = new UnitAmount()
                    {
                        CurrencyCode = "USD",
                        Value = Math.Round((item.Product.Price / dollarRate), 2).ToString()
                    }
                };
                ItemTotal itemTotal = new()
                {
                    CurrencyCode = "USD",
                    Value = Math.Round((item.Product.Price / dollarRate), 2).ToString()
                };
                itemArray[i] = orderDetails;
                total += Math.Round((item.Product.Price * item.Quantity / dollarRate), 2);
                i++;
            }
            discountAmount ??= "0";
            
            PaypalOrderDetails? paypalOrderDetails = new PaypalOrderDetails
            {
                Intent = "CAPTURE",
                PurchaseUnits = new PurchaseUnit[]
                {
                    new PurchaseUnit()
                    {
                        Items = itemArray,
                        Amount = new Amount()
                        {
                            CurrencyCode = "USD",
                            Value = Math.Round(total + Math.Round(shipping / dollarRate, 2) - (Double.Parse(discountAmount) / dollarRate), 2).ToString(),
                            Breakdown = new Breakdown()
                            {
                                ItemTotal = new ItemTotal()
                                {
                                    CurrencyCode = "USD",
                                    Value = total.ToString(),
                                },
                                Shipping = new ItemTotal()
                                {
                                    CurrencyCode = "USD",
                                    Value =  Math.Round(shipping / dollarRate, 2).ToString()
                                },
                                Discount = new ItemTotal()
                                {
                                    CurrencyCode = "USD",
                                    Value =  Math.Round(Double.Parse(discountAmount) / dollarRate, 2).ToString()
                                }
                            }
                        },
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = "https://" + domain + "/cart/checkout?handler=CapturePayment",
                    CancelUrl = "https://" + domain + "/cart/"
                }
            };
            return JsonConvert.SerializeObject(paypalOrderDetails);
        }
        public CurrencyExchange CurrencyExchangeRate()
        {
            ServiceRepository serviceObj = new ServiceRepository(1);
            HttpResponseMessage response = serviceObj.GetResponse("");
            string content = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<CurrencyExchange>(content);
        }
    }
}
