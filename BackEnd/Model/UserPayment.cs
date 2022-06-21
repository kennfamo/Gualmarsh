using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class UserPayment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PaymentType { get; set; }        
        public string? Provider { get; set; }
        public string? CCNumber { get; set; }
        public string? ExpirationDate { get; set; }
        public int? SecurityCode { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
