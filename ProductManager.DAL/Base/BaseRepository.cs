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

        public virtual async Task<TEntity?> GetEntityByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));

            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult result = new();
            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula.";
                return result;
            }

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Datos actualizados correctamente.";
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar los datos: {ex.Message}";
            }

            return result;
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new();
            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula.";
                return result;
            }

            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Datos ingresados correctamente.";
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = $"Error al ingresar los datos: {ex.Message}";
            }

            return result;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new();
            if (filter == null)
            {
                result.Success = false;
                result.Message = "El filtro no puede ser nulo.";
                return result;
            }

            try
            {
                result.Data = await Entity.Where(filter).ToListAsync();
                result.Success = true;
                result.Message = "Datos obtenidos correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los datos: {ex.Message}";
            }

            return result;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter), "El filtro no puede ser nulo.");

            return await Entity.AnyAsync(filter);
        }
    }
}
