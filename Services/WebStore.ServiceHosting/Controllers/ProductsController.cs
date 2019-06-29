using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Products;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/products")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections()
        {
            return _ProductData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _ProductData.GetBrands();
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter)
        {
            return _ProductData.GetProducts(Filter);
        }

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDTO GetProductById(int id)
        {
            return _ProductData.GetProductById(id);
        }
    }
}