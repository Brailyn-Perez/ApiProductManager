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
