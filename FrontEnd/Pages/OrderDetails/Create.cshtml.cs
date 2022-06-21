using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.OrderDetails
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public OrderDetail OrderDetail { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        public IEnumerable<SelectListItem> DiscountList { get; set; }
        public void OnGet()
        {
            ApplicationUserList = _db.ApplicationUser.GetAll().Select(i => new SelectListItem()
            {
                Text = i.FirstName + " " + i.LastName,
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
            _db.OrderDetail.Add(OrderDetail);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
