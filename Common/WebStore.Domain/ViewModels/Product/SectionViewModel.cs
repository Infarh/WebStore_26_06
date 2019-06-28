using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.ViewModels.Product
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        /// <summary>Дочерние секции</summary>
        public List<SectionViewModel> ChildSections { get; set; }  = new List<SectionViewModel>();

        /// <summary>Родительская секция</summary>
        public SectionViewModel ParentSection { get; set; }
    }
}
