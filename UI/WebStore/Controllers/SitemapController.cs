﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.Domain.Entities.Products;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IProductData _ProductData;
        public SitemapController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index()
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Shop", "Catalog")),
                new SitemapNode(Url.Action("Blog", "Home")),
                new SitemapNode(Url.Action("BlogSingle", "Home")),
                new SitemapNode(Url.Action("ContactUs", "Home")),
            };

            var sections = _ProductData.GetSections();

            foreach (var section in sections)
                if(section.ParentId != null)
                    nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { SectionId = section.Id })));

            var brands = _ProductData.GetBrands();
            foreach (var brand in brands)
                nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { BrandId = brand.Id })));

            var products = _ProductData.GetProducts(new ProductFilter());

            foreach (var product in products.Products)
                nodes.Add(new SitemapNode(Url.Action("ProductDetails", "Catalog", new { id = product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}