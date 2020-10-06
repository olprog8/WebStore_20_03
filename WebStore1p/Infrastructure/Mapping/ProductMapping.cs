using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebStore1p.ViewModels;
using WebStore1p.Domain.Entities;

namespace WebStore1p.Infrastructure.Mapping
{
    public static class ProductMapping
    {
        public static ProductViewModel ToView(this Product p) => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Order = p.Order,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        };
    }
}
