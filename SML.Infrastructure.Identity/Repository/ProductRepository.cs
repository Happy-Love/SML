using SML.Application.Interfaces.Repositories;
using SML.Domain;
using SML.Infrastructure.Identity.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SML.Infrastructure.Identity.Repository
{
    public class ProductRepository:GenericRepository<Product,IdentityContext>, IProductRepository
    {
        public ProductRepository(IdentityContext context)
            : base(context)
        {

        }
    }
}
