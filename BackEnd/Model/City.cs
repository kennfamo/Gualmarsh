using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "City")]
        public string Name { get; set; }        
        public int CantonId { get; set; }
        [ForeignKey("CantonId")]
        public Canton Canton { get; set; }
        public int PostalCode { get; set; }
    }
}
