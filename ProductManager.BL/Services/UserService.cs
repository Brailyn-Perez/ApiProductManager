using ProductManager.BL.DTOS.User;
using ProductManager.BL.DTOS.User.CreateUserDTO;
using ProductManager.BL.Interfaces.Services;
using ProductManager.DAL.Base;
using ProductManager.DAL.Repositories;

namespace ProductManager.BL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
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
