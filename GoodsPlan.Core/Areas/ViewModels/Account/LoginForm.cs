using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine.ViewModels.Account
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
