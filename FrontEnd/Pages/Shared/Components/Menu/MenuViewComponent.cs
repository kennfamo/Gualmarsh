using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Pages.Components.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ProductSubcategory> ProductSubcategoryList { get; set; }


        public IViewComponentResult Invoke()
        {
            ProductSubcategoryList = _unitOfWork.ProductSubcategory.GetAll(includeProperties: "ProductCategory");

            return View("Default", ProductSubcategoryList);
        }
    }
}
