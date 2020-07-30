using System.Collections.Generic;
using WebStore1p.Domain.Entities.Base.Interfaces;
using WebStore1p.ViewModels;

namespace WebStore1p.ViewModels
{
    //Добавляем интерфейсы чтобы навести некоторую строгость, чтобы обязательны были идентификатор, имя, порядковый номер
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        //У каждой секции будет понятие Дочерние секции
        public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();

        //Понятие Родительской секции
        public SectionViewModel ParentSection { get; set; } 
    }
}
