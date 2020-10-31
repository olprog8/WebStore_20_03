using System;
using System.Collections.Generic;
using System.Text;

using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Identity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore1p.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
