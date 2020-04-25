using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly ICatalogData _catalogData;

        public BrandsViewComponent(ICatalogData catalogData)
        {
            _catalogData = catalogData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BrandViewModel> brands = GetBrands();
            return View(brands);
        }

        private List<BrandViewModel> GetBrands()
        {
            IEnumerable<Brand> allBrands = _catalogData.GetBrands();
            List<BrandViewModel> allBrandsList = allBrands.Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
                ProductsCount = GetProductsCount(brand.Id)
            }).OrderBy(b => b.Order).ToList();

            return allBrandsList;
        }

        private int GetProductsCount(int brandId)
        {
            int productCount = 0;

            foreach (Product product in _catalogData.GetProducts(new Domain.ProductFilter()))
            {
                if (product.BrandId == brandId) productCount = productCount + 1;
            }

            return productCount;
        }
    }
}
