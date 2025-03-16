using ProductManager.BL.DTOS.User;
using ProductManager.BL.DTOS.User.CreateUserDTO;
using ProductManager.BL.Interfaces.User;
using ProductManager.DAL.Base;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.BL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(DeleteUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(CreateUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
