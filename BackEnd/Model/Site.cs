using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Site
    {
        [Key]
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Address { get; set; }
        public string ScheduleDay { get; set; }
        public string ScheduleHour { get; set; }
        public string Phone { get; set; }
    }
}
