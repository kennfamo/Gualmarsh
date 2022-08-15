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
    public class HelpfulReviewRepository : Repository<HelpfulReview>, IHelpfulReviewRepository
    {
        private readonly ApplicationDbContext _db;

        public HelpfulReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(HelpfulReview helpfulReview)
        {
            _db.HelpfulReview.Update(helpfulReview);

        }
    }
}
