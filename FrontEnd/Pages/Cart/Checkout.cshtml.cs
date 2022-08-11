using BackEnd.Model;
using BackEnd.Repository.IRepository;
using BackEnd.Utility;
using ClienteAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Nodes;

namespace FrontEnd.Pages.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public IEnumerable<UserAddress> UserAddressList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> ProvinceList { get; set; }
        public IEnumerable<SelectListItem> CantonList { get; set; }
        public IEnumerable<BackEnd.Model.Site> SiteList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public UserAddress UserAddress { get; set; }
        public string? DiscountAmount { get; set; }
        public string? DiscountCode { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public string? AccessToken { get; set; }
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                OrderHeader = new OrderHeader();
                SiteList = _unitOfWork.Site.GetAll();
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "Product,Product.ProductSubcategory,Product.ProductSubcategory.ProductCategory");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.Subtotal += (cartItem.Product.Price * cartItem.Quantity);
                }
                DiscountAmount = (string)TempData.Peek("DiscountAmount");
                DiscountCode = (string)TempData.Peek("DiscountCode");
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                UserAddressList = _unitOfWork.UserAddress.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "City,City.Canton,City.Canton.Province,ApplicationUser");
                ProvinceList = _unitOfWork.Province.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
        }
        public void PayPalLogin()
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
                PaypalModel? paypal = JsonConvert.DeserializeObject<PaypalModel>(content);
                AccessToken = paypal.AccessToken;

                
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public string OrderBodyToJson(OrderDetails orderDetails, OrderHeader orderHeader)
        {
            PaypalOrderDetails? paypalOrderDetails = new PaypalOrderDetails
            {
                Intent = "CAPTURE",
                PurchaseUnits = new PurchaseUnit[]
                    {
                        new PurchaseUnit()
                        {
                            Items = new Item[]
                            {
                               new Item()
                               {
                                   Name = orderDetails.Name,
                                   Description =  orderDetails.Product.Description,
                                   Quantity = orderDetails.Quantity.ToString(),
                                   UnitAmount = new UnitAmount()
                                   {
                                       CurrencyCode = "CRC",
                                       Value =  orderDetails.Product.Price.ToString()
                                   }
                               }
                            },
                            Amount = new Amount()
                            {
                                CurrencyCode = "CRC",
                                Value = orderHeader.Total.ToString(),
                                Breakdown = new Breakdown()
                                {
                                    ItemTotal = new ItemTotal()
                                    {
                                        CurrencyCode = "CRC",
                                        Value = (orderDetails.Quantity * orderDetails.Product.Price).ToString()
                                    }
                                }
                            }
                        }
                    },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = "https://localhost:7063/Cart/",
                    CancelUrl = "https://localhost:7063/Cart/"
                }
            };
            return JsonConvert.SerializeObject(paypalOrderDetails);
        }
            public JsonResult OnGetCanton(int provinceId)
        {
            CantonList = _unitOfWork.Canton.GetAll(filter: u => u.ProvinceId == provinceId).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return new JsonResult(CantonList);
        }
        public JsonResult OnGetCity(int cantonId)
        {
            CityList = _unitOfWork.City.GetAll(filter: u => u.CantonId == cantonId).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return new JsonResult(CityList);
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                DiscountAmount = (string)TempData.Peek("DiscountAmount");
                DiscountCode = (string)TempData.Peek("DiscountCode");
                if (DiscountAmount != null)
                {
                    OrderHeader.Total = OrderHeader.Subtotal + OrderHeader.Shipping - Double.Parse((string)TempData["DiscountAmount"]);
                }
                else
                {
                    OrderHeader.Total = OrderHeader.Subtotal + OrderHeader.Shipping;
                }
                OrderHeader.DiscountCode = DiscountCode;
                OrderHeader.Status = StaticDetails.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.ApplicationUserId = claim.Value;
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        ProductId = item.ProductId,
                        OrderId = OrderHeader.Id,
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        Quantity = item.Quantity
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                }
                _unitOfWork.Save();
                if (OrderHeader.PaymentType.Equals("PayPal"))
                {
                    PayPalLogin();
                    try
                    {
                        ServiceRepository serviceObj = new ServiceRepository(AccessToken);
                        var stringContent = new StringContent(OrderBodyToJson(OrderDetails, OrderHeader), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = serviceObj.PostAsyncStringContent("v2/checkout/orders/", stringContent);

                        response.EnsureSuccessStatusCode();

                        var content = response.Content.ReadAsStringAsync().Result;
                        PaypalResponse? paypalResponse = JsonConvert.DeserializeObject<PaypalResponse>(content);
                        string approveUrl;
                        foreach (var url in paypalResponse.Links)
                        {
                            if (url.Rel.Equals("approve"))
                            {
                                approveUrl = url.Href;
                                return Redirect(approveUrl);
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                return RedirectToPage("/Cart/Index");
            }
            return Page();
        }
    }
}
