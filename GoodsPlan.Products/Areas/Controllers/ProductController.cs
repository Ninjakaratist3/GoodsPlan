using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Products.Areas.ViewModels.Product;
using GoodsPlan.Products.Models;
using GoodsPlan.Products.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GoodsPlan.Products.Areas.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IProductService _productService;

        public ProductController(IRepository<Product> productRepository, IProductService productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.Query().ToList();

            return View(products);
        }

        [HttpGet("{id}")]
        public IActionResult ProductDetails(long id)
        {
            var product = _productRepository.Query().Where(p => p.Id == id).FirstOrDefault();

            return View(product);
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = _productService.ConvertProductFormToProduct(model);

            _productRepository.Add(product);

            return Ok();
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = _productRepository.Get(id);

            _productService.UpdateProduct(product, model);

            return Ok();
        }

        [ValidateAntiForgeryToken]
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _productRepository.Delete(id);

            return Ok();
        }
    }
}
