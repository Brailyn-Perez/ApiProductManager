using ProductManager.DAL.Repositories;
using ProductManager.DAL.Test.Base;
using System.Linq.Expressions;

namespace ProductManager.DAL.Test.Repositories.Product
{
    public class UnitTestProductRepository : BaseTest<ProductRepository>
    {
        private readonly ProductRepository _productRepository;

        public UnitTestProductRepository()
        {
            _productRepository = new ProductRepository(_context, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            // Arrange
            _context.Products.Add(new Entities.Product { Id = 1, Name = "Product1" });
            _context.Products.Add(new Entities.Product { Id = 2, Name = "Product2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_WithFilter_ShouldReturnFilteredProducts()
        {
            // Arrange
            _context.Products.Add(new Entities.Product { Id = 1, Name = "Product1" });
            _context.Products.Add(new Entities.Product { Id = 2, Name = "Product2" });
            await _context.SaveChangesAsync();
            Expression<Func<Entities.Product, bool>> filter = p => p.Name.Contains("1");

            // Act
            var result = await _productRepository.GetAllAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Entities.Product { Id = 1, Name = "Product1" };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetEntityByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldSaveProduct()
        {
            // Arrange
            var product = new Entities.Product { Id = 1, Name = "Product1", CategoryId = 1, SupplierId = 1 };
            _context.Categories.Add(new Entities.Category { Id = 1, Name = "Category1" });
            _context.Suppliers.Add(new Entities.Supplier { Id = 1, Name = "Supplier1" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.SaveEntityAsync(product);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnError_WhenCategoryDoesNotExist()
        {
            // Arrange
            var product = new Entities.Product { Id = 1, Name = "Product1", CategoryId = 1, SupplierId = 1 };
            _context.Suppliers.Add(new Entities.Supplier { Id = 1, Name = "Supplier1" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.SaveEntityAsync(product);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La categoría no existe.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnError_WhenSupplierDoesNotExist()
        {
            // Arrange
            var product = new Entities.Product { Id = 1, Name = "Product1", CategoryId = 1, SupplierId = 1 };
            _context.Categories.Add(new Entities.Category { Id = 1, Name = "Category1" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.SaveEntityAsync(product);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El proveedor no existe.", result.Message);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Entities.Product { Id = 1, Name = "Product1", CategoryId = 1, SupplierId = 1 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            product.Name = "UpdatedProduct";

            // Act
            var result = await _productRepository.UpdateEntityAsync(product);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }
    }
}
