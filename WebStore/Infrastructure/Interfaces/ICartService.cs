using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICartService
    {
        /// <summary>
        /// Уменьшить количество товара в корзине
        /// </summary>
        /// <param name="productId"></param>
        public void DecrementProductQuantity(int productId);
        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        /// <param name="productId"></param>
        public void RemoveProductFromCart(int productId);
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public void RemoveAllProductsFromCart();
        /// <summary>
        /// Добавляет товар в корзину или увеличивает его количество
        /// </summary>
        /// <param name="productId"></param>
        public void AddToCart(int productId);
        /// <summary>
        /// Получает корзину в формате CartViewModel
        /// </summary>
        /// <returns></returns>
        public CartViewModel TransformCartToViewModel();
    }
}
