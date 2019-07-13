using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Products;

namespace WebStore.Services.Map
{
    public static class ProductProductDTO
    {
        public static ProductDTO CopyTo(this Product product, ProductDTO dto)
        {
            if (product is null) return dto;
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Price = product.Price;
            dto.Order = product.Order;
            dto.ImageUrl = product.ImageUrl;
            dto.Brand = product.Brand.ToDTO();
            dto.Section = product.Section.ToDTO();
            return dto;
        }

        public static Product CopyTo(this ProductDTO dto, Product product)
        {
            if (dto is null) return product;
            product.Id = dto.Id;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Order = dto.Order;
            product.ImageUrl = dto.ImageUrl;
            product.BrandId = dto.Brand?.Id;
            product.Brand = dto.Brand.ToBrand();
            product.SectionId = dto.Section.Id;
            product.Section = dto.Section.ToSection();
            return product;
        }

        public static ProductDTO ToDTO(this Product product) => product?.CopyTo(new ProductDTO());

        public static Product ToProduct(this ProductDTO dto) => dto?.CopyTo(new Product());
    }
}
