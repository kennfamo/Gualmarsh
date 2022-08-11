using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Province")]
        public string Name {get; set;} 

    }
}
