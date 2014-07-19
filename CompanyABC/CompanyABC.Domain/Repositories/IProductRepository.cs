using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyABC.Domain.Entities;

namespace CompanyABC.Domain.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product productToSave);
        Product DeleteProduct(Guid productToDelete);
    }
}
