using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }        
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
