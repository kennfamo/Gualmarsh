using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FrontEnd.Pages.Products.Breakfast_Cereal
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<ProductCategory> ProductCategoryList { get; set; }

        public void OnGet()
        {
            ProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategory");
            
        }
    }
}
