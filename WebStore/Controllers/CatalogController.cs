using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        ICatalogData _catalogData;

        public CatalogController(ICatalogData catalogData)
        {
            _catalogData = catalogData;
        }

        public IActionResult Shop(int? sectionId, int? brandId)
        {
            IEnumerable<Product> products = _catalogData.GetProducts(new ProductFilter { BrandId = brandId, SectionId = sectionId });

            CatalogViewModel catalogViewModel = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    ImageUrl = product.ImageUrl,
                    Name = product.Name,
                    Order = product.Order,
                    Price = product.Price
                }).OrderBy(p => p.Order).ToList()
            };

            return View(catalogViewModel);
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}