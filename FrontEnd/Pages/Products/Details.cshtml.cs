using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<Review> ReviewList { get; set; }
        public int RatingTotal { get; set; }
        public int RatingAverage { get; set; }
        public void OnGet(int id)
        {           
            ShoppingCart = new()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory"),
                ProductId = id
            };
            ReviewList = _unitOfWork.Review.GetAll(filter: u => u.ProductId == id, includeProperties: "ApplicationUser");
            foreach (var review in ReviewList)
            {

                int ratingInt = Int32.Parse(review.Rating);
                RatingTotal += ratingInt;

            }
            //RatingAverage = RatingTotal / ReviewList.Count();
        }
        
        public IActionResult OnPost()
        {
            string returnUrl = Url.Content(HttpContext.Request.Path + HttpContext.Request.QueryString.ToString());
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                
                
                
                if(claim != null)
                {
                    ShoppingCart.ApplicationUserId = claim.Value;
                    ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    filter: u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    u.ProductId == ShoppingCart.ProductId);
                    if (shoppingCartFromDb == null)
                    {
                        ShoppingCart.Quantity = 1;
                        
                        _unitOfWork.ShoppingCart.Add(ShoppingCart);
                        _unitOfWork.Save();
                        return RedirectToPage("/Cart/Index");
                    }
                    else
                    {
                        ShoppingCart.Quantity = 1;
                        _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Quantity);                        
                    }
                    return RedirectToPage("/Cart/Index");
                }
                else
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity", ReturnUrl = returnUrl });
                }                
            }
            return Page();
        }
    }
}
