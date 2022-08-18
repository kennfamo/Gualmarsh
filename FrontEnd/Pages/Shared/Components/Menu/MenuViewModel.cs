using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontEnd.Pages.Components.Cart
{
    public class MenuViewModel
    {
        public IEnumerable<ProductSubcategory>? ProductSubcategoryList { get; set; }
        public IEnumerable<ProductCategory>? ProductCategoryList { get; set; }
    }
}
