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
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly ApplicationDbContext _db;

        public DiscountRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Discount discount)
        {
            var obj = _db.Discount.FirstOrDefault(u=>u.Id == discount.Id);
            obj.Name = discount.Name;
            obj.Description = discount.Description;
            obj.DiscountPercent = discount.DiscountPercent;
            obj.Active = discount.Active;
            obj.CreatedDate = discount.CreatedDate;

        }
    }
}
