using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.ProductCategories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public ProductCategory ProductCategory { get; set; }
        public void OnGet(int id)
        {
            ProductCategory = _db.ProductCategory.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            _db.ProductCategory.Update(ProductCategory);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
