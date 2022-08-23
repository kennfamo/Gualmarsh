using BackEnd.Model;
using BackEnd.Repository;
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
        private readonly IConfiguration Configuration;

        public DetailsModel(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            Configuration = configuration;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public IEnumerable<Review> ReviewList { get; set; }
        public IEnumerable<Review> ReviewListFiltered { get; set; }
        public Review Review { get; set; }
        public HelpfulReview HelpfulReview { get; set; }
        public IEnumerable<HelpfulReview> HelpfulReviewList { get; set; }
        public IEnumerable<HelpfulReview> HelpfulReviewListUser { get; set; }
        public int RatingTotal { get; set; }
        public int RatingAverage { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool ShowPrevious => PageIndex > 1;
        public bool ShowNext => PageIndex < TotalPages;

        public void OnGet(string name, int id, int pageIndex)
        {           
            ShoppingCart = new()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.ShortName == name, includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory"),
                ProductId = id
            };
            ReviewList = _unitOfWork.Review.GetAll(filter: u => u.ProductId == id, includeProperties: "ApplicationUser");
            ReviewListFiltered = _unitOfWork.Review.GetAll(filter: u => u.ProductId == id, includeProperties: "ApplicationUser").
                OrderBy(u => u.Id).Skip((pageIndex - 1) * 4).Take(4);
            TotalPages = (int)Math.Ceiling(decimal.Divide(ReviewList.Count(), 4));
            foreach (var review in ReviewList)
            {
                RatingTotal += review.Rating;

            }
            if (ReviewList.Count() > 0)
            {
                RatingAverage = RatingTotal / ReviewList.Count();
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                HelpfulReviewListUser = _unitOfWork.HelpfulReview.GetAll(u => u.ApplicationUserId == claim.Value);
            }
            HelpfulReviewList = _unitOfWork.HelpfulReview.GetAll();
            var pageSize = Configuration.GetValue("PageSize", 4);
        }

        public IActionResult OnGetHelpful(int id, int product)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);            
            if (claim != null)
            {
                HelpfulReview = new HelpfulReview();
                Review = _unitOfWork.Review.GetFirstOrDefault(u => u.Id == id);
                HelpfulReview.ReviewId = Review.Id;
                HelpfulReview.ApplicationUserId = claim.Value;
                _unitOfWork.HelpfulReview.Add(HelpfulReview);
                _unitOfWork.Save();
                
            }
            
            return RedirectToPage(new {id = product });
        }
        public IActionResult OnGetUnHelpful(int id, int product)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                HelpfulReview = _unitOfWork.HelpfulReview.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ReviewId ==id);
                _unitOfWork.HelpfulReview.Remove(HelpfulReview);
                _unitOfWork.Save();
            }

            return RedirectToPage(new { id = product });
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
