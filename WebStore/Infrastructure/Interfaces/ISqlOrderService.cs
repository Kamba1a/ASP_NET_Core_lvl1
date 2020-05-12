using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ISqlOrderService
    {
        Order CreateOrder(OrderDetailsViewModel model, CartViewModel cart, string UserName);
    }
}
