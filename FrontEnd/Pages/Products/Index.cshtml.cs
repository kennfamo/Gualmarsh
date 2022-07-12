using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FrontEnd.Pages.Products
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

        public void OnGet(int id)
        {
            
            ProductList = _unitOfWork.Product.GetAll(filter: u=>u.ProductCategoryId == id, includeProperties: "ProductCategory");

        }
    }
}
