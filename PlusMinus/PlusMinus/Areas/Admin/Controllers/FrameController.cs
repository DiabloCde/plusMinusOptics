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
    public class FrameController : Controller
    {
        private readonly IProductService<Frame> _frameService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FrameController(IProductService<Frame> frameService, IWebHostEnvironment webHostEnvironment)
        {
            _frameService = frameService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Frame> frames = _frameService.GetProducts(p => p.ProductId > 0);
            
            return View(frames);
        }

        [HttpGet]
        public IActionResult CreateFrame()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFrame(FrameViewModel frameViewModel)
        {
            Frame? frameInDb = _frameService.FirstOrDefault(f =>
                f.Name.ToLower() == frameViewModel.Name.ToLower());

            if (frameInDb is not null)
            {
                ModelState.AddModelError(frameViewModel.Name, "Frame with such name already exists.");
            }

            if (ModelState.IsValid)
            {
                Frame frame = new Frame
                {
                    Name = frameViewModel.Name,
                    Brand = frameViewModel.Brand,
                    Price = double.Parse(frameViewModel.Price.Replace('.', ',')),
                    Amount = frameViewModel.Amount,
                    Form = frameViewModel.Form,
                    Image = ImageUploader.CreatePath(frameViewModel.Image, _webHostEnvironment),
                    Color = frameViewModel.Color,
                    Material = Enum.GetName(typeof(MaterialViewModel), frameViewModel.Material),
                };

                _frameService.AddProduct(frame);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditFrame(int id)
        {
            Frame frame = _frameService.GetProductByID(id);

            if (frame is null)
            {
                return NotFound();
            }

            FrameViewModel frameViewModel = new FrameViewModel
            {
                ProductId = frame.ProductId,
                Name = frame.Name,
                Brand = frame.Brand,
                Price = frame.Price.ToString(CultureInfo.InvariantCulture),
                Amount = frame.Amount,
                Form = frame.Form,
                Color = frame.Color,
                Material = (MaterialViewModel)Enum.Parse(typeof(MaterialViewModel), frame.Material),
                ImageUrl = frame.Image,
            };

            return View(frameViewModel);
        }

        [HttpPost]
        public IActionResult EditFrame(FrameViewModel frameViewModel)
        {
            if (ModelState.IsValid)
            {
                if (frameViewModel.ProductId != null)
                {
                    Frame frame = new Frame
                    {
                        ProductId = (int)frameViewModel.ProductId,
                        Name = frameViewModel.Name,
                        Brand = frameViewModel.Brand,
                        Price = double.Parse(frameViewModel.Price.Replace('.', ',')),
                        Amount = frameViewModel.Amount,
                        Form = frameViewModel.Form,
                        Image = string.IsNullOrEmpty(frameViewModel.ImageUrl) ? ImageUploader.CreatePath(frameViewModel.Image, _webHostEnvironment) : frameViewModel.ImageUrl,
                        Color = frameViewModel.Color,
                        Material = Enum.GetName(typeof(MaterialViewModel), frameViewModel.Material),
                    };

                    _frameService.UpdateProduct(frame);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteFrame(int id)
        {
            Frame frame = _frameService.GetProductByID(id);

            if (frame is null)
            {
                return NotFound();
            }

            return View(frame);
        }

        [HttpPost]
        public IActionResult DeleteFrame(Frame frame)
        {
            _frameService.DeleteProduct(frame.ProductId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FrameDetails(int id)
        {
            Frame frame = _frameService.GetProductByID(id);

            if (frame is null)
            {
                return NotFound();
            }

            return View(frame);
        }
    }
}
