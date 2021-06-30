using GoodsPlan.Infrastructure.Models.Base;

namespace GoodsPlan.Suppliers.Models
{
    public class SupplierProduct : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public long ProductId { get; set; }
    }
}
