namespace WebStore1p.Domain.Entities.Base.Interfaces
{
    /// <summary>Именованная сущность</summary>
    public interface INamedEntity: IBaseEntity
    {
        /// <summary>Название/// </summary>
        string Name { get; set; }
    }


}
