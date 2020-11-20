using Microsoft.AspNetCore.Authorization;
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
        
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if(user==null)
                return ReturnToProducts();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product vm)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
                return ReturnToProducts();

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
        public IActionResult ReturnToProducts() {
            return RedirectToAction("Index");
        }

        public async Task<ActionResult<Product>> Details(int? id)
        {
            var product = await repository.Get((int)id);
            if (product == null)
            {
                return Content("Такого продукта не существует");
            }
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index");

            var roles = await _userManager.GetRolesAsync(user);
            
            var product = await repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            if (roles.Contains("Admin"))
            {
                await repository.Delete(id);
            }
            
            if (roles.Contains("User") && product.UserId==user.Id)
            {
                await repository.Delete(id);
            }
         
            return RedirectToAction("Index");
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await repository.Get((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
                return ReturnToProducts();
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return View(entity);
            }

            if (roles.Contains("User") && entity.UserId == user.Id)
                return View(entity);

            return ReturnToProducts();
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Edit(int id, Product entity)
        {
            if (entity.Id != id)
            {
                return NotFound("Id not equal");
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                await repository.Update(entity);
            }
            if (roles.Contains("User") && entity.UserId == user.Id)
                await repository.Update(entity);

            return RedirectToAction("Index");
        }
    }
}
