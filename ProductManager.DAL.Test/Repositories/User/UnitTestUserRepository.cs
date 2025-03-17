using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Repositories;
using System.Text.Json;

namespace ProductManager.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<ILogger<UserRepository>> _mockLogger;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockLogger = new Mock<ILogger<UserRepository>>();
            _userRepository = new UserRepository(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SaveEntityAsync_ValidUser_SavesSuccessfully()
        {
            // Arrange
            var user = new User { Name = "Test User", EMail = "test@example.com", Password = "password123" };
            var logFilePath = "user_registration_log.json";
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            // Act
            var result = await _userRepository.SaveEntityAsync(user);

            // Assert
            Assert.True(result.Success);
            Assert.True(File.Exists(logFilePath));

            var logEntries = JsonSerializer.Deserialize<List<object>>(await File.ReadAllTextAsync(logFilePath));
            Assert.NotNull(logEntries);
            Assert.Single(logEntries);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidUser_ReturnsFailure()
        {
            // Arrange
            var user = new User { Name = "", EMail = "invalidemail", Password = "short" };
            var logFilePath = "user_registration_log.json";
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            // Act
            var result = await _userRepository.SaveEntityAsync(user);

            // Assert
            Assert.False(result.Success);
            Assert.False(File.Exists(logFilePath));
        }
    }
}
