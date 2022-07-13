using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Products
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _db.Product.GetFirstOrDefault(u => u.Id == id);   
        }

        public async Task<IActionResult> OnPost()
        {
            var productCategoryFromDb = _db.Product.GetFirstOrDefault(u => u.Id == Product.Id);
            if (productCategoryFromDb != null)
            {
                _db.Product.Remove(productCategoryFromDb);
                _db.Save();
            }
            
            return RedirectToPage("Index2");
        }
    }
}
