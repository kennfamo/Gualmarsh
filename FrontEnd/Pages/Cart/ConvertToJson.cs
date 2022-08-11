using BackEnd.Model;
using Newtonsoft.Json;

namespace FrontEnd.Pages.Cart
{
    public class ConvertToJson
    {
        public string OrderBodyToJson(IEnumerable<ShoppingCart> shoppingCartList, OrderHeader orderHeader)
        {
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
                        Value = item.Product.Price.ToString()
                    }
                };
                ItemTotal itemTotal = new()
                {
                    CurrencyCode = "USD",
                    Value = item.Product.Price.ToString()
                };
                itemArray[i] = orderDetails;
                total += (item.Product.Price * item.Quantity);
                i++;
            }
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
                            Value = total.ToString(),
                            Breakdown = new Breakdown()
                            {
                                ItemTotal = new ItemTotal()
                                {
                                    CurrencyCode = "USD",
                                    Value = total.ToString(),
                                }
                            }
                        },
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = "https://localhost:7063/Cart/Checkout?handler=CapturePayment",
                    CancelUrl = "https://localhost:7063/Cart/"
                }
            };
            return JsonConvert.SerializeObject(paypalOrderDetails);
        }
    }
}
