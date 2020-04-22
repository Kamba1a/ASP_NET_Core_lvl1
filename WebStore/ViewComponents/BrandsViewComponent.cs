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
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BrandViewModel> brands = GetBrands();
            return View(brands);
        }

        private List<BrandViewModel> GetBrands()
        {
            IEnumerable<Brand> allBrands = _productData.GetBrands();
            List<BrandViewModel> allBrandsList = allBrands.Select(brand => new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
                ProductsCount = 0
            }).OrderBy(b => b.Order).ToList();

            return allBrandsList;
        }
    }
}
