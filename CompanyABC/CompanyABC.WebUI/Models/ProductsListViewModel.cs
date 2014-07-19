using System.Collections.Generic;
using CompanyABC.Domain.Entities;

namespace CompanyABC.WebUI.Models
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}