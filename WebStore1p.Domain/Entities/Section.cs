using System;
using System.Collections.Generic;
using System.Text;
using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Entities.Base.Interfaces;

namespace WebStore1p.Domain.Entities
{
    /// <summary>Секция товаров</summary>
    public class Section: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

       /// <summary>Идентификатор родительской секции</summary>
        public int? ParentId { get; set; }
    }
}
