using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myshop.core.Contracts;
using Myshop.core.Models;
using Myshop.core.ViewModels;
using Myshop.WebUI;
using Myshop.WebUI.Controllers;

namespace Myshop.WebUI.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext,productCategoryContext);

            productContext.Insert(new Product());
            var results = controller.Index() as ViewResult;
            var viewModel = (ProductViewModel)results.ViewData.Model;

            Assert.AreEqual(1, viewModel.Product.Count());

        }

       
    }
}
