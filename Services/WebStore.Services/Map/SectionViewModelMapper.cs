using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels.Product;

namespace WebStore.Services.Map
{
    public static class SectionViewModelMapper
    {
        public static void CopyTo(this Section section, SectionViewModel model)
        {
            model.Id = section.Id;
            model.Name = section.Name;
            model.Order = section.Order;
        }

        public static SectionViewModel CreateViewModel(this Section section)
        {
            var model = new SectionViewModel();
            section.CopyTo(model);
            return model;
        }

        public static void CopyTo(this SectionViewModel model, Section section)
        {
            section.Name = model.Name;
            section.Order = model.Order;
        }

        public static Section Create(this SectionViewModel model)
        {
            var section = new Section();
            model.CopyTo(section);
            return section;
        }
    }
}
