using Microsoft.EntityFrameworkCore;
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
        public override async Task<OperationResult> SaveEntityAsync(Product entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Product>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                    return isValid;

                var existsCategory = await _context.Categories.AnyAsync(x => x.Id == entity.CategoryId);
                if (!existsCategory)
                {
                    result.Success = false;
                    result.Message = "La categoría no existe.";
                    return result;
                }

                var existsSupplier = await _context.Suppliers.AnyAsync(x => x.Id == entity.SupplierId);
                if (!existsSupplier)
                {
                    result.Success = false;
                    result.Message = "El proveedor no existe.";
                    return result;
                }

                result.Data = await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }
        public override async Task<OperationResult> UpdateEntityAsync(Product entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Product>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                    return isValid;

                result.Data = await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }
    }
}
