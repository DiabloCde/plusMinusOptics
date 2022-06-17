using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusMinus.Areas.Admin.Controllers
{
    public class EyecareController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
