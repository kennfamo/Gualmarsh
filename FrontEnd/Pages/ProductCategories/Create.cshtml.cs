using BackEnd.Data;
using BackEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.ProductCategories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;        
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public ProductCategory ProductCategory { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _db.ProductCategory.AddAsync(ProductCategory);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
