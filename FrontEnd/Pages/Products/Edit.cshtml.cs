using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.Products
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ProductSubcategoryList { get; set; }
        public IEnumerable<SelectListItem> DiscountList { get; set; }
        public void OnGet(int id)
        {
            Product = _db.Product.GetFirstOrDefault(u => u.Id == id);
            ProductSubcategoryList = _db.ProductSubcategory.GetAll().Select(i => new SelectListItem()
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
            _db.Product.Update(Product);
            _db.Save();
            return RedirectToPage("List");
        }
    }
}
