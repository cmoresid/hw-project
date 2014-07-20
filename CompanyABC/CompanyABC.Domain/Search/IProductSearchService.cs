using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyABC.Domain.Entities;
using CompanyABC.Domain.Repositories;

namespace CompanyABC.Domain.Search
{
    public interface IProductSearchService
    {
        IQueryable<Product> Search(string searchQuery);
    }
}
