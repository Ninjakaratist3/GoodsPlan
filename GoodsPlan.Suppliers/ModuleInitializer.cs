using Microsoft.Extensions.DependencyInjection;
using GoodsPlan.Infrastructure.Modules;
using GoodsPlan.Suppliers.Services;

namespace GoodsPlan.Suppliers
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISupplierService, SupplierService>();
        }
    }
}
