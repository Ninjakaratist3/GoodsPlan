using GoodsPlan.Core.Models;
using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Warehouses.Areas.ViewModels.Warehouse;
using GoodsPlan.Warehouses.Models;
using GoodsPlan.Warehouses.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GoodsPlan.Warehouses.Areas.Controllers
{
    [Route("warehouses")]
    public class WarehouseController : Controller
    {
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IRepository<Warehouse> warehouseRepository,
            IRepository<User> userRepository,
            IWarehouseService warehouseService)
        {
            _warehouseRepository = warehouseRepository;
            _userRepository = userRepository;
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var warehouses = _warehouseRepository.Query()
                .Select(p => new WarehouseViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Phone = p.Phone,
                    Address = p.Address
                })
                .ToList();

            return View(warehouses);
        }

        [HttpGet("{id}")]
        public IActionResult WarehouseDetails(long id)
        {
            var warehouse = _warehouseRepository.Query()
                .Where(p => p.Id == id)
                .Select(p => new WarehouseViewModel
                {
                    Name = p.Name,
                    Phone = p.Phone,
                    Address = p.Address,
                    SizeInCubicMeters = p.SizeInCubicMeters
                })
                .FirstOrDefault();

            return View(warehouse);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("create")]
        public IActionResult Create(WarehouseForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var warehouse = _warehouseService.ConvertWarehouseFormToWarehouse(model);

            var userEmail = HttpContext.User.Identity.Name;

            if (userEmail == null)
            {
                return Redirect("/login");
            }

            warehouse.UserId = _userRepository.Query()
                .Where(p => p.Email == userEmail)
                .FirstOrDefault().Id;

            _warehouseRepository.Add(warehouse);
            _warehouseRepository.SaveChanges();

            return Redirect("/warehouses");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Update(long id)
        {
            var warehouse = _warehouseRepository.Query()
                .Where(p => p.Id == id)
                .Select(p => new WarehouseForm
                {
                    Name = p.Name,
                    Phone = p.Phone,
                    Address = p.Address,
                    SizeInCubicMeters = p.SizeInCubicMeters
                })
                .FirstOrDefault();

            return View(warehouse);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit/{id}")]
        public IActionResult Update(long id, WarehouseForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var warehouse = _warehouseRepository.Get(id);

            _warehouseService.UpdateWarehouse(warehouse, model);
            _warehouseRepository.SaveChanges();

            return Redirect("/warehouses");
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _warehouseRepository.Delete(id);
            _warehouseRepository.SaveChanges();

            return Redirect("/warehouses");
        }
    }
}
