using System.Collections.Generic;
using CompanyABC.Domain.Entities;
using PagedList;

namespace CompanyABC.WebUI.Models
{
    public class ProductsViewModel
    {
        public IPagedList<Product> Products;
    }
}