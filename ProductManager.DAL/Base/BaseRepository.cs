using Microsoft.EntityFrameworkCore;
using ProductManager.DAL.Interfaces.Base;
using System.Linq.Expressions;

namespace ProductManager.DAL.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> Entity { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult Result = new OperationResult();
            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
                Result.Success = true;
                Result.Message = "Datos Actualizados Correctamente";

            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = "Error Actualizando los datos";
            }

            return Result;
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult Result = new OperationResult();
            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();
                Result.Success = true;
                Result.Message = "Datos Ingresados Correctamente";

            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = "Error ingresando  los datos";
            }

            return Result;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();

        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult Result = new OperationResult();
            try
            {
                Result.Data = await Entity.Where(filter).ToListAsync();
                Result.Success = true;
                Result.Message = "Datos Obtenidos Correctamente";

            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = "Error Obteniendo los datos";
            }

            return Result;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }
    }
}
