using ProductManager.DAL.Repositories;
using ProductManager.DAL.Test.Base;
using System.Linq.Expressions;

namespace ProductManager.DAL.Test.Repositories.Supplier
{
    public class UnitTestSupplierRepository : BaseTest<SupplierRepository>
    {
        private readonly SupplierRepository _supplierRepository;

        public UnitTestSupplierRepository()
        {
            _supplierRepository = new SupplierRepository(_context, _mockLogger.Object);
        }

        [Fact]
        public async Task SupplierHasProduct_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var supplierId = 1;
            _context.Products.Add(new Entities.Product { Id = 1, SupplierId = supplierId, Deleted = false });
            await _context.SaveChangesAsync();

            // Act
            var result = await _supplierRepository.SupplierHasProduct(supplierId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SupplierHasProduct_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var supplierId = 1;

            // Act
            var result = await _supplierRepository.SupplierHasProduct(supplierId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSuppliers()
        {
            // Arrange
            _context.Suppliers.Add(new Entities.Supplier { Id = 1, Name = "Supplier1" });
            _context.Suppliers.Add(new Entities.Supplier { Id = 2, Name = "Supplier2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _supplierRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_WithFilter_ShouldReturnFilteredSuppliers()
        {
            // Arrange
            _context.Suppliers.Add(new Entities.Supplier { Id = 1, Name = "Supplier1" });
            _context.Suppliers.Add(new Entities.Supplier { Id = 2, Name = "Supplier2" });
            await _context.SaveChangesAsync();
            Expression<Func<Entities.Supplier, bool>> filter = s => s.Name.Contains("1");

            // Act
            var result = await _supplierRepository.GetAllAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSupplier_WhenSupplierExists()
        {
            // Arrange
            var supplier = new Entities.Supplier { Id = 1, Name = "Supplier1" };
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            // Act
            var result = await _supplierRepository.GetEntityByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(supplier.Id, result.Id);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldSaveSupplier()
        {
            // Arrange
            var supplier = new Entities.Supplier { Id = 1, Name = "Supplier1" };

            // Act
            var result = await _supplierRepository.SaveEntityAsync(supplier);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateSupplier()
        {
            // Arrange
            var supplier = new Entities.Supplier { Id = 1, Name = "Supplier1" };
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            supplier.Name = "UpdatedSupplier";

            // Act
            var result = await _supplierRepository.UpdateEntityAsync(supplier);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }
    }
}
