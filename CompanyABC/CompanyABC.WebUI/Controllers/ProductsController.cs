using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyABC.Domain.Repositories;
using CompanyABC.Domain.Entities;
using CompanyABC.WebUI.Preferences;
using CompanyABC.WebUI.Models;
using System.Net;
using CompanyABC.WebUI.Localization;
using PagedList;
using CompanyABC.Domain.Search;
using BootstrapSupport;

namespace CompanyABC.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserPreferenceService _userPreferenceService;
        private readonly ILocalizedMessageService _messageService;
        private readonly IProductSearchService _searchService;

        public ProductsController(IProductRepository productRepo, IUserPreferenceService userPrefService, ILocalizedMessageService messageService)
        {
            this._productRepository = productRepo;
            this._userPreferenceService = userPrefService;
            this._messageService = messageService;
            this._searchService = new ProductSearchService(_productRepository);
        }

        public ViewResult List(string search, int page = 1)
        {
            var products = _productRepository.Products;;

            if (!string.IsNullOrEmpty(search))
                products = _searchService.Search(search);

            var pageOfProducts = products.OrderBy(product => product.ABCID).ToPagedList(page, _userPreferenceService.Preferences.ProductsPerPage);

            return View(new ProductsViewModel()
            {
                Products = pageOfProducts
            });
        }

        public ActionResult Details(Guid id)
        {
            Product productToView = _productRepository.Products
                .Where(product => product.ABCID == id)
                .FirstOrDefault();

            if (productToView == null)
                return HttpNotFound();

            return View(productToView);
        }

        public ViewResult Create()
        {

            ViewBag.Title = "CREATE PRODUCT";
            return View("Edit", new Product() { DateCreated = DateTime.Now });
        }

        public ViewResult Edit(Guid id)
        {
            ViewBag.Title = "EDIT PRODUCT";
            Product productToEdit = _productRepository.Products.FirstOrDefault(product => product.ABCID == id);

            return View(productToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _productRepository.SaveProduct(product);
            TempData[Alerts.SUCCESS] = _messageService.ProductSaved;

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Product deletedProduct = _productRepository.DeleteProduct(id);

            if (deletedProduct != null)
                TempData[Alerts.SUCCESS] = _messageService.ProductDeleted;

            return RedirectToAction("List");
        }
    }
}
