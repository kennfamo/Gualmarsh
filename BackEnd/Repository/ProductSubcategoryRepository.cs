using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class ProductSubcategoryRepository : Repository<ProductSubcategory>, IProductSubcategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductSubcategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(ProductSubcategory productSubcategory)
        {
            var obj = _db.ProductSubcategory.FirstOrDefault(u=>u.Id == productSubcategory.Id);
            obj.Name = productSubcategory.Name;
            obj.ProductCategoryId = productSubcategory.ProductCategoryId;
            obj.ShortName = productSubcategory.ShortName;

        }
    }
}
