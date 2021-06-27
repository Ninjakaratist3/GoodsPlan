using GoodsPlan.Infrastructure.Models.Base;

namespace GoodsPlan.EmailSender.Models
{
    public class SmtpConfiguration : EntityBase
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public bool UseSsl { get; set; }

        public int Port { get; set; }
    }
}
