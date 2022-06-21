using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.OrderDetails
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _db;
        public DeleteModel(IUnitOfWork db)
        {
            _db = db;
        }

        public OrderDetail OrderDetail { get; set; }
        public void OnGet(int id)
        {
            OrderDetail = _db.OrderDetail.GetFirstOrDefault(u => u.Id == id);   
        }

        public async Task<IActionResult> OnPost()
        {
            var orderDetailFromDb = _db.OrderDetail.GetFirstOrDefault(u => u.Id == OrderDetail.Id);
            if (orderDetailFromDb != null)
            {
                _db.OrderDetail.Remove(orderDetailFromDb);
                _db.Save();
            }
            
            return RedirectToPage("Index");
        }
    }
}
