using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Cart()
        {
            return View(_cartService.TransformCartToViewModel());
        }

        public IActionResult AddToCart(int productId, string returnUrl)
        {
            _cartService.AddToCart(productId);
            return Redirect(returnUrl); //редирект на страницу, где был пользователь
        }

        public IActionResult RemoveAllProducts()
        {
            _cartService.RemoveAllProductsFromCart();
            return RedirectToAction("Cart");
        }

        public IActionResult RemoveProduct(int productId)
        {
            _cartService.RemoveProductFromCart(productId);
            return RedirectToAction("Cart");
        }

        public IActionResult DecrementProductQuantity(int productId)
        {
            _cartService.DecrementProductQuantity(productId);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult AddToCartFromProductDetails(int productId, int quantity)
        {
            _cartService.AddToCart(productId); //потом заменить на отдельный метод, который обрабатывает quantity
            return RedirectToAction("ProductDetails","Catalog", new { productId});
        }
    }
}