using GoodsPlan.EmailSender.Models;

namespace GoodsPlan.EmailSender.Services
{
    public interface ISmtpConfigurationService
    {
        public SmtpConfiguration GetConfiguration();

        public void UpdateConfiguration(SmtpConfiguration model);
    }
}
