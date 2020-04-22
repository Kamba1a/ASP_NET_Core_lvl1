using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class CategoriesViewComponent: ViewComponent
    {
        ICatalogData _catalogData;

        public CategoriesViewComponent(ICatalogData catalogData)
        {
            _catalogData = catalogData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(GetCategories());
        }

        IEnumerable<CategoryViewModel> GetCategories()
        {
            List<CategoryViewModel> categories = _catalogData.GetAllCategories()
                .Where(category => category.ParentId == null)
                .OrderBy(category => category.Order)
                .ToList();

            foreach(CategoryViewModel category in categories)
            {
                category.childCategories = _catalogData.GetAllCategories()
                    .Where(childCategory => childCategory.ParentId == category.Id)
                    .OrderBy(childCategory => childCategory.Order);
            }

            return categories;
        }
    }
}
