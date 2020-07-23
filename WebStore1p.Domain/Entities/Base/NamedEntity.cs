using WebStore1p.Domain.Entities.Base.Interfaces;

namespace WebStore1p.Domain.Entities.Base
{
    /// <summary>Именованная сущность</summary>
    public abstract class NamedEntity: BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
