using GoodsPlan.Infrastructure.Models.Base;
using GoodsPlan.Suppliers.Models;
using GoodsPlan.Warehouses.Models;

namespace GoodsPlan.Products.Models
{
    public class Product : EntityBase
    {
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

        public long UserId { get; set; }
    }
}
