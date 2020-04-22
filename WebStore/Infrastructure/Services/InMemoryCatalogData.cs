using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryCatalogData : ICatalogData
    {
        List<BrandViewModel> _brands;

        public InMemoryCatalogData()
        {
            _brands = new List<BrandViewModel>()
            {
                new BrandViewModel
                {
                    Id = 1,
                    Name = "Acne",
                    Order = 0
                },
                new BrandViewModel
                {
                    Id = 2,
                    Name = "Grüne Erde",
                    Order = 1
                },
                new BrandViewModel
                {
                    Id = 3,
                    Name = "Albiro",
                    Order = 2
                },
                new BrandViewModel
                {
                    Id = 4,
                    Name = "Ronhill",
                    Order = 3
                },
                new BrandViewModel
                {
                    Id = 5,
                    Name = "Oddmolly",
                    Order = 4
                },
                new BrandViewModel
                {
                    Id = 6,
                    Name = "Boudestijn",
                    Order = 5
                },
                new BrandViewModel
                {
                    Id = 7,
                    Name = "Rösch creative culture",
                    Order = 6
                }
            };
        }

        public IEnumerable<BrandViewModel> GetAllBrands()
        {
            return _brands;
        }
    }
}
