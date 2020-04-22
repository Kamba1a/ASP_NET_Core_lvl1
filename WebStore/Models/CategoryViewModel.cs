using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<CategoryViewModel> childCategories;
    }
}