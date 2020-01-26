﻿using Myshop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = Cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            Cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product ProductToUpdate = products.Find(p => p.Id == product.Id);
            if (ProductToUpdate!=null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (product!=null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product ProductToDelete = products.Find(p => p.Id == Id);
            if (ProductToDelete!=null)
            {
                products.Remove(ProductToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
