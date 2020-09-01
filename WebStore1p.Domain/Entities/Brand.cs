using System.Collections;
using System.Collections.Generic;

using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore1p.Domain.Entities.Base
{

    /// <summary>Бренд</summary>
    //[Table("Brand")]
    public class Brand: NamedEntity, IOrderedEntity
    {
        public int Order { get; set; } //Порядковый номер

        public virtual ICollection<Product> Products { get; set; }
    }
}
