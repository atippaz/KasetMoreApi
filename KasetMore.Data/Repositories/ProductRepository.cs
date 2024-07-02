using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KasetMore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly KasetMoreContext _context;

        public ProductRepository(KasetMoreContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.ProductImages)
                .ToListAsync();
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products
                .Where(p => p.ProductId == id)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Product>> GetProductByEmail(string email)
        {
            return await _context.Products
                .Where(p => p.UserEmail == email)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }
        public async Task<List<Product>> GetProductByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.Category == category)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }
        public async Task AddProduct(ProductModel product, List<IFormFile> images)
        {
            try
            {
                var base64Images = new List<ProductImage>();
                foreach (var image in images)
                {
                    using var stream = new MemoryStream();
                    await image.CopyToAsync(stream);
                    base64Images.Add(new ProductImage
                    {
                        Image = $"data:image / jpeg; base64,{Convert.ToBase64String(stream.ToArray())}"
                    });
                }
                var productToAdd = new Product
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Province = product.Province,
                    Rating = product.Rating,
                    Amount = product.Amount,
                    UserEmail = product.UserEmail,
                    Price = product.Price,
                    Category = product.Category,
                    ProductImages = base64Images,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };
                await _context.AddAsync(productToAdd);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateProduct(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteProduct(int id)
        {
            try
            {
                await _context.Products
                    .Where(p => p.ProductId == id)
                    .ExecuteDeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteProductImages(int[] ids)
        {
            try
            {
                await _context.ProductImages
                    .Where(p => ids.Contains(p.AttatchmentId))
                    .ExecuteDeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
