﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using WebStore1p.Domain.Entities;

using WebStore1p.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using WebStore1p.Domain.Entities.Orders;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : IdentityDbContext<User,Role,string>
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base(Options) { }
    }
}
