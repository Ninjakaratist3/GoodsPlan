using System.ComponentModel.DataAnnotations;

namespace GoodsPlan.Core.Areas.ViewModels.Account
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
