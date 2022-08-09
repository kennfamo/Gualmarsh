using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Products
{
    [BindProperties]
    public class ReviewsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Review> ReviewList { get; set; }
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            ReviewList = _unitOfWork.Review.GetAll(filter: u => u.ProductId == id, includeProperties: "Product,ApplicationUser");
        }
    }
}
