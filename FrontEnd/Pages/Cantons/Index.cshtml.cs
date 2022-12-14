using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Cantons
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public IEnumerable<Canton> Cantons { get; set; }
        public IEnumerable<Province> Provinces { get; set; }

        public IndexModel(IUnitOfWork db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Cantons = _db.Canton.GetAll(includeProperties: "Province");
            Provinces = _db.Province.GetAll();
        }
    }
}
