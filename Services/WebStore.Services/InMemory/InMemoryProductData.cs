using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Products;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Map;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public Section GetSectionById(int id) => TestData.Sections.FirstOrDefault(s => s.Id == id);

        public Brand GetBrandById(int id) => TestData.Brands.FirstOrDefault(b => b.Id == id);

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IEnumerable<Product> products = TestData.Products;
            if (Filter is null)
                return products.Select(ProductProductDTO.ToDTO);
            if (Filter.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);
            if (Filter.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);
            return products.Select(ProductProductDTO.ToDTO);
        }

        public ProductDTO GetProductById(int id) => TestData.Products.FirstOrDefault(product => product.Id == id)?.ToDTO();
    }
}
