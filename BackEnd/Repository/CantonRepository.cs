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
    public class CantonRepository : Repository<Canton>, ICantonRepository
    {
        private readonly ApplicationDbContext _db;

        public CantonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Canton canton)
        {
            var obj = _db.Canton.FirstOrDefault(u=>u.Id == canton.Id);
            obj.Name = canton.Name;
            obj.ProvinceId = canton.ProvinceId;

        }
    }
}
