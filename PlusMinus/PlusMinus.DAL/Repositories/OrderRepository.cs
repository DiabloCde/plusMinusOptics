using Microsoft.EntityFrameworkCore;
using PlusMinus.Core.Models;
using PlusMinus.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public void AddProductToOrder(OrderProduct orderProduct)
        {
            this._context.OrderProducts.Add(orderProduct);
            this._context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            Order order = this._context.Orders.Find(orderId);
            this._context.Orders.Remove(order);
            this._context.SaveChanges();
        }

        public void DeleteProductFromOrder(OrderProduct orderProduct)
        {
            OrderProduct order = this._context.OrderProducts
                .Single(x => x.OrderId == orderProduct.OrderId
                             && x.ProductId == orderProduct.ProductId);
            this._context.OrderProducts.Remove(order);
            this._context.SaveChanges();
        }

        public Order? FirstOrDefault(Expression<Func<Order, bool>> filter)
        {
            Order? order = this._context.Orders
                .Include(op => op.OrderProducts)
                .Include(u => u.User)
                .FirstOrDefault(filter);

            return order;
        }

        public Order GetOrderByID(int orderId)
        {
            return this._context.Orders.Find(orderId);
        }

        public IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> filter)
        {
            return this._context.Orders
                .Include(t => t.OrderProducts)
                .ThenInclude(t => t.Product)
                .Include(t => t.OrderProducts)
                .ThenInclude(t => t.Order)
                .Include(u => u.User)
                .Where(filter);
        }

        public void InsertOrder(Order order)
        {
            this._context.Orders.Add(order);
            this._context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            this._context.Entry(order).State = EntityState.Modified;
            this._context.SaveChanges();
        }

        public void UpdateProductInOrder(OrderProduct orderProduct)
        {
            OrderProduct order = this._context.OrderProducts
                .Single(x => x.OrderId == orderProduct.OrderId
                             && x.ProductId == orderProduct.ProductId);
            order.Amount = orderProduct.Amount;
            this._context.SaveChanges();
        }
    }
}
