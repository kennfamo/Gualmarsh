using BackEnd.Model;
using BackEnd.Repository.IRepository;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Pages.Shared.Components.Carousel
{
    public class CarouselViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;


        public CarouselViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IEnumerable<ProductPublicity> ProductList { get; set; }

        public IViewComponentResult Invoke()
        {

            ProductList = _unitOfWork.ProductPublicity.GetAll();

            return View("Default", ProductList);
        }
    }

}
