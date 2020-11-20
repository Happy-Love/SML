using Microsoft.AspNetCore.Mvc;
using SML.Application.Interfaces.Repositories;
using SML.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SML.UI.Controllers.Common
{
    public abstract class BaseController<TEntity, TRepository> : Controller
        where TEntity : BaseEntity
        where TRepository : IRepository<TEntity>
    {

        protected readonly TRepository repository;
        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntity vm) {
            return Content("CreateBase");
        }
        public async Task<ActionResult<TEntity>> Details(int? id)
        {
            var product = await repository.Get((int)id);
            if (product == null)
            {
                return Content("Такого продукта не существует");
            }
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            await repository.Delete(id);
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
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<TEntity>> Edit(int id, TEntity entity)
        {
            if (entity.Id != id)
            {
                return NotFound("Id not equal");
            }
            await repository.Update(entity);
            return RedirectToAction("Index");
        }
    }
}
