using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.Core.Models.Enumerations;
using PlusMinus.Utility;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = Roles.RoleCustomer)]
    public class CartController : Controller
    {
        private readonly IProductService<Product> _productService;

        private readonly IOrderService _orderService;
        
        public CartController(IProductService<Product> productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        
        public IActionResult Index()
        {
            Expression<Func<Order, bool>> expr = i => i.Status == OrderStatus.Cart;
            var currentOrders = _orderService.GetOrders(expr).ToList();
            Order order = currentOrders.Count > 0 ? currentOrders[0] : null;
            List<Product> products = new List<Product>();
            if (order is not null)
            {
                foreach (var orderProduct in order.OrderProducts)
                {
                    products.Add(orderProduct.Product);
                }
            }
            return View(products);
        }

        public IActionResult Shop()
        {
            Expression<Func<Product, bool>> expr = i => i.ProductId >= 0;
            var prods = _productService.GetProducts(expr);
            return View(prods);
        }
        public IActionResult MakeOrder()
        {
            Expression<Func<Order, bool>> expr = i => i.Status == OrderStatus.Cart;
            var currentOrders = _orderService.GetOrders(expr).ToList();
            Order order = currentOrders.Count > 0 ? currentOrders[0] : null;
            List<Product> products = new List<Product>();
            if (order is not null)
            {
                foreach (var orderProduct in order.OrderProducts)
                {
                    var p = orderProduct.Product;
                    p.Amount--;
                    _productService.UpdateProduct(p);
                }
            } 
            return View(); 
        }
    }
}
