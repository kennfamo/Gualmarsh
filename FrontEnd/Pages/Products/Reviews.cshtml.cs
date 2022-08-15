using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrontEnd.Pages.Products
{
    [BindProperties]
    public class ReviewsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Review Review { get; set; }
        public HelpfulReview HelpfulReview { get; set; }
        public Review PositiveReview { get; set; }
        public Review NegativeReview { get; set; }
        public IEnumerable<HelpfulReview> HelpfulReviewList { get; set; }
        public IEnumerable<HelpfulReview> HelpfulReviewListUser { get; set; }
        public IEnumerable<Review> PositiveReviewList { get; set; }
        public IEnumerable<Review> NegativeReviewList { get; set; }
        public ReviewsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Review> ReviewList { get; set; }
        public int RatingTotal{ get; set; }
        public int RatingAverage { get; set; }
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            ReviewList = _unitOfWork.Review.GetAll(filter: u => u.ProductId == id, includeProperties: "Product,ApplicationUser");
            foreach (var review in ReviewList)
            {

                int ratingInt = Int32.Parse(review.Rating);
                RatingTotal += ratingInt;

            }
            RatingAverage = RatingTotal / ReviewList.Count();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                HelpfulReviewListUser = _unitOfWork.HelpfulReview.GetAll(u => u.ApplicationUserId == claim.Value);
            }
            HelpfulReviewList = _unitOfWork.HelpfulReview.GetAll();
            int maxPositiveCount = 0;
            int maxPositiveId = 0;
            int maxNegativeCount = 0;
            int maxNegativeId = 0;

            NegativeReviewList = _unitOfWork.Review.GetAll(u => u.Rating.Equals("1") || u.Rating.Equals("2"));
            PositiveReviewList = _unitOfWork.Review.GetAll(u => u.Rating.Equals("4") || u.Rating.Equals("5"));
            foreach (var review in PositiveReviewList)
            {
                if (HelpfulReviewList.Where(u => u.ReviewId == review.Id).Count() > maxPositiveCount)
                {
                    maxPositiveCount = HelpfulReviewList.Where(u => u.ReviewId == review.Id).Count();
                    maxPositiveId = review.Id;
                }
            }
            foreach (var review in NegativeReviewList)
            {
                if (HelpfulReviewList.Where(u => u.ReviewId == review.Id).Count() > maxNegativeCount)
                {
                    maxNegativeCount = HelpfulReviewList.Where(u => u.ReviewId == review.Id).Count();
                    maxNegativeId = review.Id;
                }
            }
            if (maxPositiveId != 0)
            {
                PositiveReview = _unitOfWork.Review.GetFirstOrDefault(u => u.Id == maxPositiveId);
            }
            else
            {
                PositiveReview = _unitOfWork.Review.GetFirstOrDefault(u => u.ProductId == id && (u.Rating.Equals("4") || u.Rating.Equals("5")));
            }
            if (maxNegativeId != 0)
            {
                NegativeReview = _unitOfWork.Review.GetFirstOrDefault(u => u.Id == maxNegativeId);
            }
            else
            {
                NegativeReview = _unitOfWork.Review.GetFirstOrDefault(u => u.ProductId == id && (u.Rating.Equals("1") || u.Rating.Equals("2")));
            }

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

            return RedirectToPage(new { id = product });
        }
        public IActionResult OnGetUnHelpful(int id, int product)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                HelpfulReview = _unitOfWork.HelpfulReview.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ReviewId == id);
                _unitOfWork.HelpfulReview.Remove(HelpfulReview);
                _unitOfWork.Save();
            }

            return RedirectToPage(new { id = product });
        }

    }
}

