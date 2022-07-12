using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductCategoryRepository ProductCategory { get; }
        IProductRepository Product { get; }
        IProvinceRepository Province { get; }
        IUserPaymentRepository UserPayment { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICantonRepository Canton { get; }
        ICityRepository City { get; }
        IDiscountRepository Discount { get; }
        IUserAddressRepository UserAddress { get; }
        IReviewRepository Review { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeader { get;  }
        void Save();
    }
}
