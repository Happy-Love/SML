using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SML.Domain;
using SML.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SML.Infrastructure.Identity.Context
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);
        }
    }
}
