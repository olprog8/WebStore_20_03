using System.Collections.Generic;
using System.Linq;
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


        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query;

        }

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);
        
    }
}
