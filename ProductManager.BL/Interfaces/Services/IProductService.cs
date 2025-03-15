﻿using ProductManager.BL.Base;
using ProductManager.BL.DTOS.Product;

namespace ProductManager.BL.Interfaces.Services
{
    public interface IProductService : IBaseService<CreateProductDTO, UpdateProductDTO, DeleteProductDTO>
    {
    }
}
