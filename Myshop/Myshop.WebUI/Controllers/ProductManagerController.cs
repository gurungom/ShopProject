using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.core.Contracts;
using Myshop.core.Models;
using Myshop.core.ViewModels;
using Myshop.DataAccess.InMemory;

namespace Myshop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        
        public ProductManagerController(IRepository<Product> context, IRepository<ProductCategory> productCategories)
        {
            this.context = context;
            this.productCategories = productCategories;
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
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file!=null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//content//ProductImages//")+product.Image);
                }
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
        public ActionResult Edit(Product product,string Id,HttpPostedFileBase file)
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
                if (file!=null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                ProductToEdit.Name = product.Name;
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