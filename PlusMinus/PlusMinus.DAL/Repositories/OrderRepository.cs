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
            try
            {
                this._context.OrderProducts.Add(orderProduct);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                Order order = this._context.Orders.Find(orderId);
                this._context.Orders.Remove(order);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProductFromOrder(OrderProduct orderProduct)
        {
            try
            {
                OrderProduct order = this._context.OrderProducts
                    .Single(x => x.OrderId == orderProduct.OrderId
                    && x.ProductId == orderProduct.ProductId);
                this._context.OrderProducts.Remove(order);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Order GetOrderByID(int orderId)
        {
            try
            {
                return this._context.Orders.Find(orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Order> GetOrders(Expression<Func<Order, bool>> filter)
        {
            try
            {
                return this._context.Orders
                    .Include(t => t.OrderProducts)
                    .Where(filter)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertOrder(Order order)
        {
            try
            {
                this._context.Orders.Add(order);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                this._context.Entry(order).State = EntityState.Modified;
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProductInOrder(OrderProduct orderProduct)
        {
            try
            {
                OrderProduct order = this._context.OrderProducts
                    .Single(x => x.OrderId == orderProduct.OrderId
                    && x.ProductId == orderProduct.ProductId);
                order.Amount = orderProduct.Amount;
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
