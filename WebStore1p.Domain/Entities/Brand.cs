using System;
using System.Collections.Generic;
using System.Text;
using WebStore1p.Domain.Entities.Base.Interfaces;

namespace WebStore1p.Domain.Entities.Base
{

    /// <summary>Бренд</summary>
    public class Brand: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; } //Порядковый номер
    }
}
