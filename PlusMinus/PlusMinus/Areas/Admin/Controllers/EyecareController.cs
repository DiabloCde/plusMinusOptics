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
    public class EyecareController : Controller
    {
        private readonly IProductService<Eyecare> _eyecareService;

        public EyecareController(IProductService<Eyecare> eyecareService)
        {
            _eyecareService = eyecareService;
        }

        public IActionResult Index()
        {
            List<Eyecare> eyecares = _eyecareService.GetProducts(p => p.ProductId > 0);

            return View(eyecares);
        }

        [HttpGet]
        public IActionResult CreateEyecare()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEyecare(Eyecare eyecare)
        {
            Eyecare? eyecareInDb = _eyecareService.FirstOrDefault(f =>
                f.Name.ToLower() == eyecare.Name.ToLower());

            if (eyecareInDb is not null)
            {
                ModelState.AddModelError(eyecare.Name, "Eyecare with such name already exists.");
            }

            if (ModelState.IsValid)
            {
                _eyecareService.AddProduct(eyecare);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditEyecare(int id)
        {
            Eyecare eyecare = _eyecareService.GetProductByID(id);

            if (eyecare is null)
            {
                return NotFound();
            }

            return View(eyecare);
        }

        [HttpPost]
        public IActionResult EditEyecare(Eyecare eyecare)
        {
            if (ModelState.IsValid)
            {
                _eyecareService.UpdateProduct(eyecare);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteEyecare(int id)
        {
            Eyecare eyecare = _eyecareService.GetProductByID(id);

            if (eyecare is null)
            {
                return NotFound();
            }

            return View(eyecare);
        }

        [HttpPost]
        public IActionResult DeleteEyecare(Eyecare eyecare)
        {
            _eyecareService.DeleteProduct(eyecare.ProductId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EyecareDetails(int id)
        {
            Eyecare eyecare = _eyecareService.GetProductByID(id);

            if (eyecare is null)
            {
                return NotFound();
            }

            return View(eyecare);
        }
    }
}
