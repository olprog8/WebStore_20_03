using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.ViewModels;

namespace WebStore1p.Components
{
    //[ViewComponent(Name = "Sections")]
    public class SectionViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionViewComponent(IProductData ProductData) { _ProductData = ProductData; }
        //Возвращаем в представление секции, т.е. представление будет работать с перечислением SectionViewModel
        public IViewComponentResult Invoke() => View(GetSections());
       

    //public async Task<IViewComponentResult> InvokeAsync()
    //{

    //}

        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = _ProductData.GetSection().ToArray();
            
            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_views = parent_sections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                 }).ToList();

            // ПШ Проходим по всем родительским секциям и находим все дочерние секции
            // ПШ строим одноуровневое дерево секций
            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_section in childs)
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        ParentSection = parent_section
                    });

                // ПШ Сортируем список
                parent_section.ChildSections.Sort((a, b)=> Comparer<int>.Default.Compare(a.Order, b.Order));

            }

            parent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return parent_sections_views;
        }

}
}
