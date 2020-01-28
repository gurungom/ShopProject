using Myshop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.core.ViewModels
{
  public class ProdcutListViewMoel
    {
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
