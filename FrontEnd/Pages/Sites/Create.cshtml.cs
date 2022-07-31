using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.Sites
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public CreateModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public Site Site { get; set; }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            _db.Site.Add(Site);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
