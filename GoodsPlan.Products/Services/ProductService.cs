using GoodsPlan.Core.Models;
using GoodsPlan.Infrastructure.Data;
using GoodsPlan.Products.Areas.ViewModels.Product;
using GoodsPlan.Products.Models;
using GoodsPlan.Suppliers.Models;
using GoodsPlan.Warehouses.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodsPlan.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<User> _userRepository;

        public ProductService(IRepository<Product> productRepository,
            IRepository<Warehouse> warehouseRepository,
            IRepository<Supplier> supplierRepository,
            IRepository<User> userRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _warehouseRepository = warehouseRepository;
            _userRepository = userRepository;
        }

        public Product ConvertProductFormToProduct(ProductForm model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Code = model.Code,
                MeasureUnit = model.MeasureUnit,
                Weight = model.Weight,
                Volume = model.Volume,
                Price = model.Price,
                Description = model.Description,
                Quantity = model.Quantity,
                Supplier = _supplierRepository.Query().Include(s => s.Products).Where(s => s.Id == model.SupplierId).FirstOrDefault(),
                Warehouse = _warehouseRepository.Get(model.WarehouseId)
            };

            return product;
        }

        public void UpdateProduct(Product product, ProductForm model)
        {
            product.Name = model.Name;
            product.Code = model.Code;
            product.MeasureUnit = model.MeasureUnit;
            product.Weight = model.Weight;
            product.Volume = model.Volume;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Quantity = model.Quantity;
            product.Supplier = _supplierRepository.Get(model.SupplierId);
            product.Warehouse = _warehouseRepository.Get(model.WarehouseId);
        }
    }
}
