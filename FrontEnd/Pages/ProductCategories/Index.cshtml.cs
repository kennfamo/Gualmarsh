using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public IndexModel(IUnitOfWork db)
        {
            _db = db;
        }
        public void OnGet()
        {
            ProductCategories = _db.ProductCategory.GetAll();
        }
    }
}
