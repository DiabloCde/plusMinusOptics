using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
                    if (p.Amount < orderProduct.Amount)
                    {
                        return View();
                    }
                    p.Amount -= orderProduct.Amount;
                    _productService.UpdateProduct(p);
                }

                order.Status = OrderStatus.Paid;
                _orderService.UpdateOrder(order);
            } 
            return View(); 
        }

        public  IActionResult DeleteProductFromOrder(int productId)
        {
            //productId++;
            Expression<Func<Order, bool>> expr = i => i.Status == OrderStatus.Cart;
            var currentOrders = _orderService.GetOrders(expr).ToList();
            if (currentOrders.Count > 0)
            {
                currentOrders[0].OrderProducts.RemoveAll(x => x.ProductId == productId);
                _orderService.UpdateOrder(currentOrders[0]);
            }
            // Order order = currentOrders.Count > 0 ? currentOrders[0] : null;
            // order.OrderProducts.RemoveAll(x => x.ProductId == productId);
            // return Redirect(Request.Headers["Referer"].ToString());
            return Redirect("~/Customer/Cart");
        }
    }
}
