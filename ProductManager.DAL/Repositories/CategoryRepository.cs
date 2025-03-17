using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductManager.DAL.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> categoryHasProduct(int id)
        {
           return await _context.Products.AnyAsync(x => x.CategoryId == id && x.Deleted == false);
        }

        public override Task<List<Category>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Category, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (filter is null)
                {
                    result.Success = false;
                    result.Message = "Filter is Null";
                    return result;
                }
                result.Data = await base.GetAllAsync(filter);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }

        public override async Task<Category> GetEntityByIdAsync(int id)
        {
            return await base.GetEntityByIdAsync(id);
        }

        public override async Task<OperationResult> SaveEntityAsync(Category entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Category>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                    return isValid;

                result.Data = await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Category entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<Category>.ValidateEntityAsync(entity);
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
