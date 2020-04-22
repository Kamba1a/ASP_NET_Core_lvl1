using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent: ViewComponent
    {
        ICatalogData _catalogData;

        public BrandsViewComponent(ICatalogData catalogData)
        {
            _catalogData = catalogData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(GetBrands());
        }

        IEnumerable<BrandViewModel> GetBrands()
        {
            List<BrandViewModel> brands = _catalogData.GetAllBrands().ToList();

            foreach (BrandViewModel brand in brands)
            {
                brand.ProductCount = GetProductCount(brand.Id);
            }

            return brands.OrderBy(brand => brand.Order);
        }

        int GetProductCount(int brandId)
        {
            int productCount = 0;

            foreach (ProductViewModel product in _catalogData.GetAllProducts())
            {
                if (product.BrandId == brandId) productCount = productCount + 1;
            }

            return productCount;
        }
    }
}
