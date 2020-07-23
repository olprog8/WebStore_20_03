using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore1p.Domain.Entities.Base.Interfaces
{
    /// <summary>Базовая сущность</summary>
    public interface IBaseEntity
    {
        /// <summary>Идентификатор</summary>
        int Id { get; set; }
    }

}
