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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public void Update(Product product)
        {
            var obj = _db.Product.FirstOrDefault(u => u.Id == product.Id);
            obj.Name = product.Name;
            obj.Description = product.Description;
            obj.SKU = product.SKU;
            obj.Brand = product.Brand;
            obj.Weight = product.Weight;
            obj.ContainerType = product.ContainerType;
            obj.Inventory = product.Inventory;
            obj.Price = product.Price;
            obj.ProductCategoryId = product.ProductCategoryId;
            obj.DiscountCode = product.DiscountCode;

            if(obj.Image1 != null)
            {
                obj.Image1 = product.Image1;
            }
            if (obj.Image2 != null)
            {
                obj.Image2 = product.Image2;
            }
            if (obj.Image3 != null)
            {
                obj.Image3 = product.Image3;
            }
            if (obj.Image4 != null)
            {
                obj.Image4 = product.Image4;
            }


        }
    }
}
