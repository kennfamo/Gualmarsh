using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Search
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Product> ProductList { get; set; }
        public JsonResult OnGet()
        {
            ProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");
            List<string> products = new List<string>();

            foreach (var product in ProductList)
            {
                products.Add(product.Name);
                products.Add((product.Id).ToString());
            }

            return new JsonResult(products);
        }
        public IActionResult OnGetProdId(string name)
        {
            ProductList = _unitOfWork.Product.GetAll(filter: u => u.Name == name);
            List<string> product = new List<string>();
            foreach (var p in ProductList)
            {
                product.Add(p.ShortName + "/" + p.Id);
            }


            return new JsonResult(product);
        }

    }
}
