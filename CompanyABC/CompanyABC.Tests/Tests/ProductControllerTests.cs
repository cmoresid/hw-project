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
using CompanyABC.WebUI.Container;
using CompanyABC.WebUI.Localization;

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

            IProductControllerContainer container = new ProductControllerContainer(mockRepository.Object, mockMessageService.Object, mockUserPrefService.Object);

            ProductsController controller = new ProductsController(container);

            ProductsViewModel result = controller.List(2).Model as ProductsViewModel;
            PagingInfo pageInfo = result.PagingInfo;

            Assert.AreEqual(2, pageInfo.CurrentPageNumber);
            Assert.AreEqual(5, pageInfo.ItemsPerPage);
            Assert.AreEqual(8, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
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

            IProductControllerContainer container = new ProductControllerContainer(mockRepository.Object, mockMessageService.Object, mockUserPrefService.Object);

            ProductsController controller = new ProductsController(container);
            Product product = GenerateFakeProducts(1).FirstOrDefault();
            ActionResult result = controller.Edit(product);

            mockRepository.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
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

            IProductControllerContainer container = new ProductControllerContainer(mockRepository.Object, mockMessageService.Object, mockUserPrefService.Object);

            ProductsController controller = new ProductsController(container);
            controller.ModelState.AddModelError("error", "error");
            Product product = GenerateFakeProducts(1).FirstOrDefault();
            ActionResult result = controller.Edit(product);

            // Assert - check that the repository was not called
            mockRepository.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
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
