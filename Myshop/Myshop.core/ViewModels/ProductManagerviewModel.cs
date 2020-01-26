using Myshop.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myshop.core.ViewModels
{
    public class ProductManagerviewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
