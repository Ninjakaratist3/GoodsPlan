using GoodsPlan.EmailSender.Models;
using GoodsPlan.Infrastructure.Data;
using System.Linq;

namespace GoodsPlan.EmailSender.Services
{
    public class SmtpConfigurationService : ISmtpConfigurationService
    {
        private readonly IRepository<SmtpConfiguration> _smtpConfigurationRepository;

        public SmtpConfigurationService(IRepository<SmtpConfiguration> smtpConfigurationRepository)
        {
            _smtpConfigurationRepository = smtpConfigurationRepository;
        }

        public SmtpConfiguration GetConfiguration()
        {
            var configuration = _smtpConfigurationRepository.Query().FirstOrDefault();

            return configuration;
        }

        public void UpdateConfiguration(SmtpConfiguration model)
        {
            var configuration = GetConfiguration();

            configuration.Email = model.Email;
            configuration.Host = model.Host;
            configuration.Password = model.Password;
            configuration.Port = model.Port;
            configuration.UseSsl = model.UseSsl;

            _smtpConfigurationRepository.SaveChanges();
        }
    }
}
