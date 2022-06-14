using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(Expression<Func<Order, bool>> filter);
        Order GetOrderByID(int orderId);
        void InsertOrder(Order order);
        void DeleteOrder(int orderId);
        void UpdateOrder(Order order);
        void AddProductToOrder(OrderProduct orderProduct);
        void DeleteProductFromOrder(OrderProduct orderProduct);
        void UpdateProductInOrder(OrderProduct orderProduct);
    }
}
