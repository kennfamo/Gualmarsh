using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Products
{
    public class Index2Model : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<Product> Products { get; set; }

        public Index2Model(IUnitOfWork db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Products = _db.Product.GetAll(includeProperties: "ProductCategory,Discount");
        }
    }
}
