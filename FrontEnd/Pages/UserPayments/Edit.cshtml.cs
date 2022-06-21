using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.UserPayments
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public UserPayment UserPayment { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        public IEnumerable<SelectListItem> PaymentTypeList { get; set; }
        public IEnumerable<SelectListItem> ProviderList { get; set; }
        public void OnGet(int id)
        {
            UserPayment = _db.UserPayment.GetFirstOrDefault(u => u.Id == id);
            ApplicationUserList = _db.ApplicationUser.GetAll().Select(i => new SelectListItem()
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            });
            PaymentTypeList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Efectivo", Value = "Efectivo" },
                    new SelectListItem { Text = "Tarjeta", Value = "Tarjeta" },
                    new SelectListItem { Text = "SINPE", Value = "SINPE" },
                }, "Value", "Text"
            );
            ProviderList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "VISA", Value = "VISA" },
                    new SelectListItem { Text = "MasterCard", Value = "MasterCard" },
                    new SelectListItem { Text = "AMEX", Value = "AMEX" },
                }, "Value", "Text"
            );
        }

        public async Task<IActionResult> OnPost()
        {
            _db.UserPayment.Update(UserPayment);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
