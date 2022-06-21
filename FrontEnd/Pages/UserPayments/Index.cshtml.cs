using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.UserPayments
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<UserPayment> UserPayments { get; set; }

        public IndexModel(IUnitOfWork db)
        {
            _db = db;
        }
        public void OnGet()
        {
            UserPayments = _db.UserPayment.GetAll(includeProperties: "ApplicationUser");
        }
    }
}
