using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Repositories;
using System.Linq.Expressions;
using System.Text.Json;

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

                string logFilePath = "user_registration_log.json";
                var logEntry = new { Date = DateTime.Now, User = entity };

                List<object> logEntries = new List<object>();
                if (File.Exists(logFilePath))
                {
                    var existingLog = await File.ReadAllTextAsync(logFilePath);
                    logEntries = JsonSerializer.Deserialize<List<object>>(existingLog) ?? new List<object>();
                }

                logEntries.Add(logEntry);
                var newLog = JsonSerializer.Serialize(logEntries);
                await File.WriteAllTextAsync(logFilePath, newLog);
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
