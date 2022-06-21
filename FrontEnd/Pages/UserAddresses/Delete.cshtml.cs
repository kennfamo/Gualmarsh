using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.UserAddresses
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public UserAddress UserAddress { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public void OnGet(int id)
        {
            UserAddress = _db.UserAddress.GetFirstOrDefault(u => u.Id == id);
            CityList = _db.City.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            var userAddressFromDb = _db.UserAddress.GetFirstOrDefault(u => u.Id == UserAddress.Id);
            if (userAddressFromDb != null)
            {
                _db.UserAddress.Remove(userAddressFromDb);
                _db.Save();
            }
            
            return RedirectToPage("Index");
        }
    }
}
