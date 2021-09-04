using Cheeseria.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeseria.API.Data
{
    public class CheeseContext : DbContext
    {
        public CheeseContext(DbContextOptions<CheeseContext> options) : base(options)
        {
        }

        public DbSet<Cheese> Cheeses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CheeseEntityTypeConfiguration());
        }
        public class CheeseContextDesignFactory : IDesignTimeDbContextFactory<CheeseContext>
        {
            public CheeseContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CheeseContext>()
                    .UseSqlServer("Server=.;Initial Catalog=CheeseDb;Integrated Security=true");

                return new CheeseContext(optionsBuilder.Options);
            }
        }
    }
}