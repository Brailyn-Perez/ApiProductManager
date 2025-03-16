using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductManager.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> Login(User user)
        {
            OperationResult result = new OperationResult();
            try
            {
                var isValid = await BaseValidator<User>.ValidateEntityAsync(user);
                if (!isValid.Success)
                    return isValid;

                var User = await _context.users.FirstOrDefaultAsync(x => x.EMail == user.EMail && x.Password == user.Password);
                result.Data = User;

                if (User == null)
                {
                    result.Success = false;
                    result.Message = "Usuario o contraseña incorrectos.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ingresar los datos.");
                result.Success = false;
                result.Message = "Error al ingresar los datos.";
            }
            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(User entity)
        {
            OperationResult result = new();
            try
            {
                var isValid = await BaseValidator<User>.ValidateEntityAsync(entity);
                if (!isValid.Success)
                    return isValid;

                await base.SaveEntityAsync(entity);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ingresar los datos.");
                result.Success = false;
                result.Message = "Error al ingresar los datos.";
            }
            return result;
        }
    }
}
