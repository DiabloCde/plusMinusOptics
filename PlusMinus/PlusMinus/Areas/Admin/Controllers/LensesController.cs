using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.ViewModels;

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
                    Price = lensesViewModel.Price,
                    Amount = lensesViewModel.Amount,
                    Image = "www.google.com",
                    BaseCurve = lensesViewModel.BaseCurve,
                    Diameter = lensesViewModel.Diameter,
                    Dioptre = lensesViewModel.Dioptre,
                    ExpirationDate = new TimeSpan(lensesViewModel.ExpirationDateValue, 0 , 0, 0),
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
                Name = lenses.Name,
                Brand = lenses.Brand,
                Price = lenses.Price,
                Amount = lenses.Amount,
                BaseCurve = lenses.BaseCurve,
                Diameter = lenses.Diameter,
                Dioptre = lenses.Dioptre,
                ExpirationDateValue = lenses.ExpirationDate.Days,
                NumberOfUnits = lenses.NumberOfUnits,
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
                        Price = lensesViewModel.Price,
                        Amount = lensesViewModel.Amount,
                        Image = "www.google.com",
                        BaseCurve = lensesViewModel.BaseCurve,
                        Diameter = lensesViewModel.Diameter,
                        Dioptre = lensesViewModel.Dioptre,
                        ExpirationDate = new TimeSpan(lensesViewModel.ExpirationDateValue, 0, 0, 0),
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
