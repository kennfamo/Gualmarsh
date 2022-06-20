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
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(ProductCategory productCategory)
        {
            var obj = _db.ProductCategory.FirstOrDefault(u=>u.Id == productCategory.Id);
            obj.Name = productCategory.Name;
            
        }
    }
}
