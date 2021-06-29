using GoodsPlan.Suppliers.Models;
using System.Collections.Generic;

namespace GoodsPlan.Suppliers.Areas.ViewModels.Supplier
{
    public class SupplierForm
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<SupplierProduct> Products { get; set; }

        public long UserId { get; set; }
    }
}
