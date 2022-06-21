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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public void Update(OrderDetail orderDetail)
        {
            var obj = _db.OrderDetail.FirstOrDefault(u => u.Id == orderDetail.Id);
            obj.ApplicationUserId = orderDetail.ApplicationUserId;
            obj.Subtotal = orderDetail.Subtotal;
            obj.Total = orderDetail.Total;
            obj.DeliveryDate = orderDetail.DeliveryDate;
            obj.DeliveryDate = orderDetail.DeliveryDate;
            obj.Status = orderDetail.Status;
            obj.Tax = orderDetail.Tax;
            obj.DiscountCode = orderDetail.DiscountCode;          


        }
    }
}
