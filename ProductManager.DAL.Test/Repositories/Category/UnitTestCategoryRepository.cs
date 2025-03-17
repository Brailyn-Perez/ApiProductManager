using ProductManager.DAL.Repositories;
using ProductManager.DAL.Test.Base;
using System.Linq.Expressions;


namespace ProductManager.DAL.Test.Repositories.Category
{
    public class UnitTestCategoryRepository : BaseTest<CategoryRepository>
    {
        private readonly CategoryRepository _categoryRepository;

        public UnitTestCategoryRepository()
        {
            _categoryRepository = new CategoryRepository(_context, _mockLogger.Object);
        }

        [Fact]
        public async Task CategoryHasProduct_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var categoryId = 1;
            _context.Products.Add(new Entities.Product { Id = 1, CategoryId = categoryId, Deleted = false });
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.categoryHasProduct(categoryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CategoryHasProduct_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var categoryId = 1;

            // Act
            var result = await _categoryRepository.categoryHasProduct(categoryId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCategories()
        {
            // Arrange
            _context.Categories.Add(new Entities.Category { Id = 1, Name = "Category1" });
            _context.Categories.Add(new Entities.Category { Id = 2, Name = "Category2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_WithFilter_ShouldReturnFilteredCategories()
        {
            // Arrange
            _context.Categories.Add(new Entities.Category { Id = 1, Name = "Category1" });
            _context.Categories.Add(new Entities.Category { Id = 2, Name = "Category2" });
            await _context.SaveChangesAsync();
            Expression<Func<Entities.Category, bool>> filter = c => c.Name.Contains("1");

            // Act
            var result = await _categoryRepository.GetAllAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Entities.Category { Id = 1, Name = "Category1" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetEntityByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldSaveCategory()
        {
            // Arrange
            var category = new Entities.Category { Id = 1, Name = "Category1" };

            // Act
            var result = await _categoryRepository.SaveEntityAsync(category);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateCategory()
        {
            // Arrange
            var category = new Entities.Category { Id = 1, Name = "Category1" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            category.Name = "UpdatedCategory";

            // Act
            var result = await _categoryRepository.UpdateEntityAsync(category);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }
    }
}
