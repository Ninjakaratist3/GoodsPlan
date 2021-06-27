using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace GoodsPlan.Infrastructure.Modules
{
    public interface IModuleInitializer
    {
        void ConfigureServices(IServiceCollection serviceCollection);
    }
}
