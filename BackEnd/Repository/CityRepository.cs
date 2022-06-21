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
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(City city)
        {
            var obj = _db.City.FirstOrDefault(u=>u.Id == city.Id);
            obj.Name = city.Name;
            obj.CantonId = city.CantonId;
            obj.PostalCode = city.PostalCode;

        }
    }
}
