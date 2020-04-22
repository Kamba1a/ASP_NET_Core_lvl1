using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Shop(int? categoryId, int? brandId)
        {
            List<ProductViewModel> products = _catalogData.GetAllProducts().ToList();

            if (categoryId.HasValue)
            {
                products = _catalogData.GetAllProducts()
                    .Where(product => product.CategoryId == categoryId.Value)
                    .ToList();
            }
            else if (brandId.HasValue)
            {
                products = _catalogData.GetAllProducts()
                .Where(product => product.BrandId == brandId.Value)
                .ToList();
            }

            return View(products.OrderBy(product => product.Order));
        }
    }
}