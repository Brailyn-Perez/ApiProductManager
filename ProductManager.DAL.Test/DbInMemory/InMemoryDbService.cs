using Microsoft.EntityFrameworkCore;

namespace ProductManager.DAL.Test.DbInMemory
{
    public static class InMemoryDbService
    {
        public static ApplicationDbContext GetProductManagerDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
