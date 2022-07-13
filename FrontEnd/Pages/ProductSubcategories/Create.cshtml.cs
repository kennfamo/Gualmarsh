using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.ProductSubcategories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public ProductSubcategory ProductSubcategory { get; set; }
        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }
        public void OnGet()
        {
            ProductCategoryList = _db.ProductCategory.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            _db.ProductSubcategory.Add(ProductSubcategory);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
