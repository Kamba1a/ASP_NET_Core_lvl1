using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class CookieCartService : ICartService
    {
        ICatalogData _catalogData;
        IHttpContextAccessor _httpContextAccessor; //для работы с cookies
        string _cartName;

        private Cart Cart
        {
            get
            {
                string cookie = _httpContextAccessor.HttpContext.Request.Cookies[_cartName]; //запрос куки с определенным именем
                string json;
                Cart cart;

                if (cookie == null) //если куки не найдена, то создается новая корзина
                {
                    cart = new Cart { Items = new List<CartItem>() }; //создаем корзину
                    json = JsonConvert.SerializeObject(cart);

                    _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartName, json, new CookieOptions { Expires = DateTime.Now.AddDays(1) }); //создаем куки

                    return cart;
                }
                else //иначе возвращаем корзину из куки
                {
                    json = cookie;
                    cart = JsonConvert.DeserializeObject<Cart>(json);

                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(_cartName);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartName, json, new CookieOptions() { Expires = DateTime.Now.AddDays(1) });

                    return cart;
                }
            }
            set
            {
                string json = JsonConvert.SerializeObject(value);

                _httpContextAccessor.HttpContext.Response.Cookies.Delete(_cartName);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartName, json, new CookieOptions() { Expires = DateTime.Now.AddDays(1) });
            }
        }


        public CookieCartService(ICatalogData catalogData, IHttpContextAccessor httpContextAccessor)
        {
            _catalogData = catalogData;
            _httpContextAccessor = httpContextAccessor;

            //задаем имя корзины, которое включает имя пользователя (а если не авторизован, тогда что включить?)
            _cartName = "cart" + (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated?
                        _httpContextAccessor.HttpContext.User.Identity.Name : "");
        }


        public void DecrementProductQuantity(int productId)
        {
            Cart cart = Cart; //сначала получаем корзину из куки, либо создаем новую

            CartItem cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId); //находим товар в корзине

            if (cartItem is null) return;   //если такого товара нет, то ничего не делаем
            if (cartItem.Quantity > 0) cartItem.Quantity--; //уменьшаем количество товара
            if (cartItem.Quantity == 0) cart.Items.Remove(cartItem); //если количество достигло нуля, то удаляем товар из корзины

            Cart = cart; //сохраняем корзину в куки
        }

        public void AddToCart(int productId)
        {
            Cart cart = Cart; //сначала получаем корзину из куки, либо создаем новую

            CartItem cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId); //находим товар в корзине

            if (cartItem != null) cartItem.Quantity++; // если  товар уже добавлен, то увеличиваем его количество
            else cart.Items.Add(new CartItem { ProductId = productId, Quantity = 1 }); //иначе добавляем единицу товара в корзину

            Cart = cart; //сохраняем корзину в куки
        }

        public void RemoveAllProductsFromCart()
        {
            Cart.Items.Clear(); //удаляем все товары из корзины (изменения сразу происходят через свойство)
        }

        public void RemoveProductFromCart(int productId)
        {
            Cart cart = Cart; //сначала получаем корзину из куки, либо создаем новую

            CartItem cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId); //находим товар в корзине

            if (cartItem is null) return;   //если такого товара нет, то ничего не делаем
            else cart.Items.Remove(cartItem); //иначе удаляем товар из корзины

            Cart = cart; //сохраняем корзину в куки
        }

        public CartViewModel TransformCartToViewModel()
        {
            ////моя реализация(возможно плоха тем, что в цикле foreach идет множественное обращение к БД)
            //Dictionary<ProductViewModel, int> cartItemsDic = new Dictionary<ProductViewModel, int>();
            //foreach (CartItem cartItem in Cart.Items)
            //{
            //    Product product = _catalogData.GetProductById(cartItem.ProductId);
            //    ProductViewModel productViewModel = new ProductViewModel()
            //    {
            //        Id = product.Id,
            //        Name = product.Name,
            //        Price = product.Price,
            //        ImageUrl = product.ImageUrl
            //    };
            //    cartItemsDic.Add(productViewModel, cartItem.Quantity);
            //}
            //return new CartViewModel() { CartItems = cartItemsDic };


            //реализация из методички
            List<ProductViewModel> products = _catalogData.GetProducts(new ProductFilter()      //сначала получаем список Product
            { ProductsIdList = Cart.Items.Select(cartItem => cartItem.ProductId).ToList() })    //(фильтр по списку ID товаров из корзины)
                    .Select(product => new ProductViewModel()                                   //сразу преобразовываем каждый Product в ProductViewModel
                    {
                        Id = product.Id,
                        ImageUrl = product.ImageUrl,
                        Name = product.Name,
                        Order = product.Order,
                        Price = product.Price,
                        BrandName = product.Brand != null ? product.Brand.Name : string.Empty
                    }).ToList();
            CartViewModel cartViewModel = new CartViewModel
            {
                CartItems = Cart.Items.ToDictionary(
                    cartItemKey => products.First(product => product.Id == cartItemKey.ProductId),
                    cartItemValue => cartItemValue.Quantity)
            };
            return cartViewModel;
        }
    }
}
