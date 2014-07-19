using System;
using System.Data.Entity;
using CompanyABC.Domain.Entities;

namespace CompanyABC.Domain.EF
{
    public class CompanyABCDbContext : DbContext
    {
        public CompanyABCDbContext()
            : base("CompanyABCDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CompanyABCDbContext>());
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(product => product.ABCID);
            modelBuilder.Entity<Product>().Property(product => product.ABCID).HasColumnName("abcid");
            modelBuilder.Entity<Product>().Property(product => product.Title).HasColumnName("title");
            modelBuilder.Entity<Product>().Property(product => product.Description).HasColumnName("description");
            modelBuilder.Entity<Product>().Property(product => product.Vendor).HasColumnName("vendor");
            modelBuilder.Entity<Product>().Property(product => product.ListPrice).HasColumnName("list_price");
            modelBuilder.Entity<Product>().Property(product => product.Cost).HasColumnName("cost");
            modelBuilder.Entity<Product>().Property(product => product.Status).HasColumnName("status");
            modelBuilder.Entity<Product>().Property(product => product.Location).HasColumnName("location");
            modelBuilder.Entity<Product>().Property(product => product.DateCreated).HasColumnName("date_created");
            modelBuilder.Entity<Product>().Property(product => product.DateReceived).HasColumnName("date_received");
        }
    }
}
