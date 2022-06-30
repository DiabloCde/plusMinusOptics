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
using Microsoft.AspNetCore.Authorization;
using PlusMinus.Utility;

namespace PlusMinus.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = Roles.RoleAdmin)]
    public class LensesController : Controller
    {
        private readonly IProductService<Lenses> _lensesService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LensesController(IProductService<Lenses> lensesService, IWebHostEnvironment webHostEnvironment)
        {
            _lensesService = lensesService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            IEnumerable<Lenses> lenses = _lensesService.GetProducts(l => l.ProductId > 0);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BrandSortParam"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["DioptreSortParam"] = sortOrder == "Dioptre" ? "dioptre_desc" : "Dioptre";
            ViewData["ExpDateSortParam"] = sortOrder == "ExpDate" ? "expDate_desc" : "ExpDate";
            ViewData["BaseCurveSortParam"] = sortOrder == "BaseCurve" ? "baseCurve_desc" : "BaseCurve";
            ViewData["DiameterSortParam"] = sortOrder == "Diameter" ? "diameter_desc" : "Diameter";
            ViewData["NumberOfUnitsSortParam"] = sortOrder == "NumberOfUnits" ? "numberOfUnits_desc" : "NumberOfUnits";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["AmountSortParam"] = sortOrder == "Amount" ? "amount_desc" : "Amount";

            switch (sortOrder)
            {
                case "name_desc":
                    lenses = lenses.OrderByDescending(a => a.Name);
                    break;
                case "brand_desc":
                    lenses = lenses.OrderByDescending(a => a.Brand);
                    break;
                case "Brand":
                    lenses = lenses.OrderBy(a => a.Brand);
                    break;
                case "dioptre_desc":
                    lenses = lenses.OrderByDescending(a => a.Dioptre);
                    break;
                case "Dioptre":
                    lenses = lenses.OrderBy(a => a.Dioptre);
                    break;
                case "expDate_desc":
                    lenses = lenses.OrderByDescending(a => a.ExpirationDate);
                    break;
                case "ExpDate":
                    lenses = lenses.OrderBy(a => a.ExpirationDate);
                    break;
                case "baseCurve_desc":
                    lenses = lenses.OrderByDescending(a => a.BaseCurve);
                    break;
                case "BaseCurve":
                    lenses = lenses.OrderBy(a => a.BaseCurve);
                    break;
                case "diameter_desc":
                    lenses = lenses.OrderByDescending(a => a.Diameter);
                    break;
                case "Diameter":
                    lenses = lenses.OrderBy(a => a.Diameter);
                    break;
                case "numberOfUnits_desc":
                    lenses = lenses.OrderByDescending(a => a.NumberOfUnits);
                    break;
                case "NumberOfUnits":
                    lenses = lenses.OrderBy(a => a.NumberOfUnits);
                    break;
                case "price_desc":
                    lenses = lenses.OrderByDescending(a => a.Price);
                    break;
                case "Price":
                    lenses = lenses.OrderBy(a => a.Price);
                    break;
                case "amount_desc":
                    lenses = lenses.OrderByDescending(a => a.Amount);
                    break;
                case "Amount":
                    lenses = lenses.OrderBy(a => a.Amount);
                    break;
                default:
                    lenses = lenses.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<Lenses>.CreateAsync(lenses.AsQueryable(), pageNumber ?? 1, pageSize));
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
