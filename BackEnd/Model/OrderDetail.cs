using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }        
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public decimal Subtotal { get; set; } 
        public decimal Total { get; set; }
        public string DeliveryType { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? DiscountCode { get; set; }
        [ForeignKey("DiscountCode")]
        public Discount Discount { get; set; }
        public decimal Tax { get; set; }
        public OrderDetail()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
