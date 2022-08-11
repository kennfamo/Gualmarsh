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
    public class UserAddressRepository : Repository<UserAddress>, IUserAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public UserAddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(UserAddress userAddress)
        {
            _db.UserAddress.Update(userAddress);
        }
    }
}
