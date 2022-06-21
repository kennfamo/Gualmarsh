using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Discounts
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public Discount Discount { get; set; }
        public void OnGet(string id)
        {
            Discount = _db.Discount.GetFirstOrDefault(u => u.Name == id);
        }

        public async Task<IActionResult> OnPost()
        {
            _db.Discount.Update(Discount);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
