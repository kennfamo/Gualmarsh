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
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        private readonly ApplicationDbContext _db;

        public ProvinceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Province province)
        {
            var obj = _db.Province.FirstOrDefault(u=>u.Id == province.Id);
            obj.Name = province.Name;
            
        }
    }
}
