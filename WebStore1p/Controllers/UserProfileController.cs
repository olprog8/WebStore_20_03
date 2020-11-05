using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebStore1p.Infrastructure.Interfaces;

using WebStore1p.ViewModels.Orders;

namespace WebStore1p.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Orders([FromServices] IOrderService OrderService) => 
            View(OrderService.GetUserOrder(User.Identity.Name)
                .Select(order => new UserOrderViewModel 
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(i=> i.Price*i.Quantity)
                }));
    }
}
