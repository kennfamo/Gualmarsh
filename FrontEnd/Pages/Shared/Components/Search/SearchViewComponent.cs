using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Pages.Components.Search
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public string? SearchString { get; set; }
        public SearchViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Product> ProductList { get; set; }


        public IViewComponentResult Invoke()
        {
            ProductList = _unitOfWork.Product.GetAll(filter: u => u.Name== SearchString, includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");

            return View("Default", SearchString);
        }

    }
}
