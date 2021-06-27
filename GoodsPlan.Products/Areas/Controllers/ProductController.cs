using GoodsPlan.Products.Areas.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodsPlan.Products.Areas.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult ProductDetails(long id)
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("create")]
        public IActionResult Create(ProductForm model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("edit")]
        public IActionResult Update(long id)
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPut("edit")]
        public IActionResult Update(long id, ProductForm model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
