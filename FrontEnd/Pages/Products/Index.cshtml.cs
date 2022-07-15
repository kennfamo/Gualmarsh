using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
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
        public IEnumerable<ProductSubcategory> ProductSubcategoryList { get; set; }

        public void OnGet(int id)
        {
            ProductSubcategoryList = _unitOfWork.ProductSubcategory.GetAll(filter: u => u.Id == id, includeProperties: "ProductCategory");
            ProductList = _unitOfWork.Product.GetAll(filter: u=>u.ProductSubcategoryId == id, includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");

        }
        public IActionResult OnGetAutoComplete()
        {
            ProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");
            List<string> products = new List<string>();

            foreach (var product in ProductList)
            {
                products.Add(product.Name);
            }

            return new JsonResult(products);
        }
        public void OnPostSubmit()
        {
            
        }
    }
}
