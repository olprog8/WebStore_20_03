using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore1p.Domain.Entities;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.ViewModels;

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
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).OrderBy(p => p.Order)
            });
        }

        //ПШ метод Details будет возвращать детали товара
        public IActionResult Details()
        {
            return View();
        }
    }
}
