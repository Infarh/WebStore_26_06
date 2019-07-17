using System.Collections.Generic;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Products;

namespace WebStore.Interfaces.Services
{
    /// <summary>Сервис товаров</summary>
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        Section GetSectionById(int id);

        Brand GetBrandById(int id);

        PagedProductsDTO GetProducts(ProductFilter Filter);

        ProductDTO GetProductById(int id);
    }
}
