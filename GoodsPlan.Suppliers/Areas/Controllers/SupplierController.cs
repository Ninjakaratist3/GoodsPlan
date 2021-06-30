using GoodsPlan.Core.Models;
using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Suppliers.Areas.ViewModels.Supplier;
using GoodsPlan.Suppliers.Models;
using GoodsPlan.Suppliers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodsPlan.Suppliers.Areas.Controllers
{
    [Route("suppliers")]
    public class SupplierController : Controller
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<SupplierProduct> _supplierProductRepository;
        private readonly IRepository<User> _userRepository;
        private readonly ISupplierService _supplierService;

        public SupplierController(IRepository<Supplier> supplierRepository,
            IRepository<SupplierProduct> supplierProductRepository,
            IRepository<User> userRepository,
            ISupplierService supplierService)
        {
            _supplierRepository = supplierRepository;
            _supplierProductRepository = supplierProductRepository;
            _userRepository = userRepository;
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _supplierRepository.Query()
                .Select(p => new SupplierViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Phone = p.Phone,
                    Address = p.Address
                })
                .ToList();

            return View(suppliers);
        }

        [HttpGet("{id}")]
        public IActionResult SupplierDetails(long id)
        {
            var supplier = _supplierRepository.Query()
                .Include(s => s.Products)
                .Where(s => s.Id == id)
                .Select(s => new SupplierViewModel
                {
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email,
                    Address = s.Address,
                    Products = s.Products
                })
                .FirstOrDefault();

            return View(supplier);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("create")]
        public IActionResult Create(SupplierForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var supplier = _supplierService.ConvertSupplierFormToSupplier(model);

            var userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
            {
                return Redirect("/login");
            }

            supplier.UserId = _userRepository.Query()
                .Where(p => p.Email == userEmail)
                .FirstOrDefault().Id;

            _supplierRepository.Add(supplier);
            _supplierRepository.SaveChanges();

            return Redirect("/suppliers");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Update(long id)
        {
            var supplier = _supplierRepository.Query()
                .Where(p => p.Id == id)
                .Select(p => new SupplierForm
                {
                    Name = p.Name,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address,
                    Products = p.Products
                })
                .FirstOrDefault();

            return View(supplier);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit/{id}")]
        public IActionResult Update(long id, SupplierForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var supplier = _supplierRepository.Get(id);

            _supplierService.UpdateSupplier(supplier, model);
            _supplierRepository.SaveChanges();

            return Redirect("/suppliers");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            DeleteSupplierProducts(id);

            _supplierRepository.Delete(id);
            _supplierRepository.SaveChanges();

            return Redirect("/suppliers");
        }

        private void DeleteSupplierProducts(long supplierId)
        {
            var supplier = _supplierRepository.Query()
                .Include(s => s.Products)
                .Where(s => s.Id == supplierId)
                .FirstOrDefault();

            foreach (var supplierProduct in supplier.Products)
            {
                _supplierProductRepository.Delete(supplierProduct.Id);
            }

            _supplierProductRepository.SaveChanges();
        }
    }
}
