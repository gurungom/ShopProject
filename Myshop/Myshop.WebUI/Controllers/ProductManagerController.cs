using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.core.Models;
using Myshop.core.ViewModels;
using Myshop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productCategories;
        
        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerviewModel viewModel = new ProductManagerviewModel();
            viewModel.Product = new Product();
            viewModel.productCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product==null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerviewModel viewModel = new ProductManagerviewModel();
                viewModel.Product = product;
                viewModel.productCategories = productCategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product ProductToEdit = context.Find(Id);
            if (ProductToEdit==null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
              
                ProductToEdit.Name = product.Name;
                ProductToEdit.Image = product.Image;
                ProductToEdit.Description = product.Description;
                ProductToEdit.Price = product.Price;
                ProductToEdit.Category = product.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            Product ProductToDelete = context.Find(Id);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product ProductToDelete = context.Find(Id);
            if (ProductToDelete==null)
            {
                return HttpNotFound();
            }
            else
            {
                
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }

        }
    }
}