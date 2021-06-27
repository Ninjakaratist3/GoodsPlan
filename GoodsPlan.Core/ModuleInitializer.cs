using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using GoodsPlan.Infrastructure.Modules;

namespace CMSever.Module.Vendors
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddTransient<IVendorService, VendorService>();

        }
    }
}
