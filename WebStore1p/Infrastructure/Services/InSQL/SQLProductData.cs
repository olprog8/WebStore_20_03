﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebStore1p.Domain.Entities;
using WebStore1p.Infrastructure.Interfaces;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace WebStore1p.Infrastructure.Services.InSQL
{
    public class SQLProductData : IProductData
    {
        private readonly WebStoreDB _db;
        public SQLProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands
            .Include(brand => brand.Products)
            .AsEnumerable();
    
        public IEnumerable<Section> GetSection() => _db.Sections
            .Include(section => section.Products)
            .AsEnumerable();

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;
            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            //ПШ L7 2.32 Давайте доработаем логику фильтра в SQLProductData
            if (Filter?.Ids?.Count > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));

            return query.AsEnumerable();
        }

        public Product GetProductById(int id) => _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id);

    }
}
