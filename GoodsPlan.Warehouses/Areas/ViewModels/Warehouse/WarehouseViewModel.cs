using GoodsPlan.Core.Models;

namespace GoodsPlan.Warehouses.Areas.ViewModels.Warehouse
{
    public class WarehouseViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal SizeInCubicMeters { get; set; }

        public User User { get; set; }
    }
}
