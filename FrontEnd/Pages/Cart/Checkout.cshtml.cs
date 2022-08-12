using BackEnd.Model;
using BackEnd.Repository.IRepository;
using BackEnd.Utility;
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
using Stripe.Checkout;
using FrontEnd.Helpers;

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
        public OrderDetails OrderDetails { get; set; }
        public UserAddress UserAddress { get; set; }
        public string? DiscountAmount { get; set; }
        public string? DiscountCode { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public string? AccessToken { get; set; }
        [TempData]
        public string? PayOrderId { get; set; }
        [TempData]
        public int? OrderHeaderId { get; set; }
        private CurrencyExchange CurrencyExchange { get; set; }
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

        public IActionResult OnPostAddress()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                UserAddress.ApplicationUserId = claim.Value;
                _unitOfWork.UserAddress.Add(UserAddress);
                _unitOfWork.Save();
                return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "Product,Product.ProductSubcategory,Product.ProductSubcategory.ProductCategory");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.Subtotal += (cartItem.Product.Price * cartItem.Quantity);
                }
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
                if (DiscountCode == null)
                {
                    OrderHeader.DiscountCode = "NODISC";
                }
                else
                {
                    OrderHeader.DiscountCode = DiscountCode;
                }
                if (OrderHeader.SiteId == null)
                {
                    OrderHeader.SiteId = 15;
                }

                if (OrderHeader.UserAddressId == null)
                {
                    OrderHeader.UserAddressId = 8;
                }

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

                if (OrderHeader.PaymentType == "Credit / Debit Card")
                {
                    //Stripe Payment section
                    var domain = "https://localhost:7063";
                    var options = new SessionCreateOptions
                    {
                        LineItems = new List<SessionLineItemOptions>(),
                        PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                        Mode = "payment",
                        SuccessUrl = domain + $"/Cart/OrderConfirmation?id={OrderHeader.Id}",
                        CancelUrl = domain + "/Cart/Index",
                    };

                    //Add Line Items
                    foreach (var item in ShoppingCartList)
                    {
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)(item.Product.Price * 100),
                                Currency = "CRC",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Name
                                },
                            },
                            Quantity = item.Quantity
                        };
                        options.LineItems.Add(sessionLineItem);
                    }
                    var service = new SessionService();
                    Session session = service.Create(options);
                    Response.Headers.Add("Location", session.Url);
                    OrderHeader.SessionId = session.Id;
                    //OrderHeader.PaymentIntentId = session.PaymentIntentId;
                    _unitOfWork.Save();
                    return new StatusCodeResult(303);

                }
                else if (OrderHeader.PaymentType.Equals("PayPal"))
                {
                    try
                    {
                        Extensions extensions = new Extensions();  
                        ServiceRepository serviceObj = new ServiceRepository(extensions.PayPalLogin());
                        CurrencyExchange = extensions.CurrencyExchangeRate();
                        var stringContent = new StringContent(extensions.OrderBodyToJson(ShoppingCartList, OrderHeader, CurrencyExchange), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = serviceObj.PostAsyncStringContent("v2/checkout/orders/", stringContent);
                        var content = response.Content.ReadAsStringAsync().Result;
                        response.EnsureSuccessStatusCode();

                        PaypalResponse? paypalResponse = JsonConvert.DeserializeObject<PaypalResponse>(content);
                        string approveUrl;
                        PayOrderId = paypalResponse.Id;
                        OrderHeaderId = OrderHeader.Id;
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
        public IActionResult OnGetCapturePayment()
        {
            try
            {
                Extensions extensions = new Extensions();
                JsonProperties blankModel = new JsonProperties();
                var stringContent = new StringContent(JsonConvert.SerializeObject(blankModel), Encoding.UTF8, "application/json");
                ServiceRepository serviceObj = new ServiceRepository(extensions.PayPalLogin());
                HttpResponseMessage response = serviceObj.PostAsyncStringContent("/v2/checkout/orders/" + PayOrderId + "/capture", stringContent);
                var content = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                return RedirectToPage("/Cart/OrderConfirmation", new { id = OrderHeaderId });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

