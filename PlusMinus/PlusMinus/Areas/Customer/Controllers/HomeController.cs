using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        IUserService _userService;
        
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
