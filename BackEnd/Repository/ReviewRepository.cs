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
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;

        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public void Update(Review review)
        {
            var obj = _db.Review.FirstOrDefault(u => u.Id == review.Id);
            obj.ApplicationUserId = review.ApplicationUserId;
            obj.ProductId = review.ProductId;
            obj.Title = review.Title;
            obj.Body = review.Body;
            obj.Rating = review.Rating;
            obj.CreatedDate = review.CreatedDate;

        }
    }
}
