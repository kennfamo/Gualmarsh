using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontEnd.Pages.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public double CartTotal { get; set; }
        public CartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        
        public IViewComponentResult Invoke()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "Product,Product.ProductSubcategory,Product.ProductSubcategory.ProductCategory");
                foreach (var cartItem in ShoppingCartList)
                {
                    CartTotal += (cartItem.Product.Price * cartItem.Quantity);
                }
            }
            CartViewModel cartViewModel = new CartViewModel()
            {
                ShoppingCartListViewModel = ShoppingCartList,
                CartTotalViewModel = CartTotal
            };
            return View("Default", cartViewModel);
        }
    }
}
