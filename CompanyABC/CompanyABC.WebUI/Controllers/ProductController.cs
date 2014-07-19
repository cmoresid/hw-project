using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyABC.Domain.Repositories;
using CompanyABC.Domain.Entities;

namespace CompanyABC.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public ViewResult List()
        {
            return View(_productRepository.Products);
        }
    }
}
