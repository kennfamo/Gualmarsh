using BackEnd.Model;
using BackEnd.Repository.IRepository;
using BackEnd.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;

namespace FrontEnd.Pages.Cart
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetailsList { get; set; }
        public int OrderId { get; set; }
        public OrderConfirmationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, 
                includeProperties: "ApplicationUser,Discount,UserAddress,UserAddress.City,UserAddress.City.Canton,UserAddress.City.Canton.Province,Site");
            OrderHeader.Status = StaticDetails.StatusInProcess;
            _unitOfWork.Save();
            OrderDetailsList = _unitOfWork.OrderDetails.GetAll(filter: u => u.OrderId == id, includeProperties: "Product");
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == OrderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            OrderId = id;
        }
    }
}
