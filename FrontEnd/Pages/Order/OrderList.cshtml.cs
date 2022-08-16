using BackEnd.Model;
using BackEnd.Repository.IRepository;
using BackEnd.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages.Order
{
    [Authorize]
    public class OrderListModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderListModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderHeader> OrderHeaderList { get; set; }
        public IEnumerable<OrderDetails> OrderDetailsList { get; set; }
       

       
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                OrderHeaderList = _unitOfWork.OrderHeader.GetAll(filter: u => u.ApplicationUserId == claim.Value);
                OrderDetailsList = _unitOfWork.OrderDetails.GetAll(includeProperties: "OrderHeader,OrderHeader.ApplicationUser,Product");
            }
        }
    }
}
