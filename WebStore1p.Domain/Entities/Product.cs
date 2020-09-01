
using System;
using System.Collections.Generic;
using System.Text;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Entities.Base.Interfaces;

namespace WebStore1p.Domain.Entities
{
    public class Product: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; }

        public string ImageUrl { get; set; } //адрес картинки

        public decimal Price { get; set; }
    }
}
