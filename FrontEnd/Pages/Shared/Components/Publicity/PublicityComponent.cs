using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Pages.Shared.Components.Publicity
{
    public class PublicityViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublicityViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IEnumerable<ProductPublicity> ProductPublicityList { get; set; }
        public IViewComponentResult Invoke()
        {

            ProductPublicityList = _unitOfWork.ProductPublicity.GetAll().Take(1);

            return View("Default", ProductPublicityList);
        }
    }
}
