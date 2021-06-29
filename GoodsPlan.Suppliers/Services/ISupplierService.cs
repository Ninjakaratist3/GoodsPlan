using GoodsPlan.Suppliers.Areas.ViewModels.Supplier;
using GoodsPlan.Suppliers.Models;

namespace GoodsPlan.Suppliers.Services
{
    public interface ISupplierService
    {
        public Supplier ConvertSupplierFormToSupplier(SupplierForm model);

        public void UpdateSupplier(Supplier supplier, SupplierForm model);
    }
}
