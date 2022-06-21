using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Reviews
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public Review Review { get; set; }
        public void OnGet(int id)
        {
            Review = _db.Review.GetFirstOrDefault(u => u.Id == id);   
        }

        public async Task<IActionResult> OnPost()
        {
            var reviewFromDb = _db.Review.GetFirstOrDefault(u => u.Id == Review.Id);
            if (reviewFromDb != null)
            {
                _db.Review.Remove(reviewFromDb);
                _db.Save();
            }
            
            return RedirectToPage("Index");
        }
    }
}
