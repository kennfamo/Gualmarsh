using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.UserAddresses
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public UserAddress UserAddress { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public void OnGet()
        {
            CityList = _db.City.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ApplicationUserList = _db.ApplicationUser.GetAll().Select(i => new SelectListItem()
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            _db.UserAddress.Add(UserAddress);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
