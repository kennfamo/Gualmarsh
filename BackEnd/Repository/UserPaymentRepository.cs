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
    public class UserPaymentRepository : Repository<UserPayment>, IUserPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public UserPaymentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(UserPayment userPayment)
        {
            var obj = _db.UserPayment.FirstOrDefault(u=>u.Id == userPayment.Id);
            obj.PaymentType = userPayment.PaymentType;
            obj.Provider = userPayment.Provider;
            obj.CCNumber = userPayment.CCNumber;
            obj.ExpirationDate = userPayment.ExpirationDate;
            obj.SecurityCode = userPayment.SecurityCode;
            obj.ApplicationUserId = userPayment.ApplicationUserId;

        }
    }
}
