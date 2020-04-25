using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class SqlCatalogData : ICatalogData
    {
        WebStoreContext _webStoreContext;

        public SqlCatalogData(WebStoreContext webStoreContext)
        {
            _webStoreContext = webStoreContext;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _webStoreContext.Brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            List<Product> products = _webStoreContext.Products.ToList();

            if (filter.SectionId.HasValue)
                products = products.Where(product => product.SectionId.Equals(filter.SectionId)).ToList();
            if (filter.BrandId.HasValue)
                products = products.Where(product => product.BrandId == filter.BrandId).ToList();

            return products;
        }

        public IEnumerable<Section> GetSections()
        {
            return _webStoreContext.Sections;
        }
    }
}
