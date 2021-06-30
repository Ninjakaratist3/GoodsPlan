using Microsoft.AspNetCore.Mvc;

namespace GoodsPlan.Core.Areas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/login");
        }
    }
}
