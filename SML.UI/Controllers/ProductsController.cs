using SML.Domain;
using SML.Infrastructure.Persistence.Repository;
using SML.UI.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SML.UI.Controllers
{
    public class ProductsController : BaseController<Product, ProductRepository>
    {
        public ProductsController(ProductRepository repository) : base(repository)
        {
        }
        
    }
}
