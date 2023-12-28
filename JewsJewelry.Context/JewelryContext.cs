using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context.Contracts;
using JewsJewelry.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace JewsJewelry.Context
{
    public class JewelryContext : DbContext, IJewelryContext, IDBRead, IDBWriter, IUnitOfWork
    {
        public DbSet<Craftsman> Craftsmen { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Jewelry> Jewelries { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        public JewelryContext(DbContextOptions<JewelryContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof)
        }
        

    }
}