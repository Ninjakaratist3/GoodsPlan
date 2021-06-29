using GoodsPlan.Core.Models;
using GoodsPlan.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GoodsPlan.Core.Areas.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("user")]
        public IActionResult Index()
        {
            var userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
            {
                return Redirect("/login");
            }

            var user = _userRepository.Query()
                .Where(p => p.Email == userEmail)
                .FirstOrDefault();

            return View(user);
        }
    }
}
