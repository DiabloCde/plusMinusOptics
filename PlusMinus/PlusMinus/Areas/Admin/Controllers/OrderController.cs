﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(string sortOrder)
        {
            IEnumerable<Order> orders = _orderService.GetOrders(o => o.OrderId > 0);

            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["SurnameSortParam"] = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";

            switch (sortOrder)
            {
                case "id_desc":
                    orders = orders.OrderByDescending(a => a.OrderId);
                    break;
                case "name_desc":
                    orders = orders.OrderByDescending(a => a.User.Name);
                    break;
                case "Name":
                    orders = orders.OrderBy(a => a.User.Name);
                    break;
                case "surname_desc":
                    orders = orders.OrderByDescending(a => a.User.Surname);
                    break;
                case "Surname":
                    orders = orders.OrderBy(a => a.User.Surname);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(a => a.Date);
                    break;
                case "Date":
                    orders = orders.OrderBy(a => a.Date);
                    break;
                case "status_desc":
                    orders = orders.OrderByDescending(a => a.Status);
                    break;
                case "Status":
                    orders = orders.OrderBy(a => a.Status);
                    break;
                default:
                    orders = orders.OrderBy(a => a.OrderId);
                    break;
            }

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

        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            Order order = _orderService.GetOrderByID(id);

            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            _orderService.DeleteOrder(order.OrderId);

            return RedirectToAction("Index");
        }
    }
}
