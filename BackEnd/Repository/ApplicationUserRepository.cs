using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public void Update(ApplicationUser applicationUser)
        {
            var obj = _db.ApplicationUser.FirstOrDefault(u => u.Id == applicationUser.Id);
            obj.FirstName = applicationUser.FirstName;
            obj.LastName = applicationUser.LastName;
        }
    }
}
