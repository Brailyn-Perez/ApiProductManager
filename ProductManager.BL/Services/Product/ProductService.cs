using ProductManager.BL.DTOS.Product;
using ProductManager.BL.Interfaces.Product;
using ProductManager.DAL.Base;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.BL.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> GeAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var products = await _repository.GetAllAsync();
                products.Select(x => new ProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryId = x.CategoryId,
                    SupplierId = x.SupplierId
                }).ToList();
                result.Success = true;
                result.Data = products;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var product = await _repository.GetEntityByIdAsync(id);
                result.Data = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    SupplierId = product.SupplierId
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> Remove(DeleteProductDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var exists = await _repository.ExistsAsync(x => x.Id == dto.Id && x.Deleted == false);
                if (!exists)
                {
                    result.Success = false;
                    result.Message = "Producto no encontrado";
                }
                var product = await _repository.GetEntityByIdAsync(dto.Id);
                product.Deleted = true;

                await _repository.UpdateEntityAsync(product);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> Save(CreateProductDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var product = new DAL.Entities.Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Stock = dto.Stock,
                    CategoryId = dto.CategoryId,
                    SupplierId = dto.SupplierId
                };
                await _repository.SaveEntityAsync(product);
                result.Success = true;
                result.Data = product;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateProductDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var product = new DAL.Entities.Product
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Price = dto.Price,
                    Stock = dto.Stock,
                };
                await _repository.UpdateEntityAsync(product);
                result.Success = true;
                result.Data = product;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
