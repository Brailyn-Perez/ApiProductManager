using ProductManager.BL.DTOS.User;
using ProductManager.BL.DTOS.User.CreateUserDTO;
using ProductManager.BL.Interfaces.User;
using ProductManager.DAL.Base;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.BL.Services.User
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<OperationResult> LoginUserAsync(LoginUsersDTO user)
        {
            OperationResult result = new OperationResult();
            try
            {
                var User = new DAL.Entities.User
                {
                    EMail = user.EMail,
                    Password = _jwtService.encryptSHA256(user.Password),
                };

                result = await _repository.Login(User);

                if (!result.Success)
                {
                    result.Success = false;
                    result.Message = "Usuario o contraseña incorrecta";
                    return result;
                }            
                
                string TOKEN = _jwtService.generateJWT(user);
                result.Message = "Login exitoso";
                result.Data = TOKEN;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error en login: {ex.Message}";
                return result;
            }
            return result;
        }

        public async Task<OperationResult> RegisterUserAsync(CreateUserDTO user)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = new DAL.Entities.User
                {
                    Name = user.Name,
                    EMail = user.EMail,
                    Password = _jwtService.encryptSHA256(user.Password),
                };

                var saveResult = await _repository.SaveEntityAsync(entity);

                if (!saveResult.Success)
                {
                    result.Success = false;
                    result.Message = saveResult.Message;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al ingresar los datos.{ex.Message}";

            }
            return result;
        }
    }
}
