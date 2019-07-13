using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Products;

namespace WebStore.Services.Map
{
    public static class SectionSectionDTO
    {
        public static SectionDTO CopyTo(this Section section, SectionDTO dto)
        {
            if (section is null) return dto;
            dto.Id = section.Id;
            dto.Name = section.Name;
            return dto;
        }

        public static Section CopyTo(this SectionDTO dto, Section section)
        {
            if (dto is null) return section;
            section.Id = dto.Id;
            section.Name = dto.Name;
            return section;
        }

        public static SectionDTO ToDTO(this Section section) => section?.CopyTo(new SectionDTO());

        public static Section ToSection(this SectionDTO dto) => dto?.CopyTo(new Section());
    }
}