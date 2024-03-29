﻿using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO.Product
{
    public class ProductDTO : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public BrandDTO Brand { get; set; }

        public SectionDTO Section { get; set; }
    }
}