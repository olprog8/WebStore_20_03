using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebStore1p.Infrastructure.Interfaces;

using WebStore1p.ViewModels.Orders;

using WebStore1p.ViewModels;

namespace WebStore1p.Controllers
{


    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        //ПШ L7 в конструкторе Запрашиваем наш сервис ICartService, и сохраняем его в приватную переменную
        public CartController(ICartService CartService) => _CartService = CartService;

        //ПШ L7 Для Details На представлении вызовем преобразование во ViewModel
        //public IActionResult Details() => View(_CartService.TransformFromCart());

        //ПШ L8 После добавления OrderController внесем изменение в Details
        public IActionResult Details() => View(new CartOrderViewModel
        { CartViewModel = _CartService.TransformFromCart(),
          OrderViewModel = new OrderViewModel()
        });

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DectrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }
        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveAll()
        {
            _CartService.RemoveAll();
            return RedirectToAction(nameof(Details));
        }

        public async Task<IActionResult> CheckOut(OrderViewModel Model, [FromServices] IOrderService OrderService)
        {
            //ПШ L8 2:02 Если модель невалидна, отправляем обратно пользователю Представление корзины, и текущую модель заказа, чтобы он продолжил её радостно редактировать
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartOrderViewModel
                {
                    CartViewModel = _CartService.TransformFromCart(),
                    OrderViewModel = Model
                });

            //ПШ L8 2:03 Формируем заказ в Асинхронном виде
            var order = await OrderService.GreateOrderAsync(User.Identity.Name,
                                                            _CartService.TransformFromCart(),
                                                            Model);

            _CartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id});
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;

            return View();
        }

    }
}
