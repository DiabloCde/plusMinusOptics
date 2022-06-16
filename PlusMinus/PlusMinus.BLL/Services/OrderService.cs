using Microsoft.Extensions.Logging;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlusMinus.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public void AddOrder(Order order)
        {
            if (order is null)
            {
                throw new ArgumentException("Order cannot be null.");
            }

            try
            {
                this._orderRepository.InsertOrder(order);                 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Order was not added.");
                throw;
            }
        }

        public void AddProductToOrder(OrderProduct orderProduct)
        {
            if (orderProduct is null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            try
            {
                this._orderRepository.AddProductToOrder(orderProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product with name {ProductName} was not added.", orderProduct.Product.Name);
                throw;
            }
        }

        public void DeleteOrder(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Order id cannot be less than or equal to zero.");
            }

            try
            {
                this._orderRepository.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order with id {orderId} was not found or already deleted.", orderId);
                throw;
            }
        }

        public void DeleteProductFromOrder(OrderProduct orderProduct)
        {
            if (orderProduct is null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            try
            {
                this._orderRepository.DeleteProductFromOrder(orderProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product with name {ProductName} was not found or already deleted.", orderProduct.Product.Name);
                throw;
            }
        }

        public Order GetOrderByID(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Order id cannot be less than or equal to zero.");
            }

            try
            {
                return this._orderRepository.GetOrderByID(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order with id {orderId} was not found.", orderId);
                throw;
            }
        }

        public List<Order> GetOrders(Expression<Func<Order, bool>> filter)
        {
            try
            {
                return this._orderRepository.GetOrders(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Orders with such filter were not found.");
                throw;
            }
        }

        public void UpdateOrder(Order order)
        {
            if (order is null)
            {
                throw new ArgumentException("Order cannot be null.");
            }

            try
            {
                this._orderRepository.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order with id {orderId} was not found or updated.", order.OrderId);
                throw;
            }
        }

        public void UpdateProductInOrder(OrderProduct orderProduct)
        {
            if (orderProduct is null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            try
            {
                this._orderRepository.UpdateProductInOrder(orderProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product with name {ProductName} was not found or updated.", orderProduct.Product.Name);
                throw;
            }
        }
    }
}
