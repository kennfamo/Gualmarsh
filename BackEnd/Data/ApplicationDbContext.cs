﻿using BackEnd.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Province> Province { get; set; }
        public DbSet<Canton> Canton { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserPayment> UserPayment { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }


    }
}
