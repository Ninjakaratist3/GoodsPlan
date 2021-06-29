using GoodsPlan.Warehouses.Areas.ViewModels.Warehouse;
using GoodsPlan.Warehouses.Models;

namespace GoodsPlan.Warehouses.Services
{
    public interface IWarehouseService
    {
        public Warehouse ConvertWarehouseFormToWarehouse(WarehouseForm model);

        public void UpdateWarehouse(Warehouse warehouse, WarehouseForm model);
    }
}
