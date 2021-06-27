using Microsoft.Extensions.DependencyInjection;
using GoodsPlan.EmailSender.Services;
using GoodsPlan.Infrastructure.Modules;

namespace GoodsPlan.EmailSender
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<ISmtpConfigurationService, SmtpConfigurationService>();
        }
    }
}
