using GoodsPlan.Products.Areas.ViewModels.Product;
using GoodsPlan.Products.Models;

namespace GoodsPlan.Products.Services
{
    public class ProductService : IProductService
    {
        public Product ConvertProductFormToProduct(ProductForm model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Sku = model.Sku
            };

            return product;
        }

        public void UpdateProduct(Product product, ProductForm model)
        {
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.Sku = model.Sku;
        }
    }
}
