using GoodsPlan.Infrastructure.Models.Base;

namespace GoodsPlan.Products.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Sku { get; set; }
    }
}
