using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.UserPayments
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public UserPayment UserPayment { get; set; }
        public void OnGet(int id)
        {
            UserPayment = _db.UserPayment.GetFirstOrDefault(u => u.Id == id);   
        }

        public async Task<IActionResult> OnPost()
        {
            var userPaymentFromDb = _db.UserPayment.GetFirstOrDefault(u => u.Id == UserPayment.Id);
            if (userPaymentFromDb != null)
            {
                _db.UserPayment.Remove(userPaymentFromDb);
                _db.Save();
            }
            
            return RedirectToPage("Index");
        }
    }
}
