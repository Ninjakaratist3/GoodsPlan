using GoodsPlan.Infrastructure.Models.Base;
using System.Collections.Generic;

namespace GoodsPlan.Suppliers.Models
{
    public class Supplier : EntityBase
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<SupplierProduct> Products { get; set; }

        public long UserId { get; set; }
    }
}
