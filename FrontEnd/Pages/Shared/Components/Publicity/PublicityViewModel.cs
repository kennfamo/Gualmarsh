using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
namespace FrontEnd.Pages.Components.Publicity
{
    public class PublicityViewModel
    {
        public IEnumerable<ProductPublicity> ProductPublicityList { get; set; }
        public IEnumerable<ProductSubcategory> ProductSubCategoryList { get; set; }
        public IEnumerable<ProductCategory> ProductCategoryList { get; set; }

    }
}
