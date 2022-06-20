using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        
        IProductService<Product> _productService;
        
        public CartController(IProductService<Product> productService)
        {
            _productService = productService;
        }
        

        public IActionResult Index()
        {
            // Обращение к сервису OrderService
            return View();
        }

        public IActionResult Shop()
        {
            System.Linq.Expressions.Expression<Func<Product, bool>> expr = i => i.ProductId >= 0;
            var prods = _productService.GetProducts(expr);
            return View(prods);
        }
        public IActionResult MakeOrder()
        { 
            return View(); 
        }
    }
}
