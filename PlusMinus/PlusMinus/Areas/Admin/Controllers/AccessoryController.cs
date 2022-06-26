using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class AccessoryController : Controller
    {
        private readonly IProductService<Accessory> _accessoryService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccessoryController(IProductService<Accessory> accessoryService, IWebHostEnvironment webHostEnvironment)
        {
            _accessoryService = accessoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            IEnumerable<Accessory> accessories = _accessoryService.GetProducts(p => p.ProductId > 0);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BrandSortParam"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["ColorSortParam"] = sortOrder == "Color" ? "color_desc" : "Color";
            ViewData["MaterialSortParam"] = sortOrder == "Material" ? "material_desc" : "Material";
            ViewData["AccessoryTypeSortParam"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["PriceSortParam"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["AmountSortParam"] = sortOrder == "Amount" ? "amount_desc" : "Amount";

            switch (sortOrder)
            {
                case "name_desc":
                    accessories = accessories.OrderByDescending(a => a.Name);
                    break;
                case "brand_desc":
                    accessories = accessories.OrderByDescending(a => a.Brand);
                    break;
                case "Brand":
                    accessories = accessories.OrderBy(a => a.Brand);
                    break;
                case "color_desc":
                    accessories = accessories.OrderByDescending(a => a.Color);
                    break;
                case "Color":
                    accessories = accessories.OrderBy(a => a.Color);
                    break;
                case "material_desc":
                    accessories = accessories.OrderByDescending(a => a.Material);
                    break;
                case "Material":
                    accessories = accessories.OrderBy(a => a.Color);
                    break;
                case "type_desc":
                    accessories = accessories.OrderByDescending(a => a.AccessoryType);
                    break;
                case "Type":
                    accessories = accessories.OrderBy(a => a.AccessoryType);
                    break;
                case "price_desc":
                    accessories = accessories.OrderByDescending(a => a.Price);
                    break;
                case "Price":
                    accessories = accessories.OrderBy(a => a.Price);
                    break;
                case "amount_desc":
                    accessories = accessories.OrderByDescending(a => a.Amount);
                    break;
                case "Amount":
                    accessories = accessories.OrderBy(a => a.Amount);
                    break;
                default:
                    accessories = accessories.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<Accessory>.CreateAsync(accessories.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult CreateAccessory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccessory(AccessoryViewModel accessoryViewModel)
        {
            Accessory? accessoryInDb = _accessoryService.FirstOrDefault(a =>
                a.Name.ToLower() == accessoryViewModel.Name.ToLower());

            if (accessoryInDb is not null)
            {
                ModelState.AddModelError(accessoryViewModel.Name, "Accessory with such name already exists.");
            }

            if (ModelState.IsValid)
            {
                Accessory accessory = new Accessory
                {
                    Name = accessoryViewModel.Name,
                    Brand = accessoryViewModel.Brand,
                    Price = accessoryViewModel.Price,
                    Amount = accessoryViewModel.Amount,
                    Color = Enum.GetName(typeof(ColorViewModel), accessoryViewModel.Color),
                    Material = Enum.GetName(typeof(MaterialViewModel), accessoryViewModel.Material),
                    Image = ImageUploader.CreatePath(accessoryViewModel.Image, _webHostEnvironment),
                    AccessoryType = accessoryViewModel.AccessoryType,
                };

                _accessoryService.AddProduct(accessory);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditAccessory(int id)
        {
            Accessory accessory = _accessoryService.GetProductByID(id);

            if (accessory is null)
            {
                return NotFound();
            }

            AccessoryViewModel accessoryViewModel = new AccessoryViewModel
            {
                ProductId = accessory.ProductId,
                Name = accessory.Name,
                Brand = accessory.Brand,
                Price = accessory.Price,
                Amount = accessory.Amount,
                Color = (ColorViewModel)Enum.Parse(typeof(ColorViewModel), accessory.Color),
                Material = (MaterialViewModel)Enum.Parse(typeof(MaterialViewModel), accessory.Material),
                ImageUrl = accessory.Image,
                AccessoryType = accessory.AccessoryType,
            };

            return View(accessoryViewModel);
        }

        [HttpPost]
        public IActionResult EditAccessory(AccessoryViewModel accessoryViewModel)
        {
             if (ModelState.IsValid)
             {
                if (accessoryViewModel.ProductId != null)
                {
                    Accessory accessory = new Accessory
                    {
                        ProductId = (int)accessoryViewModel.ProductId,
                        Name = accessoryViewModel.Name,
                        Brand = accessoryViewModel.Brand,
                        Price = accessoryViewModel.Price,
                        Amount = accessoryViewModel.Amount,
                        Image = ImageUploader.CreatePath(accessoryViewModel.Image, _webHostEnvironment),
                        Color = Enum.GetName(typeof(ColorViewModel), accessoryViewModel.Color),
                        Material = Enum.GetName(typeof(MaterialViewModel), accessoryViewModel.Material),
                        AccessoryType = accessoryViewModel.AccessoryType,
                    };

                    _accessoryService.UpdateProduct(accessory);
                }

                return RedirectToAction("Index");
             }

             return View();
        }

        [HttpGet]
        public IActionResult DeleteAccessory(int id)
        {
            Accessory accessory = _accessoryService.GetProductByID(id);

            if (accessory is null)
            {
                return NotFound();
            }

            return View(accessory);
        }

        [HttpPost]
        public IActionResult DeleteAccessory(Accessory accessory)
        {
            _accessoryService.DeleteProduct(accessory.ProductId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessoryDetails(int id)
        {
            Accessory accessory = _accessoryService.GetProductByID(id);

            if (accessory is null)
            {
                return NotFound();
            }

            return View(accessory);
        }
    }
}
