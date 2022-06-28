using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.Utility;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = Roles.RoleCustomer)]
    public class CartController : Controller
    {
        private readonly IProductService<Product> _productService;
        
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
