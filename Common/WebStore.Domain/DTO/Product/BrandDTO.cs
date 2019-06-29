using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.DTO.Product
{
    public class BrandDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public BrandDTO Brand { get; set; }
    }
}
