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
    public class LensesController : Controller
    {
        private readonly IProductService<Lenses> _lensesService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LensesController(IProductService<Lenses> lensesService, IWebHostEnvironment webHostEnvironment)
        {
            _lensesService = lensesService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Lenses> lenses = _lensesService.GetProducts(l => l.ProductId > 0);

            return View(lenses);
        }

        [HttpGet]
        public IActionResult CreateLenses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLenses(LensesViewModel lensesViewModel)
        {
            if (ModelState.IsValid)
            {
                Lenses lenses = new Lenses
                {
                    Name = lensesViewModel.Name,
                    Brand = lensesViewModel.Brand,
                    Price = double.Parse(lensesViewModel.Price.Replace('.', ',')),
                    Amount = lensesViewModel.Amount,
                    Image = ImageUploader.CreatePath(lensesViewModel.Image, _webHostEnvironment),
                    BaseCurve = double.Parse(lensesViewModel.BaseCurve.Replace('.', ',')),
                    Diameter = double.Parse(lensesViewModel.Diameter.Replace('.', ',')),
                    Dioptre = double.Parse(lensesViewModel.Dioptre.Replace('.', ',')),
                    ExpirationDate = lensesViewModel.ExpirationDateValue,
                    NumberOfUnits = lensesViewModel.NumberOfUnits,
                };

                _lensesService.AddProduct(lenses);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditLenses(int id)
        {
            Lenses lenses = _lensesService.GetProductByID(id);

            if (lenses is null)
            {
                return NotFound();
            }

            LensesViewModel lensesViewModel = new LensesViewModel
            {
                ProductId = lenses.ProductId,
                Name = lenses.Name,
                Brand = lenses.Brand,
                Price = lenses.Price.ToString(CultureInfo.InvariantCulture),
                Amount = lenses.Amount,
                BaseCurve = lenses.BaseCurve.ToString(CultureInfo.InvariantCulture),
                Diameter = lenses.Diameter.ToString(CultureInfo.InvariantCulture),
                Dioptre = lenses.Dioptre.ToString(CultureInfo.InvariantCulture),
                ExpirationDateValue = lenses.ExpirationDate,
                NumberOfUnits = lenses.NumberOfUnits,
                ImageUrl = lenses.Image,
            };

            return View(lensesViewModel);
        }

        [HttpPost]
        public IActionResult EditLenses(LensesViewModel lensesViewModel)
        {
            if (ModelState.IsValid)
            {
                if (lensesViewModel.ProductId != null)
                {
                    Lenses lenses= new Lenses
                    {
                        ProductId = (int)lensesViewModel.ProductId,
                        Name = lensesViewModel.Name,
                        Brand = lensesViewModel.Brand,
                        Price = double.Parse(lensesViewModel.Price.Replace('.', ',')),
                        Amount = lensesViewModel.Amount,
                        Image = string.IsNullOrEmpty(lensesViewModel.ImageUrl) ? ImageUploader.CreatePath(lensesViewModel.Image, _webHostEnvironment) : lensesViewModel.ImageUrl,
                        BaseCurve = double.Parse(lensesViewModel.BaseCurve.Replace('.', ',')),
                        Diameter = double.Parse(lensesViewModel.Diameter.Replace('.', ',')),
                        Dioptre = double.Parse(lensesViewModel.Dioptre.Replace('.', ',')),
                        ExpirationDate = lensesViewModel.ExpirationDateValue,
                    };

                    _lensesService.UpdateProduct(lenses);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteLenses(int id)
        {
            Lenses lenses = _lensesService.GetProductByID(id);

            if (lenses is null)
            {
                return NotFound();
            }

            return View(lenses);
        }

        [HttpPost]
        public IActionResult DeleteLenses(Lenses lenses)
        {
            _lensesService.DeleteProduct(lenses.ProductId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LensesDetails(int id)
        {
            Lenses lenses = _lensesService.GetProductByID(id);

            if (lenses is null)
            {
                return NotFound();
            }

            return View(lenses);
        }
    }
}
