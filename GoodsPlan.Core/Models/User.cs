using GoodsPlan.Infrastructure.Models.Base;

namespace GoodsPlan.Core.Models
{
    public class User : EntityBase
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
    }
}
