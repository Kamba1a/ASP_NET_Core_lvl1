using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;

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
            return View(_catalogData.GetAllBrands().OrderBy(brand => brand.Order));
        }
    }
}
