using System;
using BackEnd.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository.IRepository
{
    public interface IProductPublicityRepository : IRepository<ProductPublicity>
    {
        void Update(ProductPublicity productPublicity);
    }
}
