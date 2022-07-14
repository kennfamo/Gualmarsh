using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages.Products
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
            ShoppingCart = new()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory"),
                ProductId = id
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    filter: u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    u.ProductId == ShoppingCart.ProductId);
                
                if(claim != null)
                {
                    if (shoppingCartFromDb == null)
                    {
                        ShoppingCart.Quantity = 1;
                        ShoppingCart.ApplicationUserId = claim.Value;
                        _unitOfWork.ShoppingCart.Add(ShoppingCart);
                        _unitOfWork.Save();
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        ShoppingCart.Quantity = 1;
                        _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Quantity);                        
                    }
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }                
            }
            return Page();
        }
    }
}
