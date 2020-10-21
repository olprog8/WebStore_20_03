using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore1p.Domain.Entities
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }

        //ПШ L7 Добавим свойство Идентификаторы
        public List<int> Ids { get; set; }

    }
}
