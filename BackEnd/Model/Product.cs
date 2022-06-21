using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string SKU { get; set; }
        public string Brand { get; set; }
        public string Weight { get; set; }
        public string ContainerType { get; set; }
        public int Inventory { get; set; }
        public double Price { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        public ProductCategory ProductCategory { get; set; }
        public string? DiscountCode { get; set; }
        [ForeignKey("DiscountCode")]
        public Discount Discount { get; set; }

        public Product()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
