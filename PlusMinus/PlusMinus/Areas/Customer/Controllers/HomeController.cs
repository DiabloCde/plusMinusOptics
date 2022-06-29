using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.Utility;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = Roles.RoleCustomer)]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        
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
