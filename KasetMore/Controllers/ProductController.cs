using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KasetMore.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productRepository.GetProducts());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                return Ok(await _productRepository.GetProductById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("get-by-email")]
        public async Task<IActionResult> GetProductByEmail(string email)
        {
            try
            {
                return Ok(await _productRepository.GetProductByEmail(email));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("get-by-category")]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            try
            {
                return Ok(await _productRepository.GetProductByCategory(category));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("update-product")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                await _productRepository.UpdateProduct(product);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(ProductModel product, [FromForm] List<IFormFile> images)
        {
            try
            {
                await _productRepository.AddProduct(product, images);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("delete-product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("delete-product-images")]
        public async Task<IActionResult> DeleteProductImages(int[] ids)
        {
            try
            {
                await _productRepository.DeleteProductImages(ids);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
