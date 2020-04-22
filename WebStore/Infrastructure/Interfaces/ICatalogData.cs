using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICatalogData
    {
        public IEnumerable<BrandViewModel> GetAllBrands();
        public IEnumerable<CategoryViewModel> GetAllCategories();
        public IEnumerable<ProductViewModel> GetAllProducts();
    }
}
