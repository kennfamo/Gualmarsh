using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Discount
    {
        [Key]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double DiscountPercent { get; set; }
        [Required]
        public byte Active { get; set; }
        public DateTime CreatedDate { get; set; }

        public Discount()
        {
            this.CreatedDate = DateTime.Now;
        }
    }
}
