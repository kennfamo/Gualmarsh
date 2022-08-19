using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration Configuration;

        public IndexModel(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            Configuration = configuration;
        }
        public IList<Product> ProductList { get; set; }
        public IEnumerable<Product> ProductListAll { get; set; }
        public IEnumerable<ProductSubcategory> ProductSubcategoryList { get; set; }
        public IEnumerable<ProductSubcategory> ProductSubcategoryListFilter { get; set; }
        public ProductSubcategory ProductSubcategory { get; set; }
        public IEnumerable<Review> ReviewList { get; set; }
        public IOrderedEnumerable<IGrouping<int,OrderDetails>> OrderDetailsList { get; set; }
        public double FilteredMaxPrice { get; set; }
        public double FilteredMinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        public int Count { get; set; }
        public int TotalPages { get; set; }
        public int FilteredRating { get; set; }
        public int Recent { get; set; }

        public void OnGet(string name, double min_price, double max_price, int pageIndex, int rating, int recent)
        {
            ProductSubcategory = _unitOfWork.ProductSubcategory.GetFirstOrDefault(filter: u => u.Name == name);
            ProductSubcategoryList = _unitOfWork.ProductSubcategory.GetAll(filter: u => u.ProductCategoryId == ProductSubcategory.ProductCategoryId, includeProperties: "ProductCategory");
            ProductSubcategoryListFilter = _unitOfWork.ProductSubcategory.GetAll(includeProperties: "ProductCategory");
            ProductListAll = _unitOfWork.Product.GetAll(filter: u => u.ProductSubcategoryId == ProductSubcategory.Id,
                    includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");
            OrderDetailsList = _unitOfWork.OrderDetails.GetAll().GroupBy(u => u.ProductId).OrderByDescending(p => p.Count());
            ProductList = new List<Product>();
            foreach (var order in OrderDetailsList)
            {
                ProductList.Add(_unitOfWork.Product.GetFirstOrDefault(u => u.Id == order.Key && u.ProductSubcategoryId == ProductSubcategory.Id, 
                    includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory"));
            }
            ProductList = ProductList.Skip((pageIndex - 1) * 6).Take(6).ToList();
            ReviewList = _unitOfWork.Review.GetAll();
            Count = ProductListAll.Count();
            TotalPages = (int)Math.Ceiling(decimal.Divide(Count, 6));
            foreach (var product in ProductList)
            {
                if (product.Price > MaxPrice)
                {
                    MaxPrice = product.Price;
                }
            }
            if (rating != 0)
            {
                int ratingTotal = 0;
                ProductList = new List<Product>();
                foreach (var product in ProductListAll)
                {
                    foreach (var review in ReviewList.Where(u => u.ProductId == product.Id))
                    {
                        ratingTotal += review.Rating;
                    }
                    int ratingAverage = ratingTotal / ReviewList.Where(u => u.ProductId == product.Id).Count();
                    if (ratingAverage >= rating)
                    {
                        ProductList.Add(product);
                    }
                    ratingTotal = 0;
                }
                FilteredRating = rating;
            }
            if (max_price != 0)
            {
                ProductList = ProductList.Where(u => u.Price >= min_price && u.Price <= max_price).ToList();
                FilteredMaxPrice = max_price;
                FilteredMinPrice = min_price;
            }
            else if (min_price != 0)
            {
                ProductList = ProductList.Where(u => u.Price >= min_price).ToList();
                FilteredMinPrice = min_price;
            } 
            if (recent == 1)
            {
                ProductList = ProductList.OrderByDescending(u => u.CreatedDate).ToList();
                Recent = recent;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
        }
    }
}
