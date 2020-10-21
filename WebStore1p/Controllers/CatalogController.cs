using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore1p.Domain.Entities;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.ViewModels;

using WebStore1p.Infrastructure.Mapping;

namespace WebStore1p.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;
        
        //ПШ метод Shop будет возвращать каталог товаров
        public IActionResult Shop(int? SectionId, int? BrandId)
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                //ПШ Инициализатор свойства
                SectionId = SectionId,
                BrandId = BrandId
            });

            ////Можно было сделать
            ////var filter = new ProductFilter
            ////{
            ////    SectionId = SectionId,
            ////    BrandId = BrandId
            ////}
            ////var products = _ProductData.GetProducts(filter);


            return View(new CatalogViewModel 
            {
                SectionId = SectionId,
                BrandId = BrandId,
                //ПШ получаем перечисление внутри каталога товаров
                Products = products.Select(ProductMapping.ToView).OrderBy(p => p.Order)
            });
        }

        //ПШ метод Details будет возвращать детали товара
        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.ToView());
        }
    }
}
