using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages.Products
{
    [BindProperties]
    public class AddReviewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddReviewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product Product { get; set; }
        public Review Review { get; set; }

        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                Review.ApplicationUserId = claim.Value;
                Review.ProductId = Product.Id;
                Review.CreatedDate = System.DateTime.Now;
                _unitOfWork.Review.Add(Review);
                _unitOfWork.Save();
                return RedirectToPage("../Order/OrderList");
            }
            return Page();
            
        }
    }
}
