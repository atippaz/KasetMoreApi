using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using Microsoft.AspNetCore.Http;

namespace KasetMore.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(ProductModel product, List<IFormFile> images);
        Task DeleteProduct(int id);
        Task<Product?> GetProductById(int id);
        Task<List<Product>> GetProductByEmail(string email);
        Task<List<Product>> GetProductByCategory(string category);
        Task<List<Product>> GetProducts();
        Task UpdateProduct(Product product);
        Task DeleteProductImages(int[] ids);
    }
}