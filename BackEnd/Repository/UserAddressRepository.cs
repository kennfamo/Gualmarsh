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
            var obj = _db.UserAddress.FirstOrDefault(u=>u.Id == userAddress.Id);
            obj.ApplicationUserId = userAddress.ApplicationUserId;
            obj.CityId = userAddress.CityId;
            obj.AddressLine1 = userAddress.AddressLine1;
            obj.AddressLine2 = userAddress.AddressLine2;
            obj.Phone = userAddress.Phone;
        }
    }
}
