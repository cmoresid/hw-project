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

        public void CreateProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product productToUpdate)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product productToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
