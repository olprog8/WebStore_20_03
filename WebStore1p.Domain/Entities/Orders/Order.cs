using System;
using System.Collections.Generic;


using WebStore1p.Domain.Entities.Base;
using WebStore1p.Domain.Identity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WebStore1p.Domain.Entities.Orders
{
    public class Order : NamedEntity
    {
        [Required]
        public virtual User User { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime Date { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

 }
