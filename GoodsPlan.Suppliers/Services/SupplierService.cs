using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Suppliers.Areas.ViewModels.Supplier;
using GoodsPlan.Suppliers.Models;

namespace GoodsPlan.Suppliers.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public SupplierService(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Supplier ConvertSupplierFormToSupplier(SupplierForm model)
        {
            var supplier = new Supplier()
            {
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address,
                Products = model.Products
            };

            return supplier;
        }

        public void UpdateSupplier(Supplier supplier, SupplierForm model)
        {
            supplier.Name = model.Name;
            supplier.Phone = model.Phone;
            supplier.Email = model.Email;
            supplier.Address = model.Address;
            supplier.Products = model.Products;
        }
    }
}
