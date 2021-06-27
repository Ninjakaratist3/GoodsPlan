using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using GoodsPlan.EmailSender.Services;
using GoodsPlan.EmailSender;
using GoodsPlan.Infrastructure.Modules;

namespace CMSever.Module.Vendors
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
