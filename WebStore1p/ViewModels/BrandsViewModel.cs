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
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Order { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
