using System;
using System.Collections.Generic;
using System.Text;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Entities.Base.Interfaces;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore1p.Domain.Entities
{
    /// <summary>Секция товаров</summary>
    //[Table("Sections")]
    //Для EF нельзя делать запечатанные классы sealed
    public class Section: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

       /// <summary>Идентификатор родительской секции</summary>
        public int? ParentId { get; set; }

        //Внешний ключ по свойству ParentId
        [ForeignKey(nameof(ParentId))]
        //Связь один к одному
        public virtual Section ParentSection { get; set; }
        //Связь один ко многим
        public virtual ICollection<Product> Products { get; set; }
    }
}
