using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.UserAddresses
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<UserAddress> UserAddresses { get; set; }
        public IEnumerable<City> Cities { get; set; }

        public IndexModel(IUnitOfWork db)
        {
            _db = db;
        }
        public void OnGet()
        {
            UserAddresses = _db.UserAddress.GetAll(includeProperties: "City,ApplicationUser,City.Canton,City.Canton.Province");
        }
    }
}
