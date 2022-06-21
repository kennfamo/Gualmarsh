using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FrontEnd.Pages.Discounts
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public Discount Discount { get; set; }
        public void OnGet(int id)
        {
            Discount = _db.Discount.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var discountFromDb = _db.Discount.GetFirstOrDefault(u => u.Id == Discount.Id);
            if (discountFromDb != null)
            {
                _db.Discount.Remove(discountFromDb);
                _db.Save();
            }

            return RedirectToPage("Index");
        }
    }
}
