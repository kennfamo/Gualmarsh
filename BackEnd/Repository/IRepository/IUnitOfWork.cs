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
        void Save();
    }
}
