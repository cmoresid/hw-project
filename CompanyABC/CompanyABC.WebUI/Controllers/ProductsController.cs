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
using CompanyABC.WebUI.Container;

namespace CompanyABC.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;
        private IUserPreferenceService _userPreferenceService;
        private ILocalizedMessageService _messageService;

        public ProductsController(IProductControllerContainer container)
        {
            this._productRepository = container.ProductRepository;
            this._userPreferenceService = container.UserPreferenceService;
            this._messageService = container.LocalizationMessageService;
        }

        public ViewResult List(int page = 1)
        {
            var products = _productRepository.Products
                .OrderBy(product => product.ABCID)
                .Skip((page - 1) * this._userPreferenceService.Preferences.ProductsPerPage)
                .Take(this._userPreferenceService.Preferences.ProductsPerPage);

            PagingInfo pagingInfo = new PagingInfo()
            {
                CurrentPageNumber = page,
                ItemsPerPage = this._userPreferenceService.Preferences.ProductsPerPage,
                TotalItems = _productRepository.Products.Count()
            };

            return View(new ProductsViewModel()
            {
                PagingInfo = pagingInfo,
                Products = products
            });
        }

        public ActionResult Details(string id)
        {
            Guid productId;

            if (id == null || !Guid.TryParse(id, out productId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product productToView = _productRepository.Products
                .Where(product => product.ABCID == productId)
                .FirstOrDefault();

            if (productToView == null)
            {
                return HttpNotFound();
            }

            return View(productToView);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _productRepository.SaveProduct(product);
            TempData["resultMessage"] = string.Format(_messageService.ProductSaved, product.Title);

            return RedirectToAction("List");
        }
    }
}
