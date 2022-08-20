using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class ProductPublicityRepository : Repository<ProductPublicity>,IProductPublicityRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductPublicityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductPublicity productPublicity)
        {
            var obj = _db.ProductPublicity.FirstOrDefault(u => u.Id == productPublicity.Id);
            obj.Title = productPublicity.Title;
            obj.Message = productPublicity.Message;
            obj.Image1 = productPublicity.Image1;
            obj.ProductSubcategoryId = productPublicity.ProductSubcategoryId;
            obj.ProductSubcategory = productPublicity.ProductSubcategory;
        }
    }
 
}
