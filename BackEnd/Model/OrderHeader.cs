﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double Subtotal { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double Total { get; set; }
        [Required]
        [Display(Name = "Delivery Type")]
        public string DeliveryType { get; set; }
        [Required]
        [Display(Name = "Delivery Date")]
        [NotMapped]
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }        
        public string? DiscountCode { get; set; }
        [ForeignKey("DiscountCode")]
        public Discount Discount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Tax { get; set; }        
        public int? UserPaymentId { get; set; }
        [ForeignKey("UserPaymentId")]
        public UserPayment UserPayment { get; set; }
        public int? UserAddressId { get; set; }
        [ForeignKey("UserAddressId")]
        public UserAddress UserAddress { get; set; }

        public OrderHeader()
        {
            this.OrderDate = DateTime.Now;
        }
    }
}