using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using BackEnd.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.OrderHeaders
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;

        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        public IEnumerable<SelectListItem> DiscountList { get; set; }
        public IEnumerable<SelectListItem> UserAddressList { get; set; }
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
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
            UserAddressList = _db.UserAddress.GetAll().Select(i => new SelectListItem()
            {
                Text = i.AddressLine1,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            OrderHeader.OrderDate = System.DateTime.Now;
            OrderHeader.Status = StaticDetails.StatusInProcess;
            _db.OrderHeader.Add(OrderHeader);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
