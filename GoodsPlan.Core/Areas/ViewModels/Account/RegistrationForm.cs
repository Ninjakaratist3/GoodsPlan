using GoodsPlan.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GoodsPlan.Core.Areas.ViewModels.Account
{
    public class RegistrationForm
    {
        [Required(ErrorMessage = "Укажите ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public User ConvertToUser()
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User();
            user.Email = this.Email;
            user.Name = this.Name;
            user.Password = passwordHasher.HashPassword(user, this.Password);

            return user;
        }
    }
}
