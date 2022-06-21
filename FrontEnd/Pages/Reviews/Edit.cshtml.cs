using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Pages.Reviews
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _db;        
        public EditModel(IUnitOfWork db)
        {
            _db = db;
        }
        
        public Review Review { get; set; }
        public IEnumerable<SelectListItem> RatingList { get; set; }
        public void OnGet(int id)
        {
            Review = _db.Review.GetFirstOrDefault(u => u.Id == id);
            RatingList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "1", Value = "1" },
                    new SelectListItem { Text = "2", Value = "2" },
                    new SelectListItem { Text = "3", Value = "3" },
                    new SelectListItem { Text = "4", Value = "4" },
                    new SelectListItem { Text = "5", Value = "5" },
                }, "Value", "Text"
            );
        }

        public async Task<IActionResult> OnPost()
        {
            _db.Review.Update(Review);
            _db.Save();
            return RedirectToPage("Index");
        }
    }
}
