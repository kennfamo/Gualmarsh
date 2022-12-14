using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.ProductCategories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public ProductCategory ProductCategory { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            _db.ProductCategory.Add(ProductCategory);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
