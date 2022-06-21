using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Discounts
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public Discount Discount { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            _db.Discount.Add(Discount);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
