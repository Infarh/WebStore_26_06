using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Products;
using WebStore.Domain.ViewModels.Cart;
using WebStore.Domain.ViewModels.Order;
using WebStore.Interfaces.Services;
using WebStore.Services.Map;

namespace WebStore.Services.SQL
{
    public class SqlOrdersService : IOrderService
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _UserManager;

        public SqlOrdersService(WebStoreContext db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) =>
            _db.Orders
               .Include(order => order.User)
               .Include(order => order.OrderItems)
               .Where(order => order.User.UserName == UserName)
               .AsEnumerable()
               .ToDTO();

        public OrderDTO GetOrderById(int id) =>
            _db.Orders
               .Include(o => o.OrderItems)
               .FirstOrDefault(o => o.Id == id)
               .ToDTO();

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;

            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.OrderViewModel.Name,
                    Address = OrderModel.OrderViewModel.Address,
                    Phone = OrderModel.OrderViewModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };

                _db.Orders.Add(order);

                foreach (var item in OrderModel.OrderItems)
                {
                    var product_model = _db.Products.FirstOrDefault(p => p.Id == item.Id);
                    if(product_model is null)
                        throw new InvalidOperationException($"Товар с id {item.Id} не найден в базе данных");

                    var product = _db.Products.FirstOrDefault(p => p.Id == product_model.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с идентификатором {product_model.Id} в базе данных не найден");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };

                    _db.OrderItems.Add(order_item);
                }

                _db.SaveChanges();
                transaction.Commit();

                return order.ToDTO();
            }
        }
    }
}
