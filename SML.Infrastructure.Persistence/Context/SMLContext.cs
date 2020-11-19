using Microsoft.EntityFrameworkCore;
using SML.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SML.Infrastructure.Persistence.Context
{
    public class SMLContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public SMLContext(DbContextOptions<SMLContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
      
            builder.Entity<Product>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);
            base.OnModelCreating(builder);
        }
    }

}
