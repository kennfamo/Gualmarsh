using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages.Products.Breakfast_Cereal
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }

        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductCategory"),
                ProductId = id
            };            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    filter: u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    u.ProductId == ShoppingCart.ProductId);
                if(shoppingCartFromDb == null)
                {
                    ShoppingCart.Quantity = 1;
                    _unitOfWork.ShoppingCart.Add(ShoppingCart);
                    _unitOfWork.Save();
                    return RedirectToPage("Index");
                }
                else
                {
                    ShoppingCart.Quantity = 1;
                    _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Quantity);
                    return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}
