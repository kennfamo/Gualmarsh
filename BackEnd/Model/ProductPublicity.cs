using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class ProductPublicity
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Message { get; set; }
        public string? Image1 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Province Category { get; set; }

    }
}

