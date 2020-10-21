using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebStore1p.Infrastructure.Interfaces;

namespace WebStore1p.Controllers
{


    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        //ПШ L7 в конструкторе Запрашиваем наш сервис ICartService, и сохраняем его в приватную переменную
        public CartController(ICartService CartService) => _CartService = CartService;

        //ПШ L7 Для Details На представлении вызовем преобразование во ViewModel
        public IActionResult Details() => View(_CartService.TransformFromCart());

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

    }
}
