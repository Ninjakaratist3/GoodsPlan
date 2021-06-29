using GoodsPlan.Core.Areas.ViewModels.Account;
using GoodsPlan.Core.Models;
using GoodsPlan.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoodsPlan.Core.Areas.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public AccountController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegistrationForm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = await _userRepository.Query().FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                return View();
            }

            user = model.ConvertToUser();

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            await Authenticate(user);

            return Redirect("/user");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginForm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = await _userRepository.Query()
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null && CheckedPassword(user, model.Password))
            {
                await Authenticate(user);
                return Redirect("/user");
            }
            else
            {
                return View();
            }
        }

        private bool CheckedPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "UserCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        //[ValidateAntiForgeryToken]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }
    }
}
