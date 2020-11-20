using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SML.Domain;
using SML.Domain.Entities;
using SML.Infrastructure.Identity.Repository;
using SML.UI.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SML.UI.Controllers
{
    public class ProductsController : BaseController<Product, ProductRepository>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductsController(ProductRepository repository, UserManager<ApplicationUser> userManager) : base(repository)
        {
            _userManager = userManager;
        }
        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {
            var allProducts = await repository.GetAll();
            var user = await _userManager.GetUserAsync(User);
            if(user!=null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.IsAdmin = roles.Contains("Admin");
                ViewBag.UserId = user.Id;
            }
            return View(allProducts);
        }
        public IActionResult Create()
        {
            return View();
        }
        public override async Task<IActionResult> Create(Product vm)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var product = new Product()
            {
                Title = vm.Title,
                Description = vm.Description,
                Price = vm.Price,
                UserId = user.Id,
                CreatedBy = user.UserName
            };
            var responseProduct = await repository.Add(product);
            
            if (responseProduct != null)
                return RedirectToAction("Index");
            return Content("Не удалось создать продукт!");
        }

    }
}
