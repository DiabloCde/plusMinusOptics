using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlusMinus.BLL.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders(Expression<Func<Order, bool>> filter);

        Order? FirstOrDefault(Expression<Func<Order, bool>> filter);

        Order GetOrderByID(int orderId);

        void AddOrder(Order order);

        void DeleteOrder(int orderId);

        void UpdateOrder(Order order);

        void AddProductToOrder(OrderProduct orderProduct);

        void DeleteProductFromOrder(OrderProduct orderProduct);

        void UpdateProductInOrder(OrderProduct orderProduct);
    }
}
