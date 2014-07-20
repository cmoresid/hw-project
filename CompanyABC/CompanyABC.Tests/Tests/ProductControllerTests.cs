using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyABC.Domain.Entities;
using CompanyABC.Domain.Repositories;
using CompanyABC.Domain.Constants;
using CompanyABC.WebUI.Preferences;
using CompanyABC.WebUI.Controllers;
using CompanyABC.WebUI.Models;
using CompanyABC.WebUI.Localization;
using PagedList;

namespace CompanyABC.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void BasicPaginationTest()
        {
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(mockRepo => mockRepo.Products).Returns(GenerateFakeProducts(8));

            Mock<IUserPreferenceService> mockUserPrefService = new Mock<IUserPreferenceService>();
            mockUserPrefService.Setup(prefService => prefService.Preferences).Returns(new UserPreferenceInfo()
            {
                ProductsPerPage = 5,
                ProductColumnsToDisplay = new List<string>()
            });

            Mock<ILocalizedMessageService> mockMessageService = new Mock<ILocalizedMessageService>();
            mockMessageService.Setup(messageService => messageService.ProductSaved).Returns("{0} was saved.");

            ProductsController controller = new ProductsController(mockRepository.Object, mockUserPrefService.Object, mockMessageService.Object);

            ProductsViewModel result = controller.List(2).Model as ProductsViewModel;

            var pagedList = result.Products;

            Assert.AreEqual(2, pagedList.PageNumber);
            Assert.AreEqual(5, pagedList.PageSize);
            Assert.AreEqual(8, pagedList.TotalItemCount);
            Assert.AreEqual(2, pagedList.PageCount);
        }

        [TestMethod]
        public void SaveValidEditsTest()
        {
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            Mock<IUserPreferenceService> mockUserPrefService = new Mock<IUserPreferenceService>();
            mockUserPrefService.Setup(prefService => prefService.Preferences).Returns(new UserPreferenceInfo()
            {
                ProductsPerPage = 5,
                ProductColumnsToDisplay = new List<string>()
            });

            Mock<ILocalizedMessageService> mockMessageService = new Mock<ILocalizedMessageService>();
            mockMessageService.Setup(messageService => messageService.ProductSaved).Returns("{0} was saved.");

            ProductsController controller = new ProductsController(mockRepository.Object, mockUserPrefService.Object, mockMessageService.Object);
            Product product = GenerateFakeProducts(1).FirstOrDefault();
            ActionResult result = controller.Edit(product);

            mockRepository.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void SaveInvalidChangesToProductTest()
        {
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            Mock<IUserPreferenceService> mockUserPrefService = new Mock<IUserPreferenceService>();
            mockUserPrefService.Setup(prefService => prefService.Preferences).Returns(new UserPreferenceInfo()
            {
                ProductsPerPage = 5,
                ProductColumnsToDisplay = new List<string>()
            });

            Mock<ILocalizedMessageService> mockMessageService = new Mock<ILocalizedMessageService>();
            mockMessageService.Setup(messageService => messageService.ProductSaved).Returns("{0} was saved.");

            ProductsController controller = new ProductsController(mockRepository.Object, mockUserPrefService.Object, mockMessageService.Object);
            controller.ModelState.AddModelError("error", "error");
            Product product = GenerateFakeProducts(1).FirstOrDefault();
            ActionResult result = controller.Edit(product);

            // Assert - check that the repository was not called
            mockRepository.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

       [TestMethod]
        public void DeleteValidProductsTest()
        {
            Guid productID = Guid.NewGuid();
            Product prod = new Product { ABCID = productID, Title = "Product 1" };

            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(m => m.Products).Returns(new Product[] {
                prod,
                new Product { ABCID = Guid.NewGuid(), Title = "Product 2" },
                new Product { ABCID = Guid.NewGuid(), Title = "Product 3" },
            }.AsQueryable());

            Mock<IUserPreferenceService> mockUserPrefService = new Mock<IUserPreferenceService>();
            mockUserPrefService.Setup(prefService => prefService.Preferences).Returns(new UserPreferenceInfo()
            {
                ProductsPerPage = 5,
                ProductColumnsToDisplay = new List<string>()
            });

            Mock<ILocalizedMessageService> mockMessageService = new Mock<ILocalizedMessageService>();
            mockMessageService.Setup(messageService => messageService.ProductSaved).Returns("{0} was deleted.");

            ProductsController controller = new ProductsController(mockRepository.Object, mockUserPrefService.Object, mockMessageService.Object);
            //controller.Delete(prod.ABCID);

            mockRepository.Verify(m => m.DeleteProduct(prod.ABCID));
        }

        private IQueryable<Product> GenerateFakeProducts(int size)
        {
            List<Product> products = new List<Product>();

            for (int i = 0; i < size; i++)
                products.Add(new Product() { ABCID = Guid.NewGuid(), Title = "Product " + i });

            return products.AsQueryable();
        }
    }
}
