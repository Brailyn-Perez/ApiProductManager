
using ProductManager.BL.DTOS.Product;
using ProductManager.BL.Interfaces.Services;
using ProductManager.DAL.Base;
using ProductManager.DAL.Repositories;

namespace ProductManager.BL.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
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

        public Task<OperationResult> Remove(DeleteProductDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(CreateProductDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateProductDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
