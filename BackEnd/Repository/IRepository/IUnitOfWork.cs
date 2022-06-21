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
        void Save();
    }
}
