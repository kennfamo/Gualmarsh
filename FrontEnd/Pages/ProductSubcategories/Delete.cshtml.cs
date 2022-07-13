using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.ProductSubcategories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public ProductSubcategory ProductSubcategory { get; set; }
        public void OnGet(int id)
        {
            ProductSubcategory = _db.ProductSubcategory.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var productSubcategoryFromDb = _db.ProductSubcategory.GetFirstOrDefault(u => u.Id == ProductSubcategory.Id);
            if (productSubcategoryFromDb != null)
            {
                _db.ProductSubcategory.Remove(productSubcategoryFromDb);
                _db.Save();
            }

            return RedirectToPage("Index");
        }
    }
}