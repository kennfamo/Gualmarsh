using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int ProductId { get; set; }        
        [ForeignKey("ProductId")]
        [ValidateNever]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name="Product Name")]
        public string Name { get; set; }
    }
}
