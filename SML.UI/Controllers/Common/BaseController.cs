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

    }
}
