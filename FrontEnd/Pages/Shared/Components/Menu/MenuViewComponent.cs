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
        public IEnumerable<ProductCategory> ProductCategoryList { get; set; }


        public IViewComponentResult Invoke()
        {
            ProductCategoryList = _unitOfWork.ProductCategory.GetAll();

            return View("Default", ProductCategoryList);
        }
    }
}
