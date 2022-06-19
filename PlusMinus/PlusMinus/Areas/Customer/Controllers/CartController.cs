using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            // Обращение к сервису OrderService
            return View();
        }

        public IActionResult MakeOrder()
        { 
            return View(); 
        }
    }
}
