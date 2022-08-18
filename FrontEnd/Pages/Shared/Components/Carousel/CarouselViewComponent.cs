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
        public IEnumerable<Product> ProductList { get; set; }

        public IViewComponentResult Invoke()
        {

            ProductList = _unitOfWork.Product.GetAll().Take(3);

            return View("Default", ProductList);
        }
    }

}
