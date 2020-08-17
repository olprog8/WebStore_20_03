using System;
using System.Collections.Generic;
using WebStore1p.Domain.Entities;
using WebStore1p.Domain.Entities.Base;

namespace WebStore1p.Infrastructure.Interfaces
{
    /// <summary>Каталог товаров</summary>
    public interface IProductData
    {
        //ПШ 1й метод будет возвращать все секции, которые известны системе
        /// <summary>Получить все секции</summary>
        /// <returns>Перечисление секций каталога</returns>
        IEnumerable<Section> GetSection();

        //ПШ 2й метод будет возвращать все бренды, которые известны системе
        /// <summary>Получить все бренды</summary>
        /// <returns>Перечисление брендов каталога</returns>
        IEnumerable<Brand> GetBrands();

        //ПШ нам нужны не все товары, а только отдельные по конкретной секции и бренду, 
        //для этого введем понятие фильтра товаров, поэтому фильтром будет отдельная сущность
/// <summary>Товары из каталога</summary>
/// <param name="Filter">Критерий поиска/фильтрации</param>
/// <returns>Искомые товары из каталога товаров</returns>
        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
    }
}
