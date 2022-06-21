using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository.IRepository
{
    public interface ICantonRepository : IRepository<Canton>
    {
        void Update(Canton canton);
    }
}
