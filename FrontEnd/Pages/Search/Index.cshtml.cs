using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Search
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Product> ProductList { get; set; }
        public void OnGet()
        {
           
        }
       
    }
}
