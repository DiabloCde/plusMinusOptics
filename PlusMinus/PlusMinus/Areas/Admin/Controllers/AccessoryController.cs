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
    public class AccessoryController : Controller
    {
        private readonly IProductService<Accessory> _accessoryService;

        public AccessoryController(IProductService<Accessory> accessoryService)
        {
            _accessoryService = accessoryService;
        }

        public IActionResult Index()
        {
            List<Accessory> accessories = _accessoryService.GetProducts(p => p.ProductId > 0);

            return View(accessories);
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
                    Color = accessoryViewModel.Color,
                    Material = Enum.GetName(typeof(MaterialViewModel), accessoryViewModel.Material),
                    Image = "www.google.com",
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
                Color = accessory.Color,
                Material = (MaterialViewModel)Enum.Parse(typeof(MaterialViewModel), accessory.Material),
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
                        Image = "www.google.com",
                        Color = accessoryViewModel.Color,
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
