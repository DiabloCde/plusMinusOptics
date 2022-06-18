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
    public class FrameController : Controller
    {
        private readonly IProductService<Frame> _frameService;

        public FrameController(IProductService<Frame> frameService)
        {
            _frameService = frameService;
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
            List<Frame> framesInDb = _frameService.GetProducts(f =>
                f.Name.ToLower() == frameViewModel.Name.ToLower());

            if (framesInDb.Count != 0)
            {
                ModelState.AddModelError(frameViewModel.Name, "Frame with such name already exists.");
            }

            if (ModelState.IsValid)
            {
                Frame frame = new Frame
                {
                    Name = frameViewModel.Name,
                    Brand = frameViewModel.Brand,
                    Price = frameViewModel.Price,
                    Amount = frameViewModel.Amount,
                    Form = frameViewModel.Form,
                    Image = "www.google.com",
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
                Price = frame.Price,
                Amount = frame.Amount,
                Form = frame.Form,
                Color = frame.Color,
                Material = (MaterialViewModel)Enum.Parse(typeof(MaterialViewModel), frame.Material),
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
                        Price = frameViewModel.Price,
                        Amount = frameViewModel.Amount,
                        Form = frameViewModel.Form,
                        Image = "www.google.com",
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
