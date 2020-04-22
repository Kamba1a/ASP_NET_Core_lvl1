using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
