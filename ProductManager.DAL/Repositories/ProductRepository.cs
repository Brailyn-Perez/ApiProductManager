using Microsoft.Extensions.Logging;
using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductManager.DAL.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public override Task<List<Product>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override Task<OperationResult> GetAllAsync(Expression<Func<Product, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }
        public override Task<Product> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);
        }
        public override Task<OperationResult> SaveEntityAsync(Product entity)
        {
            return base.SaveEntityAsync(entity);
        }
        public override Task<OperationResult> UpdateEntityAsync(Product entity)
        {
            return base.UpdateEntityAsync(entity);
        }
    }
}
