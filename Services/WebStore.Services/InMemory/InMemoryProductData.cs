using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Products;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IEnumerable<Product> products = TestData.Products;
            if (Filter is null)
                return products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Brand = p.Brand is null
                    ? null
                    : new BrandDTO
                    {
                        Id = p.Brand.Id,
                        Name = p.Brand.Name
                    }
                });
            if (Filter.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);
            if (Filter.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand is null
                    ? null
                    : new BrandDTO
                    {
                        Id = p.Brand.Id,
                        Name = p.Brand.Name
                    }
            });
        }

        public ProductDTO GetProductById(int id)
        {
            var p = TestData.Products.FirstOrDefault(product => product.Id == id);
            return p is null ? null : new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand is null
                    ? null
                    : new BrandDTO
                    {
                        Id = p.Brand.Id,
                        Name = p.Brand.Name
                    }
            };
        }
    }
}
