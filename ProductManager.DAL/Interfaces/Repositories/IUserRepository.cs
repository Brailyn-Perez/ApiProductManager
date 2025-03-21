﻿using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Base;

namespace ProductManager.DAL.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<OperationResult> Login(User user);
    }
}
