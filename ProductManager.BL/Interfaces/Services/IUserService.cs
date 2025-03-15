﻿using ProductManager.BL.Base;
using ProductManager.BL.DTOS.User;
using ProductManager.BL.DTOS.User.CreateUserDTO;

namespace ProductManager.BL.Interfaces.Services
{
    public interface IUserService : IBaseService<CreateUserDTO,UpdateUserDTO, DeleteUserDTO>
    {
    }
}
