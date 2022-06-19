using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;

namespace PlusMinus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            List<Order> orders = _orderService.GetOrders(o => o.OrderId > 0); 
            
            return View(orders);
        }

        [HttpGet]
        public IActionResult ChangeOrderStatus(int id)
        {
            Order order = _orderService.GetOrderByID(id);

            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult ChangeOrderStatus(Order orderViewModel)
        {
            if (orderViewModel.OrderId < 0)
            {
                return NotFound();
            }

            Order order = _orderService.GetOrderByID(orderViewModel.OrderId);
            order.Status = orderViewModel.Status;
            _orderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }
    }
}
