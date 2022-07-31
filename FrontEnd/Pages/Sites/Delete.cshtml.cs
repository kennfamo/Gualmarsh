using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Sites
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public Site Site { get; set; }
        public void OnGet(int id)
        {
            Site = _db.Site.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var siteFromDb = _db.Site.GetFirstOrDefault(u => u.Id == Site.Id);
            if (siteFromDb != null)
            {
                _db.Site.Remove(siteFromDb);
                _db.Save();
            }

            return RedirectToPage("Index");
        }
    }
}