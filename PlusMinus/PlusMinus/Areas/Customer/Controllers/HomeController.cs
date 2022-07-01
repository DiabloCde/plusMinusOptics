using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PlusMinus.ViewModels;
using PlusMinus.Core.Models.Enumerations;

namespace PlusMinus.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = Roles.RoleCustomer)]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IProductService<Accessory> _accessoryService;

        private readonly IProductService<Frame> _framesService;

        private readonly IProductService<Glasses> _glassesService;

        private readonly IProductService<Lenses> _lensesService;

        private readonly IProductService<Eyecare> _eyecareService;

        private readonly IOrderService _orderService;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, 
            IProductService<Accessory> accessoryService, IProductService<Frame> framesService, 
            IProductService<Glasses> glassesService, IProductService<Lenses> lensesService, 
            IProductService<Eyecare> eyecareService, IOrderService orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accessoryService = accessoryService;
            _framesService = framesService;
            _glassesService = glassesService;
            _lensesService = lensesService;
            _eyecareService = eyecareService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAccessories()
        {
            var prods = _accessoryService.GetProducts(i => i.ProductId >= 0);

            return View(prods);
        }

        public IActionResult GetFrames()
        {
            var prods = _framesService.GetProducts(i => i.ProductId >= 0);

            return View(prods);
        }

        public IActionResult GetGlasses()
        {
            var prods = _glassesService.GetProducts(i => i.ProductId >= 0);

            return View(prods);
        }

        public IActionResult GetLenses()
        {
            var prods = _lensesService.GetProducts(i => i.ProductId >= 0);

            return View(prods);
        }

        public IActionResult GetEyecare()
        {
            var prods = _eyecareService.GetProducts(i => i.ProductId >= 0);

            return View(prods);
        }

        [HttpGet]
        public IActionResult FramesDetails(int id)
        {
            var frame = _framesService.GetProductByID(id);

            return View(frame);
        }

        [HttpPost]
        public async Task<IActionResult> FramesDetails(Frame frame)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(claim.Value);
            var order = _orderService.FirstOrDefault(o => o.UserId == user.Id && o.Status == OrderStatus.Cart);

            if (order is null)
            {
                _orderService.AddOrder(new Order 
                {
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Status = OrderStatus.Cart,
                });

                var orderFromDb = _orderService.FirstOrDefault(o => o.UserId == user.Id && o.Status == OrderStatus.Cart);

                _orderService.AddProductToOrder(new OrderProduct
                {
                    ProductId = frame.ProductId,
                    OrderId = orderFromDb.OrderId,
                    Amount = 1,
                });
            }
            else
            {
                var orderProduct = order.OrderProducts.FirstOrDefault(o => o.ProductId == frame.ProductId);

                if (orderProduct is null)
                {
                    _orderService.AddProductToOrder(new OrderProduct
                    {
                        ProductId = frame.ProductId,
                        OrderId = order.OrderId,
                        Amount = 1,
                    });
                }
                else
                {
                    orderProduct.Amount += 1;
                }

                _orderService.UpdateProductInOrder(orderProduct);
            }



            return View(frame);
        }

        [HttpGet]
        public IActionResult AccessoriesDetails(int id)
        {
            var accessory = _accessoryService.GetProductByID(id);

            return View(accessory);
        }

        [HttpGet]
        public IActionResult EyecareDetails(int id)
        {
            var eyecare = _eyecareService.GetProductByID(id);

            return View(eyecare);
        }

        [HttpGet]
        public IActionResult GlassesDetails(int id)
        {
            var glasses = _glassesService.GetProductByID(id);

            return View(glasses);
        }

        [HttpGet]
        public IActionResult LensesDetails(int id)
        {
            var lenses = _lensesService.GetProductByID(id);

            return View(lenses);
        }

        public async Task<IActionResult> Profile()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(claim.Value);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileEdit()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(claim.Value);
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Lastname = user.Lastname,
                Address = user.Address,
                Email = user.Email,
                ImageUrl = user.Receipt,
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(UserViewModel userViewModel)
        {
            var userFromDb = await _userManager.FindByIdAsync(userViewModel.Id);
            userFromDb.Name = userViewModel.Name;
            userFromDb.Surname = userViewModel.Surname;
            userFromDb.Lastname = userViewModel.Lastname;
            userFromDb.Address = userViewModel.Address;
            userFromDb.Email = userViewModel.Email;
            userFromDb.Receipt = userViewModel.ImageUrl;
            await _userManager.UpdateAsync(userFromDb);
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
