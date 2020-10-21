using WebStore1p.Infrastructure.Interfaces;

using WebStore1p.ViewModels;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore1p.Domain.Entities;
using WebStore1p.Infrastructure.Mapping;
using WebStore1p.Models;


namespace WebStore1p.Infrastructure.Services.InCookies
{
    public class CookiesCartService : ICartService
    {
        private readonly string _CartName;

        private readonly IProductData _ProductData;
        private readonly IHttpContextAccessor _HttpContexAccessor;

        private Cart Cart
        {
            //Getter
            get { var context = _HttpContexAccessor.HttpContext;
                //ПШ L7 2.00 Куки которые улетят в ответ    
                var cookies = context.Response.Cookies;
                //ПШ L7 2.00 Куки из запроса
                var cart_cookie = context.Request.Cookies[_CartName];
                //ПШ L7 если они ещё не созданы
                if(cart_cookie is null)
                {
                    //ПШ L7 сформируем новый объект Корзины
                    var cart = new Cart();
                    //ПШ L7 и сериализуем его
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    //ПШ L7 возратим объект Корзины, как результат
                    return cart;
                }

                //ПШ L7 Если Cookies были, то их нужно заменить методом Replace на те, что у нас потребуются
                ReplaceCookies(cookies, cart_cookie);
                //ПШ L7 после Замены Десериализуем корзину
                return JsonConvert.DeserializeObject<Cart>(cart_cookie);
            }
            //Setter
            set => ReplaceCookies(_HttpContexAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
            }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            //ПШ L7 Во-первых удаляем Cookies текущей Корзины
            cookies.Delete(_CartName);
            //ПШ L7 2.19 Добавляем новые Куки, с указанием времени их действия
            cookies.Append(_CartName, cookie, new CookieOptions { Expires = DateTime.Now.AddDays(15) });


        }

        //ПШ L7 39.00 Получаем контексты
        public CookiesCartService(IProductData ProductData, IHttpContextAccessor HttpContexAccessor)
            {
            _ProductData = ProductData;
            _HttpContexAccessor = HttpContexAccessor;

            var user = HttpContexAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;
        
            _CartName = $"WebStore.cart[{user_name}]";

        }

        public void AddToCart(int id)
        {
            //ПШ L7 Извлекаем Корзину в локальный объект
            var cart = Cart;
            //ПШ L7 Ищем товар по идентификатору в Корзине
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;
            
            //ПШ L7 2:24 Записываем модифицированную корзину в Корзину, тем самым вызывая Десериализацию его.
            Cart = cart;

        }

        //Реализация интерфейса
        public void DecrementFromCart(int id)
        {
            //ПШ L7 Извлекаем Корзину в локальный объект
            var cart = Cart;
            //ПШ L7 Ищем товар по идентификатору в Корзине
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            //ПШ L7 Если товара нет, то ничего не делаем
            if (item is null) return;

            if (item.Quantity > 0)
                //ПШ L7 Декрементируем
                item.Quantity--;

            //ПШ L7 если количество товара стало равным 0, то его надо изъять из Корзины
            if (item.Quantity == 0)
                cart.Items.Remove(item);

            //ПШ L7 Записываем модифицированную корзину в Корзину, тем самым вызывая Сериализацию его.
            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            //ПШ L7 Извлекаем Корзину в локальный объект
            var cart = Cart;
            //ПШ L7 Ищем товар по идентификатору в Корзине
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null) return;

            cart.Items.Remove(item);

            Cart = cart;

        }

        //ПШ L7 Очистка корзины
        //ПШ L7 Можно просто создать новую
        //public void RemoveAll() => Cart = new Cart();

        public void RemoveAll()
        {
            var cart = Cart;

            cart.Items.Clear();

            Cart = cart;
        }
        
        //ПШ L7 Выполнить преобразоования во ViewModel
        public CartViewModel TransformFromCart()
        {
            //###
            var products = _ProductData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(item => item.ProductId).ToList()
            });

            var product_view_models = products.ToView();

            return new CartViewModel
            {
                //ПШ L7 2:30 Воспользуемся LINQ
                Items = Cart.Items.ToDictionary(
                    item => product_view_models.First(p => p.Id == item.ProductId),
                    item => item.Quantity
                    )
            };
        }
    }
}
