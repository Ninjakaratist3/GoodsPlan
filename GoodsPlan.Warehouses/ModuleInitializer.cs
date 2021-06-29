using Microsoft.Extensions.DependencyInjection;
using GoodsPlan.Infrastructure.Modules;
using GoodsPlan.Warehouses.Services;

namespace GoodsPlan.Warehouses
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IWarehouseService, WarehouseService>();
        }
    }
}
