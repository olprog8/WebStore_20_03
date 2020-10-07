using WebStore1p.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore1p.Domain.Entities.Base
{
    /// <summary>Именованная сущность</summary>
    public abstract class NamedEntity: BaseEntity, INamedEntity
    {
        [Required/*, StringLength(250)*/]
        public string Name { get; set; }
    }
}
