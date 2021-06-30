using GoodsPlan.Core.Models;
using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Products.Areas.ViewModels.Product;
using GoodsPlan.Products.Models;
using GoodsPlan.Products.Services;
using GoodsPlan.Suppliers.Models;
using GoodsPlan.Warehouses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodsPlan.Products.Areas.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<SupplierProduct> _supplierProductRepository;
        private readonly IProductService _productService;

        public ProductController(IRepository<Product> productRepository,
            IRepository<User> userRepository,
            IRepository<Warehouse> warehouseRepository,
            IRepository<Supplier> supplierRepository,
            IRepository<SupplierProduct> supplierProductRepository,
            IProductService productService)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _warehouseRepository = warehouseRepository;
            _supplierRepository = supplierRepository;
            _supplierProductRepository = supplierProductRepository;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.Query()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    MeasureUnit = p.MeasureUnit,
                    Quantity = p.Quantity,
                    Price = p.Price
                })
                .ToList();

            return View(products);
        }

        [HttpGet("{id}")]
        public IActionResult ProductDetails(long id)
        {
            var product = _productRepository.Query()
                .Where(p => p.Id == id)
                .Select(p => new ProductViewModel 
                {
                    Name = p.Name,
                    Code = p.Code,
                    MeasureUnit = p.MeasureUnit,
                    Weight = p.Weight,
                    Volume = p.Volume,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Description = p.Description,
                    Warehouse = p.Warehouse,
                    Supplier = p.Supplier
                })
                .FirstOrDefault();

            return View(product);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var model = new ProductForm() 
            { 
                Suppliers = _supplierRepository.Query().ToList(),
                Warehouses = _warehouseRepository.Query().ToList()
            };

            return View(model);
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

            var userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
            {
                return Redirect("/login");
            }

            product.UserId = _userRepository.Query()
                .Where(p => p.Email == userEmail)
                .FirstOrDefault().Id;

            AddProductToSupplier(product);

            _productRepository.Add(product);
            _productRepository.SaveChanges();

            return Redirect("/products");
        }

        private void AddProductToSupplier(Product product)
        {
            var supplierProduct = new SupplierProduct()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductId = product.Id
            };

            product.Supplier.Products.Add(supplierProduct);

            _supplierProductRepository.Add(supplierProduct);
            _supplierProductRepository.SaveChanges();
            _supplierRepository.SaveChanges();
        }

        [HttpGet("edit/{id}")]
        public IActionResult Update(long id)
        {
            var product = _productRepository.Query()
                .Where(p => p.Id == id)
                .Select(p => new ProductForm
                {
                    Name = p.Name,
                    Code = p.Code,
                    MeasureUnit = p.MeasureUnit,
                    Weight = p.Weight,
                    Volume = p.Volume,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Description = p.Description,
                    WarehouseId = p.Warehouse.Id,
                    SupplierId = p.Supplier.Id,
                    Suppliers = _supplierRepository.Query().ToList(),
                    Warehouses = _warehouseRepository.Query().ToList()
                })
                .FirstOrDefault();

            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit/{id}")]
        public IActionResult Update(long id, ProductForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = _productRepository.Get(id);

            _productService.UpdateProduct(product, model);
            _productRepository.SaveChanges();

            return Redirect("/products");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _productRepository.Delete(id);
            _productRepository.SaveChanges();

            return Redirect("/products");
        }
    }
}
