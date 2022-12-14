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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductCategory = new ProductCategoryRepository(_db);
            Product = new ProductRepository(_db);
            ProductPublicity = new ProductPublicityRepository(_db);
            Province = new ProvinceRepository(_db);
            UserPayment = new UserPaymentRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Canton = new CantonRepository(_db);
            City = new CityRepository(_db);
            Discount = new DiscountRepository(_db);
            UserAddress = new UserAddressRepository(_db);
            Review = new ReviewRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ProductSubcategory = new ProductSubcategoryRepository(_db);
            Site = new SiteRepository(_db);
            HelpfulReview = new HelpfulReviewRepository(_db);
        }

        public IProductCategoryRepository ProductCategory { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductPublicityRepository ProductPublicity { get; private set; }
        public IProvinceRepository Province { get; private set; }
        public IUserPaymentRepository UserPayment { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICantonRepository Canton { get; private set; }
        public ICityRepository City { get; private set; }
        public IDiscountRepository Discount { get; private set; }
        public IUserAddressRepository UserAddress { get; private set; }
        public IReviewRepository Review { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IProductSubcategoryRepository ProductSubcategory { get; private set; }
        public ISiteRepository Site { get; private set; }
        public IHelpfulReviewRepository HelpfulReview { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
