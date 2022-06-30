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
    [Authorize(Roles = Roles.RoleAdmin)]
    public class EyecareController : Controller
    {
        private readonly IProductService<Eyecare> _eyecareService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public EyecareController(IProductService<Eyecare> eyecareService, IWebHostEnvironment webHostEnvironment)
        {
            _eyecareService = eyecareService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            IEnumerable<Eyecare> eyecares = _eyecareService.GetProducts(p => p.ProductId > 0);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BrandSortParam"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["VolumeSortParam"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["PurposeSortParam"] = sortOrder == "Purpose" ? "purpose_desc" : "Purpose";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["AmountSortParam"] = sortOrder == "Amount" ? "amount_desc" : "Amount";

            switch (sortOrder)
            {
                case "name_desc":
                    eyecares = eyecares.OrderByDescending(a => a.Name);
                    break;
                case "brand_desc":
                    eyecares = eyecares.OrderByDescending(a => a.Brand);
                    break;
                case "Brand":
                    eyecares = eyecares.OrderBy(a => a.Brand);
                    break;
                case "volume_desc":
                    eyecares = eyecares.OrderByDescending(a => a.Volume);
                    break;
                case "Volume":
                    eyecares = eyecares.OrderBy(a => a.Volume);
                    break;
                case "purpose_desc":
                    eyecares = eyecares.OrderByDescending(a => a.Purpose);
                    break;
                case "Purpose":
                    eyecares = eyecares.OrderBy(a => a.Purpose);
                    break;
                case "price_desc":
                    eyecares = eyecares.OrderByDescending(a => a.Price);
                    break;
                case "Price":
                    eyecares = eyecares.OrderBy(a => a.Price);
                    break;
                case "amount_desc":
                    eyecares = eyecares.OrderByDescending(a => a.Amount);
                    break;
                case "Amount":
                    eyecares = eyecares.OrderBy(a => a.Amount);
                    break;
                default:
                    eyecares = eyecares.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<Eyecare>.CreateAsync(eyecares.AsQueryable(), pageNumber ?? 1, pageSize));
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
