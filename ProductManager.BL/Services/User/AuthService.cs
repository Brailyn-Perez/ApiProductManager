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

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<string> LoginUserAsync(LoginUsersDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RegisterUserAsync(CreateUserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
