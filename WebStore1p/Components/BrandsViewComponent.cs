using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.ViewModels;

namespace WebStore1p.Components
{
    public class BrandsViewComponent: ViewComponent
    {
        private readonly IProductData _ProductData;
        public BrandsViewComponent(IProductData ProductData) { _ProductData = ProductData; }

        public IViewComponentResult Invoke() => View(GetBrands());

        public IEnumerable<BrandsViewModel> GetBrands() => _ProductData
            .GetBrands()
            .Select(brand => new BrandsViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order
            })
            .OrderBy(brand => brand.Order);
    }
}
