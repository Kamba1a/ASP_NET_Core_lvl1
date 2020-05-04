using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Models
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> CartItems { get; set; }
        public int ItemsCount => CartItems?.Sum(itemCount => itemCount.Value) ?? 0;
    }
}
