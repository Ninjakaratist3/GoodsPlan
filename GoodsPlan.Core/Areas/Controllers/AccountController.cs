using GoodsPlan.Core.Models;
using GoodsPlan.EmailSender;
using GoodsPlan.EmailSender.TemplateModels;
using GoodsPlan.Infrastructure.Data;
using Medicine.ViewModels.Account;
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
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IEmailSender _emailSender;

        public AccountController(IRepository<User> userRepository,
            IRepository<UserRole> userRoleRepository,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _emailSender = emailSender;
        }

        [HttpGet("registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegisterForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = await _userRepository.Query().FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                return BadRequest();
            }

            user = model.ConvertToUser();

            UserRole userRole = await _userRoleRepository.Query().FirstOrDefaultAsync(r => r.Name == "user");
            if (userRole != null)
            {
                user.Role = userRole;
            }

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            SendRegistrationEmailMessage(user);

            await Authenticate(user);

            return Ok();
        }

        private async void SendRegistrationEmailMessage(User user)
        {
            var registrationEmailMessage = new RegistrationTemplateModel()
            {
                UserName = user.Name,
                UserEmail = user.Email,
                Subject = "Регистрация"
            };
            await _emailSender.SendAsync(registrationEmailMessage);
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
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = await _userRepository.Query()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null && CheckedPassword(user, model.Password))
            {
                await Authenticate(user);
                return Ok();
            }
            else
            {
                return BadRequest();
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "UserCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        //[ValidateAntiForgeryToken]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
