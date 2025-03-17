using Microsoft.Extensions.Logging;
using Moq;
using ProductManager.DAL.Test.DbInMemory;

namespace ProductManager.DAL.Test.Base
{
    public class BaseTest<TRepository>
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Mock<ILogger<TRepository>> _mockLogger;

        protected BaseTest()
        {
            _context = InMemoryDbService.GetProductManagerDbContext();
            _mockLogger = new Mock<ILogger<TRepository>>();
        }
    }
}
