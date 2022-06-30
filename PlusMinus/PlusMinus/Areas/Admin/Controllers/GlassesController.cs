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
using Microsoft.AspNetCore.Authorization;
using PlusMinus.Utility;

namespace PlusMinus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.RoleAdmin)]
    public class GlassesController : Controller
    {
        private readonly IProductService<Glasses> _glassesService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public GlassesController(IProductService<Glasses> glassesService, IWebHostEnvironment webHostEnvironment)
        {
            _glassesService = glassesService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            IEnumerable<Glasses> glasses = _glassesService.GetProducts(p => p.ProductId > 0);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BrandSortParam"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["ColorSortParam"] = sortOrder == "Color" ? "color_desc" : "Color";
            ViewData["MaterialSortParam"] = sortOrder == "Material" ? "material_desc" : "Material";
            ViewData["FormSortParam"] = sortOrder == "Form" ? "form_desc" : "Form";
            ViewData["DioptreSortParam"] = sortOrder == "Dioptre" ? "dioptre_desc" : "Dioptre";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["AmountSortParam"] = sortOrder == "Amount" ? "amount_desc" : "Amount";

            switch (sortOrder)
            {
                case "name_desc":
                    glasses = glasses.OrderByDescending(a => a.Name);
                    break;
                case "brand_desc":
                    glasses = glasses.OrderByDescending(a => a.Brand);
                    break;
                case "Brand":
                    glasses = glasses.OrderBy(a => a.Brand);
                    break;
                case "color_desc":
                    glasses = glasses.OrderByDescending(a => a.Color);
                    break;
                case "Color":
                    glasses = glasses.OrderBy(a => a.Color);
                    break;
                case "material_desc":
                    glasses = glasses.OrderByDescending(a => a.Material);
                    break;
                case "Material":
                    glasses = glasses.OrderBy(a => a.Color);
                    break;
                case "form_desc":
                    glasses = glasses.OrderByDescending(a => a.Form);
                    break;
                case "Form":
                    glasses = glasses.OrderBy(a => a.Form);
                    break;
                case "dioptre_desc":
                    glasses = glasses.OrderByDescending(a => a.Dioptre);
                    break;
                case "Dioptre":
                    glasses = glasses.OrderBy(a => a.Dioptre);
                    break;
                case "price_desc":
                    glasses = glasses.OrderByDescending(a => a.Price);
                    break;
                case "Price":
                    glasses = glasses.OrderBy(a => a.Price);
                    break;
                case "amount_desc":
                    glasses = glasses.OrderByDescending(a => a.Amount);
                    break;
                case "Amount":
                    glasses = glasses.OrderBy(a => a.Amount);
                    break;
                default:
                    glasses = glasses.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<Glasses>.CreateAsync(glasses.AsQueryable(), pageNumber ?? 1, pageSize));
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
