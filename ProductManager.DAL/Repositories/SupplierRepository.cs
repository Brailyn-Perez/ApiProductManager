using Microsoft.EntityFrameworkCore;
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

        public override async Task<OperationResult> SaveEntityAsync(Supplier entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Supplier>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                    return isValid;

                result.Data = await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

        public async Task<bool> SupplierHasProduct(int id)
        {
            return await _context.Products.AnyAsync(x => x.SupplierId == id && x.Deleted == false);
        }

        public override async Task<OperationResult> UpdateEntityAsync(Supplier entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Supplier>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                    return isValid;
                result.Data = await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }
    }
}
