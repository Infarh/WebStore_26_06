using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Products;

namespace WebStore.Services.Map
{
    public static class BrandBrandDTO
    {
        public static BrandDTO CopyTo(this Brand brand, BrandDTO dto)
        {
            if(brand is null) return dto;
            dto.Id = brand.Id;
            dto.Name = brand.Name;
            return dto;
        }

        public static Brand CopyTo(this BrandDTO dto, Brand brand)
        {
            if (dto is null) return brand;
            brand.Id = dto.Id;
            brand.Name = dto.Name;
            return brand;
        }

        public static BrandDTO ToDTO(this Brand brand) => brand?.CopyTo(new BrandDTO());

        public static Brand ToBrand(this BrandDTO dto) => dto?.CopyTo(new Brand());
    }
}
