using GoodsPlan.Products.Areas.ViewModels.Product;
using GoodsPlan.Products.Models;

namespace GoodsPlan.Products.Services
{
    public interface IProductService
    {
        public Product ConvertProductFormToProduct(ProductForm model);

        public void UpdateProduct(Product product, ProductForm model);
    }
}
