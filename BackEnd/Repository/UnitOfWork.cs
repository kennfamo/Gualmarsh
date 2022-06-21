﻿using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductCategory = new ProductCategoryRepository(_db);
            Product = new ProductRepository(_db);
            Province = new ProvinceRepository(_db);
            UserPayment = new UserPaymentRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Canton = new CantonRepository(_db);
        }

        public IProductCategoryRepository ProductCategory { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProvinceRepository Province { get; private set; }
        public IUserPaymentRepository UserPayment { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICantonRepository Canton { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}