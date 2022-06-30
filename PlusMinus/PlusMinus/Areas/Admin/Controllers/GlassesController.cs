using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.ViewModels;
using PlusMinus.Utils;
using Microsoft.AspNetCore.Hosting;

namespace PlusMinus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GlassesController : Controller
    {
        private readonly IProductService<Glasses> _glassesService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public GlassesController(IProductService<Glasses> glassesService, IWebHostEnvironment webHostEnvironment)
        {
            _glassesService = glassesService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Glasses> glasses = _glassesService.GetProducts(p => p.ProductId > 0);

            return View(glasses);
        }

        [HttpGet]
        public IActionResult CreateGlasses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateGlasses(GlassesViewModel glassesViewModel)
        {
            if (ModelState.IsValid)
            {
                Glasses glasses = new Glasses
                {
                    Name = glassesViewModel.Name,
                    Brand = glassesViewModel.Brand,
                    Price = glassesViewModel.Price,
                    Amount = glassesViewModel.Amount,
                    Form = glassesViewModel.Form,
                    Image = ImageUploader.CreatePath(glassesViewModel.Image, _webHostEnvironment),
                    Color = glassesViewModel.Color,
                    Material = Enum.GetName(typeof(MaterialViewModel), glassesViewModel.Material),
                    Dioptre = glassesViewModel.Dioptre,
                };

                _glassesService.AddProduct(glasses);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditGlasses(int id)
        {
            Glasses glasses = _glassesService.GetProductByID(id);

            if (glasses is null)
            {
                return NotFound();
            }

            GlassesViewModel glassesViewModel = new GlassesViewModel
            {
                ProductId = glasses.ProductId,
                Name = glasses.Name,
                Brand = glasses.Brand,
                Price = glasses.Price,
                Amount = glasses.Amount,
                Form = glasses.Form,
                Color = glasses.Color,
                Material = (MaterialViewModel)Enum.Parse(typeof(MaterialViewModel), glasses.Material),
                Dioptre = glasses.Dioptre,
                ImageUrl = glasses.Image,
            };

            return View(glassesViewModel);
        }

        [HttpPost]
        public IActionResult EditGlasses(GlassesViewModel glassesViewModel)
        {
            if (ModelState.IsValid)
            {
                if (glassesViewModel.ProductId != null)
                {
                    Glasses glasses = new Glasses
                    {
                        ProductId = (int)glassesViewModel.ProductId,
                        Name = glassesViewModel.Name,
                        Brand = glassesViewModel.Brand,
                        Price = glassesViewModel.Price,
                        Amount = glassesViewModel.Amount,
                        Form = glassesViewModel.Form,
                        Image = ImageUploader.CreatePath(glassesViewModel.Image, _webHostEnvironment),
                        Color = glassesViewModel.Color,
                        Material = Enum.GetName(typeof(MaterialViewModel), glassesViewModel.Material),
                        Dioptre = glassesViewModel.Dioptre,
                    };

                    _glassesService.UpdateProduct(glasses);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteGlasses(int id)
        {
            Glasses glasses = _glassesService.GetProductByID(id);

            if (glasses is null)
            {
                return NotFound();
            }

            return View(glasses);
        }

        [HttpPost]
        public IActionResult DeleteGlasses(Glasses glasses)
        {
            _glassesService.DeleteProduct(glasses.ProductId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GlassesDetails(int id)
        {
            Glasses glasses = _glassesService.GetProductByID(id);

            if (glasses is null)
            {
                return NotFound();
            }

            return View(glasses);
        }
    }
}
