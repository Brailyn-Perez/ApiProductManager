
using ProductManager.BL.DTOS.Supplier;
using ProductManager.BL.Interfaces.Services;
using ProductManager.DAL.Base;
using ProductManager.DAL.Repositories;

namespace ProductManager.BL.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly SupplierRepository _repository;

        public SupplierService(SupplierRepository repository)
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
