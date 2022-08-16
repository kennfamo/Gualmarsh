using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<ProductSubcategory> ProductSubcategoryList { get; set; }
        public IEnumerable<ProductSubcategory> ProductSubcategoryListFilter { get; set; }
        public ProductSubcategory ProductSubcategory { get; set; }
        public double FilteredMaxPrice { get; set; }
        public double FilteredMinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public void OnGet(string name, double min_price, double max_price)
        {
            ProductSubcategoryList = _unitOfWork.ProductSubcategory.GetAll(filter: u => u.Name == name);
            ProductSubcategoryListFilter = _unitOfWork.ProductSubcategory.GetAll(includeProperties: "ProductCategory");
            ProductSubcategory = _unitOfWork.ProductSubcategory.GetFirstOrDefault(filter: u => u.Name == name);
            ProductList = _unitOfWork.Product.GetAll(filter: u => u.ProductSubcategoryId == ProductSubcategory.Id,
                    includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");
            foreach (var product in ProductList)
            {
                if (product.Price > MaxPrice)
                {
                    MaxPrice = product.Price;
                }
            }
            if (max_price != 0)
            {
                ProductList = _unitOfWork.Product.GetAll(filter: u => u.ProductSubcategoryId == ProductSubcategory.Id && u.Price >= min_price && u.Price <= max_price, 
                    includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");
                FilteredMaxPrice = max_price;
                FilteredMinPrice = min_price;
            }
            else if (min_price != 0)
            {
                ProductList = _unitOfWork.Product.GetAll(filter: u => u.ProductSubcategoryId == ProductSubcategory.Id && u.Price >= min_price,
                    includeProperties: "ProductSubcategory,ProductSubcategory.ProductCategory");
                FilteredMinPrice = min_price;   
            }
            
        }
        
        
        public void OnPostSubmit()
        {
            
        }
    }
}
