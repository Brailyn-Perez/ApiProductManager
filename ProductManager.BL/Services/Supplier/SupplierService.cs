using ProductManager.BL.DTOS.Supplier;
using ProductManager.BL.Interfaces.Supplier;
using ProductManager.DAL.Base;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.BL.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
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

        public Task<OperationResult> Remove(DeleteSupplierDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(CreateOrUpdateSupplierDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(CreateOrUpdateSupplierDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
