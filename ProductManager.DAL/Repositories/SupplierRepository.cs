using Microsoft.Extensions.Logging;
using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductManager.DAL.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SupplierRepository> _logger;
        public SupplierRepository(ApplicationDbContext context, ILogger<SupplierRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;

        }

        public override Task<List<Supplier>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Supplier, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public override Task<Supplier> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);
        }

        public override Task<OperationResult> SaveEntityAsync(Supplier entity)
        {
            return base.SaveEntityAsync(entity);
        }

        public override Task<OperationResult> UpdateEntityAsync(Supplier entity)
        {
            return base.UpdateEntityAsync(entity);
        }
    }
}
