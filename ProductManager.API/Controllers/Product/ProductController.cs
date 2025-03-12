using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManager.API.DTOS.Product;
using ProductManager.API.Entities;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return Ok(new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId
            });
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = createDto.Name,
                Price = createDto.Price,
                Stock = createDto.Stock,
                SupplierId = createDto.SupplierId,
                CategoryId = createDto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updateDto.Name;
            product.Price = updateDto.Price;
            product.Stock = updateDto.Stock;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("product-stats")]
        public async Task<ActionResult> GetProductStats()
        {
            var products = await _context.Products.ToListAsync();

            if (!products.Any())
            {
                return NotFound("No products found.");
            }

            var maxPriceProduct = products.OrderByDescending(p => p.Price).FirstOrDefault();
            var minPriceProduct = products.OrderBy(p => p.Price).FirstOrDefault();
            var totalPrice = products.Sum(p => p.Price);
            var averagePrice = products.Average(p => p.Price);

            var result = new
            {
                MaxPriceProduct = maxPriceProduct != null ? new
                {
                    maxPriceProduct.Name,
                    maxPriceProduct.Price
                } : null,
                MinPriceProduct = minPriceProduct != null ? new
                {
                    minPriceProduct.Name,
                    minPriceProduct.Price
                } : null,
                TotalPrice = totalPrice,
                AveragePrice = averagePrice
            };

            return Ok(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound($"No products found for category {categoryId}");
            }

            return Ok(products);
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsBySupplier(int supplierId)
        {
            var products = await _context.Products
                .Where(p => p.SupplierId == supplierId)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound($"No products found for supplier {supplierId}");
            }

            return Ok(products);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalProductCount()
        {
            var productCount = await _context.Products.CountAsync();
            return Ok(productCount);
        }

    }
}
