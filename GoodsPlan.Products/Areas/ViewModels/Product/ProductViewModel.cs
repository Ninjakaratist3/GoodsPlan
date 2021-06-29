using GoodsPlan.Core.Models;
using GoodsPlan.Suppliers.Models;
using GoodsPlan.Warehouses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsPlan.Products.Areas.ViewModels.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string MeasureUnit { get; set; }

        public decimal Weight { get; set; }

        public decimal Volume { get; set; }

        public int Quantity { get; set; }

        public Supplier Supplier { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Warehouse Warehouse { get; set; }

        public User User { get; set; }
    }
}
