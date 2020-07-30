using System.Collections.Generic;
using WebStore1p.Data;
using WebStore1p.Domain.Entities;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Infrastructure.Interfaces;

namespace WebStore1p.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {

        public IEnumerable<Section> GetSection() => TestData.Sections;
        

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
    }
}
