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
    public class LensesController : Controller
    {
        private readonly IProductService<Lenses> _lensesService;

        public LensesController(IProductService<Lenses> lensesService)
        {
            _lensesService = lensesService;
        }

        public IActionResult Index()
        {
            List<Lenses> lenses = _lensesService.GetProducts(l => l.ProductId > 0);

            return View(lenses);
        }

        [HttpGet("create")]
        public IActionResult CreateLenses()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateLenses(int id)
        {
            return View();
        }
    }
}
