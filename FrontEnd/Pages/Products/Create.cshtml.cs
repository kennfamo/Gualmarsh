using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.Products
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }
        public IEnumerable<SelectListItem> DiscountList { get; set; }
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public void OnGet()
        {
            ProductCategoryList = _db.ProductCategory.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            DiscountList = _db.Discount.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Name
            });
        }

        public async Task<IActionResult> OnPost()
        {
            _db.Product.Add(Product);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
