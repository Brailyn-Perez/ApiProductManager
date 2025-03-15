using ProductManager.BL.DTOS.User;
using ProductManager.BL.DTOS.User.CreateUserDTO;
using ProductManager.DAL.Base;

namespace ProductManager.BL.Interfaces.User
{
    public interface IAuthService
    {
        Task<OperationResult> RegisterUserAsync(CreateUserDTO user);
        Task<string> LoginUserAsync(LoginUsersDTO user);
    }
}
