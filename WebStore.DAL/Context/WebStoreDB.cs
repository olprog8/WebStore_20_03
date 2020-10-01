using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using WebStore1p.Domain.Entities;

namespace WebStore.DAL.Context
{
    class WebStoreDB: DbContext
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base(Options){}
    }
}
