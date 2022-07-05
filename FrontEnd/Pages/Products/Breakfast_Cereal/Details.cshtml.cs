using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Products.Breakfast_Cereal
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product Product { get; set; }

        public void OnGet(int id)
        {
            Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductCategory");
        }
    }
}
