using GoodsPlan.Core.Models;
using GoodsPlan.Suppliers.Models;
using System.Collections.Generic;

namespace GoodsPlan.Suppliers.Areas.ViewModels.Supplier
{
    public class SupplierViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<SupplierProduct> Products { get; set; }

        public User User { get; set; }
    }
}
