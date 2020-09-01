using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;

using WebStore1p.Domain.Entities.Base.Interfaces;

namespace WebStore1p.ViewModels
{
    public class BrandsViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
