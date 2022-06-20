using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.Utils;
using PlusMinus.ViewModels;

namespace PlusMinus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EyecareController : Controller
    {
        private readonly IProductService<Eyecare> _eyecareService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public EyecareController(IProductService<Eyecare> eyecareService, IWebHostEnvironment webHostEnvironment)
        {
            _eyecareService = eyecareService;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult CreateEyecare(EyecareViewModel eyecareViewModel)
        {
            Eyecare? eyecareInDb = _eyecareService.FirstOrDefault(f =>
                f.Name.ToLower() == eyecareViewModel.Name.ToLower());

            if (eyecareInDb is not null)
            {
                ModelState.AddModelError(eyecareViewModel.Name, "Eyecare with such name already exists.");
            }

            if (ModelState.IsValid)
            {
                Eyecare eyecare = new Eyecare
                {
                    Name = eyecareViewModel.Name,
                    Brand = eyecareViewModel.Brand,
                    Price = double.Parse(eyecareViewModel.Price.Replace('.', ',')),
                    Amount = eyecareViewModel.Amount,
                    Image = ImageUploader.CreatePath(eyecareViewModel.Image, _webHostEnvironment),
                    Purpose = eyecareViewModel.Purpose,
                    Volume = eyecareViewModel.Volume,
                };

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

            EyecareViewModel eyecareViewModel = new EyecareViewModel
            {
                ProductId = eyecare.ProductId,
                Name = eyecare.Name,
                Amount = eyecare.Amount,
                Brand = eyecare.Brand,
                ImageUrl = eyecare.Image,
                Price = eyecare.Price.ToString(CultureInfo.InvariantCulture),
                Purpose = eyecare.Purpose,
                Volume = eyecare.Volume,
            };

            return View(eyecareViewModel);
        }

        [HttpPost]
        public IActionResult EditEyecare(EyecareViewModel eyecareViewModel)
        {
            if (ModelState.IsValid)
            {
                if (eyecareViewModel.ProductId != null)
                {
                    Eyecare eyecare = new Eyecare
                    {
                        ProductId = (int)eyecareViewModel.ProductId,
                        Name = eyecareViewModel.Name,
                        Brand = eyecareViewModel.Brand,
                        Price = double.Parse(eyecareViewModel.Price.Replace('.', ',')),
                        Amount = eyecareViewModel.Amount,
                        Image = ImageUploader.CreatePath(eyecareViewModel.Image, _webHostEnvironment),
                        Purpose = eyecareViewModel.Purpose,
                        Volume = eyecareViewModel.Volume,
                    };

                    _eyecareService.UpdateProduct(eyecare);
                }

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
