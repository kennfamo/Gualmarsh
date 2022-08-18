using BackEnd.Model;
using BackEnd.Repository.IRepository;
using FrontEnd.Pages.Components.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        public IEnumerable<ProductCategory> ProductCategoryList { get; set; }

        public IViewComponentResult Invoke()
        {
            ProductCategoryList = _unitOfWork.ProductCategory.GetAll();
            ProductSubcategoryList = _unitOfWork.ProductSubcategory.GetAll(includeProperties: "ProductCategory");
            MenuViewModel menuViewModel = new MenuViewModel()
            {
                ProductSubcategoryList = ProductSubcategoryList,
                ProductCategoryList = ProductCategoryList
            };
            return View("Default", menuViewModel);
        }
    }
}
