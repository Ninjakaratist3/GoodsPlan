using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Warehouses.Areas.ViewModels.Warehouse;
using GoodsPlan.Warehouses.Models;

namespace GoodsPlan.Warehouses.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse> _warehouseRepository;

        public WarehouseService(IRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public Warehouse ConvertWarehouseFormToWarehouse(WarehouseForm model)
        {
            var warehouse = new Warehouse()
            {
                Name = model.Name,
                Phone = model.Phone,
                Address = model.Address,
                SizeInCubicMeters = model.SizeInCubicMeters
            };

            return warehouse;
        }

        public void UpdateWarehouse(Warehouse warehouse, WarehouseForm model)
        {
            warehouse.Name = model.Name;
            warehouse.Phone = model.Phone;
            warehouse.Address = model.Address;
            warehouse.SizeInCubicMeters = model.SizeInCubicMeters;
        }
    }
}
