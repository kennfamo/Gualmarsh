using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.ProductCategories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
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
            var productCategoryFromDb = _db.ProductCategory.GetFirstOrDefault(u => u.Id == ProductCategory.Id);
            if (productCategoryFromDb != null)
            {
                _db.ProductCategory.Remove(productCategoryFromDb);
                _db.Save();
            }
            
            return RedirectToPage("Index");
        }
    }
}
