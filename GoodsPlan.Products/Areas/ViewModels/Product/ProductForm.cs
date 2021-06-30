using GoodsPlan.Suppliers.Models;
using GoodsPlan.Warehouses.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsPlan.Products.Areas.ViewModels.Product
{
    public class ProductForm
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string MeasureUnit { get; set; }

        public decimal Weight { get; set; }

        public decimal Volume { get; set; }

        public int Quantity { get; set; }

        public long SupplierId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public long WarehouseId { get; set; }

        public IList<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        public IList<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}
