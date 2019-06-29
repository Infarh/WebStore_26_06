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

        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter);

        ProductDTO GetProductById(int id);
    }
}
