using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages.Cart
{
    [Authorize]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public IEnumerable<UserAddress> UserAddressList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public UserAddress UserAddress { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }
        public void OnGet()
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
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                UserAddressList  = _unitOfWork.UserAddress.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "City,City.Canton,City.Canton.Province,ApplicationUser");
            }
        }
    }
}
