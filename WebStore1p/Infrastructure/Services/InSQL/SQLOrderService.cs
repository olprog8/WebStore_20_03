using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

//Это жесть!
using WebStore.DAL.Context;
using WebStore1p.Domain.Entities.Orders;
using WebStore1p.Domain.Identity;

using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.ViewModels;
using WebStore1p.ViewModels.Orders;

using Microsoft.EntityFrameworkCore;


namespace WebStore1p.Infrastructure.Services.InSQL
{
    public class SQLOrderService: IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;

        public SQLOrderService(WebStoreDB db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;

        }

        public IEnumerable<Order> GetUserOrder(string UserName) => _db.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
            .Where(order => order.User.UserName == UserName)
            .AsEnumerable();

        //###
        public Order GetOrderById(int id) => _db.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefault(order => order.Id == id);
        
        public async Task<Order> GreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);

            //ПШ L8 1.43 Начинаем Транзакцию
            using (var transaction = _db.Database.BeginTransaction())
            {
                //ПШ L8 1.45 Формируем новый заказ
                var order = new Order
                {
                    Name = OrderModel.Name,
                    Address = OrderModel.Address,
                    Phone = OrderModel.Phone,

                    User = user,

                    Date = DateTime.Now
                };

                await _db.Orders.AddAsync(order);

                //ПШ L8 1:49 Деконструктор
                foreach(var (product_mode, quantity) in Cart.Items)
                        {
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == product_mode.Id);
                        //ПШ L8 1:51 Если товар не найден
                        if(product is null)
                        throw new InvalidOperationException($"Товар с id:{product_mode.Id} в базе данных не найден!");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = quantity,
                        Product = product
                    };

                    await _db.OrderItems.AddAsync(order_item);

                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
        }

    }
}
