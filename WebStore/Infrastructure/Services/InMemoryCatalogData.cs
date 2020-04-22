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
        IEnumerable<BrandViewModel> _brands;
        IEnumerable<CategoryViewModel> _categories;

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

            _categories = new List<CategoryViewModel>()
            {
                new CategoryViewModel
                {
                    Id = 1,
                    Name = "Sportswear",
                    Order = 0,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 2,
                    Name = "Nike",
                    Order = 0,
                    ParentId = 1
                },
                new CategoryViewModel
                {
                    Id = 3,
                    Name = "Under Armour",
                    Order = 1,
                    ParentId = 1
                },
                new CategoryViewModel
                {
                    Id = 4,
                    Name = "Adidas",
                    Order = 2,
                    ParentId = 1
                },
                new CategoryViewModel
                {
                    Id = 5,
                    Name = "Puma",
                    Order = 3,
                    ParentId = 1
                },
                new CategoryViewModel
                {
                    Id = 6,
                    Name = "ASICS",
                    Order = 4,
                    ParentId = 1
                },
                new CategoryViewModel
                {
                    Id = 7,
                    Name = "Mens",
                    Order = 1,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 8,
                    Name = "Fendi",
                    Order = 0,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 9,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 10,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 11,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 12,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 13,
                    Name = "Armani",
                    Order = 5,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 14,
                    Name = "Prada",
                    Order = 6,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 15,
                    Name = "Dolce and Gabbana",
                    Order = 7,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 16,
                    Name = "Chanel",
                    Order = 8,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 17,
                    Name = "Gucci",
                    Order = 1,
                    ParentId = 7
                },
                new CategoryViewModel
                {
                    Id = 18,
                    Name = "Womens",
                    Order = 2,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 19,
                    Name = "Fendi",
                    Order = 0,
                    ParentId = 18
                },
                new CategoryViewModel
                {
                    Id = 20,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 18
                },
                new CategoryViewModel
                {
                    Id = 21,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 18
                },
                new CategoryViewModel
                {
                    Id = 22,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 18
                },
                new CategoryViewModel
                {
                    Id = 23,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 18
                },
                new CategoryViewModel
                {
                    Id = 24,
                    Name = "Kids",
                    Order = 3,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 25,
                    Name = "Fashion",
                    Order = 4,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 26,
                    Name = "Households",
                    Order = 5,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 27,
                    Name = "Interiors",
                    Order = 6,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 28,
                    Name = "Clothing",
                    Order = 7,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 29,
                    Name = "Bags",
                    Order = 8,
                    ParentId = null
                },
                new CategoryViewModel
                {
                    Id = 30,
                    Name = "Shoes",
                    Order = 9,
                    ParentId = null
                }

            };
        }

        public IEnumerable<BrandViewModel> GetAllBrands()
        {
            return _brands;
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return _categories;
        }
    }
}
