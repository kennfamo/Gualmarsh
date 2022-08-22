using BackEnd.Model;
using BackEnd.Repository;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Product> ProductListLatest { get; set; }
        public IEnumerable<Product> ProductListGreatValue { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {            
            ProductListLatest = _unitOfWork.Product.GetAll(includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory").
            OrderByDescending(u => u.CreatedDate).Take(6);
            ProductListGreatValue = _unitOfWork.Product.GetAll(filter: u => u.Brand == "Great Value").Take(6);
        }
    }
}