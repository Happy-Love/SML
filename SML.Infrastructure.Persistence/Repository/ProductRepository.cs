using SML.Application.Interfaces.Repositories;
using SML.Domain;
using SML.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SML.Infrastructure.Persistence.Repository
{
    public class ProductRepository:GenericRepository<Product,SMLContext>, IProductRepository
    {
        public ProductRepository(SMLContext context)
            : base(context)
        {

        }
    }
}
