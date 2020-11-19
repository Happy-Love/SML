using Microsoft.AspNetCore.Mvc;
using SML.Application.Interfaces.Repositories;
using SML.Domain.Common;
using SML.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SML.UI.Controllers.Common
{
    public abstract class BaseController<TEntity,TRepository> : Controller
        where TEntity : BaseEntity
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository repository;
        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ActionResult<IEnumerable<TEntity>>> Index()
        {
            return View(await repository.GetAll());
        }
       

    }
}
