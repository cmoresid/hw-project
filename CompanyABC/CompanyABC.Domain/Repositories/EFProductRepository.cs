using System;
using System.Linq;
using CompanyABC.Domain.EF;
using CompanyABC.Domain.Entities;

namespace CompanyABC.Domain.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private CompanyABCDbContext _context = new CompanyABCDbContext();

        public IQueryable<Product> Products
        {
            get { return _context.Products; }
        }

        public void SaveProduct(Product productToSave)
        {
            if (productToSave.ABCID == Guid.Empty)
            {
                productToSave.ABCID = Guid.NewGuid();
                productToSave.DateCreated = DateTime.Now;

                _context.Products.Add(productToSave);
            }
            else
            {
                Product dbProduct = _context.Products.Find(productToSave.ABCID);

                if (dbProduct != null)
                {
                    dbProduct.Cost = productToSave.Cost;
                    dbProduct.DateReceived = productToSave.DateReceived;
                    dbProduct.Description = productToSave.Description;
                    dbProduct.ListPrice = productToSave.ListPrice;
                    dbProduct.Location = productToSave.Location;
                    dbProduct.Status = productToSave.Status;
                    dbProduct.Title = productToSave.Title;
                    dbProduct.Vendor = productToSave.Vendor;
                }
            }

            _context.SaveChanges();
        }

        public Product DeleteProduct(Guid productToDelete)
        {
            Product dbProduct = _context.Products.Find(productToDelete);

            if (dbProduct == null)
            {
                return null;
            }

            _context.Products.Remove(dbProduct);
            _context.SaveChanges();

            return dbProduct;
        }
    }
}
