using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Pages.Components.Publicity
{
    public class PublicityViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublicityViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IEnumerable<ProductPublicity> ProductPublicityList { get; set; }
        public IEnumerable<ProductSubcategory> ProductSubcategoryList { get; set; }
        public IEnumerable<ProductCategory> ProductCategoryList { get; set; }
        public IViewComponentResult Invoke()
        {

            ProductPublicityList = _unitOfWork.ProductPublicity.GetAll();
            ProductCategoryList = _unitOfWork.ProductCategory.GetAll();
            ProductSubcategoryList = _unitOfWork.ProductSubcategory.GetAll(includeProperties: "ProductCategory");

            PublicityViewModel publicityViewModel = new PublicityViewModel()
            {
                ProductPublicityList = ProductPublicityList,
                ProductSubCategoryList = ProductSubcategoryList,
                ProductCategoryList = ProductCategoryList
            };

            return View("Default", publicityViewModel);
        }
    }
}
