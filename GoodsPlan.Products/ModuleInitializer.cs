using Microsoft.Extensions.DependencyInjection;
using GoodsPlan.Infrastructure.Modules;
using GoodsPlan.Products.Services;

namespace GoodsPlan.Products
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProductService, ProductService>();
        }
    }
}
