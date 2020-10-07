
using System;
using System.Collections.Generic;
using System.Text;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Entities.Base.Interfaces;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore1p.Domain.Entities
{
    /// <summary>Товар</summary>
    //[Table("Products")]
    public class Product: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }
        //Если внешний ключ опционален то int с ?
        public int? BrandId { get; set; }

        
        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }
        public string ImageUrl { get; set; } //адрес картинки
        
        [Column(TypeName = "decimal(18,2)")] //Точность цены в БАЗЕ ДАННЫХ
        public decimal Price { get; set; }

        //public string Description { get; set; }

        //[NotMapped]
        //public int NotMappedProperty { get; set; }

    }
}
