using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Products;
using WebStore.Domain.ViewModels.Product;
using WebStore.Interfaces.Services;
using WebStore.Services.Map;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }

        public IActionResult Shop(int? SectionId, int? BrandId, int Page = 1)
        {
            var page_size = int.Parse(_Configuration["PageSize"]);

            var paged_products = _ProductData.GetProducts(new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = Page,
                PageSize = page_size
            });

            var catalog_model = new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = paged_products.Products
                   .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Brand = p.Brand?.Name,
                        Order = p.Order,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    }),
                PageModel = new PageViewModel
                {
                    PageSize = page_size,
                    PageNumber = Page,
                    TotalItems = paged_products.TotalCount
                }
            };

            return View(catalog_model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Order = product.Order,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand?.Name
            });
        }

        public IActionResult GetFiltredItems(int? SectionId, int? BrandId, int Page = 1)
        {
            var products = GetProducts(SectionId, BrandId, Page);
            return PartialView("Partial/_FeaturesItems", products);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? SectionId, int? BrandId, int Page)
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize = int.Parse(_Configuration["PageSize"])
            });
            return products.Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Order = p.Order,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand?.Name ?? string.Empty
            });
        }
    }
}