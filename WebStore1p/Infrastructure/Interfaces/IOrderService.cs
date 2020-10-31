using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore1p.Models;

using WebStore1p.Domain.Entities.Orders;

using WebStore1p.ViewModels

using WebStore1p.ViewModels.Orders;

namespace WebStore1p.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrder(string UserName);

        Order GetOrderById(int id);

        Order GreateOrder(string UserName, CartViewModel Cart, OrderViewModel Order);
    }
}
