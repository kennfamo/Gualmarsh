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
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        private readonly ApplicationDbContext _db;

        public SiteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Site site)
        {
            _db.Site.Update(site);

        }
    }
}
